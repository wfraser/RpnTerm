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

        public static IList<string> Chunk(this string s, int chunkSize)
        {
            var chunks = new List<string>();
            var currentChunk = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (currentChunk.Length < chunkSize)
                {
                    currentChunk.Append(s[i]);
                }
                else
                {
                    chunks.Add(currentChunk.ToString());
                    currentChunk.Clear();
                }
            }
            if (currentChunk.Length > 0)
            {
                chunks.Add(currentChunk.ToString());
            }
            return chunks;
        }
    }
}
