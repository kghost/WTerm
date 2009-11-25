namespace WTerm
{
    partial class FontDlg
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
            System.Windows.Forms.SplitContainer splitContainer1;
            System.Windows.Forms.GroupBox Fonts;
            System.Windows.Forms.ColumnHeader HFont;
            System.Windows.Forms.ColumnHeader Offset;
            System.Windows.Forms.ColumnHeader HCharset;
            System.Windows.Forms.Button DelFont;
            System.Windows.Forms.Button AddFont;
            System.Windows.Forms.SplitContainer splitContainer2;
            System.Windows.Forms.SplitContainer splitContainer5;
            System.Windows.Forms.SplitContainer splitContainer3;
            System.Windows.Forms.GroupBox PreviewGroup;
            System.Windows.Forms.SplitContainer splitContainer4;
            System.Windows.Forms.GroupBox CharOffset;
            System.Windows.Forms.SplitContainer splitContainer6;
            System.Windows.Forms.GroupBox Charset;
            this.FontList = new System.Windows.Forms.ListView();
            this.CharSize = new System.Windows.Forms.GroupBox();
            this.SizeY = new System.Windows.Forms.TrackBar();
            this.SizeX = new System.Windows.Forms.TrackBar();
            this.PreviewTextGroup = new System.Windows.Forms.GroupBox();
            this.PreviewText = new System.Windows.Forms.TextBox();
            this.Preview = new TerminalEmulator.TerminalEmulator();
            this.OffsetY = new System.Windows.Forms.TrackBar();
            this.OffsetX = new System.Windows.Forms.TrackBar();
            this.Unicode = new System.Windows.Forms.RadioButton();
            this.Latin = new System.Windows.Forms.RadioButton();
            this.OK = new System.Windows.Forms.Button();
            this.FontDialog = new System.Windows.Forms.FontDialog();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            Fonts = new System.Windows.Forms.GroupBox();
            HFont = new System.Windows.Forms.ColumnHeader();
            Offset = new System.Windows.Forms.ColumnHeader();
            HCharset = new System.Windows.Forms.ColumnHeader();
            DelFont = new System.Windows.Forms.Button();
            AddFont = new System.Windows.Forms.Button();
            splitContainer2 = new System.Windows.Forms.SplitContainer();
            splitContainer5 = new System.Windows.Forms.SplitContainer();
            splitContainer3 = new System.Windows.Forms.SplitContainer();
            PreviewGroup = new System.Windows.Forms.GroupBox();
            splitContainer4 = new System.Windows.Forms.SplitContainer();
            CharOffset = new System.Windows.Forms.GroupBox();
            splitContainer6 = new System.Windows.Forms.SplitContainer();
            Charset = new System.Windows.Forms.GroupBox();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            Fonts.SuspendLayout();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            this.CharSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SizeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeX)).BeginInit();
            splitContainer5.Panel1.SuspendLayout();
            splitContainer5.Panel2.SuspendLayout();
            splitContainer5.SuspendLayout();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            this.PreviewTextGroup.SuspendLayout();
            PreviewGroup.SuspendLayout();
            splitContainer4.Panel1.SuspendLayout();
            splitContainer4.Panel2.SuspendLayout();
            splitContainer4.SuspendLayout();
            CharOffset.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetX)).BeginInit();
            splitContainer6.Panel1.SuspendLayout();
            splitContainer6.Panel2.SuspendLayout();
            splitContainer6.SuspendLayout();
            Charset.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new System.Drawing.Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(Fonts);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new System.Drawing.Size(755, 408);
            splitContainer1.SplitterDistance = 251;
            splitContainer1.TabIndex = 0;
            splitContainer1.TabStop = false;
            // 
            // Fonts
            // 
            Fonts.Controls.Add(this.FontList);
            Fonts.Controls.Add(DelFont);
            Fonts.Controls.Add(AddFont);
            Fonts.Dock = System.Windows.Forms.DockStyle.Fill;
            Fonts.Location = new System.Drawing.Point(0, 0);
            Fonts.Name = "Fonts";
            Fonts.Size = new System.Drawing.Size(251, 408);
            Fonts.TabIndex = 5;
            Fonts.TabStop = false;
            Fonts.Text = "Fonts";
            // 
            // FontList
            // 
            this.FontList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            HFont,
            Offset,
            HCharset});
            this.FontList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FontList.FullRowSelect = true;
            this.FontList.GridLines = true;
            this.FontList.Location = new System.Drawing.Point(3, 39);
            this.FontList.Name = "FontList";
            this.FontList.Size = new System.Drawing.Size(245, 343);
            this.FontList.TabIndex = 2;
            this.FontList.UseCompatibleStateImageBehavior = false;
            this.FontList.View = System.Windows.Forms.View.Details;
            this.FontList.SelectedIndexChanged += new System.EventHandler(this.FontList_SelectedIndexChanged);
            // 
            // HFont
            // 
            HFont.Text = "Font";
            HFont.Width = 121;
            // 
            // Offset
            // 
            Offset.Text = "Offset";
            // 
            // HCharset
            // 
            HCharset.Text = "Charset";
            // 
            // DelFont
            // 
            DelFont.Dock = System.Windows.Forms.DockStyle.Bottom;
            DelFont.Location = new System.Drawing.Point(3, 382);
            DelFont.Name = "DelFont";
            DelFont.Size = new System.Drawing.Size(245, 23);
            DelFont.TabIndex = 4;
            DelFont.Text = "Delete Font";
            DelFont.UseVisualStyleBackColor = true;
            DelFont.Click += new System.EventHandler(this.DelFont_Click);
            // 
            // AddFont
            // 
            AddFont.Dock = System.Windows.Forms.DockStyle.Top;
            AddFont.Location = new System.Drawing.Point(3, 16);
            AddFont.Name = "AddFont";
            AddFont.Size = new System.Drawing.Size(245, 23);
            AddFont.TabIndex = 0;
            AddFont.Text = "Add Font";
            AddFont.UseVisualStyleBackColor = true;
            AddFont.Click += new System.EventHandler(this.AddFont_Click);
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new System.Drawing.Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(this.CharSize);
            splitContainer2.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(splitContainer5);
            splitContainer2.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            splitContainer2.Size = new System.Drawing.Size(500, 408);
            splitContainer2.SplitterDistance = 110;
            splitContainer2.TabIndex = 0;
            splitContainer2.TabStop = false;
            // 
            // CharSize
            // 
            this.CharSize.Controls.Add(this.SizeY);
            this.CharSize.Controls.Add(this.SizeX);
            this.CharSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CharSize.Location = new System.Drawing.Point(0, 0);
            this.CharSize.Name = "CharSize";
            this.CharSize.Size = new System.Drawing.Size(500, 110);
            this.CharSize.TabIndex = 0;
            this.CharSize.TabStop = false;
            this.CharSize.Text = "Char Size";
            // 
            // SizeY
            // 
            this.SizeY.LargeChange = 500;
            this.SizeY.Location = new System.Drawing.Point(6, 59);
            this.SizeY.Maximum = 10000;
            this.SizeY.Name = "SizeY";
            this.SizeY.Size = new System.Drawing.Size(488, 45);
            this.SizeY.TabIndex = 6;
            this.SizeY.Scroll += new System.EventHandler(this.UpdateFontGroup);
            // 
            // SizeX
            // 
            this.SizeX.LargeChange = 500;
            this.SizeX.Location = new System.Drawing.Point(6, 19);
            this.SizeX.Maximum = 10000;
            this.SizeX.Name = "SizeX";
            this.SizeX.Size = new System.Drawing.Size(488, 45);
            this.SizeX.TabIndex = 5;
            this.SizeX.Scroll += new System.EventHandler(this.UpdateFontGroup);
            // 
            // splitContainer5
            // 
            splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer5.IsSplitterFixed = true;
            splitContainer5.Location = new System.Drawing.Point(0, 0);
            splitContainer5.Name = "splitContainer5";
            splitContainer5.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            splitContainer5.Panel1.Controls.Add(splitContainer3);
            // 
            // splitContainer5.Panel2
            // 
            splitContainer5.Panel2.Controls.Add(splitContainer4);
            splitContainer5.Size = new System.Drawing.Size(500, 294);
            splitContainer5.SplitterDistance = 182;
            splitContainer5.TabIndex = 0;
            splitContainer5.TabStop = false;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer3.IsSplitterFixed = true;
            splitContainer3.Location = new System.Drawing.Point(0, 0);
            splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(this.PreviewTextGroup);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(PreviewGroup);
            splitContainer3.Size = new System.Drawing.Size(500, 182);
            splitContainer3.SplitterDistance = 221;
            splitContainer3.TabIndex = 0;
            splitContainer3.TabStop = false;
            // 
            // PreviewTextGroup
            // 
            this.PreviewTextGroup.Controls.Add(this.PreviewText);
            this.PreviewTextGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewTextGroup.Location = new System.Drawing.Point(0, 0);
            this.PreviewTextGroup.Name = "PreviewTextGroup";
            this.PreviewTextGroup.Size = new System.Drawing.Size(221, 182);
            this.PreviewTextGroup.TabIndex = 1;
            this.PreviewTextGroup.TabStop = false;
            this.PreviewTextGroup.Text = "Preview Text";
            // 
            // PreviewText
            // 
            this.PreviewText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PreviewText.Location = new System.Drawing.Point(3, 16);
            this.PreviewText.Multiline = true;
            this.PreviewText.Name = "PreviewText";
            this.PreviewText.Size = new System.Drawing.Size(215, 163);
            this.PreviewText.TabIndex = 7;
            this.PreviewText.Text = "English\r\n简体中文\r\n繁體中文\r\nにほんご";
            this.PreviewText.TextChanged += new System.EventHandler(this.PreviewText_TextChanged);
            // 
            // PreviewGroup
            // 
            PreviewGroup.Controls.Add(this.Preview);
            PreviewGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            PreviewGroup.Location = new System.Drawing.Point(0, 0);
            PreviewGroup.Name = "PreviewGroup";
            PreviewGroup.Size = new System.Drawing.Size(275, 182);
            PreviewGroup.TabIndex = 1;
            PreviewGroup.TabStop = false;
            PreviewGroup.Text = "Preview";
            // 
            // Preview
            // 
            this.Preview.Am = true;
            this.Preview.BackColor = System.Drawing.Color.DarkBlue;
            this.Preview.bg = System.Drawing.Color.DarkBlue;
            this.Preview.cols = 20;
            this.Preview.fg = System.Drawing.Color.White;
            this.Preview.ForeColor = System.Drawing.Color.White;
            this.Preview.lines = 8;
            this.Preview.Location = new System.Drawing.Point(6, 16);
            this.Preview.Name = "Preview";
            this.Preview.Size = new System.Drawing.Size(200, 160);
            this.Preview.TabIndex = 8;
            this.Preview.TabStop = false;
            // 
            // splitContainer4
            // 
            splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer4.IsSplitterFixed = true;
            splitContainer4.Location = new System.Drawing.Point(0, 0);
            splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            splitContainer4.Panel1.Controls.Add(CharOffset);
            // 
            // splitContainer4.Panel2
            // 
            splitContainer4.Panel2.Controls.Add(splitContainer6);
            splitContainer4.Size = new System.Drawing.Size(500, 108);
            splitContainer4.SplitterDistance = 361;
            splitContainer4.TabIndex = 0;
            splitContainer4.TabStop = false;
            // 
            // CharOffset
            // 
            CharOffset.Controls.Add(this.OffsetY);
            CharOffset.Controls.Add(this.OffsetX);
            CharOffset.Dock = System.Windows.Forms.DockStyle.Fill;
            CharOffset.Location = new System.Drawing.Point(0, 0);
            CharOffset.Name = "CharOffset";
            CharOffset.Size = new System.Drawing.Size(361, 108);
            CharOffset.TabIndex = 0;
            CharOffset.TabStop = false;
            CharOffset.Text = "Char Offset";
            // 
            // OffsetY
            // 
            this.OffsetY.Enabled = false;
            this.OffsetY.LargeChange = 50;
            this.OffsetY.Location = new System.Drawing.Point(6, 59);
            this.OffsetY.Maximum = 1000;
            this.OffsetY.Minimum = -1000;
            this.OffsetY.Name = "OffsetY";
            this.OffsetY.Size = new System.Drawing.Size(349, 45);
            this.OffsetY.TabIndex = 10;
            this.OffsetY.Scroll += new System.EventHandler(this.OffsetY_Scroll);
            // 
            // OffsetX
            // 
            this.OffsetX.Enabled = false;
            this.OffsetX.LargeChange = 50;
            this.OffsetX.Location = new System.Drawing.Point(6, 19);
            this.OffsetX.Maximum = 1000;
            this.OffsetX.Minimum = -1000;
            this.OffsetX.Name = "OffsetX";
            this.OffsetX.Size = new System.Drawing.Size(349, 45);
            this.OffsetX.TabIndex = 9;
            this.OffsetX.Scroll += new System.EventHandler(this.OffsetX_Scroll);
            // 
            // splitContainer6
            // 
            splitContainer6.Dock = System.Windows.Forms.DockStyle.Fill;
            splitContainer6.IsSplitterFixed = true;
            splitContainer6.Location = new System.Drawing.Point(0, 0);
            splitContainer6.Name = "splitContainer6";
            splitContainer6.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer6.Panel1
            // 
            splitContainer6.Panel1.Controls.Add(Charset);
            // 
            // splitContainer6.Panel2
            // 
            splitContainer6.Panel2.Controls.Add(this.OK);
            splitContainer6.Size = new System.Drawing.Size(135, 108);
            splitContainer6.SplitterDistance = 70;
            splitContainer6.TabIndex = 0;
            splitContainer6.TabStop = false;
            // 
            // Charset
            // 
            Charset.Controls.Add(this.Unicode);
            Charset.Controls.Add(this.Latin);
            Charset.Dock = System.Windows.Forms.DockStyle.Fill;
            Charset.Location = new System.Drawing.Point(0, 0);
            Charset.Name = "Charset";
            Charset.Size = new System.Drawing.Size(135, 70);
            Charset.TabIndex = 0;
            Charset.TabStop = false;
            Charset.Text = "Charset";
            // 
            // Unicode
            // 
            this.Unicode.AutoSize = true;
            this.Unicode.Location = new System.Drawing.Point(29, 39);
            this.Unicode.Name = "Unicode";
            this.Unicode.Size = new System.Drawing.Size(65, 17);
            this.Unicode.TabIndex = 12;
            this.Unicode.TabStop = true;
            this.Unicode.Text = "Unicode";
            this.Unicode.UseVisualStyleBackColor = true;
            this.Unicode.CheckedChanged += new System.EventHandler(this.Unicode_CheckedChanged);
            // 
            // Latin
            // 
            this.Latin.AutoSize = true;
            this.Latin.Location = new System.Drawing.Point(29, 19);
            this.Latin.Name = "Latin";
            this.Latin.Size = new System.Drawing.Size(48, 17);
            this.Latin.TabIndex = 11;
            this.Latin.TabStop = true;
            this.Latin.Text = "Latin";
            this.Latin.UseVisualStyleBackColor = true;
            this.Latin.CheckedChanged += new System.EventHandler(this.Latin_CheckedChanged);
            // 
            // OK
            // 
            this.OK.Dock = System.Windows.Forms.DockStyle.Fill;
            this.OK.Location = new System.Drawing.Point(0, 0);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(135, 34);
            this.OK.TabIndex = 13;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // FontDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 408);
            this.Controls.Add(splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FontDlg";
            this.ShowInTaskbar = false;
            this.Text = "Set Fonts";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FontDlg_Load);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.ResumeLayout(false);
            Fonts.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.ResumeLayout(false);
            this.CharSize.ResumeLayout(false);
            this.CharSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SizeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeX)).EndInit();
            splitContainer5.Panel1.ResumeLayout(false);
            splitContainer5.Panel2.ResumeLayout(false);
            splitContainer5.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            splitContainer3.ResumeLayout(false);
            this.PreviewTextGroup.ResumeLayout(false);
            this.PreviewTextGroup.PerformLayout();
            PreviewGroup.ResumeLayout(false);
            splitContainer4.Panel1.ResumeLayout(false);
            splitContainer4.Panel2.ResumeLayout(false);
            splitContainer4.ResumeLayout(false);
            CharOffset.ResumeLayout(false);
            CharOffset.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OffsetX)).EndInit();
            splitContainer6.Panel1.ResumeLayout(false);
            splitContainer6.Panel2.ResumeLayout(false);
            splitContainer6.ResumeLayout(false);
            Charset.ResumeLayout(false);
            Charset.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView FontList;
        private TerminalEmulator.TerminalEmulator Preview;
        private System.Windows.Forms.GroupBox PreviewTextGroup;
        private System.Windows.Forms.TextBox PreviewText;
        private System.Windows.Forms.FontDialog FontDialog;
        private System.Windows.Forms.TrackBar OffsetY;
        private System.Windows.Forms.TrackBar OffsetX;
        private System.Windows.Forms.TrackBar SizeY;
        private System.Windows.Forms.TrackBar SizeX;
        private System.Windows.Forms.RadioButton Unicode;
        private System.Windows.Forms.RadioButton Latin;
        private System.Windows.Forms.GroupBox CharSize;
        private System.Windows.Forms.Button OK;
    }
}