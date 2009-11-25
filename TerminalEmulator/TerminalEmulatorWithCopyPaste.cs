using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TerminalEmulator
{
    public partial class TerminalEmulatorWithCopyPaste : TerminalEmulator
    {
        public TerminalEmulatorWithCopyPaste()
        {
            InitializeComponent();
            ContextMenuStrip = contextMenuStrip1;
        }

        private TerminalChars.Cursor location(int X, int Y)
        {
            int l = (int)Math.Floor(Y / _font.Size.Height);
            if (l < 0) l = 0;
            else if (l >= _chars._size.line) l = _chars._size.line - 1;
            int c = (int)Math.Floor(X / _font.Size.Width);
            if (c < 0) c = 0;
            else if (c >= _chars._size.col) c = _chars._size.col - 1;
            return new TerminalChars.Cursor(l, c);
        }

        private void CaculateRegion()
        {
            TerminalChars.Cursor begin;
            TerminalChars.Cursor end;
            SelectRange(out begin, out end);
            LinkedList<PointF> p = new LinkedList<PointF>();
            if (begin.col != 0)
            {
                if (begin.line != end.line) p.AddLast(new PointF(0, (begin.line + 1) * _font.Size.Height)); // upper left
                p.AddLast(new PointF(begin.col * _font.Size.Width, (begin.line + 1) * _font.Size.Height));
            }
            else
            {
                if (begin.line == end.line) p.AddLast(new PointF(0, (end.line + 1) * _font.Size.Height)); // bottom left
            }
            p.AddLast(new PointF(begin.col * _font.Size.Width, begin.line * _font.Size.Height)); // upper left

            if (begin.line != end.line)
            {
                p.AddFirst(new PointF(0, (end.line + 1) * _font.Size.Height));
                p.AddLast(new PointF(_chars._size.col * _font.Size.Width, begin.line * _font.Size.Height));
            }

            if (end.col != _chars._size.col - 1)
            {
                if (begin.line != end.line) p.AddLast(new PointF(_chars._size.col * _font.Size.Width, end.line * _font.Size.Height));
                p.AddLast(new PointF((end.col + 1) * _font.Size.Width, end.line * _font.Size.Height));
            }
            else
            {
                if (begin.line == end.line) p.AddLast(new PointF(_chars._size.col * _font.Size.Width, begin.line * _font.Size.Height)); // upper right
            }
            p.AddLast(new PointF((end.col + 1) * _font.Size.Width, (end.line + 1) * _font.Size.Height)); // bottom right


            {
                _polygon = new PointF[p.Count];
                int i = 0;
                foreach (PointF pf in p)
                {
                    _polygon[i++] = pf;
                }
            }

            invalidate(begin.line, end.line, 0, _chars._size.col);
        }

        private void SelectRange(out TerminalChars.Cursor begin, out TerminalChars.Cursor end)
        {
            if (_begin.GreaterThan(_end))
            {
                begin = _end;
                end = _begin;
            }
            else
            {
                begin = _begin;
                end = _end;
            }
            if (_chars[begin].Partial) begin.col -= 1;
            if (_chars[end].DoubleWidth) end.col += 1;
        }

        private void TerminalEmulatorWithCopyPaste_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != 0)
            {
                _end = location(e.X, e.Y);
                if (!_begin.Equals(_end))
                {
                    copyToolStripMenuItem.Enabled = true;
                    CaculateRegion();
                }
            }
        }

        private void TerminalEmulatorWithCopyPaste_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != 0)
            {
                if (_polygon != null)
                {
                    copyToolStripMenuItem.Enabled = false;
                    _polygon = null;
                    invalidate(_begin.line, _end.line, 0, _chars._size.col);
                }
                _begin = location(e.X, e.Y);
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TerminalChars.Cursor begin;
            TerminalChars.Cursor end;
            SelectRange(out begin, out end);
            string s = string.Empty;
            for (TerminalChars.Cursor c = begin; !c.GreaterThan(end) && !(c.line == _chars._size.line - 1 && c.col == _chars._size.col - 1); _chars.advanceVisible(ref c))
            {
                s += _chars[c].Char;
                if (c.col + 1 == _chars._size.col)
                {
                    s.TrimEnd(new char[] { ' ' });
                    s += "\r\n";
                }
            }
            Clipboard.SetText(s);
            copyToolStripMenuItem.Enabled = false;
            _polygon = null;
            invalidate(_begin.line, _end.line, 0, _chars._size.col);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSendString(Clipboard.GetText());
        }

        private TerminalChars.Cursor _begin;
        private TerminalChars.Cursor _end;
        private PointF[] _polygon;

        private void TerminalEmulatorWithCopyPaste_Paint(object sender, PaintEventArgs e)
        {
            if (_polygon != null)
            {
                using (Pen br = new Pen(Color.LightYellow))
                {
                    e.Graphics.DrawPolygon(br, _polygon);
                }
            }
        }
    }
}
