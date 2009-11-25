using System;
using System.Collections.Generic;

namespace TermType
{
    public interface ITermType
    {
        string CharMap(char c);
        string KeyMap(System.Windows.Forms.Keys c);
        void PutChar(char c);
        void PutString(string c);
    }
}
