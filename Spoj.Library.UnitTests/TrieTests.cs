using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Spoj.Library.UnitTests
{
    [TestClass]
    public class TrieTests
    {
        private static string[] strings = new[]
        {
            "", "one", "onward", "upward", "ontology", "ontologist", "upwardly", "two", "twelve", " ", "  ", "   ", "1", "12", "one", "one"
        };

        // Has one, two, upward, upward, and "  " in common with strings.
        private static string[] moreStrings = new[]
        {
            "dino", "dinosaur", "twenty", "ontologists", "    ", "13", "14", "one", "two", "upward", "upward", "  ", "twig", "twang"
        };

        private static string[] evenMoreStrings = new[]
        {
            "zebra", "yankee", "993", "bronx"
        };

        [TestMethod]
        public void VerifiesContainsWordForSmallTries()
        {
            var trie = new Trie(strings);

            Assert.IsTrue(strings.All(trie.ContainsWord));
            Assert.AreEqual(5, moreStrings.Count(trie.ContainsWord));
            Assert.AreEqual(0, evenMoreStrings.Count(trie.ContainsWord));

            trie = new Trie(moreStrings);
            Assert.IsTrue(moreStrings.All(trie.ContainsWord));
            Assert.AreEqual(6, strings.Count(trie.ContainsWord));
            Assert.AreEqual(0, evenMoreStrings.Count(trie.ContainsWord));

            Assert.IsFalse(trie.ContainsWord("on"));
            Assert.IsFalse(trie.ContainsWord("ony"));
            Assert.IsFalse(trie.ContainsWord("onyx"));
            Assert.IsFalse(trie.ContainsWord(""));
            Assert.IsFalse(trie.ContainsWord("q"));
        }

        [TestMethod]
        public void VerifiesContainsPrefixForSmallTries()
        {
            var trie = new Trie(strings);
            for (int i = 0; i < strings.Length; ++i)
            {
                for (int j = 1; j <= strings[i].Length; ++j)
                {
                    Assert.IsTrue(trie.ContainsPrefix(strings[i].Substring(0, j)));
                }
            }
            for (int i = 0; i < evenMoreStrings.Length; ++i)
            {
                for (int j = 1; j <= evenMoreStrings[i].Length; ++j)
                {
                    Assert.IsFalse(trie.ContainsPrefix(evenMoreStrings[i].Substring(0, j)));
                }
            }

            trie = new Trie(moreStrings);
            for (int i = 0; i < moreStrings.Length; ++i)
            {
                for (int j = 1; j <= moreStrings[i].Length; ++j)
                {
                    Assert.IsTrue(trie.ContainsPrefix(moreStrings[i].Substring(0, j)));
                }
            }
            for (int i = 0; i < evenMoreStrings.Length; ++i)
            {
                for (int j = 1; j <= evenMoreStrings[i].Length; ++j)
                {
                    Assert.IsFalse(trie.ContainsPrefix(evenMoreStrings[i].Substring(0, j)));
                }
            }

            Assert.IsTrue(trie.ContainsPrefix("on"));
            Assert.IsFalse(trie.ContainsPrefix("ony"));
            Assert.IsFalse(trie.ContainsPrefix("onyx"));
            Assert.IsTrue(trie.ContainsPrefix(""));
            Assert.IsFalse(trie.ContainsPrefix("q"));
        }
    }
}
