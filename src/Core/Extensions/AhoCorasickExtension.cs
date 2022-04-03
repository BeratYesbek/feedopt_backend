using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class AhoCorasickExtension
    {
        public static void SetKeywords(this AhoCorasick.AhoCorasick ahoCorasick, string[] keywords)
        {
            foreach (var keyword in keywords)
            {
                ahoCorasick.Add(keyword);
            }
        }
    }
}
