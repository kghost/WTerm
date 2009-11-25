namespace TerminalEmulator
{
    partial class TerminalEmulator
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Blink = new System.Windows.Forms.Timer(this.components);
            this.DelayPaint = new System.Windows.Forms.Timer(this.components);
            this.KeyRepeat = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Blink
            // 
            this.Blink.Enabled = true;
            this.Blink.Interval = 500;
            this.Blink.Tick += new System.EventHandler(this.Blink_Tick);
            // 
            // DelayPaint
            // 
            this.DelayPaint.Enabled = true;
            this.DelayPaint.Interval = 50;
            this.DelayPaint.Tick += new System.EventHandler(this.DelayPaint_Tick);
            // 
            // KeyRepeat
            // 
            this.KeyRepeat.Tick += new System.EventHandler(this.KeyRepeat_Tick);
            // 
            // TerminalEmulator
            // 
            this.Name = "TerminalEmulator";
            this.Load += new System.EventHandler(this.TerminalEmulator_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TerminalEmulator_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TerminalEmulator_KeyUp);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TerminalEmulator_KeyPress);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TerminalEmulator_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer Blink;
        private System.Windows.Forms.Timer DelayPaint;
        private System.Windows.Forms.Timer KeyRepeat;
    }
}
