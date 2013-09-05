//
// Copyright © William R. Fraser 2013
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RpnTerm
{
    class Program
    {
        static void Main(string[] args)
        {
            var program = new Program();
            program.Repl();
        }

        private Program()
        {
            m_stack = new CalculatorStack();
            m_display = new Display(m_stack);
        }

        private Command ParseInput(string input)
        {
            // Can't use Enum.TryParse here because it parses numbers as enum values.
            foreach (string name in Enum.GetNames(typeof(Command)))
            {
                //TODO: allow abbreviations
                if (input.Equals(name, StringComparison.InvariantCultureIgnoreCase))
                {
                    return (Command)Enum.Parse(typeof(Command), name);
                }
            }

            decimal unused;
            if (decimal.TryParse(input, out unused))
            {
                return Command.Default;
            }
            else if (input == "?")
            {
                return Command.Help;
            }
            else if (input == "+")
            {
                return Command.Add;
            }
            else if (input == "-")
            {
                return Command.Subtract;
            }
            else if (input == "*")
            {
                return Command.Multiply;
            }
            else if (input == "/")
            {
                return Command.Divide;
            }
            else
            {
                return Command.Unknown;
            }
        }

        private void Repl()
        {
            m_display.Update();

            bool exit = false;
            do
            {
                string input = Console.In.ReadLine();
                if (input == null)
                {
                    Console.Out.WriteLine("Read null; exiting.");
                    break;
                }
                m_display.UserInput(input);

                string[] words = input.Split(' ');
                Command command = ParseInput(words[0]);
                switch (command)
                {
                    case Command.Quit:
                    case Command.Exit:
                        exit = true;
                        break;

                    case Command.Help:
                        {
                            if (words.Count() > 1)
                            {
                                Command cmd = ParseInput(words[1]);
                                string help = Statics.CommandHelp[cmd];

                                if (help == null)
                                {
                                    m_display.ShowMessage("There is no help on this command.\n");
                                }
                                else
                                {
                                    m_display.ShowMessage(string.Format("{0}: {1}\n", cmd.ToString(), help));
                                }
                            }
                            else
                            {
                                var titleAttribute = (AssemblyTitleAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false).First();
                                var versionAttribute = (AssemblyFileVersionAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false).First();
                                var copyrightAttribute = (AssemblyCopyrightAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false).First();
                                IEnumerable<string> allCommands = Statics.CommandHelp.Where(pair => pair.Value != null).Select(pair => pair.Key.ToString());

                                string message = string.Format("{0} v{1} {2}\nCommands: {3}\nType \"help <command>\" for detailed information.\n",
                                    titleAttribute.Title,
                                    versionAttribute.Version,
                                    copyrightAttribute.Copyright,
                                    string.Join(", ", allCommands));

                                m_display.ShowMessage(message);
                            }
                        }
                        break;

                    case Command.Clear:
                        m_display.ClearUserInput();
                        m_display.Update();
                        break;

                    case Command.Unknown:
                        m_display.ShowMessage("unknown command");
                        break;

                    default:
                        {
                            IEnumerable<decimal> args = null;
                            try
                            {
                                if (command == Command.Default)
                                {
                                    command = Command.Push;
                                    args = new decimal[] { decimal.Parse(words[0]) };
                                }
                                else
                                {
                                    args = words.Skip(1).Select(s => decimal.Parse(s));
                                }

                                m_stack.RunCommand(command, args);
                                m_display.Update();
                            }
                            catch (FormatException)
                            {
                                m_display.ShowMessage("invalid number");
                            }
                            catch (ArgumentException ex)
                            {
                                m_display.ShowMessage(ex.Message);
                            }
                        }
                        break;
                }
            }
            while (!exit);
        }

        private Display m_display;
        private CalculatorStack m_stack;
    }
}
