using System;
using System.Collections.Generic;
using System.Text;

namespace FiniteStateMachine
{
    public class FiniteStateMachine
    {
        internal class ArrayEqualityComparer<T> : EqualityComparer<T[]>
        {
            public override bool Equals(T[] x, T[] y)
            {
                if (x.Length != y.Length)
                    return false;
                for (int i = 0; i < x.Length; i++)
                {
                    if (!x[i].Equals(y[i]))
                        return false;
                }
                return true;
            }

            public override int GetHashCode(T[] obj)
            {
                int i = 0;
                foreach (T o in obj)
                {
                    i *= 2;
                    i += o.GetHashCode();
                }
                return i;
            }
        }

        internal FiniteStateMachine(StateMachine s)
        {
            int sm = s.states;
            Stack<bool[]> stack = new Stack<bool[]>();
            Dictionary<bool[], int> states = new Dictionary<bool[], int>(new ArrayEqualityComparer<bool>());
            Dictionary<int, Dictionary<char, bool[]>> table = new Dictionary<int, Dictionary<char, bool[]>>();
            Dictionary<int, string> actions = new Dictionary<int, string>();
            {
                bool[] bs = new bool[sm];
                bs[s.start] = true;
                walknull(s, bs, s.start);
                stack.Push(bs);
            }

            while (stack.Count > 0)
            {
                bool[] c = stack.Pop();
                if (!states.ContainsKey(c))
                {
                    int current_state = states.Count;
                    states[c] = current_state;

                    {
                        string action = null;
                        int action_count = 0;
                        bool null_action = false;
                        for (int i = 0; i < c.Length; i++)
                        {
                            if (c[i] && s.actions.ContainsKey(i))
                            {
                                if (s.actions[i] == null)
                                    null_action = true;
                                else
                                {
                                    action_count++;
                                    action = s.actions[i];
                                }
                            }
                        }
                        if ((null_action ? 1 : 0) + action_count > 1) throw new Exception("Action conflict.");
                        if (action_count > 0)
                            actions[current_state] = action;
                    }

                    Dictionary<char, bool[]> charset = new Dictionary<char, bool[]>();
                    table[current_state] = charset;
                    for (int i = 0; i < c.Length; i++)
                    {
                        if (c[i])
                        {
                            walk(charset, s, i);
                        }
                    }

                    foreach (KeyValuePair<char, bool[]> newstate in charset)
                    {
                        stack.Push(newstate.Value);
                    }
                }
            }

            state_map = new FiniteState[states.Count];
            foreach (KeyValuePair<bool[], int> state in states)
            {
                FiniteState entry = new FiniteState(state.Value);
                foreach (int i in s.end)
                {
                    if (state.Key[i] == true)
                    {
                        entry.end = true;
                        break;
                    }
                }
                //if (entry.end) System.Console.Write(state.Value.ToString() + ' ');
                if (actions.ContainsKey(state.Value)) entry.action = actions[state.Value];
                state_map[state.Value] = entry;
            }
            //System.Console.WriteLine();
            foreach (KeyValuePair<int, Dictionary<char, bool[]>> state in table)
            {
                FiniteState entry = state_map[state.Key];
                foreach (KeyValuePair<char, bool[]> vector in state.Value)
                {
                    //System.Console.WriteLine(state.Key.ToString() + ' ' + vector.Key + " -> " + states[vector.Value] + " {" + state_map[states[vector.Value]].action + "}");
                    entry.table[vector.Key] = state_map[states[vector.Value]];
                }
            }
        }

        private static void walk(Dictionary<char, bool[]> charset, StateMachine s, int state)
        {
            for (int i = 0; i < s.states; i++)
            {
                char c = s.table[state, i];
                if (c == 0xFFFF)
                {
                    walk(charset, s, i);
                }
                else if (c != 0)
                {
                    if (!charset.ContainsKey(c))
                    {
                        bool[] bs = new bool[s.states];
                        charset[c] = bs;
                    }

                    bool[] b = charset[c];
                    b[i] = true;
                    walknull(s, b, i);
                }
            }
        }

        private static void walknull(StateMachine s, bool[] b, int state)
        {
            for (int i = 0; i < s.states; i++)
            {
                char c = s.table[state, i];
                if (c == 0xFFFF)
                {
                    b[i] = true;
                    walknull(s, b, i);
                }
            }
        }

        public static FiniteStateMachine Compile(System.IO.TextReader s)
        {
            return new FiniteStateMachine(lex.Parse(s));
        }

        public void GenerateCode(string space)
        {
            System.Console.WriteLine("using System;");
            System.Console.WriteLine();
            System.Console.WriteLine("namespace " + space);
            System.Console.WriteLine("{");
            System.Console.WriteLine("    internal class Input");
            System.Console.WriteLine("    {");
            System.Console.WriteLine();
            System.Console.WriteLine("        internal class ParseException : Exception");
            System.Console.WriteLine("        {");
            System.Console.WriteLine("            internal ParseException(string message) : base(message) { }");
            System.Console.WriteLine("        }");
            System.Console.WriteLine();
            System.Console.WriteLine("        public Input(Object context)");
            System.Console.WriteLine("        {");
            System.Console.WriteLine("            _context = context;");
            System.Console.WriteLine("            _current_state = new State0(_context, (char)0);");
            System.Console.WriteLine("        }");
            System.Console.WriteLine();
            System.Console.WriteLine("        public void Feed(char c)");
            System.Console.WriteLine("        {");
            System.Console.WriteLine("            _current_state = _current_state.Feed(_context, c);");
            System.Console.WriteLine("        }");
            System.Console.WriteLine();
            System.Console.WriteLine("        public void Reset()");
            System.Console.WriteLine("        {");
            System.Console.WriteLine("            _current_state = new State0(_context, (char)0);");
            System.Console.WriteLine("        }");
            System.Console.WriteLine();
            System.Console.WriteLine("        private Object _context;");
            System.Console.WriteLine("        private State _current_state;");
            System.Console.WriteLine();
            System.Console.WriteLine("        public interface State");
            System.Console.WriteLine("        {");
            System.Console.WriteLine("            State Feed(Object context, char c);");
            System.Console.WriteLine("        }");
            System.Console.WriteLine();
            for (int i = 0; i < state_map.Length; i++)
            {
                System.Console.WriteLine("        internal class State" + i.ToString() + " : State");
                System.Console.WriteLine("        {");
                System.Console.WriteLine("            internal State" + i.ToString() + "(Object context, char c)");
                System.Console.WriteLine("            {");
                if (state_map[i].action != null)
                    System.Console.WriteLine("                " + state_map[i].action);
                System.Console.WriteLine("            }");
                System.Console.WriteLine();
                System.Console.WriteLine("            public State Feed(Object context, char c)");
                System.Console.WriteLine("            {");
                System.Console.WriteLine("                switch (c)");
                System.Console.WriteLine("                {");
                FiniteState def = null;
                foreach (KeyValuePair<char, FiniteState> newstate in state_map[i].table)
                {
                    if (newstate.Key == (char)0xFFFE)
                    {
                        def = newstate.Value;
                    }
                    else
                    {
                        System.Console.WriteLine("                    case " + ((newstate.Key >= ' ' && newstate.Key <= '~') ? ("\'" + newstate.Key + '\'') : "(char)" + ((int)newstate.Key).ToString()) + ":");
                        System.Console.WriteLine("                        return new State" + newstate.Value.index.ToString() + "(context, c);");
                    }
                }
                if (def == null)
                {
                    System.Console.WriteLine("                    default:");
                    System.Console.WriteLine("                        throw new ParseException(\"Input Error.\");");
                }
                else
                {
                    System.Console.WriteLine("                    default:");
                    System.Console.WriteLine("                        return new State" + def.index.ToString() + "(context, c);");
                }
                System.Console.WriteLine("                }");
                System.Console.WriteLine("            }");
                System.Console.WriteLine("        }");
            }
            System.Console.WriteLine("    }");
            System.Console.WriteLine("}");
        }

        internal class FiniteState
        {
            internal FiniteState(int i) { index = i; }
            internal int index;
            internal bool end;
            internal string action;
            internal Dictionary<char, FiniteState> table = new Dictionary<char, FiniteState>();
        }
        internal FiniteState[] state_map;
    }
}
