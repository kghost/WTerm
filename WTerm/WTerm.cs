using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace WTerm
{
    public partial class WTerm : Form
    {
        public WTerm()
        {
            InitializeComponent();
        }

        private void WTerm_Load(object sender, EventArgs e)
        {
            try
            {
                config = (Configure)new XmlSerializer(typeof(Configure)).Deserialize(new FileStream(Application.UserAppDataPath + "\\WTerm.config", FileMode.Open));
            }
            catch (Exception)
            {
            }
            finally
            {
                if (config == null) config = new Configure();
                if (config.Program == null)
                {
                    config.Program = new Execute();
                    config.Program.Exec = "netkit_telnet.exe";
                }
                if (config.FontGroup == null) config.FontGroup = new TerminalEmulator.FontGroup(new TerminalEmulator.Font[] { new TerminalEmulator.UnicodeFont(new Font("SimSun", 15.75F), new PointF()) }, new SizeF(10.5F, 21F));
                if (config.TermType == null || config.TermType == string.Empty) config.TermType = "XTerm";
                if (config.Encoding == null || config.Encoding == string.Empty) config.Encoding = "Charset_GBK";
            }

            foreach (Type T in System.Reflection.Assembly.Load(config.Encoding).GetExportedTypes())
            {
                if (T.GetInterface("Charset.ICharset") != null)
                {
                    encoding = (Charset.ICharset)Activator.CreateInstance(T);
                    break;
                }
            }

            foreach (Type T in System.Reflection.Assembly.Load(config.TermType).GetExportedTypes())
            {
                if (T.GetInterface("TermType.ITermType") != null)
                {
                    term = (TermType.ITermType)Activator.CreateInstance(T, new object[] { terminalEmulator1 });
                    break;
                }
            }

            if (encoding == null) encoding = new Charset_GBK.GBK();
            if (term == null) term = new Xterm.Xterm(terminalEmulator1);

            terminalEmulator1.Font = config.FontGroup;
            Size = terminalEmulator1.Size + Size - panel1.Size;

            StartProcess();
        }

        void ProcessExited(object sender, EventArgs e)
        {
            term.PutString(config.Program.Exec + " Exited.\n\r");
            p = null;
        }

        delegate void PutCharCallback(byte b);
        private void PutChar(byte b)
        {
            encoding.Feed(b, new Charset.Charset.GetCharCallback(term.PutChar), null);
        }

        private void StartProcess()
        {
            if (config.Program.Exec != null)
            {
                p = new ProcessPipe(config.Program.Exec, config.Program.Args);
                p.onOutRecv += new ProcessPipe.RecvHandler(PutChar);
                p.onErrRecv += new ProcessPipe.RecvHandler(PutChar);
                if (p.Start())
                {
                    term.PutString(config.Program.Exec + " Started.\n\r");
                    p.Exited += new EventHandler(ProcessExited);
                }
                else
                {
                    term.PutString(config.Program.Exec + " Start failed.\n\r");
                    p = null;
                }
            }
        }

        private void StopProcess()
        {
            if (p != null)
                p.Stop();
        }

        private void terminalEmulator1_OnSendChar(char c)
        {
            if (p != null)
                p.Send(encoding.GetBytes(term.CharMap(c)));
            else
                StartProcess();
        }

        private void terminalEmulator1_OnSendCode(Keys c)
        {
            if (p != null)
                p.Send(encoding.GetBytes(term.KeyMap(c)));
            else
                StartProcess();
        }

        private void terminalEmulator1_OnSendString(string c)
        {
            if (p != null)
                p.Send(encoding.GetBytes(c));
            else
                StartProcess();
        }

        private void WTerm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopProcess();
        }

        private void terminalEmulator1_Resize(object sender, EventArgs e)
        {
            Size = terminalEmulator1.Size + Size - panel1.Size;
        }

        private ProcessPipe p;
        private Charset.ICharset encoding;
        private TermType.ITermType term;

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FontDlg fd = new FontDlg(terminalEmulator1.Font))
            {
                if (fd.ShowDialog() != DialogResult.Cancel)
                {
                    config.FontGroup = fd.GetFontGroup();
                    terminalEmulator1.Font = config.FontGroup;
                }
            }
        }

        private void pluginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FontDlg fd = new FontDlg(config.FontGroup))
            {
                if (fd.ShowDialog() != DialogResult.Cancel)
                {
                    config.FontGroup = fd.GetFontGroup();
                    terminalEmulator1.Font = config.FontGroup;
                }
            }
        }

        private void programToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (ProgramDlg dlg = new ProgramDlg(config.Program))
            {
                if (dlg.ShowDialog() != DialogResult.Cancel)
                {
                    StopProcess();
                    config.Program = dlg.GetExecute();
                    StartProcess();
                }
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox(System.Reflection.Assembly.GetExecutingAssembly());
            about.ShowDialog();
        }

        private void WTerm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                new XmlSerializer(typeof(Configure)).Serialize(new FileStream(Application.UserAppDataPath + "\\WTerm.config", FileMode.Create), config);
            }
            catch (Exception)
            {
            }
        }

        Configure config;
    }
}
