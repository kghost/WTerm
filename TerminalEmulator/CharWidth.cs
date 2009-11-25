using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace TerminalEmulator
{
    class CharWidth
    {
        public CharWidth()
        {
            FileStream ms = new FileStream("EastAsianWidth.gzip", FileMode.Open, FileAccess.Read, FileShare.Read);
            GZipStream zipStream = new GZipStream(ms, CompressionMode.Decompress);
            decompressedBuffer = new byte[229500];
            int offset = 0;
            int totalCount = 0;
            while (true)
            {
                int bytesRead = zipStream.Read(decompressedBuffer, offset, 229500 - offset);
                if (bytesRead == 0)
                {
                    break;
                }
                offset += bytesRead;
                totalCount += bytesRead;
            }
            zipStream.Close();
        }

        public int DoubleWidth(int c)
        {
            if (c > 917999)
                return 2; // Ambiguous
            else
            {
                return (decompressedBuffer[c / 4] & (3 << (2 * (c % 4)))) >> (2 * (c % 4));
            }
        }

        public static CharWidth Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CharWidth();
                return _instance;
            }
        }

        private byte[] decompressedBuffer;
        private static CharWidth _instance = null;

    }
}
