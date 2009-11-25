using System;

namespace WTerm
{
    public class Execute
    {
        public string[] EnvKey { get { return _env_key; } set { _env_key = value; } }
        public string[] EnvVal { get { return _env_val; } set { _env_val = value; } }
        public string Exec { get { return _exec; } set { _exec = value; } }
        public string Args { get { return _args; } set { _args = value; } }

        private string[] _env_key;
        private string[] _env_val;
        private string _exec;
        private string _args;
    }

    public class Configure
    {
        public TerminalEmulator.FontGroup FontGroup { get { return _fg; } set { _fg = value; } }
        public string Encoding { get { return _encoding; } set { _encoding = value; } }
        public string TermType { get { return _term; } set { _term = value; } }
        public Execute Program { get { return _program; } set { _program = value; } }

        private TerminalEmulator.FontGroup _fg;
        private string _encoding;
        private string _term;
        private Execute _program;
    }
}
