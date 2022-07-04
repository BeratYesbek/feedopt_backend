using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    /// <summary>
    /// Search algorithm, it is going to work with O(log(n))
    /// </summary>
    public static class AhoCorasickExtension
    {
        /// <summary>
        /// This method set banned keywords
        /// </summary>
        /// <param name="ahoCorasick"></param>
        /// <param name="keywords"></param>
        public static void SetKeywords(this AhoCorasick.AhoCorasick ahoCorasick, string[] keywords)
        {
            foreach (var keyword in keywords)
            {
                ahoCorasick.Add(keyword);
            }
        }
    }
}
