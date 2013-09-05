using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpnTerm
{
    public class Display
    {
        public Display(CalculatorStack stack)
        {
            m_stack = stack;
            //m_commandText = new Queue<string>(Console.WindowHeight);
            //m_messages = new Queue<string>(Console.WindowHeight);
        }

        private CalculatorStack m_stack;
        //private Queue<string> m_commandText;
        //private Queue<string> m_messages;

        public void Update(string promptText = "")
        {
            Console.Clear();

            int i = Math.Min(m_stack.Count, Console.WindowHeight - 1);
            foreach (decimal n in m_stack.Get(i))
            {
                Console.Out.WriteLine("{0}: {1}", i, n);
                i--;
            }

            Console.Out.Write("{0}> ", promptText);
        }

        public void ShowMessage(string err)
        {
            Update(promptText: err);
        }
    }
}
