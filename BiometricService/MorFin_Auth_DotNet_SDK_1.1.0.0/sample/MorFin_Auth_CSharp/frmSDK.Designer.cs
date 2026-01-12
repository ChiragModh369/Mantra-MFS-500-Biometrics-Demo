namespace MorFin_Auth_CSharp
{
    partial class frmSDK
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSDK));
            this.btnStartCapture = new System.Windows.Forms.Button();
            this.txtTimeout = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnVersion = new System.Windows.Forms.Button();
            this.txtQuality = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCheckDevice = new System.Windows.Forms.Button();
            this.btnAutoCapture = new System.Windows.Forms.Button();
            this.btnUninit = new System.Windows.Forms.Button();
            this.btnInit = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnSdkVersion = new System.Windows.Forms.Button();
            this.btnStopCapture = new System.Windows.Forms.Button();
            this.cmbTemplateType = new System.Windows.Forms.ComboBox();
            this.cmbImageType = new System.Windows.Forms.ComboBox();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.btn_ConnectDvc = new System.Windows.Forms.Button();
            this.btn_SupportDvc = new System.Windows.Forms.Button();
            this.btnMatchFinger = new System.Windows.Forms.Button();
            this.pnlResult = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblQltyAndNfiq = new System.Windows.Forms.Label();
            this.gbSettings = new System.Windows.Forms.GroupBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbConnectedDvc = new System.Windows.Forms.ComboBox();
            this.cmbSupportedDvc = new System.Windows.Forms.ComboBox();
            this.lblSdkVersion = new System.Windows.Forms.Label();
            this.gbActions = new System.Windows.Forms.GroupBox();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.tlHeader = new System.Windows.Forms.TableLayoutPanel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pbIcon = new System.Windows.Forms.PictureBox();
            this.pbClose = new System.Windows.Forms.PictureBox();
            this.pnlBack = new System.Windows.Forms.Panel();
            this.txt_Client_Key = new System.Windows.Forms.TextBox();
            this.lbl_Client_Key = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblInitInfo = new System.Windows.Forms.Label();
            this.picFinger = new System.Windows.Forms.PictureBox();
            this.pbIsDeviceConnected = new System.Windows.Forms.PictureBox();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.pnlResult.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbSettings.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.gbActions.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.tlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).BeginInit();
            this.pnlBack.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFinger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIsDeviceConnected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStartCapture
            // 
            this.btnStartCapture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.btnStartCapture.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.btnStartCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartCapture.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartCapture.ForeColor = System.Drawing.Color.White;
            this.btnStartCapture.Location = new System.Drawing.Point(23, 76);
            this.btnStartCapture.Name = "btnStartCapture";
            this.btnStartCapture.Size = new System.Drawing.Size(210, 24);
            this.btnStartCapture.TabIndex = 25;
            this.btnStartCapture.Text = "Start Capture";
            this.btnStartCapture.UseVisualStyleBackColor = false;
            this.btnStartCapture.Click += new System.EventHandler(this.btnStartCapture_Click);
            // 
            // txtTimeout
            // 
            this.txtTimeout.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTimeout.Location = new System.Drawing.Point(392, 168);
            this.txtTimeout.MaxLength = 5;
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(61, 23);
            this.txtTimeout.TabIndex = 20;
            this.txtTimeout.Text = "10000";
            this.txtTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(121)))));
            this.label2.Location = new System.Drawing.Point(306, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 15);
            this.label2.TabIndex = 24;
            this.label2.Text = "Timeout (ms)";
            // 
            // btnVersion
            // 
            this.btnVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(204)))));
            this.btnVersion.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.btnVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVersion.Location = new System.Drawing.Point(91, -30);
            this.btnVersion.Name = "btnVersion";
            this.btnVersion.Size = new System.Drawing.Size(101, 30);
            this.btnVersion.TabIndex = 16;
            this.btnVersion.Text = "Version";
            this.btnVersion.UseVisualStyleBackColor = false;
            // 
            // txtQuality
            // 
            this.txtQuality.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuality.Location = new System.Drawing.Point(186, 168);
            this.txtQuality.MaxLength = 3;
            this.txtQuality.Name = "txtQuality";
            this.txtQuality.Size = new System.Drawing.Size(45, 23);
            this.txtQuality.TabIndex = 19;
            this.txtQuality.Text = "60";
            this.txtQuality.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(121)))));
            this.label1.Location = new System.Drawing.Point(90, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 15);
            this.label1.TabIndex = 22;
            this.label1.Text = "Quality (1-100)";
            // 
            // btnCheckDevice
            // 
            this.btnCheckDevice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.btnCheckDevice.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.btnCheckDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckDevice.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckDevice.ForeColor = System.Drawing.Color.White;
            this.btnCheckDevice.Location = new System.Drawing.Point(23, 19);
            this.btnCheckDevice.Name = "btnCheckDevice";
            this.btnCheckDevice.Size = new System.Drawing.Size(210, 24);
            this.btnCheckDevice.TabIndex = 17;
            this.btnCheckDevice.Text = "Check Device";
            this.btnCheckDevice.UseVisualStyleBackColor = false;
            this.btnCheckDevice.Click += new System.EventHandler(this.btnCheckDevice_Click);
            // 
            // btnAutoCapture
            // 
            this.btnAutoCapture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.btnAutoCapture.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.btnAutoCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAutoCapture.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutoCapture.ForeColor = System.Drawing.Color.White;
            this.btnAutoCapture.Location = new System.Drawing.Point(244, 19);
            this.btnAutoCapture.Name = "btnAutoCapture";
            this.btnAutoCapture.Size = new System.Drawing.Size(210, 24);
            this.btnAutoCapture.TabIndex = 21;
            this.btnAutoCapture.Text = "Auto Capture";
            this.btnAutoCapture.UseVisualStyleBackColor = false;
            this.btnAutoCapture.Click += new System.EventHandler(this.btnAutoCapture_Click);
            // 
            // btnUninit
            // 
            this.btnUninit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.btnUninit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.btnUninit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUninit.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUninit.ForeColor = System.Drawing.Color.White;
            this.btnUninit.Location = new System.Drawing.Point(244, 109);
            this.btnUninit.Name = "btnUninit";
            this.btnUninit.Size = new System.Drawing.Size(210, 24);
            this.btnUninit.TabIndex = 23;
            this.btnUninit.Text = "Uninit";
            this.btnUninit.UseVisualStyleBackColor = false;
            this.btnUninit.Click += new System.EventHandler(this.btnUninit_Click);
            // 
            // btnInit
            // 
            this.btnInit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.btnInit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.btnInit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInit.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInit.ForeColor = System.Drawing.Color.White;
            this.btnInit.Location = new System.Drawing.Point(23, 48);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(210, 24);
            this.btnInit.TabIndex = 18;
            this.btnInit.Text = "Init";
            this.btnInit.UseVisualStyleBackColor = false;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(366, 23);
            this.lblStatus.TabIndex = 28;
            this.lblStatus.Text = "- NA -";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSdkVersion
            // 
            this.btnSdkVersion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.btnSdkVersion.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.btnSdkVersion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSdkVersion.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSdkVersion.ForeColor = System.Drawing.Color.White;
            this.btnSdkVersion.Location = new System.Drawing.Point(23, 19);
            this.btnSdkVersion.Name = "btnSdkVersion";
            this.btnSdkVersion.Size = new System.Drawing.Size(210, 24);
            this.btnSdkVersion.TabIndex = 29;
            this.btnSdkVersion.Text = "Get SDK Version";
            this.btnSdkVersion.UseVisualStyleBackColor = false;
            this.btnSdkVersion.Click += new System.EventHandler(this.btnVersion_Click);
            // 
            // btnStopCapture
            // 
            this.btnStopCapture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.btnStopCapture.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.btnStopCapture.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStopCapture.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStopCapture.ForeColor = System.Drawing.Color.White;
            this.btnStopCapture.Location = new System.Drawing.Point(23, 106);
            this.btnStopCapture.Name = "btnStopCapture";
            this.btnStopCapture.Size = new System.Drawing.Size(210, 24);
            this.btnStopCapture.TabIndex = 32;
            this.btnStopCapture.Text = "Stop Capture";
            this.btnStopCapture.UseVisualStyleBackColor = false;
            this.btnStopCapture.Click += new System.EventHandler(this.btnStopCapture_Click);
            // 
            // cmbTemplateType
            // 
            this.cmbTemplateType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.cmbTemplateType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTemplateType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbTemplateType.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTemplateType.ForeColor = System.Drawing.Color.White;
            this.cmbTemplateType.FormattingEnabled = true;
            this.cmbTemplateType.Items.AddRange(new object[] {
            "FMR_V2005",
            "FMR_V2011",
            "ANSI_V378"});
            this.cmbTemplateType.Location = new System.Drawing.Point(245, 137);
            this.cmbTemplateType.Name = "cmbTemplateType";
            this.cmbTemplateType.Size = new System.Drawing.Size(210, 23);
            this.cmbTemplateType.TabIndex = 34;
            // 
            // cmbImageType
            // 
            this.cmbImageType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.cmbImageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImageType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbImageType.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbImageType.ForeColor = System.Drawing.Color.White;
            this.cmbImageType.FormattingEnabled = true;
            this.cmbImageType.Items.AddRange(new object[] {
            "BMP",
            "JPEG2000",
            "WSQ",
            "RAW",
            "FIR_V2005",
            "FIR_V2011",
            "FIR_WSQ_V2005",
            "FIR_WSQ_V2011",
            "FIR_JPEG2000_V2005",
            "FIR_JPEG2000_V2011"});
            this.cmbImageType.Location = new System.Drawing.Point(245, 106);
            this.cmbImageType.Name = "cmbImageType";
            this.cmbImageType.Size = new System.Drawing.Size(210, 23);
            this.cmbImageType.TabIndex = 36;
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.btnSaveImage.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.btnSaveImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveImage.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveImage.ForeColor = System.Drawing.Color.White;
            this.btnSaveImage.Location = new System.Drawing.Point(244, 49);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(210, 24);
            this.btnSaveImage.TabIndex = 39;
            this.btnSaveImage.Text = "Save Image/Template";
            this.btnSaveImage.UseVisualStyleBackColor = false;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // btn_ConnectDvc
            // 
            this.btn_ConnectDvc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.btn_ConnectDvc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.btn_ConnectDvc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ConnectDvc.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ConnectDvc.ForeColor = System.Drawing.Color.White;
            this.btn_ConnectDvc.Location = new System.Drawing.Point(23, 75);
            this.btn_ConnectDvc.Name = "btn_ConnectDvc";
            this.btn_ConnectDvc.Size = new System.Drawing.Size(210, 24);
            this.btn_ConnectDvc.TabIndex = 48;
            this.btn_ConnectDvc.Text = "Get Connected Device List";
            this.btn_ConnectDvc.UseVisualStyleBackColor = false;
            this.btn_ConnectDvc.Click += new System.EventHandler(this.btn_ConnectDvc_Click);
            // 
            // btn_SupportDvc
            // 
            this.btn_SupportDvc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.btn_SupportDvc.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.btn_SupportDvc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SupportDvc.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SupportDvc.ForeColor = System.Drawing.Color.White;
            this.btn_SupportDvc.Location = new System.Drawing.Point(23, 47);
            this.btn_SupportDvc.Name = "btn_SupportDvc";
            this.btn_SupportDvc.Size = new System.Drawing.Size(210, 24);
            this.btn_SupportDvc.TabIndex = 49;
            this.btn_SupportDvc.Text = "Get Supported Device List";
            this.btn_SupportDvc.UseVisualStyleBackColor = false;
            this.btn_SupportDvc.Click += new System.EventHandler(this.btn_SupportDvc_Click);
            // 
            // btnMatchFinger
            // 
            this.btnMatchFinger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.btnMatchFinger.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(153)))), ((int)(((byte)(255)))));
            this.btnMatchFinger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMatchFinger.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMatchFinger.ForeColor = System.Drawing.Color.White;
            this.btnMatchFinger.Location = new System.Drawing.Point(244, 79);
            this.btnMatchFinger.Name = "btnMatchFinger";
            this.btnMatchFinger.Size = new System.Drawing.Size(210, 24);
            this.btnMatchFinger.TabIndex = 46;
            this.btnMatchFinger.Text = "Match Finger";
            this.btnMatchFinger.UseVisualStyleBackColor = false;
            this.btnMatchFinger.Click += new System.EventHandler(this.btnMatchFinger_Click);
            // 
            // pnlResult
            // 
            this.pnlResult.BackColor = System.Drawing.Color.White;
            this.pnlResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlResult.Controls.Add(this.lblStatus);
            this.pnlResult.Location = new System.Drawing.Point(494, 40);
            this.pnlResult.Name = "pnlResult";
            this.pnlResult.Size = new System.Drawing.Size(368, 25);
            this.pnlResult.TabIndex = 45;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblQltyAndNfiq);
            this.panel1.Location = new System.Drawing.Point(494, 480);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(368, 27);
            this.panel1.TabIndex = 46;
            // 
            // lblQltyAndNfiq
            // 
            this.lblQltyAndNfiq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.lblQltyAndNfiq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblQltyAndNfiq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblQltyAndNfiq.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQltyAndNfiq.ForeColor = System.Drawing.Color.White;
            this.lblQltyAndNfiq.Location = new System.Drawing.Point(0, 0);
            this.lblQltyAndNfiq.Name = "lblQltyAndNfiq";
            this.lblQltyAndNfiq.Size = new System.Drawing.Size(368, 27);
            this.lblQltyAndNfiq.TabIndex = 28;
            this.lblQltyAndNfiq.Text = "- NA -";
            this.lblQltyAndNfiq.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbSettings
            // 
            this.gbSettings.Controls.Add(this.panel3);
            this.gbSettings.Controls.Add(this.panel2);
            this.gbSettings.Controls.Add(this.cmbConnectedDvc);
            this.gbSettings.Controls.Add(this.cmbSupportedDvc);
            this.gbSettings.Controls.Add(this.lblSdkVersion);
            this.gbSettings.Controls.Add(this.btn_ConnectDvc);
            this.gbSettings.Controls.Add(this.btnSdkVersion);
            this.gbSettings.Controls.Add(this.btn_SupportDvc);
            this.gbSettings.Controls.Add(this.cmbImageType);
            this.gbSettings.Controls.Add(this.txtQuality);
            this.gbSettings.Controls.Add(this.txtTimeout);
            this.gbSettings.Controls.Add(this.label2);
            this.gbSettings.Controls.Add(this.label1);
            this.gbSettings.Controls.Add(this.cmbTemplateType);
            this.gbSettings.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(121)))));
            this.gbSettings.Location = new System.Drawing.Point(11, 38);
            this.gbSettings.Name = "gbSettings";
            this.gbSettings.Size = new System.Drawing.Size(479, 198);
            this.gbSettings.TabIndex = 50;
            this.gbSettings.TabStop = false;
            this.gbSettings.Text = "Settings";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Location = new System.Drawing.Point(23, 134);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(210, 26);
            this.panel3.TabIndex = 48;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(0, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(210, 26);
            this.label10.TabIndex = 29;
            this.label10.Text = "Select Template Format";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(0, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(210, 26);
            this.label11.TabIndex = 28;
            this.label11.Text = "- NA -";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(23, 103);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(210, 26);
            this.panel2.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(210, 26);
            this.label5.TabIndex = 29;
            this.label5.Text = "Select Image Format";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 26);
            this.label4.TabIndex = 28;
            this.label4.Text = "- NA -";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbConnectedDvc
            // 
            this.cmbConnectedDvc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.cmbConnectedDvc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConnectedDvc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbConnectedDvc.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbConnectedDvc.ForeColor = System.Drawing.Color.White;
            this.cmbConnectedDvc.FormattingEnabled = true;
            this.cmbConnectedDvc.Location = new System.Drawing.Point(245, 75);
            this.cmbConnectedDvc.Name = "cmbConnectedDvc";
            this.cmbConnectedDvc.Size = new System.Drawing.Size(210, 23);
            this.cmbConnectedDvc.TabIndex = 51;
            this.cmbConnectedDvc.SelectedIndexChanged += new System.EventHandler(this.cmbConnectedDvc_SelectedIndexChanged);
            // 
            // cmbSupportedDvc
            // 
            this.cmbSupportedDvc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.cmbSupportedDvc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupportedDvc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbSupportedDvc.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSupportedDvc.ForeColor = System.Drawing.Color.White;
            this.cmbSupportedDvc.FormattingEnabled = true;
            this.cmbSupportedDvc.Location = new System.Drawing.Point(245, 47);
            this.cmbSupportedDvc.Name = "cmbSupportedDvc";
            this.cmbSupportedDvc.Size = new System.Drawing.Size(210, 23);
            this.cmbSupportedDvc.TabIndex = 48;
            // 
            // lblSdkVersion
            // 
            this.lblSdkVersion.AutoSize = true;
            this.lblSdkVersion.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSdkVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(121)))));
            this.lblSdkVersion.Location = new System.Drawing.Point(247, 24);
            this.lblSdkVersion.Name = "lblSdkVersion";
            this.lblSdkVersion.Size = new System.Drawing.Size(32, 15);
            this.lblSdkVersion.TabIndex = 50;
            this.lblSdkVersion.Text = "-NA-";
            // 
            // gbActions
            // 
            this.gbActions.Controls.Add(this.btnCheckDevice);
            this.gbActions.Controls.Add(this.btnMatchFinger);
            this.gbActions.Controls.Add(this.btnInit);
            this.gbActions.Controls.Add(this.btnStartCapture);
            this.gbActions.Controls.Add(this.btnUninit);
            this.gbActions.Controls.Add(this.btnStopCapture);
            this.gbActions.Controls.Add(this.btnAutoCapture);
            this.gbActions.Controls.Add(this.btnSaveImage);
            this.gbActions.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbActions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(121)))));
            this.gbActions.Location = new System.Drawing.Point(10, 271);
            this.gbActions.Name = "gbActions";
            this.gbActions.Size = new System.Drawing.Size(479, 143);
            this.gbActions.TabIndex = 51;
            this.gbActions.TabStop = false;
            this.gbActions.Text = "Actions";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.tlHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(3);
            this.pnlHeader.Size = new System.Drawing.Size(870, 36);
            this.pnlHeader.TabIndex = 53;
            this.pnlHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlHeader_Paint);
            // 
            // tlHeader
            // 
            this.tlHeader.ColumnCount = 3;
            this.tlHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.209243F));
            this.tlHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 92.68293F));
            this.tlHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 3.979461F));
            this.tlHeader.Controls.Add(this.lblHeader, 1, 0);
            this.tlHeader.Controls.Add(this.pbIcon, 0, 0);
            this.tlHeader.Controls.Add(this.pbClose, 2, 0);
            this.tlHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlHeader.Location = new System.Drawing.Point(3, 3);
            this.tlHeader.Name = "tlHeader";
            this.tlHeader.RowCount = 1;
            this.tlHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlHeader.Size = new System.Drawing.Size(862, 28);
            this.tlHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(121)))));
            this.lblHeader.Location = new System.Drawing.Point(30, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(793, 28);
            this.lblHeader.TabIndex = 52;
            this.lblHeader.Text = "MorFin Auth : Mantra Softech India Pvt Ltd © 2023";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbIcon
            // 
            this.pbIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbIcon.Image = global::MorFin_Auth_CSharp.Properties.Resources.HeaderFinger;
            this.pbIcon.Location = new System.Drawing.Point(3, 3);
            this.pbIcon.Name = "pbIcon";
            this.pbIcon.Size = new System.Drawing.Size(21, 22);
            this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbIcon.TabIndex = 53;
            this.pbIcon.TabStop = false;
            // 
            // pbClose
            // 
            this.pbClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbClose.Image = global::MorFin_Auth_CSharp.Properties.Resources.Close;
            this.pbClose.Location = new System.Drawing.Point(829, 3);
            this.pbClose.Name = "pbClose";
            this.pbClose.Size = new System.Drawing.Size(30, 22);
            this.pbClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbClose.TabIndex = 54;
            this.pbClose.TabStop = false;
            this.pbClose.Click += new System.EventHandler(this.pbClose_Click);
            // 
            // pnlBack
            // 
            this.pnlBack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBack.Controls.Add(this.txt_Client_Key);
            this.pnlBack.Controls.Add(this.gbActions);
            this.pnlBack.Controls.Add(this.lbl_Client_Key);
            this.pnlBack.Controls.Add(this.panel4);
            this.pnlBack.Controls.Add(this.picFinger);
            this.pnlBack.Controls.Add(this.pnlResult);
            this.pnlBack.Controls.Add(this.pbIsDeviceConnected);
            this.pnlBack.Controls.Add(this.panel1);
            this.pnlBack.Controls.Add(this.pbLogo);
            this.pnlBack.Location = new System.Drawing.Point(0, 0);
            this.pnlBack.Name = "pnlBack";
            this.pnlBack.Size = new System.Drawing.Size(870, 513);
            this.pnlBack.TabIndex = 54;
            this.pnlBack.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBack_Paint);
            // 
            // txt_Client_Key
            // 
            this.txt_Client_Key.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Client_Key.Location = new System.Drawing.Point(103, 244);
            this.txt_Client_Key.MaxLength = 32222;
            this.txt_Client_Key.Name = "txt_Client_Key";
            this.txt_Client_Key.Size = new System.Drawing.Size(382, 23);
            this.txt_Client_Key.TabIndex = 49;
            this.txt_Client_Key.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_Client_Key
            // 
            this.lbl_Client_Key.AutoSize = true;
            this.lbl_Client_Key.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Client_Key.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(78)))), ((int)(((byte)(121)))));
            this.lbl_Client_Key.Location = new System.Drawing.Point(30, 248);
            this.lbl_Client_Key.Name = "lbl_Client_Key";
            this.lbl_Client_Key.Size = new System.Drawing.Size(70, 15);
            this.lbl_Client_Key.TabIndex = 50;
            this.lbl_Client_Key.Text = "Client Key : ";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.lblInitInfo);
            this.panel4.Location = new System.Drawing.Point(10, 422);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(477, 21);
            this.panel4.TabIndex = 47;
            // 
            // lblInitInfo
            // 
            this.lblInitInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(112)))), ((int)(((byte)(192)))));
            this.lblInitInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInitInfo.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInitInfo.ForeColor = System.Drawing.Color.White;
            this.lblInitInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInitInfo.Name = "lblInitInfo";
            this.lblInitInfo.Size = new System.Drawing.Size(477, 21);
            this.lblInitInfo.TabIndex = 28;
            this.lblInitInfo.Text = "- NA -";
            this.lblInitInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picFinger
            // 
            this.picFinger.BackColor = System.Drawing.Color.White;
            this.picFinger.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picFinger.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picFinger.Location = new System.Drawing.Point(494, 66);
            this.picFinger.Name = "picFinger";
            this.picFinger.Size = new System.Drawing.Size(368, 413);
            this.picFinger.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picFinger.TabIndex = 27;
            this.picFinger.TabStop = false;
            // 
            // pbIsDeviceConnected
            // 
            this.pbIsDeviceConnected.Image = global::MorFin_Auth_CSharp.Properties.Resources.RedFinger;
            this.pbIsDeviceConnected.Location = new System.Drawing.Point(428, 456);
            this.pbIsDeviceConnected.Name = "pbIsDeviceConnected";
            this.pbIsDeviceConnected.Size = new System.Drawing.Size(57, 48);
            this.pbIsDeviceConnected.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbIsDeviceConnected.TabIndex = 48;
            this.pbIsDeviceConnected.TabStop = false;
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::MorFin_Auth_CSharp.Properties.Resources.logo;
            this.pbLogo.Location = new System.Drawing.Point(11, 454);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(189, 50);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbLogo.TabIndex = 47;
            this.pbLogo.TabStop = false;
            // 
            // frmSDK
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(870, 513);
            this.ControlBox = false;
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.gbSettings);
            this.Controls.Add(this.btnVersion);
            this.Controls.Add(this.pnlBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSDK";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MorFin_Auth(1.0.0.1 -Beta) : Mantra Softech India Pvt Ltd © 2020";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.form1_FormClosing);
            this.Load += new System.EventHandler(this.form1_Load);
            this.pnlResult.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.gbSettings.ResumeLayout(false);
            this.gbSettings.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.gbActions.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.tlHeader.ResumeLayout(false);
            this.tlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbClose)).EndInit();
            this.pnlBack.ResumeLayout(false);
            this.pnlBack.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFinger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbIsDeviceConnected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartCapture;
        private System.Windows.Forms.TextBox txtTimeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnVersion;
        private System.Windows.Forms.TextBox txtQuality;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCheckDevice;
        private System.Windows.Forms.Button btnAutoCapture;
        private System.Windows.Forms.Button btnUninit;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.PictureBox picFinger;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnSdkVersion;
        private System.Windows.Forms.Button btnStopCapture;
        private System.Windows.Forms.ComboBox cmbTemplateType;
        private System.Windows.Forms.ComboBox cmbImageType;
        private System.Windows.Forms.Button btnSaveImage;
        private System.Windows.Forms.Panel pnlResult;
        private System.Windows.Forms.Button btnMatchFinger;
        private System.Windows.Forms.Button btn_ConnectDvc;
        private System.Windows.Forms.Button btn_SupportDvc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblQltyAndNfiq;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.PictureBox pbIsDeviceConnected;
        private System.Windows.Forms.GroupBox gbSettings;
        private System.Windows.Forms.GroupBox gbActions;
        private System.Windows.Forms.Label lblSdkVersion;
        private System.Windows.Forms.ComboBox cmbConnectedDvc;
        private System.Windows.Forms.ComboBox cmbSupportedDvc;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.TableLayoutPanel tlHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.PictureBox pbIcon;
        private System.Windows.Forms.Panel pnlBack;
        private System.Windows.Forms.PictureBox pbClose;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblInitInfo;
        private System.Windows.Forms.TextBox txt_Client_Key;
        private System.Windows.Forms.Label lbl_Client_Key;
    }
}

