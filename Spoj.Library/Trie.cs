using System.Collections.Generic;

namespace Spoj.Library
{
    // Part-way implementation of a trie, here's a good video: https://www.youtube.com/watch?v=AXjmTQ8LEoI.
    // For an application of a modified version of this trie, see PHONELST.
    public class Trie
    {
        private Node _root = new Node((char)0);

        public Trie()
        { }

        public Trie(IEnumerable<string> strings)
        {
            foreach (string s in strings)
            {
                Add(s);
            }
        }

        public void Add(string s)
        {
            Node currentNode = _root;
            Node nextNode;
            int i = 0;

            // Traverse down into the trie until we run out of characters or get to a node that needs a new child.
            while (i < s.Length && currentNode.Children.TryGetValue(s[i], out nextNode))
            {
                currentNode = nextNode;
                ++i;
            }

            // We've now exhausted the prefix of s already in the trie, so we know all characters from this point
            // forward will need to have nodes created be created and wired up.
            for (; i < s.Length; ++i)
            {
                nextNode = new Node(s[i]);
                currentNode.Children.Add(s[i], nextNode);
                currentNode = nextNode;
            }

            // currentNode is now the node corresponding to the final character in s, so we mark it as a word end.
            currentNode.IsAWordEnd = true;
        }

        // Answers if the string s has been added to the trie, or if it exists as a prefix of a longer word added to the trie.
        public bool ContainsPrefix(string s)
        {
            Node currentNode = _root;
            Node nextNode;
            int i = 0;

            // Traverse down into the trie until we run out of characters or get to a node that needs a new child.
            while (i < s.Length && currentNode.Children.TryGetValue(s[i], out nextNode))
            {
                currentNode = nextNode;
                ++i;
            }

            return i == s.Length;
        }

        // Answers if the string s has been added to the trie. If it's just a prefix of a longer word, that doesn't count.
        public bool ContainsWord(string s)
        {
            Node currentNode = _root;
            Node nextNode;
            int i = 0;

            // Traverse down into the trie until we run out of characters or get to a node that needs a new child.
            while (i < s.Length && currentNode.Children.TryGetValue(s[i], out nextNode))
            {
                currentNode = nextNode;
                ++i;
            }

            return i == s.Length && currentNode.IsAWordEnd;
        }

        private class Node
        {
            public Node(char value)
            {
                Value = value;
            }

            // Storing this value isn't necessary, it just helps me debug and think clearly about what's going on.
            public char Value { get; }

            public bool IsAWordEnd { get; set; }

            public Dictionary<char, Node> Children { get; } = new Dictionary<char, Node>();
        }
    }
}
