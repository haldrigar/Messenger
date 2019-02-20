namespace Komunikator
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageMessenger = new System.Windows.Forms.TabPage();
            this.tabPageGG = new System.Windows.Forms.TabPage();
            this.tabPageWhatsApp = new System.Windows.Forms.TabPage();
            this.tabPageSkype = new System.Windows.Forms.TabPage();
            this.tabPageSlackOpgk = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl.Controls.Add(this.tabPageMessenger);
            this.tabControl.Controls.Add(this.tabPageGG);
            this.tabControl.Controls.Add(this.tabPageWhatsApp);
            this.tabControl.Controls.Add(this.tabPageSkype);
            this.tabControl.Controls.Add(this.tabPageSlackOpgk);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ItemSize = new System.Drawing.Size(100, 25);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(0, 0);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1022, 719);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 0;
            this.tabControl.Click += new System.EventHandler(this.TabControl_Click);
            // 
            // tabPageMessenger
            // 
            this.tabPageMessenger.BackColor = System.Drawing.Color.Transparent;
            this.tabPageMessenger.Location = new System.Drawing.Point(4, 4);
            this.tabPageMessenger.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageMessenger.Name = "tabPageMessenger";
            this.tabPageMessenger.Size = new System.Drawing.Size(1014, 686);
            this.tabPageMessenger.TabIndex = 0;
            this.tabPageMessenger.Text = "Messenger";
            // 
            // tabPageGG
            // 
            this.tabPageGG.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPageGG.Location = new System.Drawing.Point(4, 4);
            this.tabPageGG.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageGG.Name = "tabPageGG";
            this.tabPageGG.Size = new System.Drawing.Size(1014, 686);
            this.tabPageGG.TabIndex = 1;
            this.tabPageGG.Text = "GG";
            // 
            // tabPageWhatsApp
            // 
            this.tabPageWhatsApp.Location = new System.Drawing.Point(4, 4);
            this.tabPageWhatsApp.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageWhatsApp.Name = "tabPageWhatsApp";
            this.tabPageWhatsApp.Size = new System.Drawing.Size(1014, 686);
            this.tabPageWhatsApp.TabIndex = 2;
            this.tabPageWhatsApp.Text = "WhatsApp";
            // 
            // tabPageSkype
            // 
            this.tabPageSkype.Location = new System.Drawing.Point(4, 4);
            this.tabPageSkype.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageSkype.Name = "tabPageSkype";
            this.tabPageSkype.Size = new System.Drawing.Size(1014, 686);
            this.tabPageSkype.TabIndex = 3;
            this.tabPageSkype.Text = "Skype";
            // 
            // tabPageSlackOpgk
            // 
            this.tabPageSlackOpgk.Location = new System.Drawing.Point(4, 4);
            this.tabPageSlackOpgk.Margin = new System.Windows.Forms.Padding(0);
            this.tabPageSlackOpgk.Name = "tabPageSlackOpgk";
            this.tabPageSlackOpgk.Size = new System.Drawing.Size(1014, 686);
            this.tabPageSlackOpgk.TabIndex = 4;
            this.tabPageSlackOpgk.Text = "Slack OPGK";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 719);
            this.Controls.Add(this.tabControl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.Text = "Komunikator";
            this.Activated += new System.EventHandler(this.FormMain_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageMessenger;
        private System.Windows.Forms.TabPage tabPageGG;
        private System.Windows.Forms.TabPage tabPageWhatsApp;
        private System.Windows.Forms.TabPage tabPageSkype;
        private System.Windows.Forms.TabPage tabPageSlackOpgk;
    }
}

