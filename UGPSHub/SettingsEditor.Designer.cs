namespace UGPSHub
{
    partial class SettingsEditor
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
            this.cancelBtn = new System.Windows.Forms.Button();
            this.okBtn = new System.Windows.Forms.Button();
            this.connectionTable = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.rnPortNameCbx = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rnBaudrateCbx = new System.Windows.Forms.ComboBox();
            this.auxGNSSLbl = new System.Windows.Forms.Label();
            this.outputLbl = new System.Windows.Forms.Label();
            this.auxGNSSBaudrateCbx = new System.Windows.Forms.ComboBox();
            this.outputPortNameCbx = new System.Windows.Forms.ComboBox();
            this.outputBaudrateCbx = new System.Windows.Forms.ComboBox();
            this.auxGNSSPortNameCbx = new System.Windows.Forms.ComboBox();
            this.isUseAUXChb = new System.Windows.Forms.CheckBox();
            this.isUseOutputChb = new System.Windows.Forms.CheckBox();
            this.refreshPortsBtn = new System.Windows.Forms.LinkLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.radialErrorThresholdEdit = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.salinityDataBaseBtn = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.salinityEdit = new System.Windows.Forms.NumericUpDown();
            this.speedOfSoundLbl = new System.Windows.Forms.Label();
            this.speedOfSoundEdit = new System.Windows.Forms.NumericUpDown();
            this.isAutoSoundSpeedChb = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.crsEstimatorFIFOSizeEdit = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.trackFilterFIFOSizeEdit = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.trackViewFIFOSizeEdit = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.restoreDefaultsBtn = new System.Windows.Forms.Button();
            this.connectionTable.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radialErrorThresholdEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.salinityEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedOfSoundEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.crsEstimatorFIFOSizeEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackFilterFIFOSizeEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackViewFIFOSizeEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // cancelBtn
            // 
            this.cancelBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(469, 535);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(113, 42);
            this.cancelBtn.TabIndex = 0;
            this.cancelBtn.Text = "CANCEL";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // okBtn
            // 
            this.okBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okBtn.Enabled = false;
            this.okBtn.Location = new System.Drawing.Point(321, 535);
            this.okBtn.Name = "okBtn";
            this.okBtn.Size = new System.Drawing.Size(113, 42);
            this.okBtn.TabIndex = 1;
            this.okBtn.Text = "OK";
            this.okBtn.UseVisualStyleBackColor = true;
            // 
            // connectionTable
            // 
            this.connectionTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.connectionTable.ColumnCount = 4;
            this.connectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.connectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.connectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.connectionTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.connectionTable.Controls.Add(this.label1, 0, 1);
            this.connectionTable.Controls.Add(this.rnPortNameCbx, 1, 1);
            this.connectionTable.Controls.Add(this.label2, 1, 0);
            this.connectionTable.Controls.Add(this.label3, 2, 0);
            this.connectionTable.Controls.Add(this.rnBaudrateCbx, 2, 1);
            this.connectionTable.Controls.Add(this.auxGNSSLbl, 0, 2);
            this.connectionTable.Controls.Add(this.outputLbl, 0, 3);
            this.connectionTable.Controls.Add(this.auxGNSSBaudrateCbx, 2, 2);
            this.connectionTable.Controls.Add(this.outputPortNameCbx, 1, 3);
            this.connectionTable.Controls.Add(this.outputBaudrateCbx, 2, 3);
            this.connectionTable.Controls.Add(this.auxGNSSPortNameCbx, 1, 2);
            this.connectionTable.Controls.Add(this.isUseAUXChb, 3, 2);
            this.connectionTable.Controls.Add(this.isUseOutputChb, 3, 3);
            this.connectionTable.Location = new System.Drawing.Point(13, 40);
            this.connectionTable.Name = "connectionTable";
            this.connectionTable.RowCount = 5;
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.connectionTable.Size = new System.Drawing.Size(569, 186);
            this.connectionTable.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "RedNODE";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rnPortNameCbx
            // 
            this.rnPortNameCbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rnPortNameCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rnPortNameCbx.FormattingEnabled = true;
            this.rnPortNameCbx.Location = new System.Drawing.Point(116, 31);
            this.rnPortNameCbx.Name = "rnPortNameCbx";
            this.rnPortNameCbx.Size = new System.Drawing.Size(121, 36);
            this.rnPortNameCbx.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(116, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 28);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(243, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 28);
            this.label3.TabIndex = 3;
            this.label3.Text = "Baudrate";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rnBaudrateCbx
            // 
            this.rnBaudrateCbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rnBaudrateCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.rnBaudrateCbx.FormattingEnabled = true;
            this.rnBaudrateCbx.Location = new System.Drawing.Point(243, 31);
            this.rnBaudrateCbx.Name = "rnBaudrateCbx";
            this.rnBaudrateCbx.Size = new System.Drawing.Size(126, 36);
            this.rnBaudrateCbx.TabIndex = 4;
            // 
            // auxGNSSLbl
            // 
            this.auxGNSSLbl.AutoSize = true;
            this.auxGNSSLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.auxGNSSLbl.Enabled = false;
            this.auxGNSSLbl.Location = new System.Drawing.Point(3, 65);
            this.auxGNSSLbl.Margin = new System.Windows.Forms.Padding(3);
            this.auxGNSSLbl.Name = "auxGNSSLbl";
            this.auxGNSSLbl.Size = new System.Drawing.Size(107, 28);
            this.auxGNSSLbl.TabIndex = 5;
            this.auxGNSSLbl.Text = "AUX GNSS";
            this.auxGNSSLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // outputLbl
            // 
            this.outputLbl.AutoSize = true;
            this.outputLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputLbl.Enabled = false;
            this.outputLbl.Location = new System.Drawing.Point(3, 99);
            this.outputLbl.Margin = new System.Windows.Forms.Padding(3);
            this.outputLbl.Name = "outputLbl";
            this.outputLbl.Size = new System.Drawing.Size(107, 28);
            this.outputLbl.TabIndex = 6;
            this.outputLbl.Text = "Output";
            this.outputLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // auxGNSSBaudrateCbx
            // 
            this.auxGNSSBaudrateCbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.auxGNSSBaudrateCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.auxGNSSBaudrateCbx.Enabled = false;
            this.auxGNSSBaudrateCbx.FormattingEnabled = true;
            this.auxGNSSBaudrateCbx.Location = new System.Drawing.Point(243, 65);
            this.auxGNSSBaudrateCbx.Name = "auxGNSSBaudrateCbx";
            this.auxGNSSBaudrateCbx.Size = new System.Drawing.Size(126, 36);
            this.auxGNSSBaudrateCbx.TabIndex = 8;
            // 
            // outputPortNameCbx
            // 
            this.outputPortNameCbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputPortNameCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outputPortNameCbx.Enabled = false;
            this.outputPortNameCbx.FormattingEnabled = true;
            this.outputPortNameCbx.Location = new System.Drawing.Point(116, 99);
            this.outputPortNameCbx.Name = "outputPortNameCbx";
            this.outputPortNameCbx.Size = new System.Drawing.Size(121, 36);
            this.outputPortNameCbx.TabIndex = 9;
            this.outputPortNameCbx.SelectedIndexChanged += new System.EventHandler(this.outputPortNameCbx_SelectedIndexChanged);
            // 
            // outputBaudrateCbx
            // 
            this.outputBaudrateCbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputBaudrateCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.outputBaudrateCbx.Enabled = false;
            this.outputBaudrateCbx.FormattingEnabled = true;
            this.outputBaudrateCbx.Location = new System.Drawing.Point(243, 99);
            this.outputBaudrateCbx.Name = "outputBaudrateCbx";
            this.outputBaudrateCbx.Size = new System.Drawing.Size(126, 36);
            this.outputBaudrateCbx.TabIndex = 7;
            // 
            // auxGNSSPortNameCbx
            // 
            this.auxGNSSPortNameCbx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.auxGNSSPortNameCbx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.auxGNSSPortNameCbx.Enabled = false;
            this.auxGNSSPortNameCbx.FormattingEnabled = true;
            this.auxGNSSPortNameCbx.Location = new System.Drawing.Point(116, 65);
            this.auxGNSSPortNameCbx.Name = "auxGNSSPortNameCbx";
            this.auxGNSSPortNameCbx.Size = new System.Drawing.Size(121, 36);
            this.auxGNSSPortNameCbx.TabIndex = 10;
            this.auxGNSSPortNameCbx.SelectedIndexChanged += new System.EventHandler(this.auxGNSSPortNameCbx_SelectedIndexChanged);
            // 
            // isUseAUXChb
            // 
            this.isUseAUXChb.AutoSize = true;
            this.isUseAUXChb.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.isUseAUXChb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.isUseAUXChb.Location = new System.Drawing.Point(375, 62);
            this.isUseAUXChb.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.isUseAUXChb.Name = "isUseAUXChb";
            this.isUseAUXChb.Size = new System.Drawing.Size(191, 34);
            this.isUseAUXChb.TabIndex = 11;
            this.isUseAUXChb.Text = "Use AUX GNSS";
            this.isUseAUXChb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.isUseAUXChb.UseVisualStyleBackColor = true;
            this.isUseAUXChb.CheckedChanged += new System.EventHandler(this.isUseAUXChb_CheckedChanged);
            // 
            // isUseOutputChb
            // 
            this.isUseOutputChb.AutoSize = true;
            this.isUseOutputChb.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.isUseOutputChb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.isUseOutputChb.Location = new System.Drawing.Point(375, 96);
            this.isUseOutputChb.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.isUseOutputChb.Name = "isUseOutputChb";
            this.isUseOutputChb.Size = new System.Drawing.Size(191, 34);
            this.isUseOutputChb.TabIndex = 12;
            this.isUseOutputChb.Text = "Use output port";
            this.isUseOutputChb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.isUseOutputChb.UseVisualStyleBackColor = true;
            this.isUseOutputChb.CheckedChanged += new System.EventHandler(this.isUseOutputChb_CheckedChanged);
            // 
            // refreshPortsBtn
            // 
            this.refreshPortsBtn.AutoSize = true;
            this.refreshPortsBtn.Location = new System.Drawing.Point(6, 9);
            this.refreshPortsBtn.Name = "refreshPortsBtn";
            this.refreshPortsBtn.Size = new System.Drawing.Size(138, 28);
            this.refreshPortsBtn.TabIndex = 2;
            this.refreshPortsBtn.TabStop = true;
            this.refreshPortsBtn.Text = "Refresh ports...";
            this.refreshPortsBtn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.refreshPortsBtn_LinkClicked);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 201F));
            this.tableLayoutPanel1.Controls.Add(this.radialErrorThresholdEdit, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.salinityDataBaseBtn, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.salinityEdit, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.speedOfSoundLbl, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.speedOfSoundEdit, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.isAutoSoundSpeedChb, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.crsEstimatorFIFOSizeEdit, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.trackFilterFIFOSizeEdit, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label11, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.trackViewFIFOSizeEdit, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 232);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(569, 279);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // radialErrorThresholdEdit
            // 
            this.radialErrorThresholdEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radialErrorThresholdEdit.Location = new System.Drawing.Point(251, 111);
            this.radialErrorThresholdEdit.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.radialErrorThresholdEdit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.radialErrorThresholdEdit.Name = "radialErrorThresholdEdit";
            this.radialErrorThresholdEdit.Size = new System.Drawing.Size(114, 34);
            this.radialErrorThresholdEdit.TabIndex = 7;
            this.radialErrorThresholdEdit.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(3, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(242, 40);
            this.label8.TabIndex = 6;
            this.label8.Text = "Radial error threshold, m";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // salinityDataBaseBtn
            // 
            this.salinityDataBaseBtn.AutoSize = true;
            this.salinityDataBaseBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.salinityDataBaseBtn.Location = new System.Drawing.Point(371, 31);
            this.salinityDataBaseBtn.Margin = new System.Windows.Forms.Padding(3);
            this.salinityDataBaseBtn.Name = "salinityDataBaseBtn";
            this.salinityDataBaseBtn.Size = new System.Drawing.Size(195, 34);
            this.salinityDataBaseBtn.TabIndex = 5;
            this.salinityDataBaseBtn.TabStop = true;
            this.salinityDataBaseBtn.Text = "Search in database";
            this.salinityDataBaseBtn.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.salinityDataBaseBtn_LinkClicked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(3, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(242, 40);
            this.label6.TabIndex = 0;
            this.label6.Text = "Salinity, PSU";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // salinityEdit
            // 
            this.salinityEdit.DecimalPlaces = 1;
            this.salinityEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.salinityEdit.Location = new System.Drawing.Point(251, 31);
            this.salinityEdit.Maximum = new decimal(new int[] {
            42,
            0,
            0,
            0});
            this.salinityEdit.Name = "salinityEdit";
            this.salinityEdit.Size = new System.Drawing.Size(114, 34);
            this.salinityEdit.TabIndex = 1;
            // 
            // speedOfSoundLbl
            // 
            this.speedOfSoundLbl.AutoSize = true;
            this.speedOfSoundLbl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.speedOfSoundLbl.Location = new System.Drawing.Point(3, 68);
            this.speedOfSoundLbl.Name = "speedOfSoundLbl";
            this.speedOfSoundLbl.Size = new System.Drawing.Size(242, 40);
            this.speedOfSoundLbl.TabIndex = 2;
            this.speedOfSoundLbl.Text = "Speed of sound, m/s";
            this.speedOfSoundLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // speedOfSoundEdit
            // 
            this.speedOfSoundEdit.DecimalPlaces = 1;
            this.speedOfSoundEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.speedOfSoundEdit.Location = new System.Drawing.Point(251, 71);
            this.speedOfSoundEdit.Maximum = new decimal(new int[] {
            1600,
            0,
            0,
            0});
            this.speedOfSoundEdit.Minimum = new decimal(new int[] {
            1300,
            0,
            0,
            0});
            this.speedOfSoundEdit.Name = "speedOfSoundEdit";
            this.speedOfSoundEdit.Size = new System.Drawing.Size(114, 34);
            this.speedOfSoundEdit.TabIndex = 3;
            this.speedOfSoundEdit.Value = new decimal(new int[] {
            1450,
            0,
            0,
            0});
            // 
            // isAutoSoundSpeedChb
            // 
            this.isAutoSoundSpeedChb.AutoSize = true;
            this.isAutoSoundSpeedChb.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.isAutoSoundSpeedChb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.isAutoSoundSpeedChb.Location = new System.Drawing.Point(371, 71);
            this.isAutoSoundSpeedChb.Name = "isAutoSoundSpeedChb";
            this.isAutoSoundSpeedChb.Size = new System.Drawing.Size(195, 34);
            this.isAutoSoundSpeedChb.TabIndex = 4;
            this.isAutoSoundSpeedChb.Text = "Auto";
            this.isAutoSoundSpeedChb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.isAutoSoundSpeedChb.UseVisualStyleBackColor = true;
            this.isAutoSoundSpeedChb.CheckedChanged += new System.EventHandler(this.isAutoSoundSpeedChb_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(3, 148);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(242, 40);
            this.label9.TabIndex = 8;
            this.label9.Text = "Course estimator FIFO size";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // crsEstimatorFIFOSizeEdit
            // 
            this.crsEstimatorFIFOSizeEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crsEstimatorFIFOSizeEdit.Location = new System.Drawing.Point(251, 151);
            this.crsEstimatorFIFOSizeEdit.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.crsEstimatorFIFOSizeEdit.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.crsEstimatorFIFOSizeEdit.Name = "crsEstimatorFIFOSizeEdit";
            this.crsEstimatorFIFOSizeEdit.Size = new System.Drawing.Size(114, 34);
            this.crsEstimatorFIFOSizeEdit.TabIndex = 9;
            this.crsEstimatorFIFOSizeEdit.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(3, 188);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(242, 40);
            this.label10.TabIndex = 10;
            this.label10.Text = "Track filter FIFO size";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trackFilterFIFOSizeEdit
            // 
            this.trackFilterFIFOSizeEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackFilterFIFOSizeEdit.Location = new System.Drawing.Point(251, 191);
            this.trackFilterFIFOSizeEdit.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.trackFilterFIFOSizeEdit.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.trackFilterFIFOSizeEdit.Name = "trackFilterFIFOSizeEdit";
            this.trackFilterFIFOSizeEdit.Size = new System.Drawing.Size(114, 34);
            this.trackFilterFIFOSizeEdit.TabIndex = 11;
            this.trackFilterFIFOSizeEdit.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(3, 228);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(242, 40);
            this.label11.TabIndex = 12;
            this.label11.Text = "Track view FIFO size";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trackViewFIFOSizeEdit
            // 
            this.trackViewFIFOSizeEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackViewFIFOSizeEdit.Location = new System.Drawing.Point(251, 231);
            this.trackViewFIFOSizeEdit.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.trackViewFIFOSizeEdit.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.trackViewFIFOSizeEdit.Name = "trackViewFIFOSizeEdit";
            this.trackViewFIFOSizeEdit.Size = new System.Drawing.Size(114, 34);
            this.trackViewFIFOSizeEdit.TabIndex = 13;
            this.trackViewFIFOSizeEdit.Value = new decimal(new int[] {
            256,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(242, 28);
            this.label4.TabIndex = 14;
            this.label4.Text = "Parameter";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(251, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 28);
            this.label5.TabIndex = 15;
            this.label5.Text = "Value";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // restoreDefaultsBtn
            // 
            this.restoreDefaultsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.restoreDefaultsBtn.Location = new System.Drawing.Point(11, 535);
            this.restoreDefaultsBtn.Name = "restoreDefaultsBtn";
            this.restoreDefaultsBtn.Size = new System.Drawing.Size(113, 42);
            this.restoreDefaultsBtn.TabIndex = 4;
            this.restoreDefaultsBtn.Text = "RESET";
            this.restoreDefaultsBtn.UseVisualStyleBackColor = true;
            this.restoreDefaultsBtn.Click += new System.EventHandler(this.restoreDefaultsBtn_Click);
            // 
            // SettingsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 589);
            this.Controls.Add(this.restoreDefaultsBtn);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.refreshPortsBtn);
            this.Controls.Add(this.connectionTable);
            this.Controls.Add(this.okBtn);
            this.Controls.Add(this.cancelBtn);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SettingsEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SettingsEditor";
            this.connectionTable.ResumeLayout(false);
            this.connectionTable.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radialErrorThresholdEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.salinityEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.speedOfSoundEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.crsEstimatorFIFOSizeEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackFilterFIFOSizeEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackViewFIFOSizeEdit)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button okBtn;
        private System.Windows.Forms.TableLayoutPanel connectionTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox rnPortNameCbx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox rnBaudrateCbx;
        private System.Windows.Forms.Label auxGNSSLbl;
        private System.Windows.Forms.Label outputLbl;
        private System.Windows.Forms.ComboBox auxGNSSBaudrateCbx;
        private System.Windows.Forms.ComboBox outputPortNameCbx;
        private System.Windows.Forms.ComboBox outputBaudrateCbx;
        private System.Windows.Forms.ComboBox auxGNSSPortNameCbx;
        private System.Windows.Forms.CheckBox isUseAUXChb;
        private System.Windows.Forms.CheckBox isUseOutputChb;
        private System.Windows.Forms.LinkLabel refreshPortsBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel salinityDataBaseBtn;
        private System.Windows.Forms.NumericUpDown salinityEdit;
        private System.Windows.Forms.Label speedOfSoundLbl;
        private System.Windows.Forms.NumericUpDown speedOfSoundEdit;
        private System.Windows.Forms.CheckBox isAutoSoundSpeedChb;
        private System.Windows.Forms.NumericUpDown radialErrorThresholdEdit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown crsEstimatorFIFOSizeEdit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown trackFilterFIFOSizeEdit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown trackViewFIFOSizeEdit;
        private System.Windows.Forms.Button restoreDefaultsBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}