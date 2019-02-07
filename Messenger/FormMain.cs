using System;
using System.Drawing;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace Messenger
{
    public partial class FormMain : Form
    {
        public FormMain()
        {

            CefSettings settings = new CefSettings
            {
                CachePath = AppDomain.CurrentDomain.BaseDirectory + @"\cache",
                Locale = "pl"
            };

            Cef.EnableHighDPISupport();
            CefSharpSettings.ShutdownOnExit = true;

            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

            TabControl tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Alignment = TabAlignment.Bottom,
                SizeMode = TabSizeMode.Fixed,
                ItemSize = new Size(100, 25)
            };

            TabPage pageMessenger = new TabPage("Messenger");
            tabControl.TabPages.Add(pageMessenger);

            ChromiumWebBrowser browserMessenger = new ChromiumWebBrowser("https://www.messenger.com");
            browserMessenger.Dock = DockStyle.Fill;
            pageMessenger.Controls.Add(browserMessenger);

            TabPage pageMessages = new TabPage("Wiadomości");
            tabControl.TabPages.Add(pageMessages);

            ChromiumWebBrowser browserMessages = new ChromiumWebBrowser("https://messages.android.com/");
            browserMessages.Dock = DockStyle.Fill;
            pageMessages.Controls.Add(browserMessages);

            TabPage pageWhatsapp = new TabPage("WhatsApp");
            tabControl.TabPages.Add(pageWhatsapp);

            ChromiumWebBrowser browserWhatsapp = new ChromiumWebBrowser("https://web.whatsapp.com/");
            browserWhatsapp.Dock = DockStyle.Fill;
            pageWhatsapp.Controls.Add(browserWhatsapp);

            Controls.Add(tabControl);

            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }
    }
}
