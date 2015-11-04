using System;
using System.Drawing;
using System.Windows.Forms;

using Gettext.WinForm;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
using ZXing.QrCode.Internal;

namespace QRUtils
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            I18N i10n = new I18N("GetTextUtils", this);
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

                    var source = new BitmapLuminanceSource(target);
                    var bitmap = new BinaryBitmap(new HybridBinarizer(source));
                    QRCodeReader reader = new QRCodeReader();
                    var result = reader.decode(bitmap);
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
                MessageBox.Show(I18N.GetString("Failed to find QRCode"));
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
            //System.Threading.Thread.Sleep(75);
            this.Opacity = 0.0f;

            edText.Text = QRDecode();

            //System.Threading.Thread.Sleep(100);
            this.Opacity = 1.0f;
            //this.Show();
        }

        private void btnQREncode_Click(object sender, EventArgs e)
        {
            string qrText = edText.Text;
            QRCode code = ZXing.QrCode.Internal.Encoder.encode(qrText, ErrorCorrectionLevel.M);
            ByteMatrix m = code.Matrix;
            int blockSize = Math.Max(picQR.Height / m.Height, 1);
            Bitmap drawArea = new Bitmap((m.Width * blockSize), (m.Height * blockSize));
            using (Graphics g = Graphics.FromImage(drawArea))
            {
                g.Clear(Color.White);
                using (Brush b = new SolidBrush(Color.Black))
                {
                    for (int row = 0; row < m.Width; row++)
                    {
                        for (int col = 0; col < m.Height; col++)
                        {
                            if (m[row, col] != 0)
                            {
                                g.FillRectangle(b, blockSize * row, blockSize * col, blockSize, blockSize);
                            }
                        }
                    }
                }
            }
            picQR.Image = drawArea;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(edText.Text);
        }

        private void btnClipFrom_Click(object sender, EventArgs e)
        {
            edText.Text = Clipboard.GetText();
        }
    }
}
