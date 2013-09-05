using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpnTerm
{
    public enum Command
    {
        Unknown,
        Help,
        Quit, Exit,
        Default,    // When a bare number is entered. Behaves like Push.
        Push,
        Pop,
        Swap,
        Add,
        Subtract,
        Multiply,
        Divide,
        Modulo,
        Power,
        Negate,
        // TODO
    };
}
