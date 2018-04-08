using System.Collections.Generic;

namespace SpojSpace.Library.Tries
{
    // Here's a good video on tries: https://www.youtube.com/watch?v=AXjmTQ8LEoI.
    // Designed with the use case of Wordament boards in mind. There, word discovery is done by searching for words
    // that are prefixed by words just searched for, so it's useful to expose terminal nodes of prior searches
    // in order to skip ahead down the trie.
    public sealed partial class Trie
    {
        private readonly IEqualityComparer<char> _charEqualityComparer;
        private readonly Node _root;

        public Trie(IEqualityComparer<char> charEqualityComparer = null)
        {
            _charEqualityComparer = charEqualityComparer;
            _root = new Node(default(char), 0, _charEqualityComparer);
        }

        public Trie(IEnumerable<string> words, IEqualityComparer<char> charEqualityComparer = null)
            : this(charEqualityComparer)
        {
            foreach (string word in words)
            {
                Add(word);
            }
        }

        public void Add(string word)
        {
            Node currentNode = _root;
            Node nextNode;
            int index = 0;

            // Traverse down into the trie until running out of characters or getting to a node that needs a new child.
            while (index < word.Length && currentNode.Children.TryGetValue(word[index], out nextNode))
            {
                currentNode = nextNode;
                ++index;
            }

            // The prefix of the word already in the trie has been exhausted, so we know all characters from this point
            // forward will need to have nodes created and wired up.
            while (index < word.Length)
            {
                nextNode = new Node(word[index], index + 1, _charEqualityComparer);
                currentNode.Children.Add(word[index], nextNode);
                currentNode = nextNode;
                ++index;
            }

            // currentNode is now the node corresponding to the final character in the word, so mark it as a word end.
            currentNode.IsAWordEnd = true;
        }

        /* In these searches, currentNode.Depth is how much length of the given string is already assumed to be matching.
         * The next relevant index for us to check is the one just past that much length, i.e., [currentNode.Depth].
         * Single-parameter overloads are provided to make writing LINQ statements more convenient (C() vs s => C(s)). */

        public bool ContainsPrefix(string prefix) => ContainsPrefix(prefix, _root);
        public bool ContainsPrefix(string prefix, Node currentNode)
        {
            currentNode = currentNode ?? _root;
            int index = currentNode.Depth;
            ContainsHelper(prefix, ref currentNode, ref index);

            return index == prefix.Length;
        }

        public bool ContainsWord(string word) => ContainsWord(word, _root);
        public bool ContainsWord(string word, Node currentNode)
        {
            currentNode = currentNode ?? _root;
            int index = currentNode.Depth;
            ContainsHelper(word, ref currentNode, ref index);

            return index == word.Length && currentNode.IsAWordEnd;
        }

        public TrieSearchResult Search(string s) => Search(s, _root);
        public TrieSearchResult Search(string s, Node currentNode)
        {
            currentNode = currentNode ?? _root;
            int index = currentNode.Depth;
            ContainsHelper(s, ref currentNode, ref index);

            return new TrieSearchResult(
                terminalNode: currentNode,
                containsPrefix: index == s.Length,
                containsWord: index == s.Length && currentNode.IsAWordEnd);
        }

        private void ContainsHelper(string s, ref Node currentNode, ref int index)
        {
            Node nextNode;

            // Traverse down into the trie until we run out of characters or get to a node that needs a new child.
            while (index < s.Length && currentNode.Children.TryGetValue(s[index], out nextNode))
            {
                currentNode = nextNode;
                ++index;
            }
        }
    }
}
