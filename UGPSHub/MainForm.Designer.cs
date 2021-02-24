namespace UGPSHub
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.connectionBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.trackBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.trackExportBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.trackClearBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.logBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.logViewCurrentBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.logPlaybackBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.utilsBtn = new System.Windows.Forms.ToolStripDropDownButton();
            this.utilsDeviceInfoBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.utilsDeviceInfoQueryBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.utilsDeviceInfoViewBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.utilsSessionResetBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.utilsLogClearAllEntriesBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.utilsSnapshotsClearAllEntriesBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.utilsTrackFilteringBtn = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsBtn = new System.Windows.Forms.ToolStripButton();
            this.infoBtn = new System.Windows.Forms.ToolStripButton();
            this.secondaryToolStrip = new System.Windows.Forms.ToolStrip();
            this.isAutoScreenshotBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.markCurrentPositionBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tracksToFitCbx = new System.Windows.Forms.ToolStripComboBox();
            this.mainStatusStrip = new System.Windows.Forms.StatusStrip();
            this.connectionStatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.secondaryStatusStrip = new System.Windows.Forms.StatusStrip();
            this.rerExeededFlagLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.buoysStatusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.geoPlot = new UCNLUI.Controls.uOSMGeoPlot();
            this.isHistoryVisibleBtn = new System.Windows.Forms.ToolStripButton();
            this.isStatisticsBtn = new System.Windows.Forms.ToolStripButton();
            this.mainToolStrip.SuspendLayout();
            this.secondaryToolStrip.SuspendLayout();
            this.mainStatusStrip.SuspendLayout();
            this.secondaryStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionBtn,
            this.toolStripSeparator1,
            this.trackBtn,
            this.logBtn,
            this.toolStripSeparator2,
            this.utilsBtn,
            this.toolStripSeparator5,
            this.settingsBtn,
            this.infoBtn});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(919, 35);
            this.mainToolStrip.TabIndex = 0;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // connectionBtn
            // 
            this.connectionBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.connectionBtn.Image = ((System.Drawing.Image)(resources.GetObject("connectionBtn.Image")));
            this.connectionBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.connectionBtn.Name = "connectionBtn";
            this.connectionBtn.Size = new System.Drawing.Size(147, 32);
            this.connectionBtn.Text = "CONNECTION";
            this.connectionBtn.Click += new System.EventHandler(this.connectionBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // trackBtn
            // 
            this.trackBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.trackBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.trackExportBtn,
            this.toolStripSeparator3,
            this.trackClearBtn});
            this.trackBtn.Enabled = false;
            this.trackBtn.Image = ((System.Drawing.Image)(resources.GetObject("trackBtn.Image")));
            this.trackBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.trackBtn.Name = "trackBtn";
            this.trackBtn.Size = new System.Drawing.Size(89, 32);
            this.trackBtn.Text = "TRACK";
            // 
            // trackExportBtn
            // 
            this.trackExportBtn.Name = "trackExportBtn";
            this.trackExportBtn.Size = new System.Drawing.Size(206, 32);
            this.trackExportBtn.Text = "EXPORT AS...";
            this.trackExportBtn.Click += new System.EventHandler(this.trackExportBtn_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(203, 6);
            // 
            // trackClearBtn
            // 
            this.trackClearBtn.Name = "trackClearBtn";
            this.trackClearBtn.Size = new System.Drawing.Size(206, 32);
            this.trackClearBtn.Text = "CLEAR";
            this.trackClearBtn.Click += new System.EventHandler(this.trackClearBtn_Click);
            // 
            // logBtn
            // 
            this.logBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.logBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logViewCurrentBtn,
            this.toolStripSeparator4,
            this.logPlaybackBtn});
            this.logBtn.Image = ((System.Drawing.Image)(resources.GetObject("logBtn.Image")));
            this.logBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.logBtn.Name = "logBtn";
            this.logBtn.Size = new System.Drawing.Size(64, 32);
            this.logBtn.Text = "LOG";
            // 
            // logViewCurrentBtn
            // 
            this.logViewCurrentBtn.Name = "logViewCurrentBtn";
            this.logViewCurrentBtn.Size = new System.Drawing.Size(231, 32);
            this.logViewCurrentBtn.Text = "VIEW CURRENT";
            this.logViewCurrentBtn.Click += new System.EventHandler(this.logViewCurrentBtn_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(228, 6);
            // 
            // logPlaybackBtn
            // 
            this.logPlaybackBtn.Name = "logPlaybackBtn";
            this.logPlaybackBtn.Size = new System.Drawing.Size(231, 32);
            this.logPlaybackBtn.Text = "PLAYBACK...";
            this.logPlaybackBtn.Click += new System.EventHandler(this.logPlaybackBtn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 35);
            // 
            // utilsBtn
            // 
            this.utilsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.utilsBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.utilsDeviceInfoBtn,
            this.utilsSessionResetBtn,
            this.toolStripSeparator7,
            this.utilsLogClearAllEntriesBtn,
            this.toolStripSeparator8,
            this.utilsSnapshotsClearAllEntriesBtn,
            this.toolStripSeparator9,
            this.utilsTrackFilteringBtn});
            this.utilsBtn.Image = ((System.Drawing.Image)(resources.GetObject("utilsBtn.Image")));
            this.utilsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.utilsBtn.Name = "utilsBtn";
            this.utilsBtn.Size = new System.Drawing.Size(78, 32);
            this.utilsBtn.Text = "UTILS";
            // 
            // utilsDeviceInfoBtn
            // 
            this.utilsDeviceInfoBtn.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.utilsDeviceInfoQueryBtn,
            this.utilsDeviceInfoViewBtn});
            this.utilsDeviceInfoBtn.Enabled = false;
            this.utilsDeviceInfoBtn.Name = "utilsDeviceInfoBtn";
            this.utilsDeviceInfoBtn.Size = new System.Drawing.Size(306, 32);
            this.utilsDeviceInfoBtn.Text = "DEVICE INFO";
            // 
            // utilsDeviceInfoQueryBtn
            // 
            this.utilsDeviceInfoQueryBtn.Name = "utilsDeviceInfoQueryBtn";
            this.utilsDeviceInfoQueryBtn.Size = new System.Drawing.Size(149, 32);
            this.utilsDeviceInfoQueryBtn.Text = "QUERY";
            this.utilsDeviceInfoQueryBtn.Click += new System.EventHandler(this.utilsDeviceInfoQueryBtn_Click);
            // 
            // utilsDeviceInfoViewBtn
            // 
            this.utilsDeviceInfoViewBtn.Name = "utilsDeviceInfoViewBtn";
            this.utilsDeviceInfoViewBtn.Size = new System.Drawing.Size(149, 32);
            this.utilsDeviceInfoViewBtn.Text = "VIEW";
            this.utilsDeviceInfoViewBtn.Visible = false;
            this.utilsDeviceInfoViewBtn.Click += new System.EventHandler(this.utilsDeviceInfoViewBtn_Click);
            // 
            // utilsSessionResetBtn
            // 
            this.utilsSessionResetBtn.Name = "utilsSessionResetBtn";
            this.utilsSessionResetBtn.Size = new System.Drawing.Size(306, 32);
            this.utilsSessionResetBtn.Text = "SESSION RESET";
            this.utilsSessionResetBtn.Click += new System.EventHandler(this.utilsSessionResetBtn_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(303, 6);
            // 
            // utilsLogClearAllEntriesBtn
            // 
            this.utilsLogClearAllEntriesBtn.Name = "utilsLogClearAllEntriesBtn";
            this.utilsLogClearAllEntriesBtn.Size = new System.Drawing.Size(306, 32);
            this.utilsLogClearAllEntriesBtn.Text = "CLEAR ALL LOGS";
            this.utilsLogClearAllEntriesBtn.Click += new System.EventHandler(this.utilsLogClearAllEntriesBtn_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(303, 6);
            // 
            // utilsSnapshotsClearAllEntriesBtn
            // 
            this.utilsSnapshotsClearAllEntriesBtn.Name = "utilsSnapshotsClearAllEntriesBtn";
            this.utilsSnapshotsClearAllEntriesBtn.Size = new System.Drawing.Size(306, 32);
            this.utilsSnapshotsClearAllEntriesBtn.Text = "CLEAR ALL SNAPSHOTS";
            this.utilsSnapshotsClearAllEntriesBtn.Click += new System.EventHandler(this.utilsScreenShotsClearAllEntriesBtn_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(303, 6);
            // 
            // utilsTrackFilteringBtn
            // 
            this.utilsTrackFilteringBtn.Name = "utilsTrackFilteringBtn";
            this.utilsTrackFilteringBtn.Size = new System.Drawing.Size(306, 32);
            this.utilsTrackFilteringBtn.Text = "TRACK FILTER...";
            this.utilsTrackFilteringBtn.Click += new System.EventHandler(this.utilsTrackFilteringBtn_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 35);
            // 
            // settingsBtn
            // 
            this.settingsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.settingsBtn.Image = ((System.Drawing.Image)(resources.GetObject("settingsBtn.Image")));
            this.settingsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(109, 32);
            this.settingsBtn.Text = "SETTINGS";
            this.settingsBtn.Click += new System.EventHandler(this.settingsBtn_Click);
            // 
            // infoBtn
            // 
            this.infoBtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.infoBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.infoBtn.Image = ((System.Drawing.Image)(resources.GetObject("infoBtn.Image")));
            this.infoBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.infoBtn.Name = "infoBtn";
            this.infoBtn.Size = new System.Drawing.Size(63, 32);
            this.infoBtn.Text = "INFO";
            this.infoBtn.Click += new System.EventHandler(this.infoBtn_Click);
            // 
            // secondaryToolStrip
            // 
            this.secondaryToolStrip.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.secondaryToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.isAutoScreenshotBtn,
            this.toolStripSeparator6,
            this.markCurrentPositionBtn,
            this.toolStripSeparator10,
            this.toolStripLabel1,
            this.tracksToFitCbx,
            this.isHistoryVisibleBtn,
            this.isStatisticsBtn});
            this.secondaryToolStrip.Location = new System.Drawing.Point(0, 35);
            this.secondaryToolStrip.Name = "secondaryToolStrip";
            this.secondaryToolStrip.Size = new System.Drawing.Size(919, 30);
            this.secondaryToolStrip.TabIndex = 1;
            this.secondaryToolStrip.Text = "toolStrip2";
            // 
            // isAutoScreenshotBtn
            // 
            this.isAutoScreenshotBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.isAutoScreenshotBtn.Image = ((System.Drawing.Image)(resources.GetObject("isAutoScreenshotBtn.Image")));
            this.isAutoScreenshotBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.isAutoScreenshotBtn.Name = "isAutoScreenshotBtn";
            this.isAutoScreenshotBtn.Size = new System.Drawing.Size(168, 27);
            this.isAutoScreenshotBtn.Text = "AUTOSCREENSHOT";
            this.isAutoScreenshotBtn.Click += new System.EventHandler(this.isAutoScreenshotBtn_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 30);
            // 
            // markCurrentPositionBtn
            // 
            this.markCurrentPositionBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.markCurrentPositionBtn.Enabled = false;
            this.markCurrentPositionBtn.Image = ((System.Drawing.Image)(resources.GetObject("markCurrentPositionBtn.Image")));
            this.markCurrentPositionBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.markCurrentPositionBtn.Name = "markCurrentPositionBtn";
            this.markCurrentPositionBtn.Size = new System.Drawing.Size(148, 27);
            this.markCurrentPositionBtn.Text = "MARK POSITION";
            this.markCurrentPositionBtn.Click += new System.EventHandler(this.markCurrentPositionBtn_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 30);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(131, 27);
            this.toolStripLabel1.Text = "TRACKS TO FIT";
            // 
            // tracksToFitCbx
            // 
            this.tracksToFitCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tracksToFitCbx.DropDownWidth = 200;
            this.tracksToFitCbx.Name = "tracksToFitCbx";
            this.tracksToFitCbx.Size = new System.Drawing.Size(200, 30);
            this.tracksToFitCbx.SelectedIndexChanged += new System.EventHandler(this.tracksToFitCbx_SelectedIndexChanged);
            // 
            // mainStatusStrip
            // 
            this.mainStatusStrip.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.mainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionStatusLbl});
            this.mainStatusStrip.Location = new System.Drawing.Point(0, 485);
            this.mainStatusStrip.Name = "mainStatusStrip";
            this.mainStatusStrip.Size = new System.Drawing.Size(919, 28);
            this.mainStatusStrip.TabIndex = 2;
            this.mainStatusStrip.Text = "statusStrip1";
            // 
            // connectionStatusLbl
            // 
            this.connectionStatusLbl.Name = "connectionStatusLbl";
            this.connectionStatusLbl.Size = new System.Drawing.Size(15, 23);
            this.connectionStatusLbl.Text = ".";
            // 
            // secondaryStatusStrip
            // 
            this.secondaryStatusStrip.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.secondaryStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rerExeededFlagLbl,
            this.toolStripStatusLabel1,
            this.buoysStatusLbl});
            this.secondaryStatusStrip.Location = new System.Drawing.Point(0, 457);
            this.secondaryStatusStrip.Name = "secondaryStatusStrip";
            this.secondaryStatusStrip.Size = new System.Drawing.Size(919, 28);
            this.secondaryStatusStrip.TabIndex = 4;
            this.secondaryStatusStrip.Text = "statusStrip1";
            // 
            // rerExeededFlagLbl
            // 
            this.rerExeededFlagLbl.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rerExeededFlagLbl.ForeColor = System.Drawing.Color.Red;
            this.rerExeededFlagLbl.Name = "rerExeededFlagLbl";
            this.rerExeededFlagLbl.Size = new System.Drawing.Size(41, 23);
            this.rerExeededFlagLbl.Text = "RER";
            this.rerExeededFlagLbl.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(99, 23);
            this.toolStripStatusLabel1.Text = "Base status:";
            // 
            // buoysStatusLbl
            // 
            this.buoysStatusLbl.Name = "buoysStatusLbl";
            this.buoysStatusLbl.Size = new System.Drawing.Size(14, 23);
            this.buoysStatusLbl.Text = ".";
            // 
            // geoPlot
            // 
            this.geoPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.geoPlot.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.geoPlot.HistoryLinesNumber = 5;
            this.geoPlot.HistoryTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.geoPlot.HistoryTextFont = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.geoPlot.HistoryVisible = true;
            this.geoPlot.LeftUpperText = null;
            this.geoPlot.LeftUpperTextColor = System.Drawing.Color.Black;
            this.geoPlot.LeftUpperTextFont = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.geoPlot.LegendFont = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.geoPlot.Location = new System.Drawing.Point(13, 70);
            this.geoPlot.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.geoPlot.MaxHistoryLineLength = 127;
            this.geoPlot.MeasurementLineColor = System.Drawing.Color.Black;
            this.geoPlot.MeasurementTextBgColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.geoPlot.MeasurementTextColor = System.Drawing.Color.Black;
            this.geoPlot.MeasurementTextFont = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.geoPlot.Name = "geoPlot";
            this.geoPlot.Padding = new System.Windows.Forms.Padding(14, 18, 14, 18);
            this.geoPlot.ScaleLineColor = System.Drawing.SystemColors.ControlText;
            this.geoPlot.ScaleLineFont = new System.Drawing.Font("Consolas", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.geoPlot.Size = new System.Drawing.Size(893, 382);
            this.geoPlot.TabIndex = 5;
            this.geoPlot.TextBackgroundColor = System.Drawing.Color.Empty;
            // 
            // isHistoryVisibleBtn
            // 
            this.isHistoryVisibleBtn.Checked = true;
            this.isHistoryVisibleBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.isHistoryVisibleBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.isHistoryVisibleBtn.Image = ((System.Drawing.Image)(resources.GetObject("isHistoryVisibleBtn.Image")));
            this.isHistoryVisibleBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.isHistoryVisibleBtn.Name = "isHistoryVisibleBtn";
            this.isHistoryVisibleBtn.Size = new System.Drawing.Size(85, 27);
            this.isHistoryVisibleBtn.Text = "HISTORY";
            this.isHistoryVisibleBtn.Click += new System.EventHandler(this.isHistoryVisibleBtn_Click);
            // 
            // isStatisticsBtn
            // 
            this.isStatisticsBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.isStatisticsBtn.Image = ((System.Drawing.Image)(resources.GetObject("isStatisticsBtn.Image")));
            this.isStatisticsBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.isStatisticsBtn.Name = "isStatisticsBtn";
            this.isStatisticsBtn.Size = new System.Drawing.Size(105, 27);
            this.isStatisticsBtn.Text = "STATISTICS";
            this.isStatisticsBtn.Click += new System.EventHandler(this.isStatisticsBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 513);
            this.Controls.Add(this.geoPlot);
            this.Controls.Add(this.secondaryStatusStrip);
            this.Controls.Add(this.mainStatusStrip);
            this.Controls.Add(this.secondaryToolStrip);
            this.Controls.Add(this.mainToolStrip);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            this.secondaryToolStrip.ResumeLayout(false);
            this.secondaryToolStrip.PerformLayout();
            this.mainStatusStrip.ResumeLayout(false);
            this.mainStatusStrip.PerformLayout();
            this.secondaryStatusStrip.ResumeLayout(false);
            this.secondaryStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStrip secondaryToolStrip;
        private System.Windows.Forms.StatusStrip mainStatusStrip;
        private System.Windows.Forms.ToolStripButton connectionBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton trackBtn;
        private System.Windows.Forms.ToolStripMenuItem trackExportBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem trackClearBtn;
        private System.Windows.Forms.ToolStripDropDownButton logBtn;
        private System.Windows.Forms.ToolStripMenuItem logViewCurrentBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem logPlaybackBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton utilsBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton settingsBtn;
        private System.Windows.Forms.ToolStripButton infoBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem utilsLogClearAllEntriesBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem utilsSnapshotsClearAllEntriesBtn;
        private System.Windows.Forms.ToolStripButton isAutoScreenshotBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripStatusLabel connectionStatusLbl;
        private System.Windows.Forms.ToolStripButton markCurrentPositionBtn;
        private System.Windows.Forms.ToolStripMenuItem utilsDeviceInfoBtn;
        private System.Windows.Forms.ToolStripMenuItem utilsDeviceInfoQueryBtn;
        private System.Windows.Forms.ToolStripMenuItem utilsDeviceInfoViewBtn;
        private System.Windows.Forms.StatusStrip secondaryStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel buoysStatusLbl;
        private System.Windows.Forms.ToolStripStatusLabel rerExeededFlagLbl;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem utilsTrackFilteringBtn;
        private System.Windows.Forms.ToolStripMenuItem utilsSessionResetBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox tracksToFitCbx;
        private UCNLUI.Controls.uOSMGeoPlot geoPlot;
        private System.Windows.Forms.ToolStripButton isHistoryVisibleBtn;
        private System.Windows.Forms.ToolStripButton isStatisticsBtn;

    }
}

