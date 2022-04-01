using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace Core.Utilities.Algorithms.SearchAlgorithm
{
    namespace AhoCorasick
    {
        public class AhoCorasick : AhoCorasick<string>,IAhoCorasick
        {
            public void Add<T>(T value)
            {
                Add(value);
            }

            public void Add<T>(IEnumerable<T> values)
            {
                foreach (var value in values)
                {
                    Add(value);
                }
            }
        }

        public class AhoCorasick<TValue> : AhoCorasick<char, TValue>
        {
        }

        public class AhoCorasick<T, TValue>
        {
           // private readonly Node
        }


        internal class Node<TNode, TNodeValue> : IEnumerable<Node<TNode, TNodeValue>>
        {
            private readonly TNode _node;
            private readonly Node<TNode, TNodeValue> _parent;
            private readonly Dictionary<TNode,Node<TNode,TNodeValue>> _children = new Dictionary<TNode,Node<TNode, TNodeValue>>();
            private readonly List<TNodeValue> _values = new List<TNodeValue>();

            public Node()
            {
                
            }

            public Node(TNode node, Node<TNode, TNodeValue> parent)
            {
                _node = node;
                _parent = parent;
            }

            public TNode Word => Word;
            public Node<TNode, TNodeValue> Parent => _parent;
            public Node<TNode,TNodeValue> Fail { get; set; }

            public Node<TNode, TNodeValue> this[TNode node]
            {
                get => _children.ContainsKey(node) ? _children[node] : null;
                set => _children[node] = value;
            }


            public List<TNodeValue> Values => _values;



            public IEnumerator<Node<TNode, TNodeValue>> GetEnumerator()
            {
                return _children.Values.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public override string ToString()
            {
                return Word.ToString();
            }
        }



    }

   
}