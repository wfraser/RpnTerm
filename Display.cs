//
// Copyright © William R. Fraser 2013
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpnTerm
{
    public class Display
    {
        private static readonly char s_verticalBar = '│';

        private CalculatorStack m_stack;
        private CircularBuffer<Tuple<string, string>> m_inputs;

        public Display(CalculatorStack stack)
        {
            m_stack = stack;
            m_inputs = new CircularBuffer<Tuple<string,string>>(Console.WindowHeight);
        }

        public void Update()
        {
            Console.Clear();

            var leftPane = new List<string>();
            int paneWidth = (Console.WindowWidth - 1) / 2;

            IEnumerator<Tuple<string, string>> inputEnum = m_inputs.GetEnumerator();

            while (true)
            {
                if (!inputEnum.MoveNext())
                {
                    break;
                }

                var sb = new StringBuilder("> ");
                for (int i = 0; i < inputEnum.Current.Item1.Length; i++)
                {
                    char c = inputEnum.Current.Item1[i];
                    if (sb.Length < paneWidth && c != '\n')
                    {
                        sb.Append(c);
                    }
                    else
                    {
                        leftPane.Add(sb.ToString());
                        sb.Clear();
                        sb.Append("...> ");
                        if (c != '\n')
                        {
                            sb.Append(c);
                        }
                    }
                }
                if (sb.Length > 0)
                {
                    leftPane.Add(sb.ToString());
                    sb.Clear();
                }

                if (inputEnum.Current.Item2 != null)
                {
                    for (int i = 0; i < inputEnum.Current.Item2.Length; i++)
                    {
                        char c = inputEnum.Current.Item2[i];
                        if (sb.Length < paneWidth && c != '\n')
                        {
                            sb.Append(c);
                        }
                        else
                        {
                            leftPane.Add(sb.ToString());
                            sb.Clear();
                            if (c != '\n')
                            {
                                sb.Append(c);
                            }
                        }
                    }
                    if (sb.Length > 0)
                    {
                        leftPane.Add(sb.ToString());
                    }
                }
            }

            var visibleInput = leftPane.Reverse<string>().Take(Console.WindowHeight - 1).Reverse<string>();
            var visibleStack = m_stack.Get(Math.Min(m_stack.Count, Console.WindowHeight - 1)).GetEnumerator();
            for (int i = 0; i < Console.WindowHeight - 1; i++)
            {
                bool haveStack = visibleStack.MoveNext();
                Console.Out.WriteLine("{0,-" + paneWidth.ToString() + "}" + s_verticalBar + "{1,-" + paneWidth.ToString() + "}",
                    visibleInput.ElementAtOrDefault(i, string.Empty),
                    haveStack ? string.Format("{0}: {1}", i + 1, visibleStack.Current.ToString()) : string.Empty);
            }

            Console.Out.Write("> ");
        }

        public void ShowMessage(string message)
        {
            m_inputs.End = new Tuple<string, string>(m_inputs.End.Item1, message);
            Update();
        }

        public void UserInput(string input)
        {
            m_inputs.PushEnd(new Tuple<string, string>(input, null));
        }

        public void ClearUserInput()
        {
            m_inputs.Clear();
        }
    }
}
