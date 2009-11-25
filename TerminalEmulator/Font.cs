using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml.Serialization;

namespace TerminalEmulator
{
    public class FontGroup
    {
        private FontGroup() { }
        public FontGroup(Font[] fonts, System.Drawing.SizeF size)
        {
            foreach (Font f in fonts) _fonts.Add(f);
            Size = size;
        }

        public List<Font> Fonts
        {
            get { return _fonts; }
            set { _fonts = value; }
        }

        public System.Drawing.SizeF Size
        {
            get { return _size; }
            set { _size = value; }
        }

        private List<Font> _fonts = new List<Font>();
        private System.Drawing.SizeF _size;
    }

    [XmlInclude(typeof(LatinFont))]
    [XmlInclude(typeof(UnicodeFont))]
    abstract public class Font
    {
        protected Font() { }
        protected Font(System.Drawing.Font Font, System.Drawing.PointF Offset) { _font = Font; _offset = Offset; }
        public abstract bool Accept(char c);
        public System.Drawing.Font GetFont { get { return _font; } }
        public System.Drawing.PointF Offset { get { return _offset; } set { _offset = value; } }

        public string FontFamily { get { return _font.FontFamily.Name.ToString(); } set { _font = new System.Drawing.Font(value, _font == null ? 8 : _font.Size); } }
        public float FontSize { get { return _font.Size; } set { _font = new System.Drawing.Font(_font == null ? String.Empty : _font.FontFamily.Name.ToString(), value); } }

        protected System.Drawing.Font _font;
        protected System.Drawing.PointF _offset;
    }

    public class LatinFont : Font
    {
        private LatinFont() { }
        public LatinFont(System.Drawing.Font Font, System.Drawing.PointF Offset) : base(Font, Offset) { }
        public override bool Accept(char c) { return c > 0 && c <= 0x7F; }
    }

    public class UnicodeFont : Font
    {
        private UnicodeFont() { }
        public UnicodeFont(System.Drawing.Font Font, System.Drawing.PointF Offset) : base(Font, Offset) { }
        public override bool Accept(char c) { return true; }
    }
}
