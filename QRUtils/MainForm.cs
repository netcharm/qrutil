using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using NGettext.WinForm;
using ZXing;
using ZXing.Common;
using ZXing.QrCode.Internal;
using ZXing.Rendering;

namespace QRUtils
{
    public enum BARCODE_TYPE {
        EXPRESS=0, ISBN=1, PRODUCT=2,
        URL=3, TEL=4, MAIL=5, SMS=6,
        VCARD=7, VCAL=8
    };
    
    public partial class MainForm : Form
    {
        private static string AppPath = AppDomain.CurrentDomain.BaseDirectory;

        private static Configuration config = ConfigurationManager.OpenExeConfiguration( Application.ExecutablePath );
        private AppSettingsSection appSection = config.AppSettings;

        // string resourceName = typeof(Program).Assembly.GetName().Name;
        private static string resourceBaseName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        private static string resourceCultureName = Thread.CurrentThread.CurrentUICulture.Name;
        private static string resourcePath = AppDomain.CurrentDomain.BaseDirectory + "locale";

        private Font monoFont = new Font("DejaVu Sans Mono", 10);
        private ErrorCorrectionLevel errorLevel = ErrorCorrectionLevel.M;

        private Color maskColor = Color.FromArgb(0, 192, 192);

        private bool overlayLogo = false;
        private string overlayLogoImage = "logo.png";
        private Color overlayBGColor = Color.Orange;
        private Dictionary<string, string> overlayIcons = new Dictionary<string, string>();
        private List<ToolStripItem> iconItems = new List<ToolStripItem>();

        // QR码数据容量
        //   数字                      最多 7089 字符
        //   字母                      最多 4296 字符
        //   二进制数（8 bits）        最多 2953 字符
        //   日文汉字（Shift JIS）     最多 1817 字符
        //   平片假名（Shift JIS）     最多 1817 字符
        //   中文汉字（UTF-8）         最多 0984 字符
        //   中文汉字（BIG5）          最多 1800 字符
        int MAX_TEXT = 7089;

        private string lastText = string.Empty;
        private int lastCursor = 0;

        private KeyboardHook hook = new KeyboardHook();


        public MainForm()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);

            I18N i10n = new I18N( null, this );

            status.Padding = new Padding( 
                status.Padding.Left,
                status.Padding.Top, 
                status.Padding.Left, 
                status.Padding.Bottom 
                );

            btnQRInput.Image = Icon.ToBitmap();

            edText.MaxLength = MAX_TEXT;

            colorDlg.Color = Color.Cyan;
            picMaskColor.BackColor = Color.Red;

            if( cbBarFormat.Items.Count > 0)
                cbBarFormat.SelectedIndex = 0;

            loadSettings();

            // register the event that is fired after the key press.
            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(hookKeyPressed);
            try
            {
                hook.RegisterHotKey(QRUtils.ModifierKeys.Win, Keys.Q);
                statusLabelHotkey.Text = string.Format( I18N._( "Hotkey: {0}" ), "WIN + Q" );
            }
            catch
            {
                MessageBox.Show( this, I18N._( "Failed to bind hotkey Win+Q!" ), I18N._( "Error" ), MessageBoxButtons.OK, MessageBoxIcon.Error );
                //status.Items[0]
                statusLabelHotkey.Text = string.Format( I18N._( "Hotkey: {0}" ), I18N._( "None" ) );
            }
        }

        private void loadSettings()
        {
            bool update = false;
            ///
            /// QR Detect indicator mask color
            ///
            try
            {
                maskColor = ColorTranslator.FromHtml( appSection.Settings["MaskColor"].Value );
            }
            catch
            {
                appSection.Settings.Add( "MaskColor", ColorTranslator.ToHtml( maskColor ) );
                update = true;
            }
            colorDlg.Color = maskColor;
            picMaskColor.BackColor = maskColor;

            ///
            /// Overlay Logo Icon
            ///
            try
            {
                overlayLogo = Convert.ToBoolean( appSection.Settings["OverlayLogo"].Value );
            }
            catch
            {
                appSection.Settings.Add( "OverlayLogo", overlayLogo.ToString() );
                update = true;
            }
            chkOverLogo.Checked = overlayLogo;

            try
            {
                overlayBGColor = ColorTranslator.FromHtml( appSection.Settings["OverlayBGColor"].Value );
            }
            catch
            {
                appSection.Settings.Add( "OverlayBGColor", ColorTranslator.ToHtml( overlayBGColor ) );
                update = true;
            }

            try
            {
                overlayLogoImage = appSection.Settings["OverlayLogoImage"].Value;
            }
            catch
            {
                appSection.Settings.Add( "OverlayLogoImage", overlayLogoImage );
                update = true;
            }

            ///
            /// QR Encode Error Level
            ///
            string errorString = "M";
            try
            {
                errorString = appSection.Settings["ErrorCorrectionLevel"].Value;
            }
            catch
            {
                appSection.Settings.Add( "ErrorCorrectionLevel", errorString );
                update = true;
            }

            if ( string.Equals( errorString, I18N._( "L" ), StringComparison.CurrentCultureIgnoreCase ) )
            {
                errorLevel = ErrorCorrectionLevel.L;
            }
            else if ( string.Equals( errorString, I18N._( "M" ), StringComparison.CurrentCultureIgnoreCase ) )
            {
                errorLevel = ErrorCorrectionLevel.M;
            }
            else if ( string.Equals( errorString, I18N._( "Q" ), StringComparison.CurrentCultureIgnoreCase ) )
            {
                errorLevel = ErrorCorrectionLevel.Q;
            }
            else if ( string.Equals( errorString, I18N._( "H" ), StringComparison.CurrentCultureIgnoreCase ) )
            {
                errorLevel = ErrorCorrectionLevel.H;
            }
            else
            {
                errorLevel = ErrorCorrectionLevel.M;
            }
            cbErrorLevel.SelectedIndex = cbErrorLevel.Items.IndexOf( I18N._( errorString ) );

            ///
            /// QR Decode Options
            ///
            try
            {
                chkDecodeFormat1D.Checked = Convert.ToBoolean( appSection.Settings["DecodeFormat1D"].Value );
            }
            catch
            {
                appSection.Settings.Add( "DecodeFormat1D", chkDecodeFormat1D.Checked.ToString() );
                update = true;
            }

            try
            {
                chkDecodeFormatDM.Checked = Convert.ToBoolean( appSection.Settings["DecodeFormatDM"].Value );
            }
            catch
            {
                appSection.Settings.Add( "DecodeFormatDM", chkDecodeFormatDM.Checked.ToString() );
                update = true;
            }

            try
            {
                chkDecodeFormatQR.Checked = Convert.ToBoolean( appSection.Settings["DecodeFormatQR"].Value );
            }
            catch
            {
                appSection.Settings.Add( "DecodeFormatQR", chkDecodeFormatQR.Checked.ToString() );
                update = true;
            }

            if(update) config.Save();
        }

        private void hookKeyPressed(object sender, KeyPressedEventArgs e)
        {
            // show the keys pressed in a label.
            //edText.Text = e.Modifier.ToString() + " + " + e.Key.ToString();
            btnQRDecode.PerformClick();
        }

        private void listIcons(string folder)
        {
            if ( Directory.Exists( folder ) )
            {
                List<string> exts = new List<string>(){ ".png", ".bmp", ".jpg", ".ico", ".gif" };
                string[] logoFiles = Directory.GetFiles( $"{AppPath}icons" );
                if ( logoFiles.Length == logoItems.DropDownItems.Count ) return;

                overlayIcons.Clear();
                logoItems.DropDownItems.Clear();
                //foreach( ToolStripItem item in iconItems )
                //{
                //    iconItems.Remove( item );
                //    item.Dispose();
                //}
                for ( int i = iconItems.Count - 1; i >= 0; i-- )
                {
                    ToolStripItem item = iconItems[i];
                    iconItems.Remove( item );
                    item.Dispose();
                }
                iconItems.Clear();

                foreach ( string logoFile in logoFiles )
                {
                    string itemText = Path.GetFileName(logoFile);
                    string itemExt = Path.GetExtension(logoFile);
                    if ( exts.Contains( itemExt.ToLower() ) )
                    {
                        overlayIcons.Add( itemText, logoFile );

                        using ( var fs = new FileStream( logoFile, FileMode.Open ) )
                        {
                            var img = new Bitmap(fs, true);
                            ToolStripItem item = logoItems.DropDownItems.Add( itemText, (Bitmap)img.Clone() );
                            iconItems.Add( item );
                            img.Dispose();
                        }
                    }
                }
            }
        }

        private bool isAllowedDrag( IDataObject Data )
        {
            string[] allowed_fmts = {
                DataFormats.FileDrop,
                DataFormats.Text,
                DataFormats.UnicodeText,
                DataFormats.OemText,
                DataFormats.Html,
                DataFormats.Rtf,
                DataFormats.StringFormat
            };

            string[] fmts = Data.GetFormats( true );

            return ( fmts.Intersect( allowed_fmts ).Any() );
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
            if (mark.X <= 0 || mark.Y <= 0) return 0;

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

            Color colorMark = bwQR.GetPixel(origX, origY);
            for (var i = 2; i < 100; i++ )
            {
                Color color = bwQR.GetPixel(origX - i, origY);
                //if(color.ToArgb() == Color.White.ToArgb())
                if ( color.ToArgb() != colorMark.ToArgb() )
                {
                    pixelWidth = i;
                    break;
                }
            }
            return ( pixelWidth );
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

        private string calcISBN_10( string text )
        {
            text = text.Replace( "-", "" ).Replace( " ", "" );

            long value = 0;
            if ( !long.TryParse( text, out value ) ) return ( string.Empty );
            if ( text.Length != 9 && text.Length != 10 )
            {
                if ( text.Length == 12 || text.Length == 13 )
                {
                    text = text.Substring( 3, 9 );
                }
                else
                    return ( string.Empty );
            }

            int[] w = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var cd = 0;
            for ( int i = 0; i < 9; i++ )
            {
                cd += (int) Char.GetNumericValue( text[i] ) * w[i];
            }
            var N = cd % 11;
            if ( N == 10 )
                cd = 'X';
            else
                cd = ( N == 11 ) ? 0 : N;
            return text.Substring( 0, 9 ) + cd.ToString();
        }

        private string calcISBN_13(string text)
        {
            text = text.Replace( "-", "" ).Replace( " ", "" );

            long value = 0;
            if ( !long.TryParse( text, out value ) ) return ( string.Empty );
            if ( text.Length != 12 && text.Length != 13 ) return ( string.Empty );

            int[] w = { 1, 3, 1, 3, 1, 3, 1, 3, 1, 3, 1, 3 };
            var cd = 0;
            for ( int i = 0; i < 12; i++ )
            {
                cd += (int) Char.GetNumericValue( text[i] ) * w[i];
            }
            cd = ( cd % 10 == 0 ) ? 0 : 10 - ( cd % 10 );
            return text.Substring( 0, 12 ) + cd.ToString();
        }

        private Bitmap BarEncode( string text, BarcodeFormat barFormat = BarcodeFormat.CODE_128, bool isbn = false )
        {
            var width = 256;
            var height = 128;
            var margin = 0;

            if ( string.IsNullOrEmpty( text ) ) return ( new Bitmap( width, height ) );

            string barText = text;
            int maxText = 16;
            switch ( barFormat )
            {
                case BarcodeFormat.CODE_128:
                    width = 256;
                    height = 72;
                    margin = 7;
                    maxText = 232;
                    break;
                case BarcodeFormat.EAN_13:
                    //height = (int) ( width * 25.93 / 37.29 );
                    height = (int) ( width * 26.26 / 37.29 );
                    if ( isbn )
                    {
                        string isbn13 = calcISBN_13(barText);
                        string isbn10 = calcISBN_10(barText);
                        if ( string.IsNullOrEmpty( isbn13 ) ) return ( new Bitmap( width, height ) );
                        //if ( string.IsNullOrEmpty( isbn10 ) ) return ( new Bitmap( width, height ) );
                        barText = isbn13;
                    }
                    break;
                default:
                    return ( new Bitmap( width, height ) );
            }
            var bw = new BarcodeWriter();

            bw.Options.Width = width;
            bw.Options.Height = height;
            //bw.Options.PureBarcode = false;
            bw.Options.Hints.Add( EncodeHintType.MARGIN, margin );
            bw.Options.Hints.Add( EncodeHintType.DISABLE_ECI, true );
            bw.Options.Hints.Add( EncodeHintType.CHARACTER_SET, "UTF-8" );

            bw.Renderer = new BitmapRenderer();
            bw.Format = barFormat;
            if ( barText.Length > maxText )
            {
                barText = barText.Substring( 0, maxText );
            }
            try
            {
                BitMatrix bm = bw.Encode( barText );
                int[] rectangle = bm.getEnclosingRectangle();
                var bmW = rectangle[2];
                var bmH = rectangle[3];
                //bw.Options.Width = ( bmW <= 256 ) ? ( bmW + 32 ) : (int)( bmW * ( 1 + 32 / 256 ) );
                bw.Options.Width = (int)( bmW * 1.25);

                Bitmap barcodeBitmap = bw.Write(barText);
                return ( barcodeBitmap );
            }
            catch ( WriterException )
            {
                MessageBox.Show( this, I18N._( "Text data can not be encoded to barcode!" ), I18N._( "Error" ), MessageBoxButtons.OK, MessageBoxIcon.Error );
                return ( new Bitmap( width, height ) );
            }
        }

        private Bitmap drawOverlayLogo( Bitmap image, string logo )
        {
            string logoFile = $"{AppPath}icons\\{logo}";
            try
            {
                logoFile = overlayIcons[logo];
            }
            catch
            {
            }

            if ( File.Exists( logoFile ) )
            {
                Bitmap logoImage = new Bitmap(logoFile );
                return drawOverlayLogo( image, logoImage );
            }
            else
                return image;
        }

        private Bitmap drawOverlayLogo(Bitmap image, Bitmap logo)
        {
            if ( logo == null ) return image;

            int lw = logo.Width;
            int lh = logo.Height;

            Bitmap roundedLogo = new Bitmap(lw, lh);
            using ( Graphics g = Graphics.FromImage( roundedLogo ) )
            {
                int CornerRadius = Math.Min(lw, lh) / 4;
                //Color BackgroundColor = Color.Transparent;
                //Color BackgroundColor = Color.Orange;
                Color BackgroundColor = overlayBGColor;

                GraphicsPath gp = new GraphicsPath();
                gp.AddArc( 0, 0, CornerRadius, CornerRadius, 180, 90 );
                gp.AddArc( lw - CornerRadius, 0, CornerRadius, CornerRadius, 270, 90 );
                gp.AddArc( lw - CornerRadius, lh - CornerRadius, CornerRadius, CornerRadius, 0, 90 );
                gp.AddArc( 0, lh - CornerRadius, CornerRadius, CornerRadius, 90, 90 );

                g.SmoothingMode = SmoothingMode.HighQuality;
                g.SetClip( gp );
                g.Clear( BackgroundColor );
                g.DrawImage( logo, lw / 16, lh / 16, lw - lw / 8, lh - lh / 8 );
            }
#if DEBUG
            roundedLogo.Save("RoundedLogo.png");
#endif
            //int deltaWidth = image.Width - logo.Width;
            //int deltaHeigth = image.Height - logo.Height;
            int w = image.Width / 4;
            int h = image.Height / 4;
            int x = (image.Width - image.Width / 4) / 2;
            int y = (image.Height - image.Height / 4) / 2;
            using ( Graphics g = Graphics.FromImage( image ) )
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                //g.DrawImage( roundedLogo, new Point( x, y ) );
                g.DrawImage( roundedLogo, x, y, w, h);
            }
            return image;
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
            //bw.Options.Hints.Add(EncodeHintType.PDF417_COMPACT, true);
            //bw.Options.Hints.Add(EncodeHintType.PDF417_COMPACTION, ZXing.PDF417.Internal.Compaction.AUTO);

            bw.Renderer = new BitmapRenderer();
            bw.Format = BarcodeFormat.QR_CODE;
            if (qrText.Length > MAX_TEXT)
            {
                qrText = qrText.Substring(0, MAX_TEXT);
            }
            try
            {
                Bitmap qrBitmap = bw.Write(qrText);
                if(chkOverLogo.Checked)
                {
                    qrBitmap = drawOverlayLogo( qrBitmap, overlayLogoImage );
                }
                return ( qrBitmap );
            }
            catch ( WriterException )
            {
                MessageBox.Show( this, I18N._( "Text too long!" ), I18N._( "Error" ), MessageBoxButtons.OK, MessageBoxIcon.Error );
                return ( new Bitmap( width, height ) );
            }
        }

        private void setDecodeOptions(BarcodeReader br)
        {
            br.AutoRotate = true;
            br.TryInverted = true;
            if(cbDecodeUTF8.Checked)
            {
                br.Options.CharacterSet = "UTF-8";
            }
            br.Options.TryHarder = true;
            br.Options.PureBarcode = false;
            //br.Options.ReturnCodabarStartEnd = true;
            //br.Options.UseCode39ExtendedMode = true;
            //br.Options.UseCode39RelaxedExtendedMode = true;
            br.Options.PossibleFormats = new List<BarcodeFormat>
            {
                //BarcodeFormat.All_1D,
                //BarcodeFormat.DATA_MATRIX,
                //BarcodeFormat.AZTEC,
                //BarcodeFormat.PDF_417,
                //BarcodeFormat.QR_CODE
            };
            if (chkDecodeFormat1D.Checked)
            {
                br.Options.PossibleFormats.Add(BarcodeFormat.All_1D);
                br.Options.PossibleFormats.Add(BarcodeFormat.CODABAR);
                br.Options.PossibleFormats.Add(BarcodeFormat.CODE_128);
                br.Options.PossibleFormats.Add(BarcodeFormat.CODE_39);
                br.Options.PossibleFormats.Add(BarcodeFormat.CODE_93);
                br.Options.PossibleFormats.Add(BarcodeFormat.EAN_13);
                br.Options.PossibleFormats.Add(BarcodeFormat.EAN_8);
                br.Options.PossibleFormats.Add(BarcodeFormat.ITF);
                br.Options.PossibleFormats.Add(BarcodeFormat.MAXICODE);
                br.Options.PossibleFormats.Add(BarcodeFormat.RSS_14);
                br.Options.PossibleFormats.Add(BarcodeFormat.RSS_EXPANDED);
                br.Options.PossibleFormats.Add(BarcodeFormat.UPC_A);
                br.Options.PossibleFormats.Add(BarcodeFormat.UPC_E);
                br.Options.PossibleFormats.Add(BarcodeFormat.UPC_EAN_EXTENSION);
            }
            if (chkDecodeFormatDM.Checked) br.Options.PossibleFormats.Add(BarcodeFormat.DATA_MATRIX);
            if (chkDecodeFormatQR.Checked) br.Options.PossibleFormats.Add(BarcodeFormat.QR_CODE);
        }

        private string QRDecode(Bitmap qrImage, bool showMask=true)
        {
            using (qrImage)
            {
                //var br = new BarcodeReader(null,
                //                           bitmap => new BitmapLuminanceSource(bitmap),
                //                           luminance => new GlobalHistogramBinarizer(luminance));
                var br = new BarcodeReader();
                setDecodeOptions(br);

                try
                {
                    var result = br.Decode(qrImage);
                    if (result != null)
                    {
                        if(showMask) ShowQRCodeMask(result, qrImage);
                        SystemSounds.Beep.Play();
                        return (result.Text);
                    }
                    else
                    {
#if DEBUG
                        MessageBox.Show(this, "Failed to find Code!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
#endif
                        SystemSounds.Exclamation.Play();
                        return (string.Empty);
                    }
                }
                catch
                {
                    SystemSounds.Exclamation.Play();
                    return (string.Empty);
                }

            }
        }

        private List<string> QRDecodeMulti(Bitmap qrImage, bool showMask = true )
        {
            using (qrImage)
            {
#if DEBUG
                qrImage.Save("screensnap.png");
#endif
                //var br = new BarcodeReader( null, 
                //                            bitmap => new BitmapLuminanceSource(bitmap),
                //                            luminance => new GlobalHistogramBinarizer(luminance));
                var br = new BarcodeReader();
                setDecodeOptions(br);

                try
                {
                    var results = br.DecodeMultiple(qrImage);
                    if (results != null)
                    {
                        var textList = new List<string>();
                        foreach (var result in results)
                        {
                            if ( showMask ) ShowQRCodeMask( result, qrImage );
                            textList.Add(result.Text);
                        }
                        //SystemSounds.Asterisk.Play();
                        SystemSounds.Beep.Play();
                        return (textList);
                    }
                    else
                    {
#if DEBUG
                        MessageBox.Show(this, "Failed to find Code!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
#endif
                        SystemSounds.Exclamation.Play();
                        return (new List<string>());
                    }
                }
                catch
                {
                    SystemSounds.Exclamation.Play();
                    return (new List<string>());
                }
            }
        }

        private void btnQRDecode_Click(object sender, EventArgs e)
        {
            bool MULTI = chkMultiDecode.Checked; //true;

            //this.Hide();
            //Application.DoEvents();
            if ( WindowState != FormWindowState.Minimized)
            {
                Opacity = 0.0f;
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
                statusLabelDecodeCount.Text = string.Format(I18N._("Code Found: {0}"), results.Count);
            }
            else
            {
                edText.Text = QRDecode(getScreenSnapshot());
                //status.Items[2]
                if(edText.Text.Length>0)
                {
                    statusLabelDecodeCount.Text = string.Format("Code Found: {0}", 1);
                }
                else
                {
                    statusLabelDecodeCount.Text = string.Format("Code Found: {0}", 0);
                }
            }

            Opacity = 1.0f;
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
                lastText = edText.Text;
                lastCursor = edText.SelectionStart;

                if ( edText.SelectionLength > 0 )
                {
                    edText.Text = edText.Text.Remove( edText.SelectionStart, edText.SelectionLength );
                    edText.SelectionStart = lastCursor;
                }
                var cliptext = Clipboard.GetText(TextDataFormat.UnicodeText);
                string text = edText.Text.Insert( edText.SelectionStart, cliptext );
                if (text.Length>MAX_TEXT)
                {
                    edText.Text = text.Substring(0, MAX_TEXT);
                }
                else
                {
                    edText.Text = text;
                }
                edText.SelectionStart = lastCursor + cliptext.Length;
                e.Handled = true;
            }
            else if ( e.Control && e.KeyCode == Keys.Z )
            {
                if(!edText.CanUndo)
                {
                    edText.Text = lastText;
                    edText.SelectionStart = lastCursor;
                    //e.Handled = true;
                }
            }

        }

        private void edText_TextChanged(object sender, EventArgs e)
        {
            //status.Items[1]
            statusLabelTextCount.Text = string.Format(I18N._("Text Count: {0}"), edText.Text.Length.ToString());
        }

        private void picMaskColor_Click(object sender, EventArgs e)
        {
            colorDlg.Color = picMaskColor.BackColor;
            colorDlg.ShowDialog();
            picMaskColor.BackColor = colorDlg.Color;
            maskColor = colorDlg.Color;

            try
            {
                appSection.Settings["MaskColor"].Value = ColorTranslator.ToHtml( maskColor );
            }
            catch
            {
                appSection.Settings.Add( "MaskColor", ColorTranslator.ToHtml( maskColor ) );
            }

            //appSection.Settings["MaskColor"].Value = $"{colorDlg.Color.R}, {colorDlg.Color.G}, {colorDlg.Color.B}, {colorDlg.Color.A}";
            config.Save(); 
        }

        private void cbErrorLevel_SelectedIndexChanged( object sender, EventArgs e )
        {
            if ( !this.Visible ) return;

            string errorString = cbErrorLevel.SelectedItem.ToString();
            if ( string.Equals( errorString, I18N._( "L" ), StringComparison.CurrentCultureIgnoreCase ) )
            {
                errorLevel = ErrorCorrectionLevel.L;
            }
            else if ( string.Equals( errorString, I18N._( "M" ), StringComparison.CurrentCultureIgnoreCase ) )
            {
                errorLevel = ErrorCorrectionLevel.M;
            }
            else if ( string.Equals( errorString, I18N._( "Q" ), StringComparison.CurrentCultureIgnoreCase ) )
            {
                errorLevel = ErrorCorrectionLevel.Q;
            }
            else if ( string.Equals( errorString, I18N._( "H" ), StringComparison.CurrentCultureIgnoreCase ) )
            {
                errorLevel = ErrorCorrectionLevel.H;
            }
            else
            {
                errorLevel = ErrorCorrectionLevel.Q;
            }
            appSection.Settings["ErrorCorrectionLevel"].Value = errorString;
            config.Save();
        }

        private void chkDecodeFormat_CheckedChanged(object sender, EventArgs e)
        {
            if ( this.Visible )
            {
                appSection.Settings["DecodeFormat1D"].Value = chkDecodeFormat1D.Checked.ToString();
                appSection.Settings["DecodeFormatDM"].Value = chkDecodeFormatDM.Checked.ToString();
                appSection.Settings["DecodeFormatQR"].Value = chkDecodeFormatQR.Checked.ToString();
                config.Save();
            }
        }

        private void chkMultiDecode_CheckedChanged(object sender, EventArgs e)
        {
            //grpDecodeFormat.Enabled = chkMultiDecode.Checked;
            if(chkDecodeFormatQR.Checked || chkDecodeFormatDM.Checked || chkDecodeFormat1D.Checked)
            {

            }
            else
            {
                if(chkMultiDecode.Checked) chkDecodeFormatQR.Checked = true;
            }
        }

        private void btnBarCode_Click( object sender, EventArgs e )
        {
            var targetBar = (BARCODE_TYPE) Enum.ToObject( typeof( BARCODE_TYPE ), cbBarFormat.SelectedIndex );
            var targetFromat = BarcodeFormat.CODE_39;

            bool isbn = false;
            bool OneD = true;

            string infoText = edText.Text;

            switch(cbBarFormat.SelectedIndex)
            {
                case 0:
                    targetFromat = BarcodeFormat.CODE_128;
                    OneD = true;
                    break;
                case 1:
                    targetFromat = BarcodeFormat.EAN_13;
                    isbn = true;
                    OneD = true;
                    break;
                case 2:
                    targetFromat = BarcodeFormat.EAN_13;
                    OneD = true;
                    break;
                case 3:
                    targetFromat = BarcodeFormat.QR_CODE;
                    OneD = false;
                    if(!infoText.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) ||
                       !infoText.StartsWith( "https://", StringComparison.InvariantCultureIgnoreCase) )
                    {
                        infoText = "http://" + infoText;
                    }
                    break;
                case 4:
                    targetFromat = BarcodeFormat.QR_CODE;
                    OneD = false;
                    if ( !infoText.StartsWith( "tel:", StringComparison.InvariantCultureIgnoreCase ) )
                    {
                        infoText = "TEL:" + infoText;
                    }
                    break;
                case 5:
                    targetFromat = BarcodeFormat.QR_CODE;
                    OneD = false;
                    if ( !infoText.StartsWith( "mailto:", StringComparison.InvariantCultureIgnoreCase ) )
                    {
                        var mail = "abc@abc.com";
                        var subject = "main to ...";
                        infoText = $"MAILTO:{mail}?SUBJECT={subject}&BODY={infoText}";
                    }
                    break;
                case 6:
                    targetFromat = BarcodeFormat.QR_CODE;
                    OneD = false;
                    if ( !infoText.StartsWith( "smsto:", StringComparison.InvariantCultureIgnoreCase ) )
                    {
                        var phone = "1234567890";
                        infoText = $"SMSTO:{phone}:{infoText}";
                    }
                    break;
                case 7:
                    targetFromat = BarcodeFormat.QR_CODE;
                    OneD = false;
                    break;
                case 8:
                    targetFromat = BarcodeFormat.QR_CODE;
                    OneD = false;
                    break;
                default:
                    targetFromat = BarcodeFormat.CODE_128;
                    OneD = true;
                    break;
            }
            if(OneD)
            {
                infoText = infoText.Replace( "-", "" ).Replace( " ", "" );
                picQR.Image = BarEncode( infoText, targetFromat, isbn );
            }
            else
            {
                picQR.Image = QREncode( edText.Text );
            }
        }

        private void btnQRInput_ButtonClick( object sender, EventArgs e )
        {
            FormQRInput form = new FormQRInput();
            form.Icon = Icon;
            DialogResult dlgResult = form.ShowDialog();
            if(dlgResult == DialogResult.OK)
            {
                edText.Text = form.QRContent;
                edText.SelectionStart = edText.Text.Length;
                btnQREncode.PerformClick();
            }
            form.Close();
            form.Dispose();
        }

        private void picQR_DoubleClick( object sender, EventArgs e )
        {
            var timestamp = DateTime.Now.ToString( "yyyyMMddTHHmmsszz" );
            var errorlevel = cbErrorLevel.Text;
            picQR.Image.Save( $"QR_{timestamp}_{errorlevel}.png" );
        }

        private void chkOverLogo_CheckedChanged( object sender, EventArgs e )
        {
            if ( this.Visible )
            {
                appSection.Settings["OverlayLogo"].Value = chkDecodeFormatQR.Checked.ToString();
                config.Save();
            }
        }

        private void logoItems_DropDownItemClicked( object sender, ToolStripItemClickedEventArgs e )
        {
            overlayLogoImage = e.ClickedItem.Text;
            try
            {
                appSection.Settings["OverlayLogoImage"].Value = overlayLogoImage;
            }
            catch
            {
                appSection.Settings.Add( "OverlayLogoImage", overlayLogoImage );
            }
            config.Save();
        }

        private void logoItems_DropDownOpening( object sender, EventArgs e )
        {
            listIcons( $"{AppPath}icons" );
        }

        private void MainForm_DragOver( object sender, DragEventArgs e )
        {
            edText.EnableAutoDragDrop = false;
            edText.AllowDrop = false;
            if ( isAllowedDrag( e.Data ) )
            {
                if ( e.Data.GetDataPresent( DataFormats.FileDrop ) )
                {
                    string[] dragFiles = (string [])e.Data.GetData(DataFormats.FileDrop, true);
                    if ( dragFiles.Length > 0 )
                    {
                        List<string> exts = new List<string>(){ ".png", ".bmp", ".jpg", ".ico", ".gif" };
                        string dragFileName = dragFiles[0].ToString();
                        if ( exts.Contains( Path.GetExtension( dragFileName ).ToLower() ) )
                            e.Effect = DragDropEffects.Copy;
                        else
                            e.Effect = DragDropEffects.None;
                    }
                }
                else
                {
                    edText.AllowDrop = true;
                    edText.EnableAutoDragDrop = true;
                    e.Effect = DragDropEffects.Copy;
                }
            }
            return;
        }

        private void MainForm_DragDrop( object sender, DragEventArgs e )
        {
            // Determine whether string data exists in the drop data. If not, then 
            // the drop effect reflects that the drop cannot occur. 
            if ( e.Data.GetDataPresent( DataFormats.FileDrop ) )
            {
                //e.Effect = DragDropEffects.Copy;
                try
                {
                    string[] dragFiles = (string [])e.Data.GetData(DataFormats.FileDrop, true);
                    if ( dragFiles.Length > 0 )
                    {
                        List<string> exts = new List<string>(){ ".png", ".bmp", ".jpg", ".ico", ".gif" };
                        string dragFileName = dragFiles[0].ToString();
                        if ( exts.Contains( Path.GetExtension( dragFileName ).ToLower() ))
                        {
                            Bitmap qrImage = new Bitmap(dragFileName);
                            string qrText = QRDecode( qrImage, false );
                            qrImage.Dispose();
                            if ( !string.IsNullOrEmpty( qrText ) )
                            {
                                edText.Text = qrText;
                                picQR.Load( dragFileName );
                            }
                        }
                    }
                }
                catch
                {

                }
            }
            else if ( isAllowedDrag( e.Data ) )
            {
                edText.Text = e.Data.GetData( "System.String", true ).ToString();
            }
            return;
        }

    }
}
