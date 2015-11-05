﻿namespace QRUtils
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
            this.pnlQR.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlQR
            // 
            this.pnlQR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            // 
            // edText
            // 
            this.edText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edText.EnableAutoDragDrop = true;
            this.edText.Font = new System.Drawing.Font("DejaVu Sans Mono", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.edText.HideSelection = false;
            this.edText.ImeMode = System.Windows.Forms.ImeMode.On;
            this.edText.Location = new System.Drawing.Point(8, 8);
            this.edText.MaxLength = 2000;
            this.edText.Name = "edText";
            this.edText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.edText.ShowSelectionMargin = true;
            this.edText.Size = new System.Drawing.Size(370, 260);
            this.edText.TabIndex = 1;
            this.edText.Text = "";
            this.edText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.edText_KeyDown);
            // 
            // btnQREncode
            // 
            this.btnQREncode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQREncode.Location = new System.Drawing.Point(393, 27);
            this.btnQREncode.Name = "btnQREncode";
            this.btnQREncode.Size = new System.Drawing.Size(49, 44);
            this.btnQREncode.TabIndex = 2;
            this.btnQREncode.Text = "==> QR";
            this.btnQREncode.UseVisualStyleBackColor = true;
            this.btnQREncode.Click += new System.EventHandler(this.btnQREncode_Click);
            // 
            // btnQRDecode
            // 
            this.btnQRDecode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnQRDecode.Location = new System.Drawing.Point(393, 211);
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
            this.btnClipFrom.Location = new System.Drawing.Point(394, 89);
            this.btnClipFrom.Name = "btnClipFrom";
            this.btnClipFrom.Size = new System.Drawing.Size(47, 44);
            this.btnClipFrom.TabIndex = 4;
            this.btnClipFrom.Text = "<== Clip";
            this.btnClipFrom.UseVisualStyleBackColor = true;
            this.btnClipFrom.Click += new System.EventHandler(this.btnClipFrom_Click);
            // 
            // btnClipTo
            // 
            this.btnClipTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClipTo.Location = new System.Drawing.Point(393, 149);
            this.btnClipTo.Name = "btnClipTo";
            this.btnClipTo.Size = new System.Drawing.Size(48, 44);
            this.btnClipTo.TabIndex = 5;
            this.btnClipTo.Text = "==> Clip";
            this.btnClipTo.UseVisualStyleBackColor = true;
            this.btnClipTo.Click += new System.EventHandler(this.btnClipTo_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 277);
            this.Controls.Add(this.btnClipTo);
            this.Controls.Add(this.btnClipFrom);
            this.Controls.Add(this.btnQRDecode);
            this.Controls.Add(this.btnQREncode);
            this.Controls.Add(this.edText);
            this.Controls.Add(this.pnlQR);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QRCode Utils";
            this.TopMost = true;
            this.pnlQR.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picQR)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlQR;
        private System.Windows.Forms.Button btnQREncode;
        private System.Windows.Forms.Button btnQRDecode;
        private System.Windows.Forms.PictureBox picQR;
        private System.Windows.Forms.Button btnClipFrom;
        private System.Windows.Forms.Button btnClipTo;
        private System.Windows.Forms.RichTextBox edText;
    }
}

