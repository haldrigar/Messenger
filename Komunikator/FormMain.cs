using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;
using Komunikator.Handlers;

namespace Komunikator
{
    public partial class FormMain : Form
    {
        [DllImport("user32.dll")]
        static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

        ChromiumWebBrowser _browserMessenger;
        private bool _browserMessengerInitialized;
        private bool _tabMessengerUnread;

        ChromiumWebBrowser _browserGg;
        private bool _browserGgInitialized;
        private bool _tabGgUnread;

        ChromiumWebBrowser _browserWhatsapp;
        private bool _browserWhatsappInitialized;
        private bool _tabWhatsappUnread;

        ChromiumWebBrowser _browserSkype;
        private bool _browserSkypeInitialized;
        private bool _tabSkypeUnread;

        ChromiumWebBrowser _browserSlackOpgk;
        private bool _browserSlackOpgkInitialized;
        private bool _tabSlackOpgkUnread;



        public FormMain()
        {
            InitializeChromium();

            InitializeComponent();
        }

        private void InitializeChromium()
        {
            CefSettings settings = new CefSettings
            {
                CachePath = AppDomain.CurrentDomain.BaseDirectory + @"\cache",
                Locale = "pl"
            };

            settings.CefCommandLineArgs.Add("enable-media-stream", "1");


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
            _browserMessenger.TitleChanged += OnBrowserMessengerTitleChanged;

            // --------------------------------------------------------------------------------------------------------------------------------------

            _browserGg = new ChromiumWebBrowser("https://gg.pl")
            {
                Dock = DockStyle.Fill,
                DownloadHandler = new DownloadHandler(),
                RequestHandler = new RequestHandler()
            };

            _browserGg.LoadingStateChanged += BrowserGg_LoadingStateChanged;
            _browserGg.TitleChanged += OnBrowserGgTitleChanged;

            // --------------------------------------------------------------------------------------------------------------------------------------

            _browserWhatsapp = new ChromiumWebBrowser("https://web.whatsapp.com")
            {
                Dock = DockStyle.Fill,
                DownloadHandler = new DownloadHandler(),
                RequestHandler = new RequestHandler()
            };

            _browserWhatsapp.LoadingStateChanged += BrowserWhatsapp_LoadingStateChanged;
            _browserWhatsapp.TitleChanged += OnBrowserWhatsappTitleChanged;

            // --------------------------------------------------------------------------------------------------------------------------------------

            _browserSkype = new ChromiumWebBrowser("https://preview.web.skype.com")
            {
                Dock = DockStyle.Fill,
                DownloadHandler = new DownloadHandler(),
                RequestHandler = new RequestHandler()
            };
            _browserSkype.LoadingStateChanged += BrowserSkype_LoadingStateChanged;
            _browserSkype.TitleChanged += OnBrowserSkypeTitleChanged;

            // --------------------------------------------------------------------------------------------------------------------------------------

            _browserSlackOpgk = new ChromiumWebBrowser("https://opgkgdansk.slack.com")
            {
                Dock = DockStyle.Fill,
                DownloadHandler = new DownloadHandler(),
                RequestHandler = new RequestHandler()
            };
            _browserSlackOpgk.LoadingStateChanged += BrowserSlackOpgk_LoadingStateChanged;
            _browserSlackOpgk.TitleChanged += OnBrowserSlackOpgkTitleChanged;
        }

        private void BrowserMessenger_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {

            if (e.IsLoading == false)
            {
                _browserMessengerInitialized = true;

                if (_browserGgInitialized == false)
                {
                    tabControl.InvokeOnUiThreadIfRequired(() => tabControl.SelectTab("tabPageGG"));
                }
            }
        }

        private void OnBrowserMessengerTitleChanged(object sender, TitleChangedEventArgs args)
        {
            if (!args.Title.Equals("Messenger") && _tabMessengerUnread == false && _browserMessengerInitialized && !tabPageMessenger.IsActiveControl())
            {
                _tabMessengerUnread = true;

                tabPageMessenger.InvokeOnUiThreadIfRequired(() => tabPageMessenger.Text = "Messenger (+)");

                this.InvokeOnUiThreadIfRequired(() => FlashWindow(Handle, true));

            }
        }

        private void BrowserGg_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading == false)
            {
                _browserGgInitialized = true;

                if (_browserWhatsappInitialized == false)
                {
                    tabControl.InvokeOnUiThreadIfRequired(() => tabControl.SelectTab("tabPageWhatsApp"));
                }
            }
        }

        private void OnBrowserGgTitleChanged(object sender, TitleChangedEventArgs args)
        {
            if (!args.Title.Equals("GG") && _tabGgUnread == false && _browserGgInitialized && !this.IsActiveControl())
            {
                _tabGgUnread = true;

                tabPageGG.InvokeOnUiThreadIfRequired(() => tabPageGG.Text = "GG (+)");

                this.InvokeOnUiThreadIfRequired(() => FlashWindow(Handle, true));
            }
        }

        private void BrowserWhatsapp_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading == false)
            {
                _browserWhatsappInitialized = true;

                if (_browserSkypeInitialized == false)
                {
                    tabControl.InvokeOnUiThreadIfRequired(() => tabControl.SelectTab("tabPageSkype"));
                }
            }
        }

        private void OnBrowserWhatsappTitleChanged(object sender, TitleChangedEventArgs args)
        {
            if (!args.Title.Equals("WhatsApp") && _tabWhatsappUnread == false && _browserWhatsappInitialized && !this.IsActiveControl())
            {
                _tabWhatsappUnread = true;

                tabPageWhatsApp.InvokeOnUiThreadIfRequired(() => tabPageWhatsApp.Text = "WhatsApp (+)");

                this.InvokeOnUiThreadIfRequired(() => FlashWindow(Handle, true));
            }
        }

        private void BrowserSkype_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading == false)
            {
                _browserSkypeInitialized = true;

                if (_browserSlackOpgkInitialized == false)
                {
                    tabControl.InvokeOnUiThreadIfRequired(() => tabControl.SelectTab("tabPageSlackOpgk"));
                }
            }
        }

        private void OnBrowserSkypeTitleChanged(object sender, TitleChangedEventArgs args)
        {
            if (!args.Title.Equals("Skype") && _tabSkypeUnread == false && _browserSkypeInitialized && !this.IsActiveControl())
            {
                _tabSkypeUnread = true;

                tabPageSkype.InvokeOnUiThreadIfRequired(() => tabPageSkype.Text = "Skype (+)");

                this.InvokeOnUiThreadIfRequired(() => FlashWindow(Handle, true));
            }
        }

        private void BrowserSlackOpgk_LoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading == false)
            {
                _browserSlackOpgkInitialized = true;

                if (_browserMessengerInitialized == false)
                {
                    tabControl.InvokeOnUiThreadIfRequired(() => tabControl.SelectTab("tabPageMesseneger"));
                }
            }
        }

        private void OnBrowserSlackOpgkTitleChanged(object sender, TitleChangedEventArgs args)
        {
            if (args.Title.StartsWith("!") && _tabSlackOpgkUnread == false && _browserSlackOpgkInitialized && !this.IsActiveControl())
            {
                _tabSlackOpgkUnread = true;

                tabPageSlackOpgk.InvokeOnUiThreadIfRequired(() => tabPageSlackOpgk.Text = "Slack OPGK (+)");

                this.InvokeOnUiThreadIfRequired(() => FlashWindow(Handle, true));
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            tabPageMessenger.Controls.Add(_browserMessenger);
            
            tabPageGG.Controls.Add(_browserGg);

            tabPageWhatsApp.Controls.Add(_browserWhatsapp);

            tabPageSkype.Controls.Add(_browserSkype);

            tabPageSlackOpgk.Controls.Add(_browserSlackOpgk);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void TabControl_Click(object sender, EventArgs e)
        {
            switch (tabControl.SelectedTab.Name)
            {
                case "tabPageMessenger":
                    tabPageMessenger.Text = "Messenger";
                    _tabMessengerUnread = false;
                    break;

                case "tabPageGG":
                    tabPageGG.Text = "GG";
                    _tabGgUnread = false;
                    break;

                case "tabPageWhatsApp":
                    tabPageWhatsApp.Text = "WhatsApp";
                    _tabWhatsappUnread = false;
                    break;

                case "tabPageSkype":
                    tabPageSkype.Text = "Skype";
                    _tabSkypeUnread = false;
                    break;

                case "tabPageSlackOpgk":
                    tabPageSlackOpgk.Text = "Slack OPGK";
                    _tabSlackOpgkUnread = false;
                    break;
            }
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            if (_browserMessengerInitialized && _browserGgInitialized && _browserWhatsappInitialized && _browserSkypeInitialized)
            {
                switch (tabControl.SelectedTab.Name)
                {
                    case "tabPageMessenger":
                        tabPageMessenger.Text = "Messenger";
                        _tabMessengerUnread = false;
                        break;

                    case "tabPageGG":
                        tabPageGG.Text = "GG";
                        _tabGgUnread = false;
                        break;

                    case "tabPageWhatsApp":
                        tabPageWhatsApp.Text = "WhatsApp";
                        _tabWhatsappUnread = false;
                        break;

                    case "tabPageSkype":
                        tabPageSkype.Text = "Skype";
                        _tabSkypeUnread = false;
                        break;

                    case "tabPageSlackOpgk":
                        tabPageSlackOpgk.Text = "Slack OPGK";
                        _tabSlackOpgkUnread = false;
                        break;
                }
            }
        }
    }
}
