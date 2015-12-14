using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NGettext.WinForm;

namespace QRUtils
{
    public partial class FormQRInput : Form
    {
        private string QRText = string.Empty;
        public string QRContent
        {
            get { return QRText; }
            set { QRText = value; }
        }

        public FormQRInput()
        {
            InitializeComponent();
            Application.EnableVisualStyles();
            this.Icon = Icon.ExtractAssociatedIcon( Application.ExecutablePath );

            I18N i10n = new I18N( null, this);

            if ( cbWifiNetwork.Items.Count > 0 )
                cbWifiNetwork.SelectedIndex = 0;
        }

        private void btnOK_Click( object sender, EventArgs e )
        {
            switch(tabsQR.SelectedIndex)
            {
                case 0: // Contact
                    //MECARD:N:-Name-;ORG:-Company-;TEL:1234567890;URL:Website.com;EMAIL:abc@abc.com;ADR:-Address- -Address 2-;NOTE:-Memo--Title-;;
                    // or
                    //BEGIN:VCARD
                    //VERSION:3.0
                    //N:-Name-
                    //ORG:-Company-
                    //TITLE:-Title-
                    //TEL:1234567890
                    //URL:Website.com
                    //EMAIL:abc@abc.com
                    //ADR:-Address- -Address 2-
                    //NOTE:-Memo-
                    //END:VCARD
                    QRText = "";
                    break;
                case 1: // Calendar
                    // All Day Event
                    //BEGIN:VEVENT
                    //SUMMARY:title
                    //DTSTART;VALUE=DATE:20151101
                    //DTEND;VALUE=DATE:20151201
                    //LOCATION:Location
                    //DESCRIPTION:Description
                    //END:VEVENT
                    // Single Event
                    //BEGIN:VEVENT
                    //SUMMARY:title
                    //DTSTART:20151101T104400Z
                    //DTEND:20151130T114400Z
                    //LOCATION:Location
                    //DESCRIPTION:Description
                    //END:VEVENT
                    break;
                case 2: // E-Mail mailto:abc.@abc.com
                        // MATMSG:TO:abc@de.com;SUB:title;BODY:contents;;
                    QRText = $"MAILTO:{edMailTo.Text.Trim()}";
                    break;
                case 3: // Geo    geo:-Latitude-,-Longitude-?q=-Query-
                    if(chkGeoMap.Checked)
                    {
                        var rb = grpGeoMap.Controls.OfType<RadioButton>().FirstOrDefault( r => r.Checked );

                    }
                    else
                    {
                        var query = string.IsNullOrEmpty(edGeoQuery.Text) ? string.Empty : $"?q={edGeoQuery.Text}";
                        QRText = $"GEO:{edGeoLat.Text.Trim()},{edGeoLon.Text.Trim()}{query}";
                    }
                    break;
                case 4: // Phone  tel:-Phone-
                    QRText = $"TEL:{edPhone.Text.Trim()}";
                    break;
                case 5: // SMS    smsto:-Phone-:-Text-
                    QRText = $"SMSTO:{edSmsTo.Text.Trim()}:{edSmsText.Text.Trim()}";
                    break;
                case 6: // WIFI   WIFI:S:-SSID-;T:WPA;P:-PASS-;H:true;;
                    var net = string.Empty;
                    switch( cbWifiNetwork.SelectedIndex )
                    {
                        case 0: net = $"T:WPA;"; break;
                        case 1: net = $"T:WEP;"; break;
                    }
                    var hidden = (chkWifiHidden.Checked) ? "H:true" : "";
                    QRText = $"WIFI:S:{edWifiSSID.Text.Trim()};{net}P:{edWifiPass.Text};{hidden};";
                    break;
                case 7: // URL
                    QRText = edURL.Text.Trim();
                    if ( !QRText.StartsWith("http://") || 
                         !QRText.StartsWith( "https://" ) ||
                         !QRText.StartsWith( "ftp://" ) ||
                         !QRText.StartsWith( "ftps://" )
                         )
                    {
                        QRText = $"http://{QRText}";
                    }
                    break;
                default:
                    QRText = string.Empty;
                    break;
            }
            
        }
    }
}
