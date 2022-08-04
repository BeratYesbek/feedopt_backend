using System.Collections.Generic;
namespace Core.Utilities.Algorithms.SearchAlgorithm
{
    public interface IAhoCorasick
    {
        void Add(IEnumerable<string> value);
    }
}
