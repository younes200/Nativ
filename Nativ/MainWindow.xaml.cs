using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using CefSharp;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace InteractiveObject.Nativ
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IRequestHandler
    {

        #region Angle

        /// <summary>
        /// ScreenOwner Dependency Property
        /// </summary>
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register("Angle", typeof(double), typeof(Window),
                new FrameworkPropertyMetadata(0.0));

        /// <summary>
        /// Gets or sets the ScreenOwner property.  This dependency property 
        /// indicates the index of the screen on which the window is displayed.
        /// </summary>
        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }


        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WebView.RequestHandler = this;
            this.WebView.Loaded += new RoutedEventHandler(OnWebViewLoaded);
            NativAPI.Instance.FlipEvent += new EventHandler(Instance_FlipEvent);

            this.WebView.RegisterJsObject("nativ", NativAPI.Instance);
            
            Maximize();
        }


        /// <summary>
        /// Handles the FlipEvent event of the Instance control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void Instance_FlipEvent(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                Angle = (Angle == 0) ? 180 : 0;
            }));
        }

        /// <summary>
        /// Called when [web view loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        void OnWebViewLoaded(object sender, RoutedEventArgs e)
        {
            this.WebView.ContentsWidth = (int)Width;
            this.WebView.ContentsHeight = (int)Height;

            this.WebView.Load("file:///" + System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "/webapp/index.html");
        }

        #endregion

        #region Internal
        /// <summary>
        /// Maximizes this instance.
        /// </summary>
        private void Maximize()
        {

            System.Drawing.Rectangle bounds = Screen.AllScreens[0].Bounds;


            Width = bounds.Width;
            Height = bounds.Height;


            Left = bounds.Left;
            Top = bounds.Top;

            BorderThickness = new Thickness(0);
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            WindowState = WindowState.Normal;


        }
        #endregion


        #region IRequestHandler

        public bool GetAuthCredentials(IWebBrowser browser, bool isProxy, string host, int port, string realm, string scheme, ref string username, ref string password)
        {
            throw new NotImplementedException();
        }

        public bool GetDownloadHandler(IWebBrowser browser, string mimeType, string fileName, long contentLength, ref IDownloadHandler handler)
        {
            throw new NotImplementedException();
        }

        public bool OnBeforeBrowse(IWebBrowser browser, IRequest request, NavigationType naigationvType, bool isRedirect)
        {
            return false;
        }

        public bool OnBeforeResourceLoad(IWebBrowser browser, IRequestResponse requestResponse)
        {
            //IRequest request = requestResponse.Request;
            //if (request.Url.EndsWith("test.jpg"))
            //{
            //    string resourceName = "InteractiveObject.webapp.test.jpg";
            //    Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
                
            //        if (stream != null)
            //        {
            //            requestResponse.RespondWith(stream, "image/jpg");
            //        }
                
               
            //}
            return false;
        }

        public void OnResourceResponse(IWebBrowser browser, string url, int status, string statusText, string mimeType, System.Net.WebHeaderCollection headers)
        {
            
        }

        #endregion

    }
}
