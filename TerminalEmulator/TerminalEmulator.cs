using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;

namespace TerminalEmulator
{
    public partial class TerminalEmulator : UserControl, Terminal.ITerminalEmulator
    {
        public TerminalEmulator()
            : this(80, 24)
        {
        }

        public TerminalEmulator(int cols, int lines)
        {
            _inv_lock = new object();
            _font = new FontGroup(new Font[] { new UnicodeFont(new System.Drawing.Font("Consolas", 16.0F), new PointF()) }, new SizeF(10.0F, 20.0F));
            _chars = new TerminalChars(this, cols, lines);
            SetStyle(ControlStyles.ResizeRedraw, true);
            DoubleBuffered = true;
            InitializeComponent();
        }

        private void CaculateSize()
        {
            SizeF n = new SizeF(Font.Size.Width * cols, Font.Size.Height * lines);
            if (Math.Abs(Size.Width - n.Width) > 5 || Math.Abs(Size.Height - n.Height) > 5)
            {
                base.Size = new Size((int)Math.Ceiling(n.Width), (int)Math.Ceiling(n.Height));
                Invalidate();
            }
        }

        private void TerminalEmulator_Load(object sender, EventArgs e)
        {
            CaculateSize();
        }

        internal void invalidate(TerminalChars.Cursor pos)
        {
            invalidate(pos.line, pos.col);
        }

        internal void invalidate(int line, int col)
        {
            invalidate(line, line, col, col);
        }

        internal void invalidate(int begin_line, int end_line, int begin_col, int end_col)
        {
            lock (_inv_lock)
            {
                if (_inv == null) _inv = new Region();
                _inv.Union(new RectangleF(begin_col * Font.Size.Width, begin_line * Font.Size.Height, (end_col - begin_col + 1) * Font.Size.Width, (end_line - begin_line + 1) * Font.Size.Height));
            }
        }

        private void DelayPaint_Tick(object sender, EventArgs e)
        {
            if (Monitor.TryEnter(_inv_lock))
            {
                try
                {
                    if (_inv != null)
                    {
                        Invalidate(_inv);
                        _inv.Dispose();
                        _inv = null;
                    }
                }
                finally
                {
                    Monitor.Exit(_inv_lock);
                }
            }
        }

        private void TerminalEmulator_Paint(object sender, PaintEventArgs e)
        {
            int begin_line = (int)Math.Floor(e.ClipRectangle.Top / Font.Size.Height);
            int end_line = (int)Math.Ceiling(e.ClipRectangle.Bottom / Font.Size.Height);
            int begin_col = (int)Math.Floor(e.ClipRectangle.Left / Font.Size.Width);
            int end_col = (int)Math.Ceiling(e.ClipRectangle.Right / Font.Size.Width);

            drawChars(e.Graphics, begin_line, end_line, begin_col, end_col);

            if (_blink && cursor.line >= begin_line && cursor.line <= end_line && cursor.col >= begin_col && cursor.col <= end_col)
            {
                using (Brush br = new SolidBrush(Color.Yellow))
                {
                    e.Graphics.FillRectangle(br, new RectangleF(new PointF(cursor.col * Font.Size.Width, cursor.line * Font.Size.Height), Font.Size));
                }
            }
        }

        protected virtual void drawChars(Graphics graphics, int begin_line, int end_line, int begin_col, int end_col)
        {
            for (int i = begin_line; i <= end_line && i < lines; i++)
            {
                for (int j = begin_col; j <= end_col && j < cols; j++)
                {
                    if (_chars[i, j] != null)
                    {
                        DrawChar(_chars, graphics, i, j, j == begin_col);
                    }
                }
            }
        }

        protected void DrawChar(TerminalChars chars, Graphics graphics, int i, int j, bool draw_second_part)
        {
            if (chars[i, j].DoubleWidth) // a width char
            {
                SizeF t = Font.Size;
                t.Width *= 2;
                RectangleF rec = new RectangleF(new PointF(j * Font.Size.Width, i * Font.Size.Height), t);
                chars[i, j].Draw(graphics, Font, rec);
            }
            else if (draw_second_part && chars[i, j].Partial) // second part of width char
            {
                SizeF t = Font.Size;
                t.Width *= 2;
                RectangleF rec = new RectangleF(new PointF((j - 1) * Font.Size.Width, i * Font.Size.Height), t);
                chars[i, j - 1].Draw(graphics, Font, rec);
            }
            else // a normal char
            {
                RectangleF rec = new RectangleF(new PointF(j * Font.Size.Width, i * Font.Size.Height), Font.Size);
                chars[i, j].Draw(graphics, Font, rec);
            }
        }

        protected virtual TerminalChars.Cursor cursor
        {
            get { return _chars.cursor; }
        }

        public int cols
        {
            get
            {
                return _chars._size.col;
            }
            set
            {
                _chars = new TerminalChars(this, value, lines, _chars);
                CaculateSize();
            }
        }

        public int lines
        {
            get
            {
                return _chars._size.line;
            }
            set
            {
                _chars = new TerminalChars(this, cols, value, _chars);
                CaculateSize();
            }
        }

        private void Blink_Tick(object sender, EventArgs e)
        {
            // Blink cursor.
            _blink = !_blink;
            invalidate(_old_cursor);
            invalidate(cursor);
            _old_cursor = cursor;
        }

        public class TerminalChars
        {
            TerminalEmulator _parent;
            internal Cursor _size;
            internal Cursor _cursor;
            internal TerminalChar[,] _char;

            internal bool am = true;

            public Color fg;
            public Color bg;

            public struct Cursor
            {
                public Cursor(int l, int c)
                {
                    line = l;
                    col = c;
                }

                public override int GetHashCode()
                {
                    return line.GetHashCode() + col.GetHashCode();
                }

                public bool GreaterThan(Cursor rhs)
                {
                    return line > rhs.line || (line == rhs.line && col > rhs.col);
                }

                public override bool Equals(Object obj)
                {
                    Cursor rhs = (Cursor)obj;
                    return (line == rhs.line) && (col == rhs.col);
                }

                internal int col;
                internal int line;
            }

            public TerminalChars(TerminalEmulator parent, Color foreground, Color background, int cols, int lines)
            {
                _parent = parent;
                _size.col = cols;
                _size.line = lines;
                _char = new TerminalChar[_size.line, _size.col];

                fg = foreground;
                bg = background;

                clear();
            }

            internal TerminalChars(TerminalEmulator parent, int cols, int lines)
                : this(parent, parent.ForeColor, parent.BackColor, cols, lines)
            { }

            internal TerminalChars(TerminalEmulator parent, int cols, int lines, TerminalChars src)
            {
                _parent = parent;
                _size.col = cols;
                _size.line = lines;
                _char = new TerminalChar[_size.line, _size.col];

                am = src.am;

                fg = src.fg;
                bg = src.bg;

                for (int i = 0; i < Math.Min(_size.line, src._size.line); i++)
                {
                    for (int j = 0; j < Math.Min(_size.col, src._size.col); j++)
                    {
                        _char[i, j] = src._char[i, j];
                    }
                    for (int j = src._size.col; j < _size.col; j++)
                    {
                        _char[i, j] = new TerminalCharSpace(bg);
                    }
                }
                for (int i = src._size.line; i < _size.line; i++)
                {
                    for (int j = 0; j < _size.col; j++)
                    {
                        _char[i, j] = _char[i, j] = new TerminalCharSpace(bg);
                    }
                }
            }

            internal TerminalChar this[int line, int col]
            {
                get
                {
                    return _char[line, col];
                }
                set
                {
                    int l = col, r = col;
                    if (!value.Partial && _char[line, col].Partial)
                    {
                        _char[line, col - 1] = new TerminalCharSpace(((TerminalCharSpace)_char[line, col - 1]).bg);
                        l = col - 1;
                    }
                    if (_char[line, col].DoubleWidth && !value.DoubleWidth)
                    {
                        _char[line, col + 1] = new TerminalCharSpace(((TerminalCharSpace)_char[line, col]).bg);
                        r = col + 1;
                    }
                    _char[line, col] = value;
                    if (value.DoubleWidth)
                    {
                        r = col + 1;
                        if (_char[line, col + 1].DoubleWidth)
                        {
                            _char[line, col + 2] = new TerminalCharSpace(((TerminalCharSpace)_char[line, col + 1]).bg);
                            r = col + 2;
                        }
                        _char[line, col + 1] = TerminalChar.UnVisible;
                    }
                    _parent.invalidate(line, line, l, r);
                }
            }

            internal TerminalChar this[Cursor pos]
            {
                get
                {
                    return this[pos.line, pos.col];
                }
                set
                {
                    this[pos.line, pos.col] = value;
                }
            }

            public Cursor cursor
            {
                get { return _cursor; }
                set { _cursor = value; }
            }

            public void advance(int i)
            {
                advance(ref _cursor, i);
            }

            public void advance(ref Cursor cur, int i)
            {
                if (i > 0)
                {
                    while (i + cur.col >= _size.col)
                    {
                        if (cur.line != _size.line - 1)
                        {
                            i -= _size.col - cur.col;
                            cur.col = 0;
                            if (++cur.line == _size.line) --cur.line;
                        }
                        else
                        {
                            cur.col = 0;
                            i = 0;
                        }
                    }
                }
                else if (i < 0)
                {
                    while (i + cur.col < 0)
                    {
                        if (cur.line != 0)
                        {
                            cur.line--;
                            i += (cur.col + 1);
                            cur.col = _size.col - 1;
                        }
                        else
                        {
                            cur.col = 0;
                            i = 0;
                        }
                    }
                }
                cur.col += i;
            }

            public void advanceVisible(ref Cursor cur)
            {
                for (advance(ref cur, 1); this[cur] != null && this[cur].Partial; advance(ref cur, 1)) ;
            }

            public void advanceVisible()
            {
                advanceVisible(ref _cursor);
            }

            public void put(char c, int attribute)
            {
                TerminalCharVisible tc = new TerminalCharVisible(c, 0, fg, bg);
                if (_cursor.col + (tc.DoubleWidth ? 2 : 1) > _size.col)
                {
                    if (!am) return;
                    ret();
                    newline();
                }
                this[_cursor.line, _cursor.col++] = tc;
                if (tc.DoubleWidth) _cursor.col++;
            }

            public void clear()
            {
                for (int i = 0; i < _size.line; i++)
                {
                    for (int j = 0; j < _size.col; j++)
                    {
                        _char[i, j] = _char[i, j] = new TerminalCharSpace(bg);
                    }
                }
                _parent.invalidate(0, _size.line - 1, 0, _size.col - 1);
            }

            internal void newline()
            {
                // TODO : newline type, scroll or back
                if (++_cursor.line == _size.line)
                {
                    --_cursor.line;
                    for (int i = 0; i < _size.line - 1; i++)
                    {
                        for (int j = 0; j < _size.col; j++)
                        {
                            this[i, j] = this[i + 1, j];
                        }
                    }
                    for (int j = 0; j < _size.col; j++)
                    {
                        this[_size.line - 1, j] = new TerminalCharSpace(bg);
                    }
                }
            }

            internal void ret()
            {
                _cursor.col = 0;
            }

            internal void ed(int mode)
            {
                switch (mode)
                {
                    case 0:
                        for (int i = _cursor.line + 1; i < _size.line; i++)
                            for (int j = 0; j < _size.col; j++)
                                this[i, j] = new TerminalCharSpace(bg);
                        el(0);
                        break;
                    case 1:
                        for (int i = 0; i < _cursor.line; i++)
                            for (int j = 0; j < _size.col; j++)
                                this[i, j] = new TerminalCharSpace(bg);
                        el(1);
                        break;
                    case 2:
                        for (int i = 0; i < _size.line; i++)
                            for (int j = 0; j < _size.col; j++)
                                this[i, j] = new TerminalCharSpace(bg);
                        break;
                }
            }

            internal void el(int mode)
            {
                switch (mode)
                {
                    case 0:
                        for (int i = _cursor.col; i < _size.col; i++)
                        {
                            this[_cursor.line, i] = new TerminalCharSpace(bg);
                        }
                        break;
                    case 1:
                        for (int i = 0; i < _cursor.col; i++)
                        {
                            this[_cursor.line, i] = new TerminalCharSpace(bg);
                        }
                        break;
                    case 2:
                        for (int i = 0; i < _size.col; i++)
                        {
                            this[_cursor.line, i] = new TerminalCharSpace(bg);
                        }
                        break;
                }
            }

            internal void mv(int line, int col)
            {
                if (line >= 0 && line < _size.line && col >= 0 && col <= _size.col)
                {
                    _cursor.line = line;
                    _cursor.col = col;
                }
            }

            internal void del()
            {
                this[_cursor] = new TerminalCharSpace(bg);
            }
        }

        #region Terminal Interface
        public void PutChar(char c, int attribute)
        {
            _chars.put(c, attribute);
        }

        public void PutNewLine()
        {
            _chars.newline();
        }

        public void PutCarriageRet()
        {
            _chars.ret();
        }

        public void EarseDisplay(int mode)
        {
            _chars.ed(mode);
        }

        public void EarseLine(int mode)
        {
            _chars.el(mode);
        }

        public void MoveCursor(int line, int col)
        {
            _chars.mv(line, col);
        }

        public void DelChar()
        {
            _chars.del();
        }

        public void BackSpace()
        {
            _chars.advance(-1);
            _chars.del();
        }

        public bool Am
        {
            get { return _chars.am; }
            set { _chars.am = value; }
        }
        #endregion

        public delegate void SendChar(char c);
        public event SendChar OnSendChar;
        public delegate void SendCode(Keys c);
        public event SendCode OnSendCode;
        public delegate void SendString(string c);
        public event SendString OnSendString;

        public Color fg
        {
            get { return _chars.fg; }
            set { _chars.fg = value; }
        }

        public Color bg
        {
            get { return _chars.bg; }
            set { _chars.bg = value; }
        }

        public Color default_fg { get { return ForeColor; } }
        public Color default_bg { get { return BackColor; } }

        public override Color BackColor { set { base.BackColor = value; bg = base.BackColor; _chars.clear(); } }
        public override Color ForeColor { set { base.ForeColor = value; fg = base.ForeColor; _chars.clear(); } }
        public new Size Size { get { return base.Size; } set { } }

        public new FontGroup Font
        {
            get { return _font; }
            set { _font = value; CaculateSize(); Invalidate(); }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            keyData &= Keys.KeyCode;
            return base.IsInputKey(keyData) || keyData == Keys.Tab || (keyData >= Keys.Left && keyData <= Keys.Down);
        }

        private void TerminalEmulator_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyRepeat.Enabled = false;
            if (OnSendChar != null) OnSendChar(e.KeyChar);
        }

        private void TerminalEmulator_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.None)
            {
                KeyRepeat.Enabled = true;
                KeyRepeat.Interval = 500;
                _repeat_key = e.KeyCode;
                DoSendCode(_repeat_key);
            }
        }

        private void KeyRepeat_Tick(object sender, EventArgs e)
        {
            KeyRepeat.Interval = 200;
            DoSendCode(_repeat_key);
        }

        protected virtual void TerminalEmulator_KeyUp(object sender, KeyEventArgs e)
        {
            KeyRepeat.Enabled = false;
        }

        protected virtual void DoSendCode(Keys c)
        {
            if (OnSendCode != null) OnSendCode(c);
        }

        protected virtual void DoSendString(string c)
        {
            if (OnSendString != null) OnSendString(c);
        }

        private Keys _repeat_key;
        protected TerminalChars _chars;
        protected FontGroup _font;
        private bool _blink;
        private Region _inv;
        private Object _inv_lock;
        private TerminalChars.Cursor _old_cursor;
    }
}
