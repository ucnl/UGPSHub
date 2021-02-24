using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Forms;
using UCNLDrivers;
using UCNLUI.Dialogs;

namespace UGPSHub
{
    public partial class SettingsEditor : Form
    {
        #region Properties

        string rnPortName
        {
            get { return UIUtils.TryGetCbxItem(rnPortNameCbx); }
            set { UIUtils.TrySetCbxItem(rnPortNameCbx, value); }
        }

        BaudRate rnBaudrate
        {
            get { return (BaudRate)Enum.Parse(typeof(BaudRate), UIUtils.TryGetCbxItem(rnBaudrateCbx)); }
            set { UIUtils.TrySetCbxItem(rnBaudrateCbx, value.ToString()); }
        }

        bool isUseAUXGNSS
        {
            get { return isUseAUXChb.Checked; }
            set { isUseAUXChb.Checked = value; }
        }

        string auxGNSSPortName
        {
            get { return UIUtils.TryGetCbxItem(auxGNSSPortNameCbx); }
            set { UIUtils.TrySetCbxItem(auxGNSSPortNameCbx, value); }
        }

        BaudRate auxGNSSBaudrate
        {
            get { return (BaudRate)Enum.Parse(typeof(BaudRate), UIUtils.TryGetCbxItem(auxGNSSBaudrateCbx)); }
            set { UIUtils.TrySetCbxItem(auxGNSSBaudrateCbx, value.ToString()); }
        }

        bool isUseOutput
        {
            get { return isUseOutputChb.Checked; }
            set { isUseOutputChb.Checked = value; }
        }

        string outputPortName
        {
            get { return UIUtils.TryGetCbxItem(outputPortNameCbx); }
            set { UIUtils.TrySetCbxItem(outputPortNameCbx, value); }
        }

        BaudRate outputBaudrate
        {
            get { return (BaudRate)Enum.Parse(typeof(BaudRate), UIUtils.TryGetCbxItem(outputBaudrateCbx)); }
            set { UIUtils.TrySetCbxItem(outputBaudrateCbx, value.ToString()); }
        }


        double salinity
        {
            get { return Convert.ToDouble(salinityEdit.Value); }
            set { UIUtils.TrySetNEditValue(salinityEdit, value); }
        }

        bool isAutoSpeedOfSound
        {
            get { return isAutoSoundSpeedChb.Checked; }
            set { isAutoSoundSpeedChb.Checked = value; }
        }

        double speedOfSound
        {
            get { return Convert.ToDouble(speedOfSoundEdit.Value); }
            set { UIUtils.TrySetNEditValue(speedOfSoundEdit, value); }
        }

        double radialErrorThreshold
        {
            get { return Convert.ToDouble(radialErrorThresholdEdit.Value); }
            set { UIUtils.TrySetNEditValue(radialErrorThresholdEdit, value); }
        }

        int crsEstimatorFIFOSize
        {
            get { return Convert.ToInt32(crsEstimatorFIFOSizeEdit.Value); }
            set { UIUtils.TrySetNEditValue(crsEstimatorFIFOSizeEdit, value); }
        }

        int trackFilterFIFOSize
        {
            get { return Convert.ToInt32(trackFilterFIFOSizeEdit.Value); }
            set { UIUtils.TrySetNEditValue(trackFilterFIFOSizeEdit, value); }
        }

        int trackViewFIFOSize
        {
            get { return Convert.ToInt32(trackViewFIFOSizeEdit.Value); }
            set { UIUtils.TrySetNEditValue(trackViewFIFOSizeEdit, value); }
        }

        bool screenShotsNamesByTime
        {
            get { return isScreenshotsNamingByTime.Checked; }
            set { isScreenshotsNamingByTime.Checked = value; }
        }

        public SettingsContainer Value
        {
            get
            {
                SettingsContainer result = new SettingsContainer();

                result.RedNODEPortName = rnPortName;
                result.RedNODEPortBaudrate = rnBaudrate;

                result.IsUseAUXGNSS = isUseAUXGNSS;
                result.AUXGNSSPortName = auxGNSSPortName;
                result.AUXGNSSPortBaudrate = auxGNSSBaudrate;

                result.IsUseOutputPort = isUseOutput;
                result.OutputPortName = outputPortName;
                result.OutputPortBaudrate = outputBaudrate;

                result.Salinity_psu = salinity;

                result.IsAutoSoundspeed = isAutoSpeedOfSound;
                result.SoundSpeed_mps = speedOfSound;

                result.RadialErrorThreshold_m = radialErrorThreshold;
                result.CourseEstimatorFIFOSize = crsEstimatorFIFOSize;
                result.TrackFilterFIFOSize = trackFilterFIFOSize;
                result.NumberOfPointsToShow = trackViewFIFOSize;

                result.IsScreenshotsNamingByTime = screenShotsNamesByTime;

                return result;
            }
            set
            {
                rnPortName = value.RedNODEPortName;
                rnBaudrate = value.RedNODEPortBaudrate;

                isUseAUXGNSS = value.IsUseAUXGNSS;
                auxGNSSPortName = value.AUXGNSSPortName;
                auxGNSSBaudrate = value.AUXGNSSPortBaudrate;

                isUseOutput = value.IsUseOutputPort;
                outputPortName = value.OutputPortName;
                outputBaudrate = value.OutputPortBaudrate;

                salinity = value.Salinity_psu;

                isAutoSpeedOfSound = value.IsAutoSoundspeed;
                speedOfSound = value.SoundSpeed_mps;

                radialErrorThreshold = value.RadialErrorThreshold_m;
                crsEstimatorFIFOSize = value.CourseEstimatorFIFOSize;
                trackFilterFIFOSize = value.TrackFilterFIFOSize;
                trackViewFIFOSize = value.NumberOfPointsToShow;

                screenShotsNamesByTime = value.IsScreenshotsNamingByTime;
            }
        }

        #endregion

        #region Constructor

        public SettingsEditor()
        {
            InitializeComponent();

            var baudRates = Enum.GetNames(typeof(BaudRate));

            rnBaudrateCbx.Items.AddRange(baudRates);
            auxGNSSBaudrateCbx.Items.AddRange(baudRates);
            outputBaudrateCbx.Items.AddRange(baudRates);

            rnBaudrate = BaudRate.baudRate9600;
            auxGNSSBaudrate = BaudRate.baudRate9600;
            outputBaudrate = BaudRate.baudRate9600;

            RefreshPorts();
            CheckValidity();
        }

        #endregion

        #region Methods

        private void RefreshPorts()
        {
            var portNames = SerialPort.GetPortNames();

            if (portNames.Length > 0)
            {
                rnPortNameCbx.Items.Clear();
                auxGNSSPortNameCbx.Items.Clear();
                outputPortNameCbx.Items.Clear();

                rnPortNameCbx.Items.AddRange(portNames);
                auxGNSSPortNameCbx.Items.AddRange(portNames);
                outputPortNameCbx.Items.AddRange(portNames);

                rnPortNameCbx.SelectedIndex = 0;
                auxGNSSPortNameCbx.SelectedIndex = 0;
                outputPortNameCbx.SelectedIndex = 0;
            }
        }

        private void CheckValidity()
        {
            bool result = !string.IsNullOrEmpty(rnPortName);

            List<string> portNames = new List<string>();
            portNames.Add(rnPortName);

            if (isUseAUXGNSS)
            {
                if (!string.IsNullOrEmpty(auxGNSSPortName) && !portNames.Contains(auxGNSSPortName))
                    portNames.Add(auxGNSSPortName);
                else
                    result = false;
            }

            if (isUseOutput)
            {
                if (!string.IsNullOrEmpty(outputPortName) && !portNames.Contains(outputPortName))
                    portNames.Add(outputPortName); // for future extensions
                else
                    result = false;
            }
            
            okBtn.Enabled = result;
        }


        #endregion

        #region Handlers

        private void isUseAUXChb_CheckedChanged(object sender, System.EventArgs e)
        {
            auxGNSSLbl.Enabled = isUseAUXChb.Checked;
            auxGNSSPortNameCbx.Enabled = isUseAUXChb.Checked;
            auxGNSSBaudrateCbx.Enabled = isUseAUXChb.Checked;
            CheckValidity();
        }

        private void isUseOutputChb_CheckedChanged(object sender, System.EventArgs e)
        {
            outputLbl.Enabled = isUseOutputChb.Checked;
            outputPortNameCbx.Enabled = isUseOutputChb.Checked;
            outputBaudrateCbx.Enabled = isUseOutputChb.Checked;
            CheckValidity();
        }

        private void auxGNSSPortNameCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckValidity();
        }

        private void outputPortNameCbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckValidity();
        }

        private void isAutoSoundSpeedChb_CheckedChanged(object sender, EventArgs e)
        {
            speedOfSoundLbl.Enabled = !isAutoSoundSpeedChb.Checked;
            speedOfSoundEdit.Enabled = !isAutoSoundSpeedChb.Checked;
        }

        private void refreshPortsBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RefreshPorts();
            CheckValidity();
        }

        private void salinityDataBaseBtn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (SalinityDialog sDialog = new SalinityDialog())
            {
                if (sDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    salinity = sDialog.Salinity;
                }
            }
        }

        private void restoreDefaultsBtn_Click(object sender, EventArgs e)
        {
            Value = new SettingsContainer();
        }

        #endregion        
    }
}
