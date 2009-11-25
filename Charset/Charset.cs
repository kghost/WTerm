using System;

namespace Charset
{
    public class Charset
    {
        public delegate void GetCharCallback(char c);
    }

    public interface ICharset
    {
        void Feed(byte b, Charset.GetCharCallback callback, Charset.GetCharCallback error);
        byte[] GetBytes(string s);
    }
}
