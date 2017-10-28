using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;

namespace QRUtils
{
    class QRSimpleHTTPServer
    {
        //
        // Code source from gist page:
        // https://gist.github.com/aksakalli/9191056
        //
        private static string AppPath = AppDomain.CurrentDomain.BaseDirectory;

        private readonly string[] _indexFiles = 
        {
            "index.html",
            "index.htm",
            "default.html",
            "default.htm"
        };

        private static IDictionary<string, string> _mimeTypeMappings = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            #region extension to MIME type list
            {".apk", "application/octet-stream"},
            {".asf", "video/x-ms-asf"},
            {".asx", "video/x-ms-asf"},
            {".avi", "video/x-msvideo"},
            {".bin", "application/octet-stream"},
            {".cco", "application/x-cocoa"},
            {".crt", "application/x-x509-ca-cert"},
            {".css", "text/css"},
            {".deb", "application/octet-stream"},
            {".der", "application/x-x509-ca-cert"},
            {".dll", "application/octet-stream"},
            {".dmg", "application/octet-stream"},
            {".ear", "application/java-archive"},
            {".eot", "application/octet-stream"},
            {".exe", "application/octet-stream"},
            {".flv", "video/x-flv"},
            {".gif", "image/gif"},
            {".hqx", "application/mac-binhex40"},
            {".htc", "text/x-component"},
            {".htm", "text/html"},
            {".html", "text/html"},
            {".ico", "image/x-icon"},
            {".img", "application/octet-stream"},
            {".iso", "application/octet-stream"},
            {".jar", "application/java-archive"},
            {".jardiff", "application/x-java-archive-diff"},
            {".jng", "image/x-jng"},
            {".jnlp", "application/x-java-jnlp-file"},
            {".jpeg", "image/jpeg"},
            {".jpg", "image/jpeg"},
            {".js", "application/x-javascript"},
            {".mml", "text/mathml"},
            {".mng", "video/x-mng"},
            {".mov", "video/quicktime"},
            {".mp3", "audio/mpeg"},
            {".mpeg", "video/mpeg"},
            {".mpg", "video/mpeg"},
            {".msi", "application/octet-stream"},
            {".msm", "application/octet-stream"},
            {".msp", "application/octet-stream"},
            {".pdb", "application/x-pilot"},
            {".pdf", "application/pdf"},
            {".pem", "application/x-x509-ca-cert"},
            {".pl", "application/x-perl"},
            {".pm", "application/x-perl"},
            {".png", "image/png"},
            {".prc", "application/x-pilot"},
            {".ra", "audio/x-realaudio"},
            {".rar", "application/x-rar-compressed"},
            {".rpm", "application/x-redhat-package-manager"},
            {".rss", "text/xml"},
            {".run", "application/x-makeself"},
            {".sea", "application/x-sea"},
            {".shtml", "text/html"},
            {".sit", "application/x-stuffit"},
            {".swf", "application/x-shockwave-flash"},
            {".tcl", "application/x-tcl"},
            {".tk", "application/x-tcl"},
            {".txt", "text/plain"},
            {".war", "application/java-archive"},
            {".wbmp", "image/vnd.wap.wbmp"},
            {".wmv", "video/x-ms-wmv"},
            {".xml", "text/xml"},
            {".xpi", "application/x-xpinstall"},
            {".zip", "application/zip"},
            #endregion
        };

        private Thread _serverThread;
        private string _rootDirectory;
        private HttpListener _listener;

        private int _port;
        public int Port
        {
            get { return _port; }
            private set { }
        }

        //private string defaultURI = "http://localhost:8081/8657/";
        private int defaultPort = 8081;

        /// <summary>
        /// Construct server with given port.
        /// </summary>
        /// <param name="path">Directory path to serve.</param>
        /// <param name="port">Port of the server.</param>
        public QRSimpleHTTPServer( string path, int port )
        {
            this.Initialize( path, port );
        }

        /// <summary>
        /// Construct server with suitable port.
        /// </summary>
        /// <param name="path">Directory path to serve.</param>
        public QRSimpleHTTPServer( string path )
        {
            //get an empty port
            TcpListener l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            int port = ((IPEndPoint)l.LocalEndpoint).Port;
            l.Stop();
            this.Initialize( path, port );
        }

        /// <summary>
        /// Construct server with suitable port.
        /// </summary>
        public QRSimpleHTTPServer()
        {
            string addr = GetLocalIPAddress();
            if(!string.IsNullOrEmpty(addr))
            {
                string path = AppDomain.CurrentDomain.BaseDirectory;
                this.Initialize( path, defaultPort );
            }
        }

        /// <summary>
        /// Get Local IP address for display & listen
        /// </summary>
        private string GetLocalIPAddress()
        {
            if ( !System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() )
            {
                return null;
            }

            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach ( var ip in host.AddressList )
            {
                if ( ip.AddressFamily == AddressFamily.InterNetwork )
                {
                    return ip.ToString();
                }
            }
            throw new Exception( "Local IP Address Not Found!" );
        }

        /// <summary>
        /// Stop server and dispose all functions.
        /// </summary>
        public void Stop()
        {
            _serverThread.Abort();
            _listener.Stop();
        }

        private void Listen()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add( "http://*:" + _port.ToString() + "/" );
            _listener.Prefixes.Add( "http://*:" + _port.ToString() + "/8657/" );
            _listener.Start();
            while ( true )
            {
                try
                {
                    HttpListenerContext context = _listener.GetContext();
                    Process( context );
                }
                catch ( Exception ex )
                {

                }
            }
        }

        private void Process( HttpListenerContext context )
        {
            string filename = context.Request.Url.AbsolutePath;
            Console.WriteLine( filename );
            filename = filename.Substring( 1 );

            if ( string.IsNullOrEmpty( filename ) )
            {
                foreach ( string indexFile in _indexFiles )
                {
                    if ( File.Exists( Path.Combine( _rootDirectory, indexFile ) ) )
                    {
                        filename = indexFile;
                        break;
                    }
                }
            }

            if(filename.Trim( new Char[] { ' ', '*', '/', '\\' } ) == "8657")
            {
                sendFileListPage(context);
            }
            else
            {
                filename = Path.Combine( _rootDirectory, filename );

                if ( File.Exists( filename ) )
                {
                    try
                    {
                        Stream input = new FileStream(filename, FileMode.Open);

                        //Adding permanent http response headers
                        string mime;
                        context.Response.ContentType = _mimeTypeMappings.TryGetValue( Path.GetExtension( filename ), out mime ) ? mime : "application/octet-stream";
                        context.Response.ContentLength64 = input.Length;
                        context.Response.AddHeader( "Date", DateTime.Now.ToString( "r" ) );
                        context.Response.AddHeader( "Last-Modified", File.GetLastWriteTime( filename ).ToString( "r" ) );

                        byte[] buffer = new byte[1024 * 16];
                        int nbytes;
                        while ( ( nbytes = input.Read( buffer, 0, buffer.Length ) ) > 0 )
                            context.Response.OutputStream.Write( buffer, 0, nbytes );
                        input.Close();
                        context.Response.OutputStream.Flush();

                        context.Response.StatusCode = (int) HttpStatusCode.OK;
                    }
                    catch ( Exception ex )
                    {
                        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                    }
                }
                else
                {
                    context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                }
            }
            context.Response.OutputStream.Close();
        }

        public Stream GenerateStreamFromString( string s )
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write( s );
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        private void sendFileListPage( HttpListenerContext context )
        {
            try
            {
                string title = "Windows PC: /8657/";
                List<string> filelist = new List<string>() { "test.apk" };
                string content = htmlHeader(title) + htmlItem(filelist) + htmlFooter();
                using ( Stream input = GenerateStreamFromString( content ) )
                {
                    // ... Do stuff to stream
                    //Stream input = new MemoryStream();

                    //Adding permanent http response headers
                    context.Response.ContentType = "text/html";
                    context.Response.ContentLength64 = input.Length;
                    context.Response.AddHeader( "Date", DateTime.Now.ToString( "r" ) );
                    context.Response.AddHeader( "Last-Modified", DateTime.Now.ToString( "r" ) );

                    byte[] buffer = new byte[1024 * 16];
                    int nbytes;
                    while ( ( nbytes = input.Read( buffer, 0, buffer.Length ) ) > 0 )
                        context.Response.OutputStream.Write( buffer, 0, nbytes );
                    input.Close();
                    context.Response.OutputStream.Flush();
                }
                context.Response.StatusCode = (int) HttpStatusCode.OK;
            }
            catch ( Exception ex )
            {
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            }
        }

        private string loadTemplate( string templateName )
        {
            string content = "";
            string templateFile = $"{AppPath}\\template\\{templateName}";
            try
            {   // Open the text file using a stream reader.
                using ( StreamReader sr = new StreamReader( templateFile ) )
                {
                    // Read the stream to a string, and write the string to the console.
                    //string line = sr.ReadToEnd();
                    //Console.WriteLine( line );
                    content = sr.ReadToEnd();
                }
            }
            catch ( Exception e )
            {
                content = "";
                //Console.WriteLine( "The file could not be read:" );
                //Console.WriteLine( e.Message );
            }
            return ( content );
        }

        private string replaceTemplate( string templateFile, Dictionary<string, string> vars )
        {
            string template = loadTemplate(templateFile);

            foreach ( KeyValuePair<string, string> pair in vars )
            {
                Regex rgx = new Regex($@"(?i)\{{\{{\s*({pair.Key})\s*\}}\}}");
                template = rgx.Replace( template, pair.Value );
            }
            return ( template );

        }

        private string htmlHeader(string title)
        {
            Dictionary<string, string> vars = new Dictionary<string, string>();

            vars.Add( "title", title );
            string content = replaceTemplate("header.html", vars);

            return ( content );
        }

        private string htmlItem( List<string> filelist )
        {
            Dictionary<string, string> vars = new Dictionary<string, string>();

            string content = "";
            string filesize = "";
            string filetime = "";


            foreach ( string file in filelist )
            {
                if( File.Exists(file))
                {
                    string filepath = string.IsNullOrEmpty(Path.GetDirectoryName(file)) ? "" : Path.GetDirectoryName(file);
                    string filename = Path.GetFileName(file);
                    filesize = new FileInfo( file ).Length.ToString();
                    filetime = File.GetLastAccessTime( file ).ToString();
                    // 40k 15/12/5 下午5:23
                    filesize = "40K";
                    filetime = "15/12/5 下午5:23";
                    vars.Add( "filepath", filepath );
                    vars.Add( "filename", filename );
                    vars.Add( "filesize", filesize );
                    vars.Add( "filetime", filetime );
                    content += replaceTemplate( "item.html", vars );
                }
            }

            return ( content );
        }

        private string htmlFooter()
        {
            string content = loadTemplate("footer.html"); ;
            return ( content );
        }

        private void Initialize( string path, int port )
        {
            this._rootDirectory = path;
            this._port = port;
            _serverThread = new Thread( this.Listen );
            _serverThread.Start();
        }

    }
}

