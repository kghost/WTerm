using System;
using System.Collections.Generic;
using System.Text;

namespace FiniteStateMachine
{
    public class StateMachine
    {
        internal int start;
        internal HashSet<int> end = new HashSet<int>();
        internal char[,] table;
        internal int states;
        internal Dictionary<int, string> actions = new Dictionary<int, string>();

        private StateMachine() { }

        internal static StateMachine CharMachine(char c, string action)
        {
            StateMachine s = new StateMachine();
            s.table = new char[2, 2];
            s.states = 2;
            s.table[0, 1] = c;
            s.actions[1] = action;
            s.start = 0;
            s.end.Add(1);
            return s;
        }

        internal static StateMachine UnionMachine(StateMachine a, StateMachine b)
        {
            StateMachine s = new StateMachine();
            s.table = new char[a.states + b.states + 2, a.states + b.states + 2];
            s.states = a.states + b.states + 2;

            // copy a's states
            for (int i = 0; i < a.states; i++)
            {
                for (int j = 0; j < a.states; j++)
                {
                    s.table[i, j] = a.table[i, j];
                }
            }

            foreach (KeyValuePair<int, string> actions in a.actions)
            {
                s.actions[actions.Key] = actions.Value;
            }

            // copy b's states
            for (int i = 0; i < b.states; i++)
            {
                for (int j = 0; j < b.states; j++)
                {
                    s.table[i + a.states, j + a.states] = b.table[i, j];
                }
            }

            foreach (KeyValuePair<int, string> actions in b.actions)
            {
                s.actions[actions.Key + a.states] = actions.Value;
            }

            // fix start
            s.start = a.states + b.states;
            s.table[a.states + b.states, a.start] = (char)0xFFFF;
            s.table[a.states + b.states, b.start + a.states] = (char)0xFFFF;

            // fix end
            foreach (int i in a.end)
            {
                s.table[i, a.states + b.states + 1] = (char)0xFFFF;
            }

            foreach (int i in b.end)
            {
                s.table[a.states + i, a.states + b.states + 1] = (char)0xFFFF;
            }

            s.end.Add(a.states + b.states + 1);

            return s;
        }

        internal static StateMachine concatenationMachine(StateMachine pre, StateMachine next)
        {
            StateMachine s = new StateMachine();
            s.table = new char[pre.states + next.states, pre.states + next.states];
            s.states = pre.states + next.states;
            for (int i = 0; i < pre.states; i++)
            {
                for (int j = 0; j < pre.states; j++)
                {
                    s.table[i, j] = pre.table[i, j];
                }
            }

            foreach (KeyValuePair<int, string> actions in pre.actions)
            {
                s.actions[actions.Key] = actions.Value;
            }

            for (int i = 0; i < next.states; i++)
            {
                for (int j = 0; j < next.states; j++)
                {
                    s.table[i + pre.states, j + pre.states] = next.table[i, j];
                }
            }

            foreach (KeyValuePair<int, string> actions in next.actions)
            {
                s.actions[actions.Key + pre.states] = actions.Value;
            }

            foreach (int i in pre.end)
            {
                s.table[i, next.start + pre.states] = (char)0xFFFF;
            }

            s.start = pre.start;
            foreach (int i in next.end)
            {
                s.end.Add(i + pre.states);
            }

            return s;
        }

        internal static StateMachine KleenestarMachine(StateMachine o)
        {
            StateMachine s = new StateMachine();
            s.start = o.start;
            s.table = (char[,])o.table.Clone();
            s.states = o.states;
            foreach (KeyValuePair<int, string> action in o.actions)
            {
                s.actions[action.Key] = action.Value;
            }

            foreach (int i in o.end)
            {
                s.table[i, s.start] = (char)0xFFFF;
            }
            s.end.Add(s.start);
            return s;
        }
    }
}
