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

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        private bool IsActive(IntPtr handle)
        {
            IntPtr activeHandle = GetForegroundWindow();
            return (activeHandle == handle);
        }

        ChromiumWebBrowser _messengerWebBrowser;
        private bool _messengerWebBrowserInitialized;
        private bool _messengerTabUnread;

        ChromiumWebBrowser _ggWebBrowser;
        private bool _ggWebBrowserInitialized;
        private bool _ggTabUnread;

        ChromiumWebBrowser _whatsAppWebBrowser;
        private bool _whatsAppWebBrowserInitialized;
        private bool _whatsAppTabUnread;

        ChromiumWebBrowser _skypeWebBrowser;
        private bool _skypeWebBrowserInitialized;
        private bool _skypeTabUnread;

        ChromiumWebBrowser _slackOpgkWebBrowser;
        private bool _slackOpgkWebBrowserInitialized;
        private bool _slackTabOpgkUnread;

        ChromiumWebBrowser _slackGisnetWebBrowser;
        private bool _slackGisnetWebBrowserInitialized;
        private bool _slackTabGisnetUnread;

        ChromiumWebBrowser _smsWebBrowser;
        private bool _smsWebBrowserInitialized;
        private bool _smsTabUnread;

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
                Locale = "pl",
                LogSeverity = LogSeverity.Warning
            };

            settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            settings.CefCommandLineArgs.Add("no-proxy-server", "1");
            settings.CefCommandLineArgs.Add("allow-running-insecure-content", "1");
            
            //settings.SetOffScreenRenderingBestPerformanceArgs();
 
            //settings.CefCommandLineArgs.Add("disable-gpu-vsync", "1");
            //settings.CefCommandLineArgs.Add("disable-direct-write", "1");


            //Cef.EnableHighDPISupport();


            Cef.Initialize(settings);

            // --------------------------------------------------------------------------------------------------------------------------------------

            _messengerWebBrowser = new ChromiumWebBrowser("https://www.messenger.com")
            {
                Name = "Messenger",
                Dock = DockStyle.Fill,
                DownloadHandler = new DownloadHandler(),
                RequestHandler = new RequestHandler()
            };

            _messengerWebBrowser.TitleChanged += OnMessengerWebBrowserTitleChanged;
            _messengerWebBrowser.LoadingStateChanged += OnBrowserLoadingStateChanged;

            // --------------------------------------------------------------------------------------------------------------------------------------

            _ggWebBrowser = new ChromiumWebBrowser("https://gg.pl")
            {
                Name = "GG",
                Dock = DockStyle.Fill,
                DownloadHandler = new DownloadHandler(),
                RequestHandler = new RequestHandler()
            };

            _ggWebBrowser.TitleChanged += OnGgWebBrowserTitleChanged;
            _ggWebBrowser.LoadingStateChanged += OnBrowserLoadingStateChanged;

            // --------------------------------------------------------------------------------------------------------------------------------------

            _whatsAppWebBrowser = new ChromiumWebBrowser("https://web.whatsapp.com")
            {
                Name = "WhatsApp",
                Dock = DockStyle.Fill,
                DownloadHandler = new DownloadHandler(),
                RequestHandler = new RequestHandler()
            };

            _whatsAppWebBrowser.TitleChanged += OnWhatsAppWebBrowserTitleChanged;
            _whatsAppWebBrowser.LoadingStateChanged += OnBrowserLoadingStateChanged;

            // --------------------------------------------------------------------------------------------------------------------------------------

            _skypeWebBrowser = new ChromiumWebBrowser("https://preview.web.skype.com")
            {
                Name = "Skype",
                Dock = DockStyle.Fill,
                DownloadHandler = new DownloadHandler(),
                RequestHandler = new RequestHandler()
            };

            _skypeWebBrowser.TitleChanged += OnSkypeWebBrowserTitleChanged;
            _skypeWebBrowser.LoadingStateChanged += OnBrowserLoadingStateChanged;

            // --------------------------------------------------------------------------------------------------------------------------------------

            _slackOpgkWebBrowser = new ChromiumWebBrowser("https://opgkgdansk.slack.com")
            {
                Name = "SlackOPGK",
                Dock = DockStyle.Fill,
                DownloadHandler = new DownloadHandler(),
                RequestHandler = new RequestHandler()
            };

            _slackOpgkWebBrowser.TitleChanged += OnSlackOpgkWebBrowserTitleChanged;
            _slackOpgkWebBrowser.LoadingStateChanged += OnBrowserLoadingStateChanged;

            // --------------------------------------------------------------------------------------------------------------------------------------

            _slackGisnetWebBrowser = new ChromiumWebBrowser("https://gisnetgda.slack.com")
            {
                Name = "SlackGISNET",
                Dock = DockStyle.Fill,
                DownloadHandler = new DownloadHandler(),
                RequestHandler = new RequestHandler()
            };

            _slackGisnetWebBrowser.TitleChanged += OnSlackGisnetWebBrowserTitleChanged;
            _slackGisnetWebBrowser.LoadingStateChanged += OnBrowserLoadingStateChanged;

            // --------------------------------------------------------------------------------------------------------------------------------------

            _smsWebBrowser = new ChromiumWebBrowser("https://messages.android.com")
            {
                Name = "SMS",
                Dock = DockStyle.Fill,
                DownloadHandler = new DownloadHandler(),
                RequestHandler = new RequestHandler()
            };

            _smsWebBrowser.TitleChanged += OnSmsWebBrowserTitleChanged;
            _smsWebBrowser.LoadingStateChanged += OnBrowserLoadingStateChanged;
        }

        private void OnBrowserLoadingStateChanged(object sender, LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading == false)
            {
                switch (((ChromiumWebBrowser)sender).Name)
                {
                    case "Messenger":
                        _messengerWebBrowserInitialized = true;
                        break;

                    case "GG":
                        _ggWebBrowserInitialized = true;
                        break;

                    case "WhatsApp":
                        _whatsAppWebBrowserInitialized = true;
                        break;
                    
                    case "Skype":
                        _skypeWebBrowserInitialized = true;
                        break;
                    
                    case "SlackOPGK":
                        _slackOpgkWebBrowserInitialized = true;
                        break;
                    
                    case "SlackGISNET":
                        _slackGisnetWebBrowserInitialized = true;
                        break;

                    case "SMS":
                        _smsWebBrowserInitialized = true;
                        break;                }
            }
        }

        private void OnMessengerWebBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            string activeTabName = (string)Invoke(new Func<string>(() => tabControl.SelectedTab.Name));
            IntPtr activeHandle = (IntPtr)Invoke(new Func<IntPtr>(() => Handle));

            if (!args.Title.Equals("Messenger") && _messengerTabUnread == false && _messengerWebBrowserInitialized)
            {
                if (IsActive(activeHandle) && activeTabName == "tabPageMessenger")
                {
                    _messengerTabUnread = false;

                    tabPageMessenger.InvokeOnUiThreadIfRequired(() => tabPageMessenger.Text = "Messenger");
                }
                else
                {
                    _messengerTabUnread = true;

                    tabPageMessenger.InvokeOnUiThreadIfRequired(() => tabPageMessenger.Text = "Messenger (+)");
                }
                
                this.InvokeOnUiThreadIfRequired(() => FlashWindow(activeHandle, true));
            }
        }

        private void OnGgWebBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            string activeTabName = (string)Invoke(new Func<string>(() => tabControl.SelectedTab.Name));
            IntPtr activeHandle = (IntPtr)Invoke(new Func<IntPtr>(() => Handle));

            if (!args.Title.Equals("GG") && _ggTabUnread == false && _ggWebBrowserInitialized)
            {
                if (IsActive(activeHandle) && activeTabName == "tabPageGG")
                {
                    _ggTabUnread = false;

                    tabPageGG.InvokeOnUiThreadIfRequired(() => tabPageGG.Text = "GG");
                }
                else
                {
                    _ggTabUnread = true;

                    tabPageGG.InvokeOnUiThreadIfRequired(() => tabPageGG.Text = "GG (+)");
                }
                 
                this.InvokeOnUiThreadIfRequired(() => FlashWindow(activeHandle, true));
            }
        }

        private void OnWhatsAppWebBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            string activeTabName = (string)Invoke(new Func<string>(() => tabControl.SelectedTab.Name));
            IntPtr activeHandle = (IntPtr)Invoke(new Func<IntPtr>(() => Handle));

            if (!args.Title.Equals("WhatsApp") && _whatsAppTabUnread == false && _whatsAppWebBrowserInitialized)
            {
                if (IsActive(activeHandle) && activeTabName == "tabPageWhatsApp")
                {
                    _whatsAppTabUnread = false;

                    tabPageWhatsApp.InvokeOnUiThreadIfRequired(() => tabPageWhatsApp.Text = "WhatsApp");
                }
                else
                {
                    _whatsAppTabUnread = true;

                    tabPageWhatsApp.InvokeOnUiThreadIfRequired(() => tabPageWhatsApp.Text = "WhatsApp (+)");
                }

                this.InvokeOnUiThreadIfRequired(() => FlashWindow(activeHandle, true));
            }
        }

        private void OnSkypeWebBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            if (!args.Title.Equals("Skype") && _skypeTabUnread == false && _skypeWebBrowserInitialized)
            {
                string activeTabName = (string)Invoke(new Func<string>(() => tabControl.SelectedTab.Name));
                IntPtr activeHandle = (IntPtr)Invoke(new Func<IntPtr>(() => Handle));

                if (IsActive(activeHandle) && activeTabName == "tabPageSkype")
                {
                    _skypeTabUnread = false;

                    tabPageSkype.InvokeOnUiThreadIfRequired(() => tabPageSkype.Text = "Skype");
                }
                else    
                {
                    _skypeTabUnread = true;

                    tabPageSkype.InvokeOnUiThreadIfRequired(() => tabPageSkype.Text = "Skype (+)");
                }

                this.InvokeOnUiThreadIfRequired(() => FlashWindow(activeHandle, true));
            }
        }

        private void OnSlackOpgkWebBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            string activeTabName = (string)Invoke(new Func<string>(() => tabControl.SelectedTab.Name));
            IntPtr activeHandle = (IntPtr)Invoke(new Func<IntPtr>(() => Handle));

            if (args.Title.StartsWith("!") && _slackTabOpgkUnread == false && _slackOpgkWebBrowserInitialized)
            {
                if (IsActive(activeHandle) && activeTabName == "tabPageSlackOPGK")
                {
                    _slackTabOpgkUnread = false;

                    tabPageSlackOPGK.InvokeOnUiThreadIfRequired(() => tabPageSlackOPGK.Text = "Slack OPGK");
                }
                else
                {
                    _slackTabOpgkUnread = true;

                    tabPageSlackOPGK.InvokeOnUiThreadIfRequired(() => tabPageSlackOPGK.Text = "Slack OPGK (+)");
                }
               
                this.InvokeOnUiThreadIfRequired(() => FlashWindow(activeHandle, true));
            }
        }

        private void OnSlackGisnetWebBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            string activeTabName = (string)Invoke(new Func<string>(() => tabControl.SelectedTab.Name));
            IntPtr activeHandle = (IntPtr)Invoke(new Func<IntPtr>(() => Handle));

            if (args.Title.StartsWith("!") && _slackTabGisnetUnread == false && _slackGisnetWebBrowserInitialized)
            {
                if (IsActive(activeHandle) && activeTabName == "tabPageSlackGISNET")
                {
                    _slackTabGisnetUnread = false;

                    tabPageSlackGISNET.InvokeOnUiThreadIfRequired(() => tabPageSlackGISNET.Text = "Slack GISNET");
                }
                else
                {
                    _slackTabGisnetUnread = true;

                    tabPageSlackGISNET.InvokeOnUiThreadIfRequired(() => tabPageSlackGISNET.Text = "Slack GISNET (+)");
                }
               
                this.InvokeOnUiThreadIfRequired(() => FlashWindow(activeHandle, true));
            }
        }

        private void OnSmsWebBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            string activeTabName = (string)Invoke(new Func<string>(() => tabControl.SelectedTab.Name));
            IntPtr activeHandle = (IntPtr)Invoke(new Func<IntPtr>(() => Handle));

            if (args.Title.Contains("(") && _smsTabUnread == false && _smsWebBrowserInitialized)
            {
                if (IsActive(activeHandle) && activeTabName == "tabPageSMS")
                {
                    _smsTabUnread = false;

                    tabPageSMS.InvokeOnUiThreadIfRequired(() => tabPageSMS.Text = "SMS");
                }
                else
                {
                    _smsTabUnread = true;

                    tabPageSMS.InvokeOnUiThreadIfRequired(() => tabPageSMS.Text = "SMS (+)");
                }
               
                this.InvokeOnUiThreadIfRequired(() => FlashWindow(activeHandle, true));
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            tabPageMessenger.Controls.Add(_messengerWebBrowser);
            tabControl.SelectTab("tabPageMessenger");

            tabPageGG.Controls.Add(_ggWebBrowser);
            tabControl.SelectTab("tabPageGG");
            
            tabPageWhatsApp.Controls.Add(_whatsAppWebBrowser);
            tabControl.SelectTab("tabPageWhatsApp");

            tabPageSkype.Controls.Add(_skypeWebBrowser);
            tabControl.SelectTab("tabPageSkype");

            tabPageSlackOPGK.Controls.Add(_slackOpgkWebBrowser);
            tabControl.SelectTab("tabPageSlackOPGK");

            tabPageSlackGISNET.Controls.Add(_slackGisnetWebBrowser);
            tabControl.SelectTab("tabPageSlackGISNET");

            tabPageSMS.Controls.Add(_smsWebBrowser);
            tabControl.SelectTab("tabPageSMS");

            tabControl.SelectTab(Properties.Settings.Default.LastTab);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();

            string activeTabName = (string)Invoke(new Func<string>(() => tabControl.SelectedTab.Name));

            Properties.Settings.Default.LastTab = activeTabName;
            Properties.Settings.Default.Save();
        }
        
        private void ResetStatus(object sender, EventArgs e)
        {
            switch (tabControl.SelectedTab.Name)
            {
                case "tabPageMessenger":
                    tabPageMessenger.Text = "Messenger";
                    _messengerTabUnread = false;
                    break;

                case "tabPageGG":
                    tabPageGG.Text = "GG";
                    _ggTabUnread = false;
                    break;

                case "tabPageWhatsApp":
                    tabPageWhatsApp.Text = "WhatsApp";
                    _whatsAppTabUnread = false;
                    break;

                case "tabPageSkype":
                    tabPageSkype.Text = "Skype";
                    _skypeTabUnread = false;
                    break;

                case "tabPageSlackOPGK":
                    tabPageSlackOPGK.Text = "Slack OPGK";
                    _slackTabOpgkUnread = false;
                    break;

                case "tabPageSlackGISNET":
                    tabPageSlackGISNET.Text = "Slack GISNET";
                    _slackTabGisnetUnread = false;
                    break;

                case "tabPageSMS":
                    tabPageSMS.Text = "SMS";
                    _smsTabUnread = false;
                    break;
            }
        }
    }
}
