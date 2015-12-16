namespace QRUtils
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                hook.Dispose();
                monoFont.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnlQR = new System.Windows.Forms.Panel();
            this.picQR = new System.Windows.Forms.PictureBox();
            this.edText = new System.Windows.Forms.RichTextBox();
            this.btnQREncode = new System.Windows.Forms.Button();
            this.btnQRDecode = new System.Windows.Forms.Button();
            this.btnClipFrom = new System.Windows.Forms.Button();
            this.btnClipTo = new System.Windows.Forms.Button();
            this.pnlOption = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDecodeUTF8 = new System.Windows.Forms.CheckBox();
            this.cbBarFormat = new System.Windows.Forms.ComboBox();
            this.grpDecodeFormat = new System.Windows.Forms.GroupBox();
            this.chkDecodeFormat1D = new System.Windows.Forms.CheckBox();
            this.chkDecodeFormatDM = new System.Windows.Forms.CheckBox();
            this.chkDecodeFormatQR = new System.Windows.Forms.CheckBox();
            this.chkOverLogo = new System.Windows.Forms.CheckBox();
            this.lblErrorLevel = new System.Windows.Forms.Label();
            this.cbErrorLevel = new System.Windows.Forms.ComboBox();
            this.lblMaskColor = new System.Windows.Forms.Label();
            this.picMaskColor = new System.Windows.Forms.PictureBox();
            this.chkMultiDecode = new System.Windows.Forms.CheckBox();
            this.colorDlg = new System.Windows.Forms.ColorDialog();
            this.status = new System.Windows.Forms.StatusStrip();
            this.statusLabelHotkey = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelTextCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelDecodeCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnQRInput = new System.Windows.Forms.ToolStripSplitButton();
            this.logoItems = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBarCode = new System.Windows.Forms.Button();
            this.pnlQR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).BeginInit();
            this.pnlOption.SuspendLayout();
            this.grpDecodeFormat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMaskColor)).BeginInit();
            this.status.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlQR
            // 
            this.pnlQR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlQR.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlQR.BackColor = System.Drawing.SystemColors.Desktop;
            this.pnlQR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlQR.Controls.Add(this.picQR);
            this.pnlQR.Location = new System.Drawing.Point(455, 8);
            this.pnlQR.Margin = new System.Windows.Forms.Padding(0);
            this.pnlQR.Name = "pnlQR";
            this.pnlQR.Padding = new System.Windows.Forms.Padding(2);
            this.pnlQR.Size = new System.Drawing.Size(260, 260);
            this.pnlQR.TabIndex = 0;
            // 
            // picQR
            // 
            this.picQR.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picQR.BackColor = System.Drawing.SystemColors.Window;
            this.picQR.Location = new System.Drawing.Point(1, 1);
            this.picQR.Margin = new System.Windows.Forms.Padding(0);
            this.picQR.Name = "picQR";
            this.picQR.Padding = new System.Windows.Forms.Padding(2);
            this.picQR.Size = new System.Drawing.Size(256, 256);
            this.picQR.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picQR.TabIndex = 0;
            this.picQR.TabStop = false;
            this.picQR.DoubleClick += new System.EventHandler(this.picQR_DoubleClick);
            // 
            // edText
            // 
            this.edText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edText.EnableAutoDragDrop = true;
            this.edText.Font = new System.Drawing.Font("DejaVu Sans Mono", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edText.HideSelection = false;
            this.edText.ImeMode = System.Windows.Forms.ImeMode.On;
            this.edText.Location = new System.Drawing.Point(8, 8);
            this.edText.MaxLength = 2000;
            this.edText.Name = "edText";
            this.edText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.edText.ShowSelectionMargin = true;
            this.edText.Size = new System.Drawing.Size(441, 464);
            this.edText.TabIndex = 1;
            this.edText.Text = "";
            this.edText.TextChanged += new System.EventHandler(this.edText_TextChanged);
            this.edText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.edText_KeyDown);
            // 
            // btnQREncode
            // 
            this.btnQREncode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQREncode.Location = new System.Drawing.Point(455, 281);
            this.btnQREncode.Name = "btnQREncode";
            this.btnQREncode.Size = new System.Drawing.Size(49, 44);
            this.btnQREncode.TabIndex = 2;
            this.btnQREncode.Text = "==> QR";
            this.btnQREncode.UseVisualStyleBackColor = true;
            this.btnQREncode.Click += new System.EventHandler(this.btnQREncode_Click);
            // 
            // btnQRDecode
            // 
            this.btnQRDecode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQRDecode.Location = new System.Drawing.Point(666, 281);
            this.btnQRDecode.Name = "btnQRDecode";
            this.btnQRDecode.Size = new System.Drawing.Size(49, 44);
            this.btnQRDecode.TabIndex = 3;
            this.btnQRDecode.Text = "<== QR";
            this.btnQRDecode.UseVisualStyleBackColor = true;
            this.btnQRDecode.Click += new System.EventHandler(this.btnQRDecode_Click);
            // 
            // btnClipFrom
            // 
            this.btnClipFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClipFrom.Location = new System.Drawing.Point(509, 281);
            this.btnClipFrom.Name = "btnClipFrom";
            this.btnClipFrom.Size = new System.Drawing.Size(47, 44);
            this.btnClipFrom.TabIndex = 4;
            this.btnClipFrom.Text = "<== Clip";
            this.btnClipFrom.UseVisualStyleBackColor = true;
            this.btnClipFrom.Click += new System.EventHandler(this.btnClipFrom_Click);
            // 
            // btnClipTo
            // 
            this.btnClipTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClipTo.Location = new System.Drawing.Point(613, 281);
            this.btnClipTo.Name = "btnClipTo";
            this.btnClipTo.Size = new System.Drawing.Size(48, 44);
            this.btnClipTo.TabIndex = 5;
            this.btnClipTo.Text = "==> Clip";
            this.btnClipTo.UseVisualStyleBackColor = true;
            this.btnClipTo.Click += new System.EventHandler(this.btnClipTo_Click);
            // 
            // pnlOption
            // 
            this.pnlOption.Controls.Add(this.label1);
            this.pnlOption.Controls.Add(this.cbDecodeUTF8);
            this.pnlOption.Controls.Add(this.cbBarFormat);
            this.pnlOption.Controls.Add(this.grpDecodeFormat);
            this.pnlOption.Controls.Add(this.chkOverLogo);
            this.pnlOption.Controls.Add(this.lblErrorLevel);
            this.pnlOption.Controls.Add(this.cbErrorLevel);
            this.pnlOption.Controls.Add(this.lblMaskColor);
            this.pnlOption.Controls.Add(this.picMaskColor);
            this.pnlOption.Controls.Add(this.chkMultiDecode);
            this.pnlOption.Location = new System.Drawing.Point(456, 331);
            this.pnlOption.Name = "pnlOption";
            this.pnlOption.Size = new System.Drawing.Size(258, 141);
            this.pnlOption.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Location = new System.Drawing.Point(4, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 16);
            this.label1.TabIndex = 18;
            this.label1.Text = "BarCode:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbDecodeUTF8
            // 
            this.cbDecodeUTF8.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cbDecodeUTF8.Location = new System.Drawing.Point(149, 28);
            this.cbDecodeUTF8.Name = "cbDecodeUTF8";
            this.cbDecodeUTF8.Size = new System.Drawing.Size(106, 20);
            this.cbDecodeUTF8.TabIndex = 17;
            this.cbDecodeUTF8.Text = "Force UTF-8";
            this.cbDecodeUTF8.UseVisualStyleBackColor = true;
            // 
            // cbBarFormat
            // 
            this.cbBarFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBarFormat.FormattingEnabled = true;
            this.cbBarFormat.Items.AddRange(new object[] {
            "Express",
            "ISBN",
            "Product",
            "URL",
            "Phone",
            "Mail",
            "SMS",
            "vCard",
            "vCalendar"});
            this.cbBarFormat.Location = new System.Drawing.Point(71, 47);
            this.cbBarFormat.Name = "cbBarFormat";
            this.cbBarFormat.Size = new System.Drawing.Size(61, 20);
            this.cbBarFormat.TabIndex = 16;
            // 
            // grpDecodeFormat
            // 
            this.grpDecodeFormat.Controls.Add(this.chkDecodeFormat1D);
            this.grpDecodeFormat.Controls.Add(this.chkDecodeFormatDM);
            this.grpDecodeFormat.Controls.Add(this.chkDecodeFormatQR);
            this.grpDecodeFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpDecodeFormat.Location = new System.Drawing.Point(144, 52);
            this.grpDecodeFormat.Name = "grpDecodeFormat";
            this.grpDecodeFormat.Size = new System.Drawing.Size(112, 87);
            this.grpDecodeFormat.TabIndex = 15;
            this.grpDecodeFormat.TabStop = false;
            this.grpDecodeFormat.Text = "Decode Format";
            // 
            // chkDecodeFormat1D
            // 
            this.chkDecodeFormat1D.AutoEllipsis = true;
            this.chkDecodeFormat1D.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDecodeFormat1D.Location = new System.Drawing.Point(6, 58);
            this.chkDecodeFormat1D.Name = "chkDecodeFormat1D";
            this.chkDecodeFormat1D.Size = new System.Drawing.Size(99, 17);
            this.chkDecodeFormat1D.TabIndex = 17;
            this.chkDecodeFormat1D.Text = "All 1D Code";
            this.chkDecodeFormat1D.UseVisualStyleBackColor = true;
            this.chkDecodeFormat1D.CheckedChanged += new System.EventHandler(this.chkDecodeFormat_CheckedChanged);
            // 
            // chkDecodeFormatDM
            // 
            this.chkDecodeFormatDM.AutoEllipsis = true;
            this.chkDecodeFormatDM.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDecodeFormatDM.Location = new System.Drawing.Point(6, 37);
            this.chkDecodeFormatDM.Name = "chkDecodeFormatDM";
            this.chkDecodeFormatDM.Size = new System.Drawing.Size(99, 17);
            this.chkDecodeFormatDM.TabIndex = 16;
            this.chkDecodeFormatDM.Text = "Data Matrix";
            this.chkDecodeFormatDM.UseVisualStyleBackColor = true;
            this.chkDecodeFormatDM.CheckedChanged += new System.EventHandler(this.chkDecodeFormat_CheckedChanged);
            // 
            // chkDecodeFormatQR
            // 
            this.chkDecodeFormatQR.AutoEllipsis = true;
            this.chkDecodeFormatQR.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkDecodeFormatQR.Checked = true;
            this.chkDecodeFormatQR.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDecodeFormatQR.Location = new System.Drawing.Point(6, 16);
            this.chkDecodeFormatQR.Name = "chkDecodeFormatQR";
            this.chkDecodeFormatQR.Size = new System.Drawing.Size(99, 17);
            this.chkDecodeFormatQR.TabIndex = 15;
            this.chkDecodeFormatQR.Text = "QR Code";
            this.chkDecodeFormatQR.UseVisualStyleBackColor = true;
            this.chkDecodeFormatQR.CheckedChanged += new System.EventHandler(this.chkDecodeFormat_CheckedChanged);
            // 
            // chkOverLogo
            // 
            this.chkOverLogo.AutoEllipsis = true;
            this.chkOverLogo.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkOverLogo.Location = new System.Drawing.Point(2, 104);
            this.chkOverLogo.Name = "chkOverLogo";
            this.chkOverLogo.Size = new System.Drawing.Size(130, 24);
            this.chkOverLogo.TabIndex = 13;
            this.chkOverLogo.Text = "Overlay Logo";
            this.chkOverLogo.UseVisualStyleBackColor = true;
            this.chkOverLogo.CheckedChanged += new System.EventHandler(this.chkOverLogo_CheckedChanged);
            // 
            // lblErrorLevel
            // 
            this.lblErrorLevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblErrorLevel.Location = new System.Drawing.Point(4, 78);
            this.lblErrorLevel.Name = "lblErrorLevel";
            this.lblErrorLevel.Size = new System.Drawing.Size(88, 16);
            this.lblErrorLevel.TabIndex = 11;
            this.lblErrorLevel.Text = "ErrorLevel:";
            this.lblErrorLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbErrorLevel
            // 
            this.cbErrorLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbErrorLevel.FormattingEnabled = true;
            this.cbErrorLevel.Items.AddRange(new object[] {
            "L",
            "M",
            "Q",
            "H"});
            this.cbErrorLevel.Location = new System.Drawing.Point(96, 76);
            this.cbErrorLevel.Name = "cbErrorLevel";
            this.cbErrorLevel.Size = new System.Drawing.Size(36, 20);
            this.cbErrorLevel.TabIndex = 10;
            this.cbErrorLevel.SelectedIndexChanged += new System.EventHandler(this.cbErrorLevel_SelectedIndexChanged);
            // 
            // lblMaskColor
            // 
            this.lblMaskColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblMaskColor.Location = new System.Drawing.Point(4, 8);
            this.lblMaskColor.Name = "lblMaskColor";
            this.lblMaskColor.Size = new System.Drawing.Size(88, 36);
            this.lblMaskColor.TabIndex = 9;
            this.lblMaskColor.Text = "Mask Color:";
            // 
            // picMaskColor
            // 
            this.picMaskColor.Location = new System.Drawing.Point(95, 7);
            this.picMaskColor.Name = "picMaskColor";
            this.picMaskColor.Size = new System.Drawing.Size(36, 36);
            this.picMaskColor.TabIndex = 8;
            this.picMaskColor.TabStop = false;
            this.picMaskColor.Click += new System.EventHandler(this.picMaskColor_Click);
            // 
            // chkMultiDecode
            // 
            this.chkMultiDecode.AutoEllipsis = true;
            this.chkMultiDecode.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkMultiDecode.Checked = true;
            this.chkMultiDecode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMultiDecode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkMultiDecode.Location = new System.Drawing.Point(135, 6);
            this.chkMultiDecode.Name = "chkMultiDecode";
            this.chkMultiDecode.Size = new System.Drawing.Size(120, 20);
            this.chkMultiDecode.TabIndex = 7;
            this.chkMultiDecode.Text = "Multiple Decode ";
            this.chkMultiDecode.UseVisualStyleBackColor = true;
            this.chkMultiDecode.CheckedChanged += new System.EventHandler(this.chkMultiDecode_CheckedChanged);
            // 
            // colorDlg
            // 
            this.colorDlg.FullOpen = true;
            this.colorDlg.ShowHelp = true;
            // 
            // status
            // 
            this.status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelHotkey,
            this.statusLabelTextCount,
            this.statusLabelDecodeCount,
            this.statusLabelInfo,
            this.btnQRInput});
            this.status.Location = new System.Drawing.Point(0, 479);
            this.status.Name = "status";
            this.status.ShowItemToolTips = true;
            this.status.Size = new System.Drawing.Size(724, 22);
            this.status.SizingGrip = false;
            this.status.TabIndex = 8;
            // 
            // statusLabelHotkey
            // 
            this.statusLabelHotkey.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusLabelHotkey.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLabelHotkey.DoubleClickEnabled = true;
            this.statusLabelHotkey.Name = "statusLabelHotkey";
            this.statusLabelHotkey.Size = new System.Drawing.Size(99, 17);
            this.statusLabelHotkey.Text = "Hotkey: WIN + Q";
            // 
            // statusLabelTextCount
            // 
            this.statusLabelTextCount.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusLabelTextCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLabelTextCount.DoubleClickEnabled = true;
            this.statusLabelTextCount.Name = "statusLabelTextCount";
            this.statusLabelTextCount.Size = new System.Drawing.Size(87, 17);
            this.statusLabelTextCount.Text = "Text Count: 0";
            // 
            // statusLabelDecodeCount
            // 
            this.statusLabelDecodeCount.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusLabelDecodeCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLabelDecodeCount.DoubleClickEnabled = true;
            this.statusLabelDecodeCount.Name = "statusLabelDecodeCount";
            this.statusLabelDecodeCount.Size = new System.Drawing.Size(87, 17);
            this.statusLabelDecodeCount.Text = "Code Found: 0";
            // 
            // statusLabelInfo
            // 
            this.statusLabelInfo.Name = "statusLabelInfo";
            this.statusLabelInfo.Size = new System.Drawing.Size(404, 17);
            this.statusLabelInfo.Spring = true;
            this.statusLabelInfo.Text = "Ready";
            this.statusLabelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnQRInput
            // 
            this.btnQRInput.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnQRInput.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.logoItems});
            this.btnQRInput.Image = ((System.Drawing.Image)(resources.GetObject("btnQRInput.Image")));
            this.btnQRInput.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnQRInput.Name = "btnQRInput";
            this.btnQRInput.Size = new System.Drawing.Size(32, 20);
            this.btnQRInput.Text = "QR Input Form";
            this.btnQRInput.ToolTipText = "Special QR Information Input";
            this.btnQRInput.ButtonClick += new System.EventHandler(this.btnQRInput_ButtonClick);
            // 
            // logoItems
            // 
            this.logoItems.Name = "logoItems";
            this.logoItems.Size = new System.Drawing.Size(94, 22);
            this.logoItems.Text = "Logo";
            this.logoItems.DropDownOpening += new System.EventHandler(this.logoItems_DropDownOpening);
            this.logoItems.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.logoItems_DropDownItemClicked);
            // 
            // btnBarCode
            // 
            this.btnBarCode.Location = new System.Drawing.Point(565, 281);
            this.btnBarCode.Name = "btnBarCode";
            this.btnBarCode.Size = new System.Drawing.Size(40, 44);
            this.btnBarCode.TabIndex = 9;
            this.btnBarCode.Text = "Bar Code";
            this.btnBarCode.UseVisualStyleBackColor = true;
            this.btnBarCode.Click += new System.EventHandler(this.btnBarCode_Click);
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 501);
            this.Controls.Add(this.btnBarCode);
            this.Controls.Add(this.status);
            this.Controls.Add(this.pnlOption);
            this.Controls.Add(this.btnClipTo);
            this.Controls.Add(this.btnClipFrom);
            this.Controls.Add(this.btnQRDecode);
            this.Controls.Add(this.btnQREncode);
            this.Controls.Add(this.edText);
            this.Controls.Add(this.pnlQR);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QRCode Utils";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.MainForm_DragOver);
            this.pnlQR.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).EndInit();
            this.pnlOption.ResumeLayout(false);
            this.grpDecodeFormat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMaskColor)).EndInit();
            this.status.ResumeLayout(false);
            this.status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlQR;
        private System.Windows.Forms.Button btnQREncode;
        private System.Windows.Forms.Button btnQRDecode;
        private System.Windows.Forms.PictureBox picQR;
        private System.Windows.Forms.Button btnClipFrom;
        private System.Windows.Forms.Button btnClipTo;
        private System.Windows.Forms.RichTextBox edText;
        private System.Windows.Forms.Panel pnlOption;
        private System.Windows.Forms.CheckBox chkMultiDecode;
        private System.Windows.Forms.Label lblMaskColor;
        private System.Windows.Forms.PictureBox picMaskColor;
        private System.Windows.Forms.ColorDialog colorDlg;
        private System.Windows.Forms.Label lblErrorLevel;
        private System.Windows.Forms.ComboBox cbErrorLevel;
        private System.Windows.Forms.CheckBox chkOverLogo;
        private System.Windows.Forms.StatusStrip status;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelHotkey;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelTextCount;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelDecodeCount;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelInfo;
        private System.Windows.Forms.GroupBox grpDecodeFormat;
        private System.Windows.Forms.CheckBox chkDecodeFormat1D;
        private System.Windows.Forms.CheckBox chkDecodeFormatDM;
        private System.Windows.Forms.CheckBox chkDecodeFormatQR;
        private System.Windows.Forms.ComboBox cbBarFormat;
        private System.Windows.Forms.Button btnBarCode;
        private System.Windows.Forms.ToolStripSplitButton btnQRInput;
        private System.Windows.Forms.ToolStripMenuItem logoItems;
        private System.Windows.Forms.CheckBox cbDecodeUTF8;
        private System.Windows.Forms.Label label1;
    }
}

