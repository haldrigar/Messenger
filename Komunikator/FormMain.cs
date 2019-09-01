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

        // Define the Win32 API methods we are going to use
        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        private static extern bool InsertMenu(IntPtr hMenu, int wPosition, int wFlags, int wIdNewItem, string lpNewItem);

        /// Define our Constants we will use
        private const int WmSyscommand = 0x112;
        private const int MfSeparator = 0x800;
        private const int MfByposition = 0x400;

        // The constants we'll use to identify our custom system menu items
        private const int RefreshSysMenuId = 1000;
        private const int AboutSysMenuId = 1001;

        private static bool IsActive(IntPtr handle)
        {
            IntPtr activeHandle = GetForegroundWindow();
            return (activeHandle == handle);
        }

        private ChromiumWebBrowser _messengerWebBrowser;
        private bool _messengerWebBrowserInitialized;
        private bool _messengerTabUnread;

        private ChromiumWebBrowser _ggWebBrowser;
        private bool _ggWebBrowserInitialized;
        private bool _ggTabUnread;

        private ChromiumWebBrowser _whatsAppWebBrowser;
        private bool _whatsAppWebBrowserInitialized;
        private bool _whatsAppTabUnread;

        private ChromiumWebBrowser _telegramWebBrowser;
        private bool _telegramWebBrowserInitialized;
        private bool _telegramTabUnread;

        private ChromiumWebBrowser _skypeWebBrowser;
        private bool _skypeWebBrowserInitialized;
        private bool _skypeTabUnread;

        private ChromiumWebBrowser _slackOpgkWebBrowser;
        private bool _slackOpgkWebBrowserInitialized;
        private bool _slackTabOpgkUnread;

        private ChromiumWebBrowser _slackGisnetWebBrowser;
        private bool _slackGisnetWebBrowserInitialized;
        private bool _slackTabGisnetUnread;

        private bool _iconBlink;

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

            Cef.EnableHighDPISupport();

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
            _messengerWebBrowser.FrameLoadEnd += OnWebBrowserFrameLoadEnd;

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
            _ggWebBrowser.FrameLoadEnd += OnWebBrowserFrameLoadEnd;

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
            _whatsAppWebBrowser.FrameLoadEnd += OnWebBrowserFrameLoadEnd;

            // --------------------------------------------------------------------------------------------------------------------------------------

            _telegramWebBrowser = new ChromiumWebBrowser("https://web.telegram.org")
            {
                Name = "Telegram",
                Dock = DockStyle.Fill,
                DownloadHandler = new DownloadHandler(),
                RequestHandler = new RequestHandler()
            };

            _telegramWebBrowser.TitleChanged += OnTelegramWebBrowserTitleChanged;
            _telegramWebBrowser.LoadingStateChanged += OnBrowserLoadingStateChanged;
            _telegramWebBrowser.FrameLoadEnd += OnWebBrowserFrameLoadEnd;

            // --------------------------------------------------------------------------------------------------------------------------------------

            _skypeWebBrowser = new ChromiumWebBrowser("https://web.skype.com")
            {
                Name = "Skype",
                Dock = DockStyle.Fill,
                DownloadHandler = new DownloadHandler(),
                RequestHandler = new RequestHandler()
            };

            _skypeWebBrowser.TitleChanged += OnSkypeWebBrowserTitleChanged;
            _skypeWebBrowser.LoadingStateChanged += OnBrowserLoadingStateChanged;
            _skypeWebBrowser.FrameLoadEnd += OnWebBrowserFrameLoadEnd;

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
            _slackOpgkWebBrowser.FrameLoadEnd += OnWebBrowserFrameLoadEnd;

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
            _slackGisnetWebBrowser.FrameLoadEnd += OnWebBrowserFrameLoadEnd;
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

                    case "Telegram":
                        _telegramWebBrowserInitialized = true;
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
                }
            }
        }

        private void OnWebBrowserFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (e.Frame.IsMain)
            {
                e.Browser.MainFrame.ExecuteJavaScriptAsync("document.body.style.overflow = 'hidden'");
            }
        }

        private void OnMessengerWebBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            string activeTabName = (string)Invoke(new Func<string>(() => tabControl.SelectedTab.Name));
            IntPtr activeHandle = (IntPtr)Invoke(new Func<IntPtr>(() => Handle));

            if (!args.Title.Equals("Messenger") && _messengerTabUnread == false && _messengerWebBrowserInitialized)
            {
                notifyIcon.BalloonTipTitle = @"Messenger";
                notifyIcon.BalloonTipText = args.Title;
                notifyIcon.ShowBalloonTip(5000);  

                if (IsActive(activeHandle) && activeTabName == "tabPageMessenger")
                {
                    _messengerTabUnread = false;

                    tabPageMessenger.InvokeOnUiThreadIfRequired(() => tabPageMessenger.Text = @"Messenger");

                    timer.Stop();
                    notifyIcon.Icon = Resources.chat_480;
                }
                else
                {
                    _messengerTabUnread = true;

                    tabPageMessenger.InvokeOnUiThreadIfRequired(() => tabPageMessenger.Text = @"Messenger (+)");

                    timer.Start();    
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
                notifyIcon.BalloonTipTitle = @"GG";
                notifyIcon.BalloonTipText = args.Title;
                notifyIcon.ShowBalloonTip(5000); 

                if (IsActive(activeHandle) && activeTabName == "tabPageGG")
                {
                    _ggTabUnread = false;

                    tabPageGG.InvokeOnUiThreadIfRequired(() => tabPageGG.Text = @"GG");

                    timer.Stop();
                    notifyIcon.Icon = Resources.chat_480;
                }
                else
                {
                    _ggTabUnread = true;

                    tabPageGG.InvokeOnUiThreadIfRequired(() => tabPageGG.Text = @"GG (+)");

                    timer.Start();    
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
                notifyIcon.BalloonTipTitle = @"WhatsApp";
                notifyIcon.BalloonTipText = args.Title;
                notifyIcon.ShowBalloonTip(5000); 

                if (IsActive(activeHandle) && activeTabName == "tabPageWhatsApp")
                {
                    _whatsAppTabUnread = false;

                    tabPageWhatsApp.InvokeOnUiThreadIfRequired(() => tabPageWhatsApp.Text = @"WhatsApp");

                    timer.Stop();
                    notifyIcon.Icon = Resources.chat_480;
                }
                else
                {
                    _whatsAppTabUnread = true;

                    tabPageWhatsApp.InvokeOnUiThreadIfRequired(() => tabPageWhatsApp.Text = @"WhatsApp (+)");

                    timer.Start();    
                }

                this.InvokeOnUiThreadIfRequired(() => FlashWindow(activeHandle, true));
            }
        }

        private void OnTelegramWebBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            string activeTabName = (string)Invoke(new Func<string>(() => tabControl.SelectedTab.Name));
            IntPtr activeHandle = (IntPtr)Invoke(new Func<IntPtr>(() => Handle));

            if (!args.Title.Equals("Telegram Web") && _telegramTabUnread == false && _telegramWebBrowserInitialized)
            {
                notifyIcon.BalloonTipTitle = @"Telegram";
                notifyIcon.BalloonTipText = args.Title;
                notifyIcon.ShowBalloonTip(5000); 

                if (IsActive(activeHandle) && activeTabName == "tabPageTelegram")
                {
                    _telegramTabUnread = false;

                    tabPageTelegram.InvokeOnUiThreadIfRequired(() => tabPageTelegram.Text = @"Telegram");

                    timer.Stop();
                    notifyIcon.Icon = Resources.chat_480;
                }
                else
                {
                    _telegramTabUnread = true;

                    tabPageTelegram.InvokeOnUiThreadIfRequired(() => tabPageTelegram.Text = @"Telegram (+)");

                    timer.Start();    
                }

                this.InvokeOnUiThreadIfRequired(() => FlashWindow(activeHandle, true));
            }
        }

        private void OnSkypeWebBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            if (!args.Title.Equals("Skype") && _skypeTabUnread == false && _skypeWebBrowserInitialized)
            {
                notifyIcon.BalloonTipTitle = @"Skype";
                notifyIcon.BalloonTipText = args.Title;
                notifyIcon.ShowBalloonTip(5000); 

                string activeTabName = (string)Invoke(new Func<string>(() => tabControl.SelectedTab.Name));
                IntPtr activeHandle = (IntPtr)Invoke(new Func<IntPtr>(() => Handle));

                if (IsActive(activeHandle) && activeTabName == "tabPageSkype")
                {
                    _skypeTabUnread = false;

                    tabPageSkype.InvokeOnUiThreadIfRequired(() => tabPageSkype.Text = @"Skype");

                    timer.Stop();
                    notifyIcon.Icon = Resources.chat_480;
                }
                else    
                {
                    _skypeTabUnread = true;

                    tabPageSkype.InvokeOnUiThreadIfRequired(() => tabPageSkype.Text = @"Skype (+)");

                    timer.Start();    
                }

                this.InvokeOnUiThreadIfRequired(() => FlashWindow(activeHandle, true));
            }
        }

        private void OnSlackOpgkWebBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            string activeTabName = (string)Invoke(new Func<string>(() => tabControl.SelectedTab.Name));
            IntPtr activeHandle = (IntPtr)Invoke(new Func<IntPtr>(() => Handle));

            if ((args.Title.Contains("!") || args.Title.Contains("*")) && _slackTabOpgkUnread == false && _slackOpgkWebBrowserInitialized)
            {
                notifyIcon.BalloonTipTitle = @"SlackOpgk";
                notifyIcon.BalloonTipText = args.Title;
                notifyIcon.ShowBalloonTip(5000); 

                if (IsActive(activeHandle) && activeTabName == "tabPageSlackOPGK")
                {
                    _slackTabOpgkUnread = false;

                    tabPageSlackOPGK.InvokeOnUiThreadIfRequired(() => tabPageSlackOPGK.Text = @"Slack OPGK");

                    timer.Stop();
                    notifyIcon.Icon = Resources.chat_480;
                }
                else
                {
                    _slackTabOpgkUnread = true;

                    tabPageSlackOPGK.InvokeOnUiThreadIfRequired(() => tabPageSlackOPGK.Text = @"Slack OPGK (+)");

                    timer.Start();    
                }
               
                this.InvokeOnUiThreadIfRequired(() => FlashWindow(activeHandle, true));
            }
        }

        private void OnSlackGisnetWebBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            string activeTabName = (string)Invoke(new Func<string>(() => tabControl.SelectedTab.Name));
            IntPtr activeHandle = (IntPtr)Invoke(new Func<IntPtr>(() => Handle));

            if ((args.Title.Contains("!") || args.Title.Contains("*")) && _slackTabGisnetUnread == false && _slackGisnetWebBrowserInitialized)
            {
                notifyIcon.BalloonTipTitle = @"SlackGisne";
                notifyIcon.BalloonTipText = args.Title;
                notifyIcon.ShowBalloonTip(5000); 

                if (IsActive(activeHandle) && activeTabName == "tabPageSlackGISNET")
                {
                    _slackTabGisnetUnread = false;

                    tabPageSlackGISNET.InvokeOnUiThreadIfRequired(() => tabPageSlackGISNET.Text = @"Slack GISNET");

                    timer.Stop();
                    notifyIcon.Icon = Resources.chat_480;
                }
                else
                {
                    _slackTabGisnetUnread = true;

                    tabPageSlackGISNET.InvokeOnUiThreadIfRequired(() => tabPageSlackGISNET.Text = @"Slack GISNET (+)");

                    timer.Start();    
                }
               
                this.InvokeOnUiThreadIfRequired(() => FlashWindow(activeHandle, true));
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Get the Handle for the Forms System Menu
            IntPtr systemMenuHandle = GetSystemMenu(Handle, false);

            // Create our new System Menu items just before the Close menu item
            InsertMenu(systemMenuHandle, 5, MfByposition | MfSeparator, 0, string.Empty); // <-- Add a menu seperator
            InsertMenu(systemMenuHandle, 6, MfByposition, RefreshSysMenuId, "Refresh");
            InsertMenu(systemMenuHandle, 7, MfByposition, AboutSysMenuId, "About");

            tabPageMessenger.Controls.Add(_messengerWebBrowser);
            tabControl.SelectTab("tabPageMessenger");

            tabPageGG.Controls.Add(_ggWebBrowser);
            tabControl.SelectTab("tabPageGG");
            
            tabPageWhatsApp.Controls.Add(_whatsAppWebBrowser);
            tabControl.SelectTab("tabPageWhatsApp");

            tabPageTelegram.Controls.Add(_telegramWebBrowser);
            tabControl.SelectTab("tabPageTelegram");

            tabPageSkype.Controls.Add(_skypeWebBrowser);
            tabControl.SelectTab("tabPageSkype");

            tabPageSlackOPGK.Controls.Add(_slackOpgkWebBrowser);
            tabControl.SelectTab("tabPageSlackOPGK");

            tabPageSlackGISNET.Controls.Add(_slackGisnetWebBrowser);
            tabControl.SelectTab("tabPageSlackGISNET");

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
            timer.Stop();
            notifyIcon.Icon = Resources.chat_480;

            switch (tabControl.SelectedTab.Name)
            {
                case "tabPageMessenger":
                    tabPageMessenger.Text = @"Messenger";
                    _messengerTabUnread = false;
                    break;

                case "tabPageGG":
                    tabPageGG.Text = @"GG";
                    _ggTabUnread = false;
                    break;

                case "tabPageWhatsApp":
                    tabPageWhatsApp.Text = @"WhatsApp";
                    _whatsAppTabUnread = false;
                    break;

                case "tabPageTelegram":
                    tabPageTelegram.Text = @"Telegram";
                    _telegramTabUnread = false;
                    break;

                case "tabPageSkype":
                    tabPageSkype.Text = @"Skype";
                    _skypeTabUnread = false;
                    break;

                case "tabPageSlackOPGK":
                    tabPageSlackOPGK.Text = @"Slack OPGK";
                    _slackTabOpgkUnread = false;
                    break;

                case "tabPageSlackGISNET":
                    tabPageSlackGISNET.Text = @"Slack GISNET";
                    _slackTabGisnetUnread = false;
                    break;
            }
        }

        protected override void WndProc(ref Message m)
        {
            // Check if a System Command has been executed
            if (m.Msg == WmSyscommand)
            {
                // Execute the appropriate code for the System Menu item that was clicked
                switch (m.WParam.ToInt32())
                {
                    case RefreshSysMenuId:

                        _messengerWebBrowser.Reload();
                        _ggWebBrowser.Reload();
                        _whatsAppWebBrowser.Reload();
                        _telegramWebBrowser.Reload();
                        _skypeWebBrowser.Reload();
                        _slackOpgkWebBrowser.Reload();
                        _slackGisnetWebBrowser.Reload();

                        break;

                    case AboutSysMenuId:
                        
                        break;
                }
            }

            base.WndProc(ref m);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool bHandled = false;
            // switch case is the easy way, a hash or map would be better, 
            // but more work to get set up.

            switch (keyData)
            {
                case Keys.F5:

                    _messengerWebBrowser.Reload();
                    _ggWebBrowser.Reload();
                    _whatsAppWebBrowser.Reload();
                    _telegramWebBrowser.Reload();
                    _skypeWebBrowser.Reload();
                    _slackOpgkWebBrowser.Reload();
                    _slackGisnetWebBrowser.Reload();

                    bHandled = true;
                    break;
            }
            return bHandled;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_iconBlink)
            {
                notifyIcon.Icon = Resources.chat_490;
                _iconBlink = false;
            }
            else
            {
                notifyIcon.Icon = Resources.chat_480;
                _iconBlink = true;
            }
        }
    }
}
