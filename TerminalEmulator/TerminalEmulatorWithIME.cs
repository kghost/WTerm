using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TerminalEmulator
{
    public partial class TerminalEmulatorWithIME : TerminalEmulatorWithCopyPaste
    {
        public TerminalEmulatorWithIME()
        {
            InitializeComponent();
        }

        private TerminalChars _ime;

        private const int WM_IME_STARTCOMPOSITION = 0x010D;
        private const int WM_IME_ENDCOMPOSITION = 0x010E;
        private const int WM_IME_COMPOSITION = 0x010F;
        private const int WM_IME_CONTROL = 0x0283;

        private const int IMC_GETCANDIDATEPOS = 0x0007;
        private const int IMC_SETCANDIDATEPOS = 0x0008;
        private const int IMC_GETCOMPOSITIONFONT = 0x0009;
        private const int IMC_SETCOMPOSITIONFONT = 0x000A;
        private const int IMC_GETCOMPOSITIONWINDOW = 0x000B;
        private const int IMC_SETCOMPOSITIONWINDOW = 0x000C;
        private const int IMC_GETSTATUSWINDOWPOS = 0x000F;
        private const int IMC_SETSTATUSWINDOWPOS = 0x0010;
        private const int IMC_OPENSTATUSWINDOW = 0x0022;

        private const int CFS_DEFAULT = 0x0000;
        private const int CFS_RECT = 0x0001;
        private const int CFS_POINT = 0x0002;
        private const int CFS_FORCE_POSITION = 0x0020;
        private const int CFS_CANDIDATEPOS = 0x0040;
        private const int CFS_EXCLUDE = 0x0080;

        private const int GCS_COMPREADSTR = 0x0001;
        private const int GCS_COMPREADATTR = 0x0002;
        private const int GCS_COMPREADCLAUSE = 0x0004;
        private const int GCS_COMPSTR = 0x0008;
        private const int GCS_COMPATTR = 0x0010;
        private const int GCS_COMPCLAUSE = 0x0020;
        private const int GCS_CURSORPOS = 0x0080;
        private const int GCS_DELTASTART = 0x0100;
        private const int GCS_RESULTREADSTR = 0x0200;
        private const int GCS_RESULTREADCLAUSE = 0x0400;
        private const int GCS_RESULTSTR = 0x0800;
        private const int GCS_RESULTCLAUSE = 0x1000;

        [DllImport("Imm32.dll")]
        private static extern IntPtr ImmGetContext(IntPtr hWnd);

        [DllImport("Imm32.dll", EntryPoint = "ImmGetCompositionStringW")]
        private static extern int ImmGetCompositionString(IntPtr hIMC, int dwIndex, byte[] lpBuf, int dwBufLen);

        [DllImport("Imm32.dll")]
        private static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);

        [DllImport("Imm32.dll")]
        private static extern bool ImmSetCandidateWindow(IntPtr hIMC, ref CANDIDATEFORM lpCandidate);

        protected override void drawChars(Graphics graphics, int begin_line, int end_line, int begin_col, int end_col)
        {
            base.drawChars(graphics, begin_line, end_line, begin_col, end_col);

            if (_ime != null)
                for (int i = begin_line; i <= end_line && i < lines; i++)
                {
                    for (int j = begin_col; j <= end_col && j < cols; j++)
                    {
                        DrawChar(_ime, graphics, i, j, j == begin_col);
                    }
                }
        }

        protected override TerminalEmulator.TerminalChars.Cursor cursor
        {
            get { return (_ime == null) ? _chars.cursor : _ime.cursor; }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct CANDIDATEFORM
        {
            [MarshalAs(UnmanagedType.U4)]
            internal UInt32 dwIndex;
            [MarshalAs(UnmanagedType.U4)]
            internal UInt32 dwStyle;
            [MarshalAs(UnmanagedType.Struct)]
            internal Point ptCurrentPos;
            [MarshalAs(UnmanagedType.Struct)]
            internal Rectangle rcArea;
        }

        protected override void TerminalEmulator_KeyUp(object sender, KeyEventArgs e)
        {
            if (_ime == null)
                base.TerminalEmulator_KeyUp(sender, e);
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_IME_STARTCOMPOSITION:
                    _ime = new TerminalChars(this, Color.Red, Color.Transparent, cols, lines);
                    return;
                case WM_IME_ENDCOMPOSITION:
                    _ime = null;
                    Invalidate();
                    return;
                case WM_IME_COMPOSITION:
                    if (_ime != null)
                    {
                        IntPtr hIMC = ImmGetContext(Handle);
                        if ((((int)m.LParam) & GCS_COMPSTR) != 0)
                        {
                            int dwSize = ImmGetCompositionString(hIMC, GCS_COMPSTR, null, 0);
                            byte[] bs = new byte[dwSize];
                            ImmGetCompositionString(hIMC, GCS_COMPSTR, bs, bs.Length);
                            string str = UnicodeEncoding.Unicode.GetString(bs);
                            _ime.clear();
                            _ime.cursor = _chars._cursor;
                            _ime.bg = Color.Black;
                            _ime.fg = Color.Red;
                            foreach (char c in str)
                            {
                                _ime.put(c, 0);
                            }
                            _ime.bg = Color.Transparent;
                            _ime.fg = Color.Red;
                        }

                        if ((((int)m.LParam) & GCS_CURSORPOS) != 0)
                        {
                            _ime.cursor = _chars._cursor;
                            for (int pos = ImmGetCompositionString(hIMC, GCS_CURSORPOS, null, 0); pos != 0; --pos)
                            {
                                _ime.advanceVisible();
                            }
                        }

                        CANDIDATEFORM l = new CANDIDATEFORM();
                        l.dwIndex = 0;
                        l.dwStyle = CFS_CANDIDATEPOS;
                        l.ptCurrentPos.X = (int)(10 + _ime.cursor.col * Font.Size.Width);
                        l.ptCurrentPos.Y = (int)(10 + _ime.cursor.line * Font.Size.Height);
                        ImmSetCandidateWindow(hIMC, ref l);

                        ImmReleaseContext(Handle, hIMC);
                    }
                    base.WndProc(ref m);
                    return;
                default:
                    base.WndProc(ref m);
                    return;
            }
        }

        protected override bool CanEnableIme { get { return true; } }
    }
}
