namespace QRUtils
{
    partial class FormQRInput
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabsQR = new System.Windows.Forms.TabControl();
            this.tabPageContact = new System.Windows.Forms.TabPage();
            this.tabPageCalendar = new System.Windows.Forms.TabPage();
            this.tabPageEmail = new System.Windows.Forms.TabPage();
            this.tabPageGeo = new System.Windows.Forms.TabPage();
            this.tabPagePhone = new System.Windows.Forms.TabPage();
            this.tabPageSMS = new System.Windows.Forms.TabPage();
            this.tabPageWifi = new System.Windows.Forms.TabPage();
            this.tabPageURL = new System.Windows.Forms.TabPage();
            this.btnOK = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.grpWifiSSID = new System.Windows.Forms.GroupBox();
            this.edWifiSSID = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.edWifiPass = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkWifiHidden = new System.Windows.Forms.CheckBox();
            this.cbWifiNetwork = new System.Windows.Forms.ComboBox();
            this.grpURL = new System.Windows.Forms.GroupBox();
            this.edURL = new System.Windows.Forms.TextBox();
            this.grpSmsTo = new System.Windows.Forms.GroupBox();
            this.edSmsTo = new System.Windows.Forms.TextBox();
            this.grpSmsText = new System.Windows.Forms.GroupBox();
            this.edSmsText = new System.Windows.Forms.TextBox();
            this.grpPhone = new System.Windows.Forms.GroupBox();
            this.edPhone = new System.Windows.Forms.TextBox();
            this.grpGeoLat = new System.Windows.Forms.GroupBox();
            this.edGeoLat = new System.Windows.Forms.TextBox();
            this.grpGeoQuery = new System.Windows.Forms.GroupBox();
            this.edGeoQuery = new System.Windows.Forms.TextBox();
            this.grpGeoLon = new System.Windows.Forms.GroupBox();
            this.edGeoLon = new System.Windows.Forms.TextBox();
            this.grpMailTo = new System.Windows.Forms.GroupBox();
            this.edMailTo = new System.Windows.Forms.TextBox();
            this.tabsQR.SuspendLayout();
            this.tabPageEmail.SuspendLayout();
            this.tabPageGeo.SuspendLayout();
            this.tabPagePhone.SuspendLayout();
            this.tabPageSMS.SuspendLayout();
            this.tabPageWifi.SuspendLayout();
            this.tabPageURL.SuspendLayout();
            this.grpWifiSSID.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpURL.SuspendLayout();
            this.grpSmsTo.SuspendLayout();
            this.grpSmsText.SuspendLayout();
            this.grpPhone.SuspendLayout();
            this.grpGeoLat.SuspendLayout();
            this.grpGeoQuery.SuspendLayout();
            this.grpGeoLon.SuspendLayout();
            this.grpMailTo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabsQR
            // 
            this.tabsQR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabsQR.Controls.Add(this.tabPageContact);
            this.tabsQR.Controls.Add(this.tabPageCalendar);
            this.tabsQR.Controls.Add(this.tabPageEmail);
            this.tabsQR.Controls.Add(this.tabPageGeo);
            this.tabsQR.Controls.Add(this.tabPagePhone);
            this.tabsQR.Controls.Add(this.tabPageSMS);
            this.tabsQR.Controls.Add(this.tabPageWifi);
            this.tabsQR.Controls.Add(this.tabPageURL);
            this.tabsQR.Location = new System.Drawing.Point(12, 12);
            this.tabsQR.Name = "tabsQR";
            this.tabsQR.SelectedIndex = 0;
            this.tabsQR.ShowToolTips = true;
            this.tabsQR.Size = new System.Drawing.Size(589, 375);
            this.tabsQR.TabIndex = 0;
            // 
            // tabPageContact
            // 
            this.tabPageContact.Location = new System.Drawing.Point(4, 22);
            this.tabPageContact.Name = "tabPageContact";
            this.tabPageContact.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageContact.Size = new System.Drawing.Size(581, 349);
            this.tabPageContact.TabIndex = 1;
            this.tabPageContact.Text = "Contact";
            this.tabPageContact.UseVisualStyleBackColor = true;
            // 
            // tabPageCalendar
            // 
            this.tabPageCalendar.Location = new System.Drawing.Point(4, 22);
            this.tabPageCalendar.Name = "tabPageCalendar";
            this.tabPageCalendar.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCalendar.Size = new System.Drawing.Size(581, 349);
            this.tabPageCalendar.TabIndex = 2;
            this.tabPageCalendar.Text = "Calendar";
            this.tabPageCalendar.UseVisualStyleBackColor = true;
            // 
            // tabPageEmail
            // 
            this.tabPageEmail.Controls.Add(this.grpMailTo);
            this.tabPageEmail.Location = new System.Drawing.Point(4, 22);
            this.tabPageEmail.Name = "tabPageEmail";
            this.tabPageEmail.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEmail.Size = new System.Drawing.Size(581, 349);
            this.tabPageEmail.TabIndex = 3;
            this.tabPageEmail.Text = "Email";
            this.tabPageEmail.UseVisualStyleBackColor = true;
            // 
            // tabPageGeo
            // 
            this.tabPageGeo.Controls.Add(this.grpGeoLon);
            this.tabPageGeo.Controls.Add(this.grpGeoQuery);
            this.tabPageGeo.Controls.Add(this.grpGeoLat);
            this.tabPageGeo.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeo.Name = "tabPageGeo";
            this.tabPageGeo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeo.Size = new System.Drawing.Size(581, 349);
            this.tabPageGeo.TabIndex = 4;
            this.tabPageGeo.Text = "Geo";
            this.tabPageGeo.UseVisualStyleBackColor = true;
            // 
            // tabPagePhone
            // 
            this.tabPagePhone.Controls.Add(this.grpPhone);
            this.tabPagePhone.Location = new System.Drawing.Point(4, 22);
            this.tabPagePhone.Name = "tabPagePhone";
            this.tabPagePhone.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePhone.Size = new System.Drawing.Size(581, 349);
            this.tabPagePhone.TabIndex = 5;
            this.tabPagePhone.Text = "Phone";
            this.tabPagePhone.UseVisualStyleBackColor = true;
            // 
            // tabPageSMS
            // 
            this.tabPageSMS.Controls.Add(this.grpSmsText);
            this.tabPageSMS.Controls.Add(this.grpSmsTo);
            this.tabPageSMS.Location = new System.Drawing.Point(4, 22);
            this.tabPageSMS.Name = "tabPageSMS";
            this.tabPageSMS.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSMS.Size = new System.Drawing.Size(581, 349);
            this.tabPageSMS.TabIndex = 7;
            this.tabPageSMS.Text = "SMS";
            this.tabPageSMS.UseVisualStyleBackColor = true;
            // 
            // tabPageWifi
            // 
            this.tabPageWifi.Controls.Add(this.groupBox2);
            this.tabPageWifi.Controls.Add(this.groupBox1);
            this.tabPageWifi.Controls.Add(this.grpWifiSSID);
            this.tabPageWifi.Location = new System.Drawing.Point(4, 22);
            this.tabPageWifi.Name = "tabPageWifi";
            this.tabPageWifi.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWifi.Size = new System.Drawing.Size(581, 349);
            this.tabPageWifi.TabIndex = 6;
            this.tabPageWifi.Text = "WIFI";
            this.tabPageWifi.ToolTipText = "WIFI Network Connection";
            this.tabPageWifi.UseVisualStyleBackColor = true;
            // 
            // tabPageURL
            // 
            this.tabPageURL.Controls.Add(this.grpURL);
            this.tabPageURL.Location = new System.Drawing.Point(4, 22);
            this.tabPageURL.Name = "tabPageURL";
            this.tabPageURL.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageURL.Size = new System.Drawing.Size(581, 349);
            this.tabPageURL.TabIndex = 0;
            this.tabPageURL.Text = "URL";
            this.tabPageURL.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnOK.Location = new System.Drawing.Point(522, 405);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button2.Location = new System.Drawing.Point(425, 405);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // grpWifiSSID
            // 
            this.grpWifiSSID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpWifiSSID.Controls.Add(this.edWifiSSID);
            this.grpWifiSSID.Location = new System.Drawing.Point(16, 16);
            this.grpWifiSSID.Name = "grpWifiSSID";
            this.grpWifiSSID.Size = new System.Drawing.Size(550, 55);
            this.grpWifiSSID.TabIndex = 8;
            this.grpWifiSSID.TabStop = false;
            this.grpWifiSSID.Text = "SSID";
            // 
            // edWifiSSID
            // 
            this.edWifiSSID.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edWifiSSID.Location = new System.Drawing.Point(13, 20);
            this.edWifiSSID.MaxLength = 63;
            this.edWifiSSID.Name = "edWifiSSID";
            this.edWifiSSID.Size = new System.Drawing.Size(525, 21);
            this.edWifiSSID.TabIndex = 2;
            this.edWifiSSID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.edWifiPass);
            this.groupBox1.Location = new System.Drawing.Point(16, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(550, 55);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Password";
            // 
            // edWifiPass
            // 
            this.edWifiPass.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edWifiPass.Location = new System.Drawing.Point(13, 20);
            this.edWifiPass.MaxLength = 63;
            this.edWifiPass.Name = "edWifiPass";
            this.edWifiPass.Size = new System.Drawing.Size(525, 21);
            this.edWifiPass.TabIndex = 4;
            this.edWifiPass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkWifiHidden);
            this.groupBox2.Controls.Add(this.cbWifiNetwork);
            this.groupBox2.Location = new System.Drawing.Point(16, 158);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(550, 55);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Network";
            // 
            // chkWifiHidden
            // 
            this.chkWifiHidden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.chkWifiHidden.AutoSize = true;
            this.chkWifiHidden.Location = new System.Drawing.Point(311, 22);
            this.chkWifiHidden.Name = "chkWifiHidden";
            this.chkWifiHidden.Size = new System.Drawing.Size(60, 16);
            this.chkWifiHidden.TabIndex = 11;
            this.chkWifiHidden.Text = "Hidden";
            this.chkWifiHidden.UseVisualStyleBackColor = true;
            // 
            // cbWifiNetwork
            // 
            this.cbWifiNetwork.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.cbWifiNetwork.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWifiNetwork.FormattingEnabled = true;
            this.cbWifiNetwork.Items.AddRange(new object[] {
            "WPA/WPA2",
            "WEP",
            "No Encryption"});
            this.cbWifiNetwork.Location = new System.Drawing.Point(180, 20);
            this.cbWifiNetwork.Name = "cbWifiNetwork";
            this.cbWifiNetwork.Size = new System.Drawing.Size(121, 20);
            this.cbWifiNetwork.TabIndex = 10;
            // 
            // grpURL
            // 
            this.grpURL.Controls.Add(this.edURL);
            this.grpURL.Location = new System.Drawing.Point(16, 16);
            this.grpURL.Name = "grpURL";
            this.grpURL.Size = new System.Drawing.Size(550, 55);
            this.grpURL.TabIndex = 9;
            this.grpURL.TabStop = false;
            this.grpURL.Text = "WEB Site URL";
            // 
            // edURL
            // 
            this.edURL.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edURL.Location = new System.Drawing.Point(12, 20);
            this.edURL.MaxLength = 512;
            this.edURL.Name = "edURL";
            this.edURL.Size = new System.Drawing.Size(525, 21);
            this.edURL.TabIndex = 2;
            // 
            // grpSmsTo
            // 
            this.grpSmsTo.Controls.Add(this.edSmsTo);
            this.grpSmsTo.Location = new System.Drawing.Point(16, 16);
            this.grpSmsTo.Name = "grpSmsTo";
            this.grpSmsTo.Size = new System.Drawing.Size(550, 55);
            this.grpSmsTo.TabIndex = 10;
            this.grpSmsTo.TabStop = false;
            this.grpSmsTo.Text = "SMS To";
            // 
            // edSmsTo
            // 
            this.edSmsTo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edSmsTo.Location = new System.Drawing.Point(12, 20);
            this.edSmsTo.MaxLength = 32;
            this.edSmsTo.Name = "edSmsTo";
            this.edSmsTo.Size = new System.Drawing.Size(525, 21);
            this.edSmsTo.TabIndex = 2;
            // 
            // grpSmsText
            // 
            this.grpSmsText.Controls.Add(this.edSmsText);
            this.grpSmsText.Location = new System.Drawing.Point(16, 87);
            this.grpSmsText.Name = "grpSmsText";
            this.grpSmsText.Size = new System.Drawing.Size(550, 248);
            this.grpSmsText.TabIndex = 11;
            this.grpSmsText.TabStop = false;
            this.grpSmsText.Text = "SMS Text";
            // 
            // edSmsText
            // 
            this.edSmsText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edSmsText.Location = new System.Drawing.Point(12, 20);
            this.edSmsText.MaxLength = 70;
            this.edSmsText.Multiline = true;
            this.edSmsText.Name = "edSmsText";
            this.edSmsText.Size = new System.Drawing.Size(525, 214);
            this.edSmsText.TabIndex = 2;
            // 
            // grpPhone
            // 
            this.grpPhone.Controls.Add(this.edPhone);
            this.grpPhone.Location = new System.Drawing.Point(16, 16);
            this.grpPhone.Name = "grpPhone";
            this.grpPhone.Size = new System.Drawing.Size(550, 55);
            this.grpPhone.TabIndex = 11;
            this.grpPhone.TabStop = false;
            this.grpPhone.Text = "Phone Number";
            // 
            // edPhone
            // 
            this.edPhone.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edPhone.Location = new System.Drawing.Point(12, 20);
            this.edPhone.MaxLength = 32;
            this.edPhone.Name = "edPhone";
            this.edPhone.Size = new System.Drawing.Size(525, 21);
            this.edPhone.TabIndex = 2;
            // 
            // grpGeoLat
            // 
            this.grpGeoLat.Controls.Add(this.edGeoLat);
            this.grpGeoLat.Location = new System.Drawing.Point(16, 16);
            this.grpGeoLat.Name = "grpGeoLat";
            this.grpGeoLat.Size = new System.Drawing.Size(550, 55);
            this.grpGeoLat.TabIndex = 12;
            this.grpGeoLat.TabStop = false;
            this.grpGeoLat.Text = "Latitude";
            // 
            // edGeoLat
            // 
            this.edGeoLat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edGeoLat.Location = new System.Drawing.Point(12, 20);
            this.edGeoLat.MaxLength = 32;
            this.edGeoLat.Name = "edGeoLat";
            this.edGeoLat.Size = new System.Drawing.Size(525, 21);
            this.edGeoLat.TabIndex = 2;
            this.edGeoLat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // grpGeoQuery
            // 
            this.grpGeoQuery.Controls.Add(this.edGeoQuery);
            this.grpGeoQuery.Location = new System.Drawing.Point(16, 158);
            this.grpGeoQuery.Name = "grpGeoQuery";
            this.grpGeoQuery.Size = new System.Drawing.Size(550, 55);
            this.grpGeoQuery.TabIndex = 13;
            this.grpGeoQuery.TabStop = false;
            this.grpGeoQuery.Text = "Query";
            // 
            // edGeoQuery
            // 
            this.edGeoQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edGeoQuery.Location = new System.Drawing.Point(12, 20);
            this.edGeoQuery.MaxLength = 32;
            this.edGeoQuery.Name = "edGeoQuery";
            this.edGeoQuery.Size = new System.Drawing.Size(525, 21);
            this.edGeoQuery.TabIndex = 2;
            this.edGeoQuery.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // grpGeoLon
            // 
            this.grpGeoLon.Controls.Add(this.edGeoLon);
            this.grpGeoLon.Location = new System.Drawing.Point(16, 87);
            this.grpGeoLon.Name = "grpGeoLon";
            this.grpGeoLon.Size = new System.Drawing.Size(550, 55);
            this.grpGeoLon.TabIndex = 14;
            this.grpGeoLon.TabStop = false;
            this.grpGeoLon.Text = "Longitude";
            // 
            // edGeoLon
            // 
            this.edGeoLon.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edGeoLon.Location = new System.Drawing.Point(12, 20);
            this.edGeoLon.MaxLength = 32;
            this.edGeoLon.Name = "edGeoLon";
            this.edGeoLon.Size = new System.Drawing.Size(525, 21);
            this.edGeoLon.TabIndex = 2;
            this.edGeoLon.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // grpMailTo
            // 
            this.grpMailTo.Controls.Add(this.edMailTo);
            this.grpMailTo.Location = new System.Drawing.Point(16, 16);
            this.grpMailTo.Name = "grpMailTo";
            this.grpMailTo.Size = new System.Drawing.Size(550, 55);
            this.grpMailTo.TabIndex = 12;
            this.grpMailTo.TabStop = false;
            this.grpMailTo.Text = "Mail to";
            // 
            // edMailTo
            // 
            this.edMailTo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edMailTo.Location = new System.Drawing.Point(12, 20);
            this.edMailTo.MaxLength = 32;
            this.edMailTo.Name = "edMailTo";
            this.edMailTo.Size = new System.Drawing.Size(525, 21);
            this.edMailTo.TabIndex = 2;
            // 
            // FormQRInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(613, 440);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.tabsQR);
            this.Name = "FormQRInput";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "QR Information Input";
            this.tabsQR.ResumeLayout(false);
            this.tabPageEmail.ResumeLayout(false);
            this.tabPageGeo.ResumeLayout(false);
            this.tabPagePhone.ResumeLayout(false);
            this.tabPageSMS.ResumeLayout(false);
            this.tabPageWifi.ResumeLayout(false);
            this.tabPageURL.ResumeLayout(false);
            this.grpWifiSSID.ResumeLayout(false);
            this.grpWifiSSID.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpURL.ResumeLayout(false);
            this.grpURL.PerformLayout();
            this.grpSmsTo.ResumeLayout(false);
            this.grpSmsTo.PerformLayout();
            this.grpSmsText.ResumeLayout(false);
            this.grpSmsText.PerformLayout();
            this.grpPhone.ResumeLayout(false);
            this.grpPhone.PerformLayout();
            this.grpGeoLat.ResumeLayout(false);
            this.grpGeoLat.PerformLayout();
            this.grpGeoQuery.ResumeLayout(false);
            this.grpGeoQuery.PerformLayout();
            this.grpGeoLon.ResumeLayout(false);
            this.grpGeoLon.PerformLayout();
            this.grpMailTo.ResumeLayout(false);
            this.grpMailTo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabsQR;
        private System.Windows.Forms.TabPage tabPageURL;
        private System.Windows.Forms.TabPage tabPageContact;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabPage tabPageCalendar;
        private System.Windows.Forms.TabPage tabPageEmail;
        private System.Windows.Forms.TabPage tabPageGeo;
        private System.Windows.Forms.TabPage tabPagePhone;
        private System.Windows.Forms.TabPage tabPageWifi;
        private System.Windows.Forms.TabPage tabPageSMS;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkWifiHidden;
        private System.Windows.Forms.ComboBox cbWifiNetwork;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox edWifiPass;
        private System.Windows.Forms.GroupBox grpWifiSSID;
        private System.Windows.Forms.TextBox edWifiSSID;
        private System.Windows.Forms.GroupBox grpURL;
        private System.Windows.Forms.TextBox edURL;
        private System.Windows.Forms.GroupBox grpSmsText;
        private System.Windows.Forms.TextBox edSmsText;
        private System.Windows.Forms.GroupBox grpSmsTo;
        private System.Windows.Forms.TextBox edSmsTo;
        private System.Windows.Forms.GroupBox grpPhone;
        private System.Windows.Forms.TextBox edPhone;
        private System.Windows.Forms.GroupBox grpGeoQuery;
        private System.Windows.Forms.TextBox edGeoQuery;
        private System.Windows.Forms.GroupBox grpGeoLat;
        private System.Windows.Forms.TextBox edGeoLat;
        private System.Windows.Forms.GroupBox grpGeoLon;
        private System.Windows.Forms.TextBox edGeoLon;
        private System.Windows.Forms.GroupBox grpMailTo;
        private System.Windows.Forms.TextBox edMailTo;
    }
}