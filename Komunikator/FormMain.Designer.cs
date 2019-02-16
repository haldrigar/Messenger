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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageMessenger = new System.Windows.Forms.TabPage();
            this.tabPageGG = new System.Windows.Forms.TabPage();
            this.tabPageWhatsApp = new System.Windows.Forms.TabPage();
            this.tabPageSkype = new System.Windows.Forms.TabPage();
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
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ItemSize = new System.Drawing.Size(100, 25);
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1092, 719);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 0;
            // 
            // tabPageMessenger
            // 
            this.tabPageMessenger.Location = new System.Drawing.Point(4, 4);
            this.tabPageMessenger.Name = "tabPageMessenger";
            this.tabPageMessenger.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMessenger.Size = new System.Drawing.Size(1084, 686);
            this.tabPageMessenger.TabIndex = 0;
            this.tabPageMessenger.Text = "Messenger";
            this.tabPageMessenger.UseVisualStyleBackColor = true;
            // 
            // tabPageGG
            // 
            this.tabPageGG.Location = new System.Drawing.Point(4, 4);
            this.tabPageGG.Name = "tabPageGG";
            this.tabPageGG.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGG.Size = new System.Drawing.Size(1084, 686);
            this.tabPageGG.TabIndex = 1;
            this.tabPageGG.Text = "GG";
            this.tabPageGG.UseVisualStyleBackColor = true;
            // 
            // tabPageWhatsApp
            // 
            this.tabPageWhatsApp.Location = new System.Drawing.Point(4, 4);
            this.tabPageWhatsApp.Name = "tabPageWhatsApp";
            this.tabPageWhatsApp.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageWhatsApp.Size = new System.Drawing.Size(1084, 686);
            this.tabPageWhatsApp.TabIndex = 2;
            this.tabPageWhatsApp.Text = "WhatsApp";
            this.tabPageWhatsApp.UseVisualStyleBackColor = true;
            // 
            // tabPageSkype
            // 
            this.tabPageSkype.Location = new System.Drawing.Point(4, 4);
            this.tabPageSkype.Name = "tabPageSkype";
            this.tabPageSkype.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSkype.Size = new System.Drawing.Size(1084, 686);
            this.tabPageSkype.TabIndex = 3;
            this.tabPageSkype.Text = "Skype";
            this.tabPageSkype.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1092, 719);
            this.Controls.Add(this.tabControl);
            this.Name = "FormMain";
            this.Text = "Komunikator";
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

    }
}

