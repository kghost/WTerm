using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FiniteStateMachine
{
    internal class lex
    {
        internal static StateMachine Parse(System.IO.TextReader terminfo)
        {
            //return (StateMachine)new yy().yyparse(new input(terminfo), new yydebug.yyDebugSimple(), new Dictionary<string, StateMachine>());
            return (StateMachine)new yy().yyparse(new input(terminfo), new Dictionary<string, StateMachine>());
        }

        internal class input : yyParser.yyInput
        {
            public input(System.IO.TextReader s)
            {
                cs = s;
            }

            public bool advance()
            {
                int i = cs.Read();
                if (i == -1) return false;
                switch ((char)i)
                {
                    case '\r':
                        {
                            int j = cs.Read();
                            if (j == '\n')
                            {
                                tok = Token.E;
                                current = null;
                            }
                            else
                            {
                                throw new Exception("Meet \r without \n.");
                            }
                        }
                        break;
                    case '\n':
                        tok = Token.E;
                        current = null;
                        break;
                    case '\\':
                        {
                            tok = Token.C;
                            int j = cs.Read();
                            switch ((char)j)
                            {
                                case 'E':
                                    current = '\x1B';
                                    break;
                                case 'n':
                                    current = '\n';
                                    break;
                                case 'r':
                                    current = '\r';
                                    break;
                                case 'b':
                                    current = '\b';
                                    break;
                                case 'd':
                                    current = (char)127;
                                    break;
                                default:
                                    current = (char)j;
                                    break;
                            }
                        }
                        break;
                    case '$':
                        tok = Token.DL;
                        current = null;
                        break;
                    case ':':
                        tok = Token.CL;
                        current = null;
                        break;
                    case '(':
                        tok = Token.L;
                        current = null;
                        break;
                    case ')':
                        tok = Token.R;
                        current = null;
                        break;
                    case '|':
                        tok = Token.O;
                        current = null;
                        break;
                    case '*':
                        tok = Token.S;
                        current = null;
                        break;
                    case '{':
                        tok = Token.LS;
                        current = null;
                        break;
                    case '}':
                        tok = Token.RS;
                        current = null;
                        break;
                    case '[':
                        tok = Token.LC;
                        current = null;
                        break;
                    case ']':
                        tok = Token.RC;
                        current = null;
                        break;
                    case '.':
                        tok = Token.D;
                        current = null;
                        break;
                    default:
                        tok = Token.C;
                        current = (char)i;
                        break;
                }
                return true;
            }

            public int token()
            {
                return tok;
            }

            public Object value()
            {
                return current;
            }

            private int tok;
            private Object current;
            private System.IO.TextReader cs;
        }
    }
}
