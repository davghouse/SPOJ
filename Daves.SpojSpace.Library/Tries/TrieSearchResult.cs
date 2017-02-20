namespace Daves.SpojSpace.Library.Tries
{
    public sealed class TrieSearchResult
    {
        // Provided to make beginning a prefix search chain more convenient (result.TerminalNode vs result?.TerminalNode)
        public TrieSearchResult()
        { }

        public TrieSearchResult(Trie.Node terminalNode, bool containsPrefix, bool containsWord)
        {
            TerminalNode = terminalNode;
            ContainsPrefix = containsPrefix;
            ContainsWord = containsWord;
        }

        public Trie.Node TerminalNode { get; }
        public bool ContainsPrefix { get; }
        public bool ContainsWord { get; }
    }
}
