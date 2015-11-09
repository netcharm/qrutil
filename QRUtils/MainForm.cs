using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using ZXing;
using ZXing.QrCode.Internal;
using ZXing.Rendering;

namespace QRUtils
{
    public partial class MainForm : Form
    {
        private Font monoFont = new Font("DejaVu Sans Mono", 10);
        private ErrorCorrectionLevel errorLevel = ErrorCorrectionLevel.M;

        // QR码数据容量
        //   数字                      最多 7089 字符
        //   字母                      最多 4296 字符
        //   二进制数（8 bits）         最多 2953 字符
        //   日文汉字（Shift JIS）      最多 1817 字符
        //   平片假名（Shift JIS）      最多 1817 字符
        //   中文汉字（UTF-8）          最多 0984 字符
        //   中文汉字（BIG5）           最多 1800 字符
        int MAX_TEXT = 7089;

        private KeyboardHook hook = new KeyboardHook();

        public MainForm()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            edText.MaxLength = MAX_TEXT;

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
                MessageBox.Show(this, "Failed to bind hotkey Win+Q!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //status.Items[0]
                statusLabelHotkey.Text = String.Format("Hotkey: {0}", "None");
            }
        }

        private void loadSettings()
        {
            Properties.Settings.Default.Reload();

            Color maskColor = (Color)Properties.Settings.Default["MaskColor"];
            colorDlg.Color = maskColor;
            picMaskColor.BackColor = maskColor;

            string errorString = Properties.Settings.Default["ErrorCorrectionLevel"].ToString();
            if      (string.Equals(errorString, "L", StringComparison.CurrentCultureIgnoreCase))
            {
                errorLevel = ErrorCorrectionLevel.L;
            }
            else if (string.Equals(errorString, "M", StringComparison.CurrentCultureIgnoreCase))
            {
                errorLevel = ErrorCorrectionLevel.M;
            }
            else if (string.Equals(errorString, "Q", StringComparison.CurrentCultureIgnoreCase))
            {
                errorLevel = ErrorCorrectionLevel.Q;
            }
            else if (string.Equals(errorString, "H", StringComparison.CurrentCultureIgnoreCase))
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

        private Bitmap getScreenSnapshot()
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

        private float calcBorderWidth(Bitmap QRImage, Point mark, Size size)
        {
            float pixelWidth = 1.0f;

            int X = Math.Max(0, mark.X - 100);
            int Y = Math.Max(0, mark.Y - 100);
            int W = Math.Min(QRImage.Width - X, size.Width + 200);
            int H = Math.Min(QRImage.Height - Y, size.Height + 200);
            Bitmap bwQR = QRImage.Clone(new Rectangle(X, Y, W, H), PixelFormat.Format1bppIndexed);
            #if DEBUG 
            bwQR.Save("test-bw.png");
            #endif

            int origX = 100;
            int origY = 100;
            if (mark.X < 100) origX = mark.X;
            if (mark.Y < 100) origY = mark.Y;

            for (var i = 2; i < 100; i++ )
            {
                var color = bwQR.GetPixel(origX - i, origY);
                if(color.ToArgb() == Color.White.ToArgb())
                {
                    pixelWidth = i;
                    break;
                }
            }
            return (pixelWidth);
        }

        private void ShowQRCodeMask(Result result, Bitmap QRImage)
        {
            QRCodeSplashForm splash = new QRCodeSplashForm();
            splash.Panel.BackColor = picMaskColor.BackColor;

            float minX = int.MaxValue, minY = int.MaxValue, maxX = 0, maxY = 0;
            object orientation = 0;
            float Angle = 0;
            if (result.ResultMetadata.ContainsKey(ResultMetadataType.ORIENTATION))
            {
                result.ResultMetadata.TryGetValue(ResultMetadataType.ORIENTATION, out orientation);
                Angle = (float)((int)orientation * Math.PI / 180);
            }
            foreach (ResultPoint point in result.ResultPoints)
            {
                float x = point.X;
                float y = point.Y;
                if (Angle != 0)
                {
                    //x = QRImage.Width - (float)Math.Abs(point.X * Math.Cos(Angle) - point.Y * Math.Sin(Angle));
                    //y = QRImage.Height - (float)Math.Abs(point.Y * Math.Cos(Angle) + point.X * Math.Sin(Angle));
                    x = QRImage.Width - (float)Math.Abs(point.X * Math.Cos(Angle) + point.Y * Math.Sin(Angle));
                    y = (float)Math.Abs(-point.Y * Math.Cos(Angle) + point.X * Math.Sin(Angle));
                }

                minX = Math.Min(minX, x);
                minY = Math.Min(minY, y);
                maxX = Math.Max(maxX, x);
                maxY = Math.Max(maxY, y);
            }

            Point mark = new Point((int)minX, (int)minY);
            Size size = new Size((int)(maxX - minX), (int)(maxY - minY));
            float pixelWidth = calcBorderWidth(QRImage, mark, size);
            float margin = pixelWidth * 3;
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

        private Bitmap QREncode(string text)
        {
            var width = 512;
            var height = 512;
            var margin = 0;

            if (string.IsNullOrEmpty(text)) return (new Bitmap(width, height));

            string qrText = text;

            var bw = new BarcodeWriter();

            bw.Options.Width = width;
            bw.Options.Height = height;
            bw.Options.PureBarcode = false;
            bw.Options.Hints.Add(EncodeHintType.ERROR_CORRECTION, errorLevel);
            bw.Options.Hints.Add(EncodeHintType.MARGIN, margin);
            bw.Options.Hints.Add(EncodeHintType.DISABLE_ECI, true);
            bw.Options.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");
            bw.Options.Hints.Add(EncodeHintType.PDF417_COMPACT, true);
            bw.Options.Hints.Add(EncodeHintType.PDF417_COMPACTION, ZXing.PDF417.Internal.Compaction.AUTO);

            bw.Renderer = new BitmapRenderer();
            bw.Format = BarcodeFormat.QR_CODE;
            if (qrText.Length > MAX_TEXT)
            {
                qrText = qrText.Substring(0, MAX_TEXT);
            }
            try
            {
                Bitmap barcodeBitmap = bw.Write(qrText);
                return (barcodeBitmap);
            }
            catch(WriterException)
            {
                MessageBox.Show(this, "Text too long!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return (new Bitmap(width, height));
            }
        }

        private string QRDecode(Bitmap qrImage)
        {
            using (qrImage)
            {
                var br = new BarcodeReader();
                br.AutoRotate = true;
                br.Options.CharacterSet = "UTF-8";
                br.Options.TryHarder = true;
                br.Options.PureBarcode = false;
                br.TryInverted = true;

                var result = br.Decode(qrImage);
                if (result != null)
                {
                    ShowQRCodeMask(result, qrImage);
                    return (result.Text);
                }
                else
                {
                    #if DENUG
                    MessageBox.Show(this, "Failed to find QRCode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    #endif
                    return (string.Empty);
                }
            }
        }

        private List<string> QRDecodeMulti(Bitmap qrImage)
        {
            using (qrImage)
            {
                #if DEBUG
                qrImage.Save("screensnap.png");
                #endif
                //var br = new BarcodeReader( null, 
                //                                  bitmap => new BitmapLuminanceSource(bitmap),
                //                                  luminance => new GlobalHistogramBinarizer(luminance));
                var br = new BarcodeReader();
                br.AutoRotate = true;
                br.TryInverted = true;
                br.Options.CharacterSet = "UTF-8";
                br.Options.TryHarder = true;
                br.Options.PureBarcode = false;
                br.Options.PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE };

                var results = br.DecodeMultiple(qrImage);
                if (results != null)
                {
                    var textList = new List<string>();
                    foreach (var result in results)
                    {
                        ShowQRCodeMask(result, qrImage);
                        textList.Add(result.Text);
                    }
                    return (textList);
                }
                else
                {
                    #if DEBUG
                    MessageBox.Show(this, "Failed to find QRCode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    #endif
                    return (new List<string>());
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
                var results = QRDecodeMulti(getScreenSnapshot());
                foreach (var result in results)
                {
                    edText.Text += result + "\n\n";
                }
                //status.Items[2]
                statusLabelDecodeCount.Text = String.Format("QR Found: {0}", results.Count);
            }
            else
            {
                edText.Text = QRDecode(getScreenSnapshot());
                //status.Items[2]
                statusLabelDecodeCount.Text = String.Format("QR Found: {0}", 1);
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
            if (!string.IsNullOrEmpty( edText.Text ))
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
                string text = edText.Text + Clipboard.GetText(TextDataFormat.UnicodeText);
                if(text.Length>MAX_TEXT)
                {
                    edText.Text = text.Substring(0, MAX_TEXT);
                }
                else
                {
                    edText.Text = text;
                }
                
                e.Handled = true;
            }
        }

        private void edText_TextChanged(object sender, EventArgs e)
        {
            //status.Items[1]
            statusLabelTextCount.Text = String.Format("Text Count: {0}", edText.Text.Length.ToString());
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
            string errorString = cbErrorLevel.SelectedItem.ToString();
            if (string.Equals(errorString, "L", StringComparison.CurrentCultureIgnoreCase))
            {
                errorLevel = ErrorCorrectionLevel.L;
            }
            else if (string.Equals(errorString, "M", StringComparison.CurrentCultureIgnoreCase))
            {
                errorLevel = ErrorCorrectionLevel.M;
            }
            else if (string.Equals(errorString, "Q", StringComparison.CurrentCultureIgnoreCase))
            {
                errorLevel = ErrorCorrectionLevel.Q;
            }
            else if (string.Equals(errorString, "H", StringComparison.CurrentCultureIgnoreCase))
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
