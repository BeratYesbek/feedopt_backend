using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Algorithms.SearchAlgorithm
{
    public interface IAhoCorasick
    {
        void Add(IEnumerable<string> value);
    }
}
