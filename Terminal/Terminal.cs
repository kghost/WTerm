using System;

namespace Terminal
{
    public interface ITerminalEmulator
    {
        void PutCarriageRet();
        void PutNewLine();
        void PutChar(char c, int attribute);
        void EarseDisplay(int mode);
        void EarseLine(int mode);
        void MoveCursor(int line, int col);
        void DelChar();
        void BackSpace();

        bool Am
        {
            get;
            set;
        }

        System.Drawing.Color fg
        {
            get;
            set;
        }

        System.Drawing.Color bg
        {
            get;
            set;
        }

        System.Drawing.Color default_fg
        {
            get;
        }

        System.Drawing.Color default_bg
        {
            get;
        }
    }
}
