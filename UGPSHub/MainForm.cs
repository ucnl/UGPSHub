using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using UCNLDrivers;
using UCNLKML;
using UCNLNav;
using UCNLNMEA;
using UCNLUI.Dialogs;
using uOSM;

namespace UGPSHub
{
    public partial class MainForm : Form
    {
        #region Properties

        CCore core;

        TSLogProvider logger;
        SimpleSettingsProviderXML<SettingsContainer> settingsProvider;
        uOSMTileProvider tProvider;

        LogPlayer lPlayer;
        
        string settingsFileName;
        string logPath;
        string logFileName;
        string snapshotsPath;
        string tileDBPath;

        bool isRestart = false;
        bool isAutoscreenshot = false;

        bool tracksChanged = false;
        bool TracksChanged
        {
            get { return tracksChanged; }
            set
            {
                if (value != tracksChanged)
                {
                    tracksChanged = value;
                    InvokeSetEnabledState(mainToolStrip, trackBtn, tracksChanged);
                }
            }
        }

        bool uPositionValid = false;
        bool UPositionValid
        {
            get { return uPositionValid; }
            set
            {
                if (value != uPositionValid)
                {
                    uPositionValid = value;
                    InvokeSetEnabledState(mainToolStrip, markCurrentPositionBtn, uPositionValid);
                }
            }
        }

        bool isScreenshotsNamesByTime = true;
        int sc_num = 0;

        Dictionary<string, List<GeoPoint3DETm>> tracks = new Dictionary<string, List<GeoPoint3DETm>>();        

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();

            #region paths & filenames

            DateTime startTime = DateTime.Now;
            settingsFileName = Path.ChangeExtension(Application.ExecutablePath, "settings");
            logPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "LOG");
            logFileName = StrUtils.GetTimeDirTreeFileName(startTime, Application.ExecutablePath, "LOG", "log", true);
            snapshotsPath = StrUtils.GetTimeDirTree(startTime, Application.ExecutablePath, "SCREENSHOTS", false);
            tileDBPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Cache\\Tiles\\");

            #endregion

            #region MainForm text

            this.Text = string.Format("{0} v{1}", Application.ProductName, Assembly.GetExecutingAssembly().GetName().Version.ToString());

            #endregion

            #region logger

            logger = new TSLogProvider(logFileName);
            logger.WriteStart();
            logger.Write(string.Format("{0} v{1}", Application.ProductName, Assembly.GetExecutingAssembly().GetName().Version.ToString()));
            logger.TextAddedEvent += (o, e) => InvokeAppendHisotryLine(e.Text);

            #endregion

            #region settings

            settingsProvider = new SimpleSettingsProviderXML<SettingsContainer>();
            settingsProvider.isSwallowExceptions = false;

            logger.Write(string.Format("Loading settings from {0}", settingsFileName));

            try
            {
                settingsProvider.Load(settingsFileName);
            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
            }

            logger.Write("Current application settings:");
            logger.Write(settingsProvider.Data.ToString());

            #endregion                 

            #region custom UI

            isScreenshotsNamesByTime = settingsProvider.Data.IsScreenshotsNamingByTime;

            List<string> trackNames = new List<string>();
            trackNames.Add("ALL");
            trackNames.Add("UNR (FLT)");
            trackNames.Add("UNR (FLT)+Marked");            

            geoPlot.InitTrack("UNR (RAW)", 64, Color.Orange, 1, 4, true, Color.Yellow, 1, 200);
            geoPlot.InitTrack("UNR (FLT)", settingsProvider.Data.NumberOfPointsToShow, Color.Red, 1, 4, true, Color.Red, 1, 200);

            if (settingsProvider.Data.IsUseAUXGNSS)
            {
                geoPlot.InitTrack("AUX GNSS", 64, Color.Blue, 1, 4, true, Color.Blue, 1, 200);                
                trackNames.Add("AUX GNSS");
                trackNames.Add("UNR (FLT)+AUX GNSS");
                trackNames.Add("UNR (FLT)+Marked+AUX GNSS");
            }

            geoPlot.InitTrack("Marked", 256, Color.Black, 4, 4, false, Color.Black, 1, 200);

            geoPlot.InitTrack("BASE 1", 4, Color.DarkRed, 2, 4, false, Color.Salmon, 1, 200);
            geoPlot.InitTrack("BASE 2", 4, Color.DarkOrange, 2, 4, false, Color.Gold, 1, 200);
            geoPlot.InitTrack("BASE 3", 4, Color.Green, 2, 4, false, Color.MediumSpringGreen, 1, 200);
            geoPlot.InitTrack("BASE 4", 4, Color.Purple, 2, 4, false, Color.SkyBlue, 1, 200);

            tracksToFitCbx.Items.Clear();
            tracksToFitCbx.Items.AddRange(trackNames.ToArray());
            tracksToFitCbx.SelectedIndex = 0;

            geoPlot.SetTracksVisibility(true);
            geoPlot.TextBackgroundColor = Color.FromArgb(127, Color.White);

            geoPlot.HistoryVisible = true;
            
            #endregion

            #region tProvider

            tProvider = new uOSMTileProvider(256, 19, new Size(256, 256), tileDBPath, settingsProvider.Data.TileServers);
            geoPlot.ConnectTileProvider(tProvider);

            #endregion
            
            #region core

            core = new CCore(new SerialPortSettings(settingsProvider.Data.RedNODEPortName, 
                settingsProvider.Data.RedNODEPortBaudrate,
                System.IO.Ports.Parity.None, 
                DataBits.dataBits8, 
                System.IO.Ports.StopBits.One, 
                System.IO.Ports.Handshake.None),
                settingsProvider.Data.CourseEstimatorFIFOSize,
                settingsProvider.Data.TrackFilterFIFOSize,
                settingsProvider.Data.MaxDistToResetTrackSmoother_m);

            core.SalinityPSU = settingsProvider.Data.Salinity_psu;

            if (!settingsProvider.Data.IsAutoSoundspeed)
                core.SoundSpeed = settingsProvider.Data.SoundSpeed_mps;

            core.DeviceInfoUpdatedChangedEvent += (o, e) =>
                {
                    InvokeSetVisibleState(mainToolStrip, utilsDeviceInfoQueryBtn, !core.IsDeviceInfoUpdated);
                    InvokeSetVisibleState(mainToolStrip, utilsDeviceInfoViewBtn, core.IsDeviceInfoUpdated);

                    if (core.IsDeviceInfoUpdated)
                        this.Invoke((MethodInvoker)delegate { utilsDeviceInfoViewBtn_Click(null, null); });
                };

            core.LogEvent += (o, e) => logger.Write(string.Format("{0}: {1}", e.EventType, e.LogString));
            core.PortsStateChangedEvent += (o, e) =>
                {
                    InvokeSetStatusStripLblText(mainStatusStrip, connectionStatusLbl, core.GetPortsStateDescription());
                };
            core.RadialErrorThresholdExceededChanged += (o, e) =>
                {
                    InvokeSetVisibleState(mainStatusStrip, rerExeededFlagLbl, core.RadialErrorThrehsoldExceeded);
                };

            core.RedNODEPortBusyChanged += (o, e) =>
                {
                    InvokeSetEnabledState(mainToolStrip, utilsDeviceInfoBtn, core.IsOpen && !core.RedNodePortBusy);
                };

            core.SystemUpdateEvent += (o, e) =>
                {
                    StringBuilder sb = new StringBuilder();

                    UPositionValid = core.UPositionValid;

                    var nText = core.GetNavigationInfo();
                    if (!string.IsNullOrEmpty(nText))
                        sb.AppendFormat(" N̲A̲V̲I̲G̲A̲T̲I̲O̲N̲\r\n{0}\r\n", nText);

                    var pText = core.GetRNLocationInfo();
                    if (!string.IsNullOrEmpty(pText))
                        sb.AppendFormat(" U̲N̲R̲ ̲P̲O̲S̲I̲T̲I̲O̲N̲\r\n{0}\r\n", pText);

                    var aText = core.GetAmbInfo();
                    if (!string.IsNullOrEmpty(aText))
                        sb.AppendFormat("\r\n M̲I̲S̲C̲\r\n{0}", aText);

                    if (core.IsStatistics)
                    {
                        var sText = core.GetStatistics();
                        if (!string.IsNullOrEmpty(sText))
                            sb.AppendFormat("\r\n S̲T̲A̲T̲I̲S̲T̲I̲C̲S̲\r\n{0}", sText);
                    }

                    InvokeSetLeftUpperCornerTextPlot(sb.ToString(), true);
                    InvokeSetStatusStripLblText(secondaryStatusStrip, buoysStatusLbl, core.GetBasesDescription());
                };

            core.TrackUpdatedEvent += (o, e) =>
                {
                    if (!tracks.ContainsKey(e.TrackID))
                        tracks.Add(e.TrackID, new List<GeoPoint3DETm>());
                    tracks[e.TrackID].Add(e.TrackPoint);
                    InvokeUpdateTracksPlot(e, true);

                    TracksChanged = true;

                    if (isAutoscreenshot)
                        InvokeSaveFullScreenshot();
                };

            if (settingsProvider.Data.IsUseAUXGNSS)
                core.InitAuxPort(new SerialPortSettings(settingsProvider.Data.AUXGNSSPortName,
                    settingsProvider.Data.AUXGNSSPortBaudrate,
                    System.IO.Ports.Parity.None,
                    DataBits.dataBits8, 
                    System.IO.Ports.StopBits.One, 
                    System.IO.Ports.Handshake.None));

            if (settingsProvider.Data.IsUseOutputPort)
                core.InitOutPort(new SerialPortSettings(settingsProvider.Data.OutputPortName,
                    settingsProvider.Data.OutputPortBaudrate,
                    System.IO.Ports.Parity.None,
                    DataBits.dataBits8,
                    System.IO.Ports.StopBits.One,
                    System.IO.Ports.Handshake.None));

            #endregion

            #region lPlayer

            lPlayer = new LogPlayer();
            lPlayer.NewLogLineHandler += (o, e) =>
            {
                core.Emulate(e.Line);
            };
            lPlayer.LogPlaybackFinishedHandler += (o, e) =>
            {                
                if (InvokeRequired)
                {
                    Invoke((MethodInvoker)delegate
                    {
                        settingsBtn.Enabled = true;
                        connectionBtn.Enabled = true;
                        logPlaybackBtn.Text = "▶ Playback...";
                        MessageBox.Show(string.Format("Log file \"{0}\" playback is finished", lPlayer.LogFileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    });
                }
                else
                {
                    settingsBtn.Enabled = true;
                    connectionBtn.Enabled = true;
                    logPlaybackBtn.Text = "▶ Playback...";
                    MessageBox.Show(string.Format("Log file \"{0}\" playback is finished", lPlayer.LogFileName), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            #endregion
        }

        #endregion

        #region Methods

        #region UI Invokers

        private void InvokeSaveFullScreenshot()
        {
            if (this.InvokeRequired)
                this.Invoke((MethodInvoker)delegate { SaveFullScreenshot(); });
            else
                SaveFullScreenshot();
        }

        private void InvokeAppendHisotryLine(string line)
        {
            if (geoPlot.InvokeRequired)
                geoPlot.Invoke((MethodInvoker)delegate 
                { 
                    geoPlot.AppendHistory(line);
                });
            else
            {
                geoPlot.AppendHistory(line);
            }
        }

        private void InvokeSetStatusStripLblText(StatusStrip strip, ToolStripStatusLabel lbl, string text)
        {
            if (strip.InvokeRequired)
            {
                strip.Invoke((MethodInvoker)delegate { lbl.Text = text; });
            }
            else
            {
                lbl.Text = text;
            }
        }

        private void InvokeSetEnabledState(ToolStrip strip, ToolStripItem item, bool enabled)
        {
            if (strip.InvokeRequired)
            {
                strip.Invoke((MethodInvoker)delegate { item.Enabled = enabled; });
            }
            else
            {
                item.Enabled = enabled;
            }
        }

        private void InvokeSetEnabledState(Control ctrl, bool enabled)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke((MethodInvoker)delegate { ctrl.Enabled = enabled; });
            }
            else
            {
                ctrl.Enabled = enabled;
            }
        }

        private void InvokeSetVisibleState(StatusStrip strip, ToolStripStatusLabel lbl, bool isVisible)
        {
            if (strip.InvokeRequired)
                strip.Invoke((MethodInvoker)delegate { lbl.Visible = isVisible; });
            else
                lbl.Visible = isVisible;
        }

        private void InvokeSetVisibleState(ToolStrip strip, ToolStripItem item, bool isVisible)
        {
            if (strip.InvokeRequired)
                strip.Invoke((MethodInvoker)delegate { item.Visible = isVisible; });
            else
                item.Visible = isVisible;
        }

        private void InvokeUpdateTracksPlot(TrackUpdateEventArgs e, bool isInvalidate)
        {
            if (geoPlot.InvokeRequired)
            {
                geoPlot.Invoke((MethodInvoker)delegate
                {
                    if (!double.IsNaN(e.Course_deg))
                        geoPlot.AddPoint(e.TrackID, e.TrackPoint.Latitude, e.TrackPoint.Longitude, e.Course_deg);
                    else
                        geoPlot.AddPoint(e.TrackID, e.TrackPoint.Latitude, e.TrackPoint.Longitude);

                    if (isInvalidate)
                        geoPlot.Invalidate();
                });
            }
            else
            {
                if (!double.IsNaN(e.Course_deg))
                    geoPlot.AddPoint(e.TrackID, e.TrackPoint.Latitude, e.TrackPoint.Longitude, e.Course_deg);
                else
                    geoPlot.AddPoint(e.TrackID, e.TrackPoint.Latitude, e.TrackPoint.Longitude);

                if (isInvalidate)
                    geoPlot.Invalidate();
            }
        }

        private void InvokeSetLeftUpperCornerTextPlot(string text, bool isInvalidate)
        {
            if (geoPlot.InvokeRequired)
                geoPlot.Invoke((MethodInvoker)delegate
                {
                    geoPlot.LeftUpperText = text;
                    if (isInvalidate)
                        geoPlot.Invalidate();
                });
            else
            {
                geoPlot.LeftUpperText = text;
                if (isInvalidate)
                    geoPlot.Invalidate();
            }
        }

        private void InvokeUpdatePlot()
        {
            if (geoPlot.InvokeRequired)
                geoPlot.Invoke((MethodInvoker)delegate { geoPlot.Invalidate(); });
            else
                geoPlot.Invalidate();
        }


        #endregion

        private void ProcessException(Exception ex, bool isMsgBox)
        {
            logger.Write(ex);

            if (isMsgBox)
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SaveFullScreenshot()
        {
            Bitmap target = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(target, this.DisplayRectangle);

            try
            {
                if (!Directory.Exists(snapshotsPath))
                    Directory.CreateDirectory(snapshotsPath);

                if (isScreenshotsNamesByTime)
                    target.Save(Path.Combine(snapshotsPath, string.Format("{0}.{1}", StrUtils.GetHMSString(), ImageFormat.Png)));
                else
                    target.Save(Path.Combine(snapshotsPath, string.Format("{0}.{1}", sc_num++, ImageFormat.Png)));                
            }
            catch
            {
                //
            }
        }

        private void OnConnectionStateChanged(bool state)
        {
            connectionBtn.Checked = state;
            utilsDeviceInfoBtn.Enabled = state;
            settingsBtn.Enabled = !state;
            logPlaybackBtn.Enabled = !state;
            utilsLogClearAllEntriesBtn.Enabled = !state;
            utilsTrackFilteringBtn.Enabled = !state;

            rerExeededFlagLbl.Visible = false;
        }

        private bool TracksExportToKML(string fileName)
        {
            KMLData data = new KMLData(fileName, string.Format("Generated by {0}", Application.ProductName));
            List<KMLLocation> kmlTrack;

            foreach (var item in tracks)
            {
                kmlTrack = new List<KMLLocation>();
                foreach (var point in item.Value)
                    kmlTrack.Add(new KMLLocation(point.Longitude, point.Latitude, -point.Depth));

                data.Add(new KMLPlacemark(string.Format("{0} track", item.Key), "", kmlTrack.ToArray()));
            }

            bool isOk = false;
            try
            {
                TinyKML.Write(data, fileName);
                isOk = true;
            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
            }

            return isOk;
        }

        private bool TracksExportToCSV(string fileName)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var track in tracks)
            {
                sb.AppendFormat("\r\nTrack name: {0}\r\n", track.Key);
                sb.Append("HH:MM:SS;LAT;LON;DPT;\r\n");

                foreach (var point in track.Value)
                {
                    sb.AppendFormat(CultureInfo.InvariantCulture,
                        "{0:00};{1:00};{2:00};{3:F06};{4:F06};{5:F03}\r\n",
                        point.TimeStamp.Hour,
                        point.TimeStamp.Minute,
                        point.TimeStamp.Second,
                        point.Latitude,
                        point.Longitude,
                        point.Depth);
                }
            }

            bool isOk = false;
            try
            {
                File.WriteAllText(fileName, sb.ToString());
                isOk = true;
            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
            }

            return isOk;
        }

        private int DeleteDirectoryTree(string rootPath)
        {
            var dirs = Directory.GetDirectories(rootPath);
            int dirNum = 0;
            foreach (var item in dirs)
            {
                try
                {
                    Directory.Delete(item, true);
                    dirNum++;
                }
                catch (Exception ex)
                {
                    ProcessException(ex, true);
                }
            }

            return dirNum;
        }

        private void ProcessAnalyzeLog(string fileName)
        {
            try
            {
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s = string.Empty;
                    while ((s = sr.ReadLine()) != null)
                    {
                        int idx = s.IndexOf(NMEAParser.SentenceStartDelimiter);
                        if (idx >= 0)
                        {
                            core.Emulate(s);
                            Application.DoEvents();
                        }
                    }
                }

                MessageBox.Show(string.Format("Playback of '{0}' is finished", Path.GetFileNameWithoutExtension(fileName)),
                    "Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
            }
        }

        #endregion

        #region Handlers

        #region UI

        #region mainToolStrip

        private void connectionBtn_Click(object sender, EventArgs e)
        {
            if (core.IsOpen)
            {
                try
                {
                    core.Close();                    
                }
                catch (Exception ex)
                {
                    ProcessException(ex, true);
                }

                OnConnectionStateChanged(false);
            }
            else
            {
                try
                {
                    core.Open();
                    OnConnectionStateChanged(true);
                }
                catch (Exception ex)
                {
                    ProcessException(ex, true);
                }
            }
        }

        #region TRACK

        private void trackExportBtn_Click(object sender, EventArgs e)
        {
            bool isSaved = false;

            using (SaveFileDialog sDilog = new SaveFileDialog())
            {
                sDilog.Title = "Exporting tracks...";
                sDilog.Filter = "Google KML (*.kml)|*.kml|CSV (*.csv)|*.csv";
                sDilog.FileName = string.Format("UGPSHub_Tracks_{0}", StrUtils.GetHMSString());

                if (sDilog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (sDilog.FilterIndex == 1)
                        isSaved = TracksExportToKML(sDilog.FileName);
                    else if (sDilog.FilterIndex == 2)
                        isSaved = TracksExportToCSV(sDilog.FileName);
                }
            }

            if (isSaved &&
                MessageBox.Show("Tracks saved, do you want to clear all tracks data?",
                "Question",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                tracks.Clear();
                TracksChanged = false;
            }
        }

        private void trackClearBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to clear all tracks data?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                tracks.Clear();
                TracksChanged = false;
            }
        }

        #endregion

        #region LOG

        private void logViewCurrentBtn_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(logger.FileName);
            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
            }
        }

        private void logPlaybackBtn_Click(object sender, EventArgs e)
        {
            if (lPlayer.IsRunning)
            {
                if (MessageBox.Show("Log playback is currently active, do you want to stop abort it?",
                    "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    lPlayer.RequestToStop();
            }
            else
            {
                using (OpenFileDialog oDialog = new OpenFileDialog())
                {
                    oDialog.Title = "Select a log file to playback...";
                    oDialog.DefaultExt = "log";
                    oDialog.Filter = "Log files (*.log)|*.log";

                    if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        lPlayer.Playback(oDialog.FileName);

                        logPlaybackBtn.Text = "⏹ Stop playback";
                        settingsBtn.Enabled = false;
                        connectionBtn.Enabled = false;
                    }
                }
            }
        }

        #endregion
      
        #region UTILS

        private void utilsDeviceInfoQueryBtn_Click(object sender, EventArgs e)
        {
            try
            {
                core.QueryDeviceInfo();
            }
            catch (Exception ex)
            {
                ProcessException(ex, true);
            }
        }

        private void utilsDeviceInfoViewBtn_Click(object sender, EventArgs e)
        {
            using (TextVIewDialog tDialog = new TextVIewDialog())
            {
                tDialog.Text = "Device information";
                tDialog.ShowText = string.Format("{0}\r\nS/N: {1}", core.DeviceInfoString, core.DeviceSerial);
                tDialog.ShowDialog();
            }            
        }

        private void utilsSessionResetBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to reset current session (tracks, filter state)?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                core.Reset();
                geoPlot.ClearTracks();
                tracks.Clear();
                TracksChanged = false;
            }
        }

        private void utilsLogClearAllEntriesBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete all log entries?",
                                "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                int dirNum = DeleteDirectoryTree(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "LOG"));
                MessageBox.Show(string.Format("{0} entries was/were deleted.", dirNum),
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }

        }

        private void utilsScreenShotsClearAllEntriesBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete all sceenshot entries?",
                                "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                int dirNum = DeleteDirectoryTree(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SCREENSHOTS"));
                MessageBox.Show(string.Format("{0} entries was/were deleted.", dirNum),
                    "Information",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void utilsTrackFilteringBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog oDialog = new OpenFileDialog())
            {
                oDialog.Title = "Select a KML file to filter...";
                oDialog.DefaultExt = "kml";
                oDialog.Filter = "KML files (*.kml)|*.kml";

                if (oDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    bool isLoaded = false;
                    KMLData kmlData = null;
                    try
                    {
                        kmlData = TinyKML.Read(oDialog.FileName);
                        isLoaded = true;
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex, true);
                    }

                    if (isLoaded)
                    {
                        TrackFilter trkFilter = new TrackFilter(8);
                        for (int i = 0; i < kmlData.Count; i++)
                        {
                            if (kmlData[i].PlacemarkItem.Count > 1)
                            {
                                trkFilter.Reset();
                                for (int j = 0; j < kmlData[i].PlacemarkItem.Count; j++)
                                {
                                    var fresult = trkFilter.Filter(kmlData[i].PlacemarkItem[j].Latitude, kmlData[i].PlacemarkItem[j].Longitude);
                                    kmlData[i].PlacemarkItem[j].Latitude = fresult.Latitude;
                                    kmlData[i].PlacemarkItem[j].Longitude = fresult.Longitude;
                                }
                            }
                        }

                        using (SaveFileDialog sDialog = new SaveFileDialog())
                        {
                            sDialog.Title = "Select filename to save filtered track...";
                            sDialog.DefaultExt = "kml";
                            sDialog.Filter = "KML files (*.kml)|*.kml";

                            var fName = string.Format("FLT_{0}", Path.GetFileName(oDialog.FileName));
                            var fPath = Path.GetDirectoryName(oDialog.FileName);

                            sDialog.FileName = Path.Combine(fPath, fName);

                            if (sDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                bool isSaved = false;
                                try
                                {
                                    TinyKML.Write(kmlData, sDialog.FileName);
                                    isSaved = true;
                                }
                                catch (Exception ex)
                                {
                                    ProcessException(ex, true);
                                }

                                if (isSaved)
                                    MessageBox.Show("Tracks successfully saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                            }                        
                        }
                    }
                }
            }
        }

        #endregion        

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            using (SettingsEditor sEditor = new SettingsEditor())
            {
                sEditor.Text = string.Format("{0} - [Settings Editor]", Application.ProductName);
                sEditor.Value = settingsProvider.Data;

                if (sEditor.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    bool isSaved = false;
                    settingsProvider.Data = sEditor.Value;

                    try
                    {
                        settingsProvider.Save(settingsFileName);
                        isSaved = true;
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex, true);
                    }

                    if ((isSaved) && (MessageBox.Show("Settings saved. Restart application to apply new settings?",
                        "Question",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes))
                    {
                        isRestart = true;
                        Application.Restart();
                    }
                }
            }
        }

        private void infoBtn_Click(object sender, EventArgs e)
        {
            using (AboutBox aDialog = new AboutBox())
            {
#if IS_DIVENET
                aDialog.IsBeringia = true;
                aDialog.Weblink = "www.divenetgps.com";

#else

                aDialog.IsBeringia = false;
                aDialog.Weblink = "www.unavlab.com";

#endif
                aDialog.ApplyAssembly(Assembly.GetExecutingAssembly());
                
                aDialog.ShowDialog();
            }
        }

        #endregion

        #region secondaryToolStrip

        private void isAutoScreenshotBtn_Click(object sender, EventArgs e)
        {
            isAutoscreenshot = !isAutoscreenshot;
            isAutoScreenshotBtn.Checked = isAutoscreenshot;
        }

        private void markCurrentPositionBtn_Click(object sender, EventArgs e)
        {
            core.MarkPosition();
        }

        private void tracksToFitCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            var trackNames = tracksToFitCbx.SelectedItem.ToString();
            if (trackNames == "ALL")
            {
                geoPlot.SetTracksVisibility(true);
            }
            else
            {
                var splits = trackNames.Split(new char[] { '+' });
                geoPlot.SetTracksVisibility(splits, true);
            }

            geoPlot.Invalidate();
        }

        private void isHistoryVisibleBtn_Click(object sender, EventArgs e)
        {
            geoPlot.HistoryVisible = !geoPlot.HistoryVisible;
            isHistoryVisibleBtn.Checked = geoPlot.HistoryVisible;
        }

        private void isLegendVisibleBtn_Click(object sender, EventArgs e)
        {
            geoPlot.LegendVisible = !geoPlot.LegendVisible;
            isLegendVisibleBtn.Checked = geoPlot.LegendVisible;
        }

        private void isStatisticsBtn_Click(object sender, EventArgs e)
        {
            core.IsStatistics = !core.IsStatistics;
            isStatisticsBtn.Checked = core.IsStatistics;
        }

        #endregion

        #region mainForm

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tracksChanged)
            {
                System.Windows.Forms.DialogResult result = System.Windows.Forms.DialogResult.Yes;
                while (tracksChanged && (result == System.Windows.Forms.DialogResult.Yes))
                {
                    result = MessageBox.Show("Tracks are not saved. Save them before exit?",
                        "Warning",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Warning);

                    if (result == System.Windows.Forms.DialogResult.Yes)
                        trackExportBtn_Click(sender, null);
                }

                e.Cancel = (result == System.Windows.Forms.DialogResult.Cancel);
            }
            else
            {
                e.Cancel = !isRestart && (MessageBox.Show(string.Format("Close {0}?", Application.ProductName),
                                                          "Question",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.Yes);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (core != null)
            {
                if (core.IsOpen)
                {
                    try
                    {
                        core.Open();
                    }
                    catch (Exception ex)
                    {
                        ProcessException(ex, false);
                    }
                }

                core.Dispose();
            }

            logger.Write("Closing application...");
            logger.FinishLog();
            logger.Flush();
        }        

        #endregion        

        #endregion        

        #endregion
    }
}
