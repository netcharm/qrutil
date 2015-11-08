using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ZXing;
using ZXing.QrCode.Internal;
using ZXing.Rendering;

namespace QRUtils
{
    public partial class MainForm : Form
    {
        private Font monoFont = new System.Drawing.Font("DejaVu Sans Mono", 10);
        private ErrorCorrectionLevel errorLevel = ErrorCorrectionLevel.M;

        private KeyboardHook hook = new KeyboardHook();

        public MainForm()
        {
            InitializeComponent();
            Application.EnableVisualStyles();

            colorDlg.Color = Color.Cyan;
            picMaskColor.BackColor = Color.Red;

            loadSettings();

            // register the event that is fired after the key press.
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hookKeyPressed);
            try
            {
                hook.RegisterHotKey(QRUtils.ModifierKeys.Win, Keys.Q);
            }
            catch
            {
                MessageBox.Show("Failed to bind hotkey Win+Q!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblHotKey.Hide();
            }
        }

        private void loadSettings()
        {
            Properties.Settings.Default.Reload();

            Color maskColor = (Color)Properties.Settings.Default["MaskColor"];
            colorDlg.Color = maskColor;
            picMaskColor.BackColor = maskColor;

            String errorString = Properties.Settings.Default["ErrorCorrectionLevel"].ToString();
            if      (String.Equals(errorString, "L", StringComparison.CurrentCultureIgnoreCase))
            {
                errorLevel = ErrorCorrectionLevel.L;
            }
            else if (String.Equals(errorString, "M", StringComparison.CurrentCultureIgnoreCase))
            {
                errorLevel = ErrorCorrectionLevel.M;
            }
            else if (String.Equals(errorString, "Q", StringComparison.CurrentCultureIgnoreCase))
            {
                errorLevel = ErrorCorrectionLevel.Q;
            }
            else if (String.Equals(errorString, "H", StringComparison.CurrentCultureIgnoreCase))
            {
                errorLevel = ErrorCorrectionLevel.H;
            }
            else
            {
                errorLevel = ErrorCorrectionLevel.Q;
            }
            cbErrorLevel.SelectedIndex = cbErrorLevel.Items.IndexOf(errorString);
        }

        private void hookKeyPressed(object sender, KeyPressedEventArgs e)
        {
            // show the keys pressed in a label.
            //edText.Text = e.Modifier.ToString() + " + " + e.Key.ToString();
            btnQRDecode.PerformClick();
        }

        private Bitmap GetScreenSnapshot()
        {
            Bitmap fullImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                          Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(fullImage))
            {
                g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                    Screen.PrimaryScreen.Bounds.Y,
                                    0, 0,
                                    fullImage.Size,
                                    CopyPixelOperation.SourceCopy);
            }
            return (fullImage);
        }

        private void ShowQRCodeMask(Result result)
        {
            QRCodeSplashForm splash = new QRCodeSplashForm();
            splash.Panel.BackColor = picMaskColor.BackColor;

            float minX = Int32.MaxValue, minY = Int32.MaxValue, maxX = 0, maxY = 0;
            foreach (ResultPoint point in result.ResultPoints)
            {
                minX = Math.Min(minX, point.X);
                minY = Math.Min(minY, point.Y);
                maxX = Math.Max(maxX, point.X);
                maxY = Math.Max(maxY, point.Y);
            }
            // make it 20% larger
            float margin = (maxX - minX) * 0.2f;
            minX -= margin;
            maxX += margin;
            minY -= margin;
            maxY += margin;
            splash.Location = new Point((int)minX, (int)minY);
            // we need a panel because a window has a minimal size
            splash.Panel.Size = new Size((int)maxX - (int)minX, (int)maxY - (int)minY);
            splash.Size = splash.Panel.Size;
            splash.Show();
            System.Threading.Thread.Sleep(250);
            splash.Close();
        }

        private Bitmap QREncode(String text)
        {
            var width = 512;
            var height = 512;
            var margin = 0;

            int MAX_TEXT = 750;

            if (String.IsNullOrEmpty(text)) return (new Bitmap(width, height));

            string qrText = text;

            var bw = new ZXing.BarcodeWriter();

            var encOptions = new ZXing.Common.EncodingOptions
            {
                Width = width,
                Height = height,
                PureBarcode = false
            };

            encOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, errorLevel);
            encOptions.Hints.Add(EncodeHintType.MARGIN, margin);
            encOptions.Hints.Add(EncodeHintType.DISABLE_ECI, true);
            encOptions.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            encOptions.Hints.Add(EncodeHintType.PDF417_COMPACT, true);
            encOptions.Hints.Add(EncodeHintType.PDF417_COMPACTION, ZXing.PDF417.Internal.Compaction.AUTO);

            bw.Renderer = new BitmapRenderer();
            bw.Options = encOptions;
            bw.Format = ZXing.BarcodeFormat.QR_CODE;
            if (qrText.Length > MAX_TEXT)
            {
                qrText = qrText.Substring(0, MAX_TEXT);
            }
            Bitmap barcodeBitmap = bw.Write(qrText);
            return (barcodeBitmap);
        }

        private String QRDecode()
        {
            using (Bitmap fullImage = GetScreenSnapshot())
            {
                var br = new ZXing.BarcodeReader();
                br.AutoRotate = true;
                br.Options.CharacterSet = "UTF-8";
                br.Options.TryHarder = true;
                br.Options.PureBarcode = false;
                br.TryInverted = true;

                var result = br.Decode(fullImage);
                if (result != null)
                {
                    ShowQRCodeMask(result);
                    return (result.Text);
                }
                else
                {
                    MessageBox.Show("Failed to find QRCode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return (String.Empty);
                }
            }
        }

        private List<String> QRDecodeMulti()
        {
            using (Bitmap fullImage = GetScreenSnapshot())
            {
                fullImage.Save("test.png");
                //var br = new ZXing.BarcodeReader( null, 
                //                                  bitmap => new BitmapLuminanceSource(bitmap),
                //                                  luminance => new GlobalHistogramBinarizer(luminance));
                var br = new ZXing.BarcodeReader();
                br.AutoRotate = true;
                br.TryInverted = true;
                br.Options.CharacterSet = "UTF-8";
                br.Options.TryHarder = true;
                br.Options.PureBarcode = true;
                br.Options.PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE };

                var results = br.DecodeMultiple(fullImage);
                if (results != null)
                {
                    var textList = new List<String>();
                    foreach (var result in results)
                    {
                        ShowQRCodeMask(result);
                        textList.Add(result.Text);
                    }
                    return (textList);
                }
                else
                {
                    MessageBox.Show("Failed to find QRCode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return (new List<String>());
                }
            }
        }

        private void btnQRDecode_Click(object sender, EventArgs e)
        {
            bool MULTI = chkMultiDecode.Checked; //true;

            //this.Hide();
            //Application.DoEvents();
            if (this.WindowState != FormWindowState.Minimized)
            {
                this.Opacity = 0.0f;
                System.Threading.Thread.Sleep(75);
            }
            if (MULTI)
            {
                edText.Clear();
                foreach (var result in QRDecodeMulti())
                {
                    edText.Text += result + "\n\n";
                }
            }
            else
            {
                edText.Text = QRDecode();
            }

            this.Opacity = 1.0f;
            //this.Show();
        }

        private void btnQREncode_Click(object sender, EventArgs e)
        {
            picQR.Image = QREncode(edText.Text);
        }

        private void btnClipTo_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty( edText.Text ))
            {
                Clipboard.SetText(edText.Text);
            }
        }

        private void btnClipFrom_Click(object sender, EventArgs e)
        {           
            edText.Text = Clipboard.GetText(TextDataFormat.UnicodeText);
        }

        private void edText_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.V) || (e.Shift && e.KeyCode == Keys.I))
            {
                edText.Text += Clipboard.GetText(TextDataFormat.UnicodeText);
                e.Handled = true;
            }
        }

        private void picMaskColor_Click(object sender, EventArgs e)
        {
            colorDlg.Color = picMaskColor.BackColor;
            colorDlg.ShowDialog();
            picMaskColor.BackColor = colorDlg.Color;
            
            Properties.Settings.Default["MaskColor"] = colorDlg.Color;
            Properties.Settings.Default.Save(); 
        }

        private void cbErrorLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            String errorString = cbErrorLevel.SelectedItem.ToString();
            if (String.Equals(errorString, "L", StringComparison.CurrentCultureIgnoreCase))
            {
                errorLevel = ErrorCorrectionLevel.L;
            }
            else if (String.Equals(errorString, "M", StringComparison.CurrentCultureIgnoreCase))
            {
                errorLevel = ErrorCorrectionLevel.M;
            }
            else if (String.Equals(errorString, "Q", StringComparison.CurrentCultureIgnoreCase))
            {
                errorLevel = ErrorCorrectionLevel.Q;
            }
            else if (String.Equals(errorString, "H", StringComparison.CurrentCultureIgnoreCase))
            {
                errorLevel = ErrorCorrectionLevel.H;
            }
            else
            {
                errorLevel = ErrorCorrectionLevel.Q;
            }

            Properties.Settings.Default["ErrorCorrectionLevel"] = errorString;
            Properties.Settings.Default.Save(); 
        }

    }
}
