using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WTerm
{
    public partial class ProgramDlg : Form
    {
        public ProgramDlg(Execute e)
        {
            InitializeComponent();
            Proc.Text = e.Exec;
            Args.Text = e.Args;
        }

        public Execute GetExecute()
        {
            Execute e = new Execute();
            e.Exec = Proc.Text;
            e.Args = Args.Text;
            return e;
        }

        private void OK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() != DialogResult.Cancel)
                {
                    Proc.Text = dlg.FileName;
                }
            }
        }
    }
}
