using System.Collections.Generic;

namespace Spoj.Library.Tries
{
    public sealed partial class Trie
    {
        public sealed class Node
        {
            internal Node(char value, int depth, IEqualityComparer<char> charEqualityComparer = null)
            {
                Value = value;
                Depth = depth;
                Children = new Dictionary<char, Node>(charEqualityComparer);
            }

            // Storing Value isn't necessary, it just helps me debug/think clearly.
            internal char Value { get; }
            internal int Depth { get; }
            internal bool IsAWordEnd { get; set; }
            internal Dictionary<char, Node> Children { get; }

            public override string ToString()
                => Depth == 0 ? "root"
                : $"value: {Value}, depth: {Depth}, children: {Children.Count}, {(IsAWordEnd ? "is a word end" : "not a word end")}";
        }
    }
}
