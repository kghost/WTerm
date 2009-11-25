using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Xterm
{
    public class Xterm : TermType.ITermType
    {
        public Xterm(Terminal.ITerminalEmulator t)
        {
            term = t;
            term.Am = true;
            param = new Stack<int>();
            input = new Input(this);
        }

        public void PutChar(char c)
        {
            lock (this)
            {
            retry:
                try
                {
                    input.Feed(c);
                }
                catch (Input.ParseException)
                {
                    param.Clear();
                    input.Reset();
                    term.PutChar('E', 0);
                    goto retry;
                }
            }
        }

        public void PutString(string s)
        {
            lock (this)
            {
                foreach (char c in s)
                    PutChar(c);
            }
        }

        public string CharMap(char c)
        {
            switch (c)
            {
                case '\r':
                    return "\n";
                default:
                    return c.ToString();
            }
        }

        public string KeyMap(Keys c)
        {
            switch (c)
            {
                case Keys.F1: return "\x1BOP";
                case Keys.F2: return "\x1BOQ";
                case Keys.F3: return "\x1BOR";
                case Keys.F4: return "\x1BOS";
                case Keys.F5: return "\x1B[15~";
                case Keys.F6: return "\x1B[17~";
                case Keys.F7: return "\x1B[18~";
                case Keys.F8: return "\x1B[19~";
                case Keys.F9: return "\x1B[20~";
                case Keys.F10: return "\x1B[21~";
                case Keys.F11: return "\x1B[23~";
                case Keys.F12: return "\x1B[24~";
                case Keys.F13: return "\x1BO2P";
                case Keys.F14: return "\x1BO2Q";
                case Keys.F15: return "\x1BO2R";
                case Keys.F16: return "\x1BO2S";
                case Keys.F17: return "\x1B[15;2~";
                case Keys.F18: return "\x1B[17;2~";
                case Keys.F19: return "\x1B[18;2~";
                case Keys.F20: return "\x1B[19;2~";
                case Keys.F21: return "\x1B[20;2~";
                case Keys.F22: return "\x1B[21;2~";
                case Keys.F23: return "\x1B[23;2~";
                case Keys.F24: return "\x1B[24;2~";
                //case Keys.kDC: return "\x1B[3;2~";
                //case Keys.kEND: return "\x1B[1;2F";
                //case Keys.kHOM: return "\x1B[1;2H";
                //case Keys.kIC: return "\x1B[2;2~";
                //case Keys.kLFT: return "\x1B[1;2D";
                //case Keys.kNXT: return "\x1B[6;2~";
                //case Keys.kPRV: return "\x1B[5;2~";
                //case Keys.kRIT: return "\x1B[1;2C";
                //case Keys.kb2: return "\x1BOE";
                //case Keys.kcbt: return "\x1B[Z";
                case Keys.Left: return "\x1BOD";
                case Keys.Down: return "\x1BOB";
                case Keys.Right: return "\x1BOC";
                case Keys.Up: return "\x1BOA";
                case Keys.End: return "\x1B[4~";
                //case Keys.kent: return "\x1BOM";
                case Keys.Home: return "\x1B[1~";
                case Keys.Insert: return "\x1B[2~";
                case Keys.Delete: return "\x1B[3~";
                //case Keys.kmous: return "\x1B[M";
                case Keys.PageDown: return "\x1B[6~";
                case Keys.PageUp: return "\x1B[5~";
                default: return null;
            }
        }

        internal Terminal.ITerminalEmulator term;
        internal Input input;
        internal Stack<int> param;

        internal Color default_fg { get { return term.default_fg; } }
        internal Color default_bg { get { return term.default_bg; } }
    }
}
