using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;

namespace WTerm
{
    class ProcessPipe
    {
        public ProcessPipe(string Name)
            : this(Name, null)
        { }

        public ProcessPipe(string Name, string Argv)
        {
            name = Name;
            argv = Argv;
        }

        public bool Start()
        {
            p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.FileName = name;
            p.StartInfo.Arguments = argv;
            p.EnableRaisingEvents = true;
            try
            {
                if (p.Start())
                {
                    ThreadStart threadDelegate = new ThreadStart(new Pipe(p.StandardOutput, onOutRecv).Do);
                    Thread newThread = new Thread(threadDelegate);
                    newThread.Name = name + " Output";
                    newThread.Start();
                    threadDelegate = new ThreadStart(new Pipe(p.StandardError, onErrRecv).Do);
                    newThread = new Thread(threadDelegate);
                    newThread.Name = name + " Error";
                    newThread.Start();
                    return true;
                }
                else
                    return false;
            }
            catch (System.ComponentModel.Win32Exception)
            {
                return false;
            }
        }

        private class Pipe
        {
            public Pipe(StreamReader S, RecvHandler H)
            {
                s = S;
                h = H;
            }

            public void Do()
            {
                for (int b = s.BaseStream.ReadByte(); b != -1; b = s.BaseStream.ReadByte())
                {
                    if (h != null) h((byte)b);
                }
            }

            private StreamReader s;
            private RecvHandler h;
        }

        public void Stop()
        {
            try
            {
                for (; !p.HasExited; p.Kill()) ;
            }
            catch (InvalidOperationException)
            { }
        }

        public delegate void RecvHandler(byte b);
        public event RecvHandler onOutRecv;
        public event RecvHandler onErrRecv;
        public void Send(byte[] bs)
        {
            p.StandardInput.BaseStream.Write(bs, 0, bs.Length);
            p.StandardInput.BaseStream.Flush();
        }

        public event EventHandler Exited
        {
            add { p.Exited += new EventHandler(value); }
            remove { p.Exited -= new EventHandler(value); }
        }

        private string name;
        private string argv;
        private System.Diagnostics.Process p;
    }
}

