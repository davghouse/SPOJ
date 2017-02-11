using System.Collections.Generic;

namespace Daves.SpojSpace.Library.Tries
{
    public sealed class TrieNode
    {
        internal TrieNode(char value, int depth)
        {
            Value = value;
            Depth = depth;
        }

        // Storing Value isn't necessary, it just helps me debug and think clearly about what's going on.
        internal char Value { get; }
        internal int Depth { get; }
        internal bool IsAWordEnd { get; set; }
        internal Dictionary<char, TrieNode> Children { get; } = new Dictionary<char, TrieNode>();

        public override string ToString()
            => Depth == 0 ? "root"
            : $"value: {Value}, depth: {Depth}, children: {Children.Count}, {(IsAWordEnd ? "is a word end" : "not a word end")}";
    }
}
