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
    public static class Statics
    {
        public static Dictionary<Command, string> CommandHelp = new Dictionary<Command, string>
        {
            { Command.Unknown,  null },
            { Command.Help,     "Prints help on any command. Arguments: name of command to give detailed help on." },
            { Command.Quit,     "Exits the program." },
            { Command.Exit,     "Exits the program." },
            { Command.Default,  null },
            { Command.Push,     "Push number(s) onto the stack. Arguments: numbers to push to the stack, in order." },
            { Command.Pop,      "Pop number(s) from the stack. Argument: how many numbers to pop. Default = 1." },
            { Command.Swap,     "Swap numbers on the stack. Arguments: indices to swap. If one argument is given, it swaps that index with #1. If no arguments are given, it swaps #1 and #2." },
            { Command.Add,      "Adds #1 and #2. Shorthand: +" },
            { Command.Subtract, "Subtracts #2 from #1. Shorthand: -" },
            { Command.Multiply, "Multiplies #1 and #2. Shorthand: *" },
            { Command.Divide,   "Divides #2 by #1. Shorthand: /" },
            { Command.Modulo,   "Gives the remainder of dividing #2 by #1." },
            { Command.Power,    "Raises #2 to the power of #1." },
            { Command.Negate,   "Negates #1." },
        };
    };
}
