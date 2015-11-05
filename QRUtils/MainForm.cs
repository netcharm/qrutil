using System;
using System.Drawing;
using System.Windows.Forms;

using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;
using System.Text;
using ZXing.Rendering;

namespace QRUtils
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
        }

        private String QRDecode()
        {
            var success = false;
            String QRString = "No QRCode!";

            using (Bitmap fullImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                            Screen.PrimaryScreen.Bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(fullImage))
                {
                    g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                     Screen.PrimaryScreen.Bounds.Y,
                                     0, 0,
                                     fullImage.Size,
                                     CopyPixelOperation.SourceCopy);
                }
                for (int i = 0; i < 5; i++)
                {
                    int marginLeft = fullImage.Width * i / 3 / 5;
                    int marginTop = fullImage.Height * i / 3 / 5;
                    Rectangle cropRect = new Rectangle(marginLeft, marginTop, fullImage.Width - marginLeft * 2, fullImage.Height - marginTop * 2);
                    Bitmap target = new Bitmap(cropRect.Width, cropRect.Height);

                    using (Graphics g = Graphics.FromImage(target))
                    {
                        g.DrawImage(fullImage,
                                     new Rectangle(0, 0, target.Width, target.Height),
                                     cropRect,
                                     GraphicsUnit.Pixel);
                    }

                    var br = new ZXing.BarcodeReader();

                    var decOptions = new ZXing.Common.DecodingOptions
                    {
                        PureBarcode = false
                    };

                    decOptions.Hints.Add(DecodeHintType.CHARACTER_SET, "UTF-8");
                    var result = br.Decode(target);

                    if (result != null)
                    {
                        QRCodeSplashForm splash = new QRCodeSplashForm();
                        float minX = Int32.MaxValue, minY = Int32.MaxValue, maxX = 0, maxY = 0;
                        foreach (ResultPoint point in result.ResultPoints)
                        {
                            minX = Math.Min(minX, point.X);
                            minY = Math.Min(minY, point.Y);
                            maxX = Math.Max(maxX, point.X);
                            maxY = Math.Max(maxY, point.Y);
                        }
                        // make it 20% larger
                        float margin = (maxX - minX) * 0.20f;
                        minX += -margin + marginLeft;
                        maxX += margin + marginLeft;
                        minY += -margin + marginTop;
                        maxY += margin + marginTop;
                        splash.Location = new Point((int)minX, (int)minY);
                        // we need a panel because a window has a minimal size
                        splash.Panel.Size = new Size((int)maxX - (int)minX, (int)maxY - (int)minY);
                        splash.Size = splash.Panel.Size;
                        splash.Show();
                        System.Threading.Thread.Sleep(250);
                        splash.Close();

                        QRString = result.ToString();
                        success = true;
                        break;
                    }
                }
            }
            if (!success)
            {
                MessageBox.Show("Failed to find QRCode!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return(String.Empty);
            }
            else
            {
                return (QRString);
            }
        }

        private void btnQRDecode_Click(object sender, EventArgs e)
        {
            //this.Hide();
            //Application.DoEvents();
            this.Opacity = 0.0f;

            edText.Text = QRDecode();

            this.Opacity = 1.0f;
            //this.Show();
        }

        private void btnQREncode_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(edText.Text)) return;

            //string qrText = Encoding.UTF8.GetString(Encoding.Default.GetBytes("中文")); //edText.Text));
            string qrText = edText.Text;

            var width = 256;
            var height = 256;
            var margin = 0;

            var bw = new ZXing.BarcodeWriter();

            var encOptions = new ZXing.Common.EncodingOptions
            {
                Width = width,
                Height = height,
                Margin = margin,
                PureBarcode = false
            };

            encOptions.Hints.Add(EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.H);
            encOptions.Hints.Add(EncodeHintType.DISABLE_ECI, true);
            encOptions.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");

            bw.Renderer = new BitmapRenderer();
            bw.Options = encOptions;
            bw.Format = ZXing.BarcodeFormat.QR_CODE;
            Bitmap barcodeBitmap = bw.Write(qrText);

            picQR.Image = barcodeBitmap;

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

        private void edText_TextChanged(object sender, EventArgs e)
        {
        }

        private void edText_Validated(object sender, EventArgs e)
        {
            edText.SelectionStart = 0;
            edText.SelectionLength = edText.TextLength;
            edText.SelectionFont = new System.Drawing.Font("DejaVu Sans Mono", 10);

        }
    }
}
