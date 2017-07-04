using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpojSpace.Library.Tries;
using System.Linq;

namespace SpojSpace.Library.UnitTests.Tries
{
    [TestClass]
    public class TrieTests
    {
        private static string[] _strings = new[]
        {
            "", "one", "onward", "upward", "ontology", "ontologist", "upwardly", "two", "twelve", " ", "  ", "   ", "1", "12", "one", "one"
        };

        // Has one, two, upward, upward, and "  " in common with strings.
        private static string[] _moreStrings = new[]
        {
            "dino", "dinosaur", "twenty", "ontologists", "    ", "13", "14", "one", "two", "upward", "upward", "  ", "twig", "twang"
        };

        private static string[] _evenMoreStrings = new[]
        {
            "zebra", "yankee", "993", "bronx"
        };

        [TestMethod]
        public void ContainsWord_ForSmallTries()
        {
            var trie = new Trie(_strings);
            Assert.IsTrue(_strings.All(trie.ContainsWord));
            Assert.AreEqual(5, _moreStrings.Count(trie.ContainsWord));
            Assert.AreEqual(0, _evenMoreStrings.Count(trie.ContainsWord));

            trie = new Trie(_moreStrings);
            Assert.IsTrue(_moreStrings.All(trie.ContainsWord));
            Assert.AreEqual(6, _strings.Count(trie.ContainsWord));
            Assert.AreEqual(0, _evenMoreStrings.Count(trie.ContainsWord));

            Assert.IsFalse(trie.ContainsWord("on"));
            Assert.IsFalse(trie.ContainsWord("ony"));
            Assert.IsFalse(trie.ContainsWord("onyx"));
            Assert.IsFalse(trie.ContainsWord(""));
            Assert.IsFalse(trie.ContainsWord("q"));
        }

        [TestMethod]
        public void ContainsPrefix_ForSmallTries()
        {
            var trie = new Trie(_strings);
            for (int i = 0; i < _strings.Length; ++i)
            {
                for (int j = 1; j <= _strings[i].Length; ++j)
                {
                    Assert.IsTrue(trie.ContainsPrefix(_strings[i].Substring(0, j)));
                }
            }
            for (int i = 0; i < _evenMoreStrings.Length; ++i)
            {
                for (int j = 1; j <= _evenMoreStrings[i].Length; ++j)
                {
                    Assert.IsFalse(trie.ContainsPrefix(_evenMoreStrings[i].Substring(0, j)));
                }
            }

            trie = new Trie(_moreStrings);
            for (int i = 0; i < _moreStrings.Length; ++i)
            {
                for (int j = 1; j <= _moreStrings[i].Length; ++j)
                {
                    Assert.IsTrue(trie.ContainsPrefix(_moreStrings[i].Substring(0, j)));
                }
            }
            for (int i = 0; i < _evenMoreStrings.Length; ++i)
            {
                for (int j = 1; j <= _evenMoreStrings[i].Length; ++j)
                {
                    Assert.IsFalse(trie.ContainsPrefix(_evenMoreStrings[i].Substring(0, j)));
                }
            }

            Assert.IsTrue(trie.ContainsPrefix("on"));
            Assert.IsFalse(trie.ContainsPrefix("ony"));
            Assert.IsFalse(trie.ContainsPrefix("onyx"));
            Assert.IsTrue(trie.ContainsPrefix(""));
            Assert.IsFalse(trie.ContainsPrefix("q"));
        }

        [TestMethod]
        public void Search_ForSmallTries()
        {
            var trie = new Trie(_strings);
            for (int i = 0; i < _strings.Length; ++i)
            {
                TrieSearchResult result = new TrieSearchResult();
                for (int j = 1; j <= _strings[i].Length; ++j)
                {
                    result = trie.Search(_strings[i].Substring(0, j), result.TerminalNode);

                    Assert.IsTrue(result.ContainsPrefix);
                    Assert.IsTrue(result.TerminalNode.Depth == j);

                    if (j == _strings[i].Length)
                    {
                        Assert.IsTrue(result.ContainsWord);
                        Assert.IsTrue(result.TerminalNode.IsAWordEnd);
                    }
                }
            }
            for (int i = 0; i < _evenMoreStrings.Length; ++i)
            {
                TrieSearchResult result = new TrieSearchResult();
                for (int j = 1; j <= _evenMoreStrings[i].Length; ++j)
                {
                    result = trie.Search(_evenMoreStrings[i].Substring(0, j), result.TerminalNode);

                    Assert.IsFalse(result.ContainsPrefix);
                    Assert.IsFalse(result.ContainsWord);
                    Assert.IsTrue(result.TerminalNode.Depth == 0);
                }
            }

            trie = new Trie(_moreStrings);
            for (int i = 0; i < _moreStrings.Length; ++i)
            {
                for (int j = 1; j <= _moreStrings[i].Length; ++j)
                {
                    Assert.IsTrue(trie.Search(_moreStrings[i].Substring(0, j)).ContainsPrefix);
                }
            }
            for (int i = 0; i < _evenMoreStrings.Length; ++i)
            {
                for (int j = 1; j <= _evenMoreStrings[i].Length; ++j)
                {
                    Assert.IsFalse(trie.Search(_evenMoreStrings[i].Substring(0, j)).ContainsPrefix);
                }
            }

            Assert.IsTrue(trie.Search("on").ContainsPrefix);
            Assert.IsFalse(trie.Search("ony").ContainsPrefix);
            Assert.IsFalse(trie.Search("onyx").ContainsPrefix);
            Assert.IsTrue(trie.Search("").ContainsPrefix);
            Assert.IsFalse(trie.Search("q").ContainsPrefix);
        }

        [TestMethod]
        public void SearchTraversal_ForSmallTries()
        {
            var result = new TrieSearchResult();
            var trie = new Trie(_strings);

            result = trie.Search("di", result.TerminalNode);
            Assert.IsFalse(result.ContainsPrefix);
            Assert.IsTrue(result.TerminalNode.Depth == 0);
            Assert.IsTrue(result.TerminalNode.Value == default(char));

            result = trie.Search("o", result.TerminalNode);
            Assert.IsTrue(result.ContainsPrefix);
            Assert.IsTrue(result.TerminalNode.Depth == 1);
            Assert.IsTrue(result.TerminalNode.Value == 'o');

            result = trie.Search("on", result.TerminalNode);
            Assert.IsTrue(result.ContainsPrefix);
            Assert.IsTrue(result.TerminalNode.Depth == 2);
            Assert.IsTrue(result.TerminalNode.Value == 'n');

            result = trie.Search("onwa", result.TerminalNode);
            Assert.IsTrue(result.ContainsPrefix);
            Assert.IsTrue(result.TerminalNode.Depth == 4);
            Assert.IsTrue(result.TerminalNode.Value == 'a');

            result = trie.Search("onwarp", result.TerminalNode);
            Assert.IsFalse(result.ContainsPrefix);
            Assert.IsTrue(result.TerminalNode.Depth == 5);
            Assert.IsTrue(result.TerminalNode.Value == 'r');

            result = trie.Search("onwardly", result.TerminalNode);
            Assert.IsFalse(result.ContainsPrefix);
            Assert.IsFalse(result.ContainsWord);
            Assert.IsTrue(result.TerminalNode.Depth == 6);
            Assert.IsTrue(result.TerminalNode.Value == 'd');
            Assert.IsTrue(result.TerminalNode.IsAWordEnd);

            result = trie.Search("onward", result.TerminalNode);
            Assert.IsTrue(result.ContainsPrefix);
            Assert.IsTrue(result.ContainsWord);
            Assert.IsTrue(result.TerminalNode.Depth == 6);
            Assert.IsTrue(result.TerminalNode.Value == 'd');
            Assert.IsTrue(result.TerminalNode.IsAWordEnd);
        }

        [TestMethod]
        public void SupportsCaseInsensitivity()
        {
            var trie = new Trie(new CaseInsensitiveCharEqualityComparer());

            trie.Add("HELLO");

            Assert.IsTrue(trie.ContainsPrefix("hell"));
            Assert.IsTrue(trie.ContainsPrefix("hElL"));
            Assert.IsTrue(trie.ContainsPrefix("HELL"));
            Assert.IsTrue(trie.ContainsWord("hello"));
            Assert.IsTrue(trie.ContainsWord("HELLO"));
            Assert.IsTrue(trie.ContainsWord("HeLlO"));

            trie.Add("help");
            var searchResult = trie.Search("hel");
            Assert.AreEqual(2, searchResult.TerminalNode.Children.Count);
            Assert.IsTrue(searchResult.TerminalNode.Children.ContainsKey('l'));
            Assert.IsTrue(searchResult.TerminalNode.Children.ContainsKey('L'));
            Assert.IsTrue(searchResult.TerminalNode.Children.ContainsKey('p'));
            Assert.IsTrue(searchResult.TerminalNode.Children.ContainsKey('P'));
        }
    }
}
