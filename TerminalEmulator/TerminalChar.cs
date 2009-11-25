using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace TerminalEmulator
{
    public abstract class TerminalChar
    {
        internal abstract void Draw(System.Drawing.Graphics graphics, FontGroup font, System.Drawing.RectangleF rec);
        internal abstract char Char { get; }
        internal abstract bool DoubleWidth { get; }
        internal virtual bool Partial
        {
            get
            {
                return false;
            }
        }

        internal static TerminalChar UnVisible
        {
            get
            {
                if (_unVisible == null)
                {
                    _unVisible = new TerminalCharUnVisible();
                }
                return _unVisible;
            }
        }

        private static TerminalChar _unVisible;
    }

    public class TerminalCharSpace : TerminalChar
    {
        public TerminalCharSpace(Color bg)
        {
            bgcolor = bg;
        }

        internal override void Draw(System.Drawing.Graphics graphics, FontGroup font, System.Drawing.RectangleF rec)
        {
            if (!br.ContainsKey(bgcolor))
                br[bgcolor] = new SolidBrush(bgcolor);
            graphics.FillRectangle(br[bgcolor], rec);
        }

        internal override char Char { get { return ' '; } }

        internal override bool DoubleWidth
        {
            get
            {
                return false;
            }
        }

        internal Color bg { get { return bgcolor; } }

        private Color bgcolor;
        private static Dictionary<Color, Brush> br = new Dictionary<Color, Brush>();
    }

    public class TerminalCharVisible : TerminalCharSpace
    {
        public TerminalCharVisible(char c, int attr, Color fg, Color bg)
            : base(bg)
        {
            value = c;
            attribute = attr;
            fgcolor = fg;
        }

        static TerminalCharVisible()
        {
            format = StringFormat.GenericTypographic;
            format.FormatFlags &= ~StringFormatFlags.LineLimit;
            format.FormatFlags |= StringFormatFlags.NoWrap;
        }

        internal override void Draw(System.Drawing.Graphics graphics, FontGroup font, System.Drawing.RectangleF rec)
        {
            base.Draw(graphics, font, rec);
            if (!br.ContainsKey(fgcolor))
                br[fgcolor] = new SolidBrush(fgcolor);
            foreach (Font f in font.Fonts)
            {
                if (f.Accept(value))
                {
                    rec.X += f.Offset.X;
                    rec.Y += f.Offset.Y;
                    graphics.DrawString(value.ToString(), f.GetFont, br[fgcolor], rec, format);
                    break;
                }
            }
        }

        internal override char Char { get { return value; } }

        internal override bool DoubleWidth
        {
            get
            {
                return CharWidth.Instance.DoubleWidth(value) != 0;
            }
        }

        private Color fgcolor;
        private int attribute; // underline reverse dim bold invis protect altcharset
        private char value; // unicode
        private static Dictionary<Color, Brush> br = new Dictionary<Color, Brush>();
        private static StringFormat format;
    }

    public class TerminalCharUnVisible : TerminalChar
    {
        internal override void Draw(System.Drawing.Graphics graphics, FontGroup _font, System.Drawing.RectangleF rec)
        {
        }

        internal override char Char { get { throw new NotImplementedException(); } }

        internal override bool DoubleWidth
        {
            get
            {
                return false;
            }
        }

        internal override bool Partial
        {
            get
            {
                return true;
            }
        }
    }
}
