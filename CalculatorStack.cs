using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpnTerm
{
    public class CalculatorStack
    {
        public CalculatorStack()
        {
            m_stack = new Stack<decimal>();
        }

        public int Count
        {
            get { return m_stack.Count; }
        }

        public IEnumerable<decimal> Get(int startingIndex)
        {
            return m_stack.Take(startingIndex).Reverse();
        }

        private void Push(decimal n)
        {
            m_stack.Push(n);
        }

        private decimal Pop()
        {
            if (m_stack.Count == 0)
            {
                throw new IndexOutOfRangeException();
            }
            return m_stack.Pop();
        }

        private void GetTwo(out decimal x, out decimal y)
        {
            try
            {
                x = Pop();
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentException("1?");
            }

            try
            {
                y = Pop();
            }
            catch (IndexOutOfRangeException)
            {
                Push(x);
                throw new ArgumentException("2?");
            }
        }

        private decimal GetOne()
        {
            try
            {
                return Pop();
            }
            catch (IndexOutOfRangeException)
            {
                throw new ArgumentException("1?");
            }
        }

        public void RunCommand(Command cmd, IEnumerable<decimal> args)
        {
            switch (cmd)
            {
                case Command.Quit:
                case Command.Exit:
                case Command.Help:
                case Command.Unknown:
                default:
                    throw new InvalidOperationException("CalculatorStack can't handle this command.");

                case Command.Push:
                    if (args.Count() == 0)
                    {
                        throw new ArgumentException("number?");
                    }
                    else
                    {
                        foreach (decimal n in args)
                        {
                            Push(n);
                        }
                    }
                    break;

                case Command.Pop:
                    {
                        decimal count = (args.Count() == 0) ? 1 : args.First();
                        if (count > m_stack.Count)
                        {
                            throw new ArgumentException("argument out of range");
                        }

                        for (int i = 0; i < count.AsInt(); i++)
                        {
                            Pop();
                        }
                    }
                    break;

                case Command.Swap:
                    {
                        int ix = args.ElementAtOrDefault(0, 2).AsInt() - 1;
                        int iy = args.ElementAtOrDefault(1, 1).AsInt() - 1;

                        if (Math.Max(ix, iy) > m_stack.Count)
                        {
                            throw new ArgumentException("argument out of range");
                        }

                        decimal[] arr = m_stack.ToArray();
                        decimal temp = arr[ix];
                        arr[ix] = arr[iy];
                        arr[iy] = temp;

                        m_stack = new Stack<decimal>(arr.Reverse());
                    }
                    break;

                case Command.Add:
                    {
                        decimal x, y;
                        GetTwo(out x, out y);
                        Push(y + x);
                    }
                    break;

                case Command.Subtract:
                    {
                        decimal x, y;
                        GetTwo(out x, out y);
                        Push(y - x);
                    }
                    break;

                case Command.Multiply:
                    {
                        decimal x, y;
                        GetTwo(out x, out y);
                        Push(y * x);
                    }
                    break;

                case Command.Divide:
                    {
                        decimal x, y;
                        GetTwo(out x, out y);
                        Push(y / x);
                    }
                    break;

                case Command.Modulo:
                    {
                        decimal x, y;
                        GetTwo(out x, out y);
                        Push(y % x);
                    }
                    break;

                case Command.Power:
                    {
                        decimal x, y;
                        GetTwo(out x, out y);
                        try
                        {
                            Push((decimal)Math.Pow(decimal.ToDouble(y), decimal.ToDouble(x)));
                        }
                        catch (OverflowException)
                        {
                            Push(y);
                            Push(x);
                            throw new ArgumentException("overflow");
                        }
                    }
                    break;

                case Command.Negate:
                    {
                        decimal x = GetOne();
                        Push(-x);
                    }
                    break;
            }
        }

        private Stack<decimal> m_stack;
    }
}
