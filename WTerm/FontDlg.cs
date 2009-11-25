using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WTerm
{
    public partial class FontDlg : Form
    {
        public FontDlg(TerminalEmulator.FontGroup fontgroup)
        {
            _fontgroup = fontgroup;
            InitializeComponent();
        }

        private TerminalEmulator.FontGroup _fontgroup;
        private void PreviewText_TextChanged(object sender, EventArgs e)
        {
            PreviewUpdate();
        }

        private void PreviewUpdate()
        {
            string s = PreviewText.Text;
            Preview.EarseDisplay(2);
            Preview.MoveCursor(0, 0);
            foreach (char c in s)
            {
                if (c == '\n')
                {
                    Preview.PutCarriageRet();
                    Preview.PutNewLine();
                }
                else
                {
                    Preview.PutChar(c, 0);
                }
            }
        }

        private void FontDlg_Load(object sender, EventArgs e)
        {
            foreach (TerminalEmulator.Font ft in _fontgroup.Fonts)
            {
                FontItem f = new FontItem();
                f.font = ft.GetFont;
                f.offset = ft.Offset;
                if (ft is TerminalEmulator.UnicodeFont)
                    f.charset = FontItem.Charset.Unicode;
                else if (ft is TerminalEmulator.LatinFont)
                    f.charset = FontItem.Charset.Latin;
                FontList.Items.Add(f.ToItem());
                ListMap[f.index] = f;
            }
            SizeX.Value = (int)_fontgroup.Size.Width * 100;
            SizeY.Value = (int)_fontgroup.Size.Height * 100;

            UpdateFontGroup();
            PreviewUpdate();
        }

        private class FontItem
        {
            public int index
            {
                get { return i.Index; }
            }
            public Font font
            {
                get { return _font; }
                set { _font = value; if (i != null) i.SubItems[0].Text = _font.ToString(); }
            }
            public PointF offset
            {
                get { return _offset; }
                set { _offset = value; if (i != null) i.SubItems[1].Text = _offset.ToString(); }
            }
            public Charset charset
            {
                get { return _charset; }
                set { _charset = value; if (i != null) i.SubItems[2].Text = _charset.ToString(); }
            }

            public enum Charset
            {
                Latin = 0,
                Unicode = 1
            }

            public ListViewItem ToItem()
            {
                if (i == null)
                {
                    i = new ListViewItem(new string[] { font.ToString(), offset.ToString(), charset.ToString() });
                }
                else
                {
                    i.SubItems[0].Text = font.ToString();
                    i.SubItems[1].Text = offset.ToString();
                    i.SubItems[2].Text = charset.ToString();
                }
                return i;
            }

            private Font _font;
            private PointF _offset;
            private Charset _charset;
            private ListViewItem i;
        }

        private void AddFont_Click(object sender, EventArgs e)
        {
            if (FontDialog.ShowDialog() != DialogResult.Cancel)
            {
                FontItem f = new FontItem();
                f.font = FontDialog.Font;
                f.charset = FontItem.Charset.Latin;
                FontList.Items.Add(f.ToItem());
                ListMap[f.index] = f;

                using (Graphics g = Graphics.FromHwnd(Handle))
                {
                    SizeF chars_size = g.MeasureString("◆", f.font, -1, StringFormat.GenericTypographic);
                    if (chars_size.Height == 0)
                    {
                        chars_size = g.MeasureString("N", f.font, -1, StringFormat.GenericTypographic);
                    }

                    if (chars_size.Height != 0 && (double)chars_size.Width / chars_size.Height > 0.8F)
                    {
                        chars_size.Width /= 2;
                    }

                    SizeX.Value = (int)Math.Max(SizeX.Value, chars_size.Width * 100);
                    SizeY.Value = (int)Math.Max(SizeY.Value, chars_size.Height * 100);
                }

                UpdateFontGroup();
            }
        }

        public TerminalEmulator.FontGroup GetFontGroup()
        {
            TerminalEmulator.Font[] fa = new TerminalEmulator.Font[ListMap.Count];
            foreach (KeyValuePair<int, FontItem> ift in ListMap)
            {
                switch (ift.Value.charset)
                {
                    case FontItem.Charset.Latin:
                        fa[ift.Key] = new TerminalEmulator.LatinFont(ift.Value.font, ift.Value.offset);
                        break;
                    case FontItem.Charset.Unicode:
                        fa[ift.Key] = new TerminalEmulator.UnicodeFont(ift.Value.font, ift.Value.offset);
                        break;
                }
            }
            return new TerminalEmulator.FontGroup(fa, new SizeF((float)SizeX.Value / 100, (float)SizeY.Value / 100));
        }

        private Dictionary<int, FontItem> ListMap = new Dictionary<int, FontItem>();

        private void UpdateFontGroup(object sender, EventArgs e)
        {
            UpdateFontGroup();
        }

        private void UpdateFontGroup()
        {
            Preview.Font = GetFontGroup();
        }

        private void FontList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (FontList.SelectedItems.Count > 0)
            {
                OffsetX.Enabled = true;
                OffsetY.Enabled = true;
                OffsetX.Value = (int)ListMap[FontList.SelectedItems[0].Index].offset.X * 100;
                OffsetY.Value = (int)ListMap[FontList.SelectedItems[0].Index].offset.Y * 100;
                Unicode.Checked = false;
                Latin.Checked = false;
                FontItem.Charset set = ListMap[FontList.SelectedItems[0].Index].charset;
                foreach (ListViewItem item in FontList.SelectedItems)
                {
                    if (set != ListMap[item.Index].charset)
                        return;
                }
                switch (set)
                {
                    case FontItem.Charset.Unicode:
                        Unicode.Checked = true;
                        break;
                    case FontItem.Charset.Latin:
                        Latin.Checked = true;
                        break;
                }
            }
            else
            {
                OffsetX.Enabled = false;
                OffsetY.Enabled = false;
            }
        }

        private void Latin_CheckedChanged(object sender, EventArgs e)
        {
            if (Latin.Checked)
            {
                foreach (ListViewItem item in FontList.SelectedItems)
                {
                    ListMap[item.Index].charset = FontItem.Charset.Latin;
                }
                UpdateFontGroup();
            }
        }

        private void Unicode_CheckedChanged(object sender, EventArgs e)
        {
            if (Unicode.Checked)
            {
                foreach (ListViewItem item in FontList.SelectedItems)
                {
                    ListMap[item.Index].charset = FontItem.Charset.Unicode;
                }
                UpdateFontGroup();
            }
        }

        private void DelFont_Click(object sender, EventArgs e)
        {
            for (int i = FontList.Items.Count - 1; i >= 0; i--)
            {
                if (FontList.Items[i].Selected)
                    FontList.Items.RemoveAt(i);
            }
        }

        private void OffsetX_Scroll(object sender, EventArgs e)
        {
            foreach (ListViewItem item in FontList.SelectedItems)
            {
                ListMap[item.Index].offset = new PointF((float)OffsetX.Value / 100, ListMap[item.Index].offset.Y);
            }
            UpdateFontGroup();
        }

        private void OffsetY_Scroll(object sender, EventArgs e)
        {
            foreach (ListViewItem item in FontList.SelectedItems)
            {
                ListMap[item.Index].offset = new PointF(ListMap[item.Index].offset.X, (float)OffsetY.Value / 100);
            }
            UpdateFontGroup();
        }

        private void OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
