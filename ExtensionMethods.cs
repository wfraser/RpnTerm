using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpnTerm
{
    static class ExtensionMethods
    {
        public static int AsInt(this decimal d)
        {
            int i = decimal.ToInt32(d);
            if (i != d)
            {
                throw new ArgumentException("bad integer");
            }
            return i;
        }

        public static T ElementAtOrDefault<T>(this IEnumerable<T> x, int index, T def)
        {
            if (x.Count() <= index)
            {
                return def;
            }
            else
            {
                return x.ElementAt(index);
            }
        }
    }
}
