using System;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Komunikator.Handlers;

namespace Komunikator
{
    public partial class FormMain : Form
    {
        readonly ChromiumWebBrowser _browserMessenger;
        private bool _browserMessengerInitialized = false;

        readonly ChromiumWebBrowser _browserGg;
        private bool _browserGgInitialized = false;

        readonly ChromiumWebBrowser _browserWhatsapp;
        private bool _browserWhatsappInitialized = false;

        public FormMain()
        {

            CefSettings settings = new CefSettings
            {
                CachePath = AppDomain.CurrentDomain.BaseDirectory + @"\cache",
                Locale = "pl"
            };

            Cef.EnableHighDPISupport();

            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

            // --------------------------------------------------------------------------------------------------------------------------------------

            _browserMessenger = new ChromiumWebBrowser("https://www.messenger.com")
            {
                Dock = DockStyle.Fill,
                DownloadHandler = new DownloadHandler(),
                RequestHandler = new RequestHandler()
            };
            _browserMessenger.LoadingStateChanged += BrowserMessenger_LoadingStateChanged;

            // --------------------------------------------------------------------------------------------------------------------------------------

            _browserGg = new ChromiumWebBrowser("https://gg.pl")
            {
                Dock = DockStyle.Fill,
                DownloadHandler = new DownloadHandler(),
                RequestHandler = new RequestHandler()
            };
            _browserGg.LoadingStateChanged += BrowserGg_LoadingStateChanged;

            // --------------------------------------------------------------------------------------------------------------------------------------

            _browserWhatsapp = new ChromiumWebBrowser("https://web.whatsapp.com")
            {
                Dock = DockStyle.Fill,
                DownloadHandler = new DownloadHandler(),
                RequestHandler = new RequestHandler()
            };
            _browserWhatsapp.LoadingStateChanged += BrowserWhatsapp_LoadingStateChanged;

            // --------------------------------------------------------------------------------------------------------------------------------------

            InitializeComponent();
        }

        private void BrowserMessenger_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {

            if (e.IsLoading == false)
            {
                _browserMessengerInitialized = true;

                if (_browserGgInitialized == false)
                {
                    tabControl.Invoke((MethodInvoker) (() => tabControl.SelectTab("tabPageGG")));
                }
            }
        }

        private void BrowserGg_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading == false)
            {
                _browserGgInitialized = true;

                if (_browserWhatsappInitialized == false)
                {
                    tabControl.Invoke((MethodInvoker) (() => tabControl.SelectTab("tabPageWhatsApp")));
                }
            }
        }

        private void BrowserWhatsapp_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading == false)
            {
                _browserWhatsappInitialized = true;

                if (_browserMessengerInitialized == false)
                {
                    tabControl.Invoke((MethodInvoker) (() => tabControl.SelectTab("tabPageMessenger")));
                }
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            tabPageMessenger.Controls.Add(_browserMessenger);
            
            tabPageGG.Controls.Add(_browserGg);

            tabPageWhatsApp.Controls.Add(_browserWhatsapp);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
