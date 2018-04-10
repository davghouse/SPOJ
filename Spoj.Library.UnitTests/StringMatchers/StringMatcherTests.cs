using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spoj.Library.StringMatchers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Spoj.Library.UnitTests.StringMatchers
{
    [TestClass]
    public class StringMatcherTests
    {
        [TestMethod]
        public void StringMatchers_ForSmallStrings()
        {
            StringMatchers_ForSmallStrings(NaiveStringMatcher.GetMatchIndices);
            StringMatchers_ForSmallStrings(KmpStringMatcher.GetMatchIndices);
        }

        private void StringMatchers_ForSmallStrings(Func<string, string, IEnumerable<int>> getMatches)
        {
            string text;

            text = "a";
            CollectionAssert.AreEqual(getMatches(text, "a").ToArray(), new[] { 0 });

            text = "aa";
            CollectionAssert.AreEqual(getMatches(text, "a").ToArray(), new[] { 0, 1 });
            CollectionAssert.AreEqual(getMatches(text, "aa").ToArray(), new[] { 0 });

            //      01234
            text = "aaaaa";
            CollectionAssert.AreEqual(getMatches(text, "a").ToArray(), new[] { 0, 1, 2, 3, 4 });
            CollectionAssert.AreEqual(getMatches(text, "aa").ToArray(), new[] { 0, 1, 2, 3 });
            CollectionAssert.AreEqual(getMatches(text, "aaa").ToArray(), new[] { 0, 1, 2 });
            CollectionAssert.AreEqual(getMatches(text, "aaaa").ToArray(), new[] { 0, 1 });
            CollectionAssert.AreEqual(getMatches(text, "aaaaa").ToArray(), new[] { 0 });

            //      012345678
            text = "abbaabbba";
            CollectionAssert.AreEqual(getMatches(text, "a").ToArray(), new[] {0, 3, 4, 8 });
            CollectionAssert.AreEqual(getMatches(text, "aa").ToArray(), new[] { 3 });
            CollectionAssert.AreEqual(getMatches(text, "aaa").ToArray(), new int[] { });
            CollectionAssert.AreEqual(getMatches(text, "b").ToArray(), new[] { 1, 2, 5, 6, 7 });
            CollectionAssert.AreEqual(getMatches(text, "bb").ToArray(), new[] { 1, 5, 6 });
            CollectionAssert.AreEqual(getMatches(text, "bbb").ToArray(), new[] { 5 });
            CollectionAssert.AreEqual(getMatches(text, "bbbb").ToArray(), new int[] { });
            CollectionAssert.AreEqual(getMatches(text, "ab").ToArray(), new[] { 0, 4});
            CollectionAssert.AreEqual(getMatches(text, "abb").ToArray(), new[] { 0, 4});
            CollectionAssert.AreEqual(getMatches(text, "abbb").ToArray(), new[] { 4 });
            CollectionAssert.AreEqual(getMatches(text, "abbba").ToArray(), new[] { 4 });
            CollectionAssert.AreEqual(getMatches(text, "abbbb").ToArray(), new int[] { });
            CollectionAssert.AreEqual(getMatches(text, "abba").ToArray(), new[] { 0 });
            CollectionAssert.AreEqual(getMatches(text, "ba").ToArray(), new[] { 2, 7 });
            CollectionAssert.AreEqual(getMatches(text, "baa").ToArray(), new[] { 2 });
            CollectionAssert.AreEqual(getMatches(text, "aab").ToArray(), new[] { 3 });
            CollectionAssert.AreEqual(getMatches(text, "aabb").ToArray(), new[] { 3 });
            CollectionAssert.AreEqual(getMatches(text, "bbba").ToArray(), new[] { 5 });
            CollectionAssert.AreEqual(getMatches(text, "bbaabbba").ToArray(), new[] { 1 });
            CollectionAssert.AreEqual(getMatches(text, "abbaabbba").ToArray(), new[] { 0 });
        }


        [TestMethod]
        public void StringMatchers_ForLargeStrings_AgreeWithEachOther()
        {
            string text = InputGenerator.GenerateRandomString(1000);
            string pattern = InputGenerator.GenerateRandomString(3);

            CollectionAssert.AreEqual(
                NaiveStringMatcher.GetMatchIndices(text, pattern).ToArray(),
                KmpStringMatcher.GetMatchIndices(text, pattern).ToArray());

            text = InputGenerator.GenerateRandomString(1000, 'a', 'b');
            pattern = InputGenerator.GenerateRandomString(3, 'a', 'b');

            CollectionAssert.AreEqual(
                NaiveStringMatcher.GetMatchIndices(text, pattern).ToArray(),
                KmpStringMatcher.GetMatchIndices(text, pattern).ToArray());
        }

        [TestMethod]
        public void ComputePrefixesLengthOfLongestProperSuffixThatIsItselfAPrefix()
            // This verifies the example shown in CLRS on page 1005.
            => CollectionAssert.AreEqual(
                new[] { 0, 0, 0, 1, 2, 3, 0, 1 },
                KmpStringMatcher.ComputePrefixesLengthOfLongestProperSuffixThatIsItselfAPrefix("ababaca").ToArray());
    }
}
