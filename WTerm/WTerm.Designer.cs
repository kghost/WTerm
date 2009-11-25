namespace WTerm
{
    partial class WTerm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripMenuItem fontToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem pluginsToolStripMenuItem;
            this.panel1 = new System.Windows.Forms.Panel();
            this.terminalEmulator1 = new TerminalEmulator.TerminalEmulatorWithIME();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            fontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            pluginsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // fontToolStripMenuItem
            // 
            fontToolStripMenuItem.Name = "fontToolStripMenuItem";
            fontToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            fontToolStripMenuItem.Text = "&Font";
            fontToolStripMenuItem.Click += new System.EventHandler(this.fontToolStripMenuItem_Click);
            // 
            // programToolStripMenuItem
            // 
            programToolStripMenuItem.Name = "programToolStripMenuItem";
            programToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            programToolStripMenuItem.Text = "&Program";
            programToolStripMenuItem.Click += new System.EventHandler(this.programToolStripMenuItem_Click);
            // 
            // pluginsToolStripMenuItem
            // 
            pluginsToolStripMenuItem.Name = "pluginsToolStripMenuItem";
            pluginsToolStripMenuItem.Size = new System.Drawing.Size(58, 20);
            pluginsToolStripMenuItem.Text = "Pl&ugins";
            pluginsToolStripMenuItem.Click += new System.EventHandler(this.pluginsToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.terminalEmulator1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 240);
            this.panel1.TabIndex = 0;
            // 
            // terminalEmulator1
            // 
            this.terminalEmulator1.Am = true;
            this.terminalEmulator1.BackColor = System.Drawing.Color.Black;
            this.terminalEmulator1.bg = System.Drawing.Color.Black;
            this.terminalEmulator1.cols = 83;
            this.terminalEmulator1.fg = System.Drawing.Color.White;
            this.terminalEmulator1.ForeColor = System.Drawing.Color.White;
            this.terminalEmulator1.lines = 24;
            this.terminalEmulator1.Location = new System.Drawing.Point(0, 0);
            this.terminalEmulator1.Name = "terminalEmulator1";
            this.terminalEmulator1.Size = new System.Drawing.Size(830, 480);
            this.terminalEmulator1.TabIndex = 0;
            this.terminalEmulator1.OnSendCode += new TerminalEmulator.TerminalEmulator.SendCode(this.terminalEmulator1_OnSendCode);
            this.terminalEmulator1.OnSendString += new TerminalEmulator.TerminalEmulator.SendString(this.terminalEmulator1_OnSendString);
            this.terminalEmulator1.OnSendChar += new TerminalEmulator.TerminalEmulator.SendChar(this.terminalEmulator1_OnSendChar);
            this.terminalEmulator1.Resize += new System.EventHandler(this.terminalEmulator1_Resize);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            fontToolStripMenuItem,
            programToolStripMenuItem,
            pluginsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // WTerm
            // 
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "WTerm";
            this.Text = "WTerm";
            this.Load += new System.EventHandler(this.WTerm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WTerm_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WTerm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private TerminalEmulator.TerminalEmulatorWithIME terminalEmulator1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

