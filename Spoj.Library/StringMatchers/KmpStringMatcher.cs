using System;
using System.Collections.Generic;

namespace Spoj.Library.StringMatchers
{
    // This is taken from CLRS. It maintains the one-based indexing used in it (since it's most natural), but gets a
    // little ugly as the strings have to be zero-based. Where relevant, the original indices are shown in parentheses.
    public static class KmpStringMatcher
    {
        public static IEnumerable<int> GetMatchIndices(string text, string pattern)
        {
            IReadOnlyList<int> prefixesLengthOfLongestProperSuffixThatIsItselfAPrefix =
                ComputePrefixesLengthOfLongestProperSuffixThatIsItselfAPrefix(pattern);
            int matchedCharactersCount = 0;

            for (int i = 1; i <= text.Length; ++i)
            {
                while (matchedCharactersCount > 0 && pattern[(matchedCharactersCount + 1) - 1] != text[(i) - 1])
                {
                    matchedCharactersCount = prefixesLengthOfLongestProperSuffixThatIsItselfAPrefix[matchedCharactersCount];
                }

                if (pattern[(matchedCharactersCount + 1) - 1] == text[(i) - 1])
                {
                    ++matchedCharactersCount;
                }

                if (matchedCharactersCount == pattern.Length)
                {
                    yield return i - pattern.Length;

                    matchedCharactersCount = prefixesLengthOfLongestProperSuffixThatIsItselfAPrefix[matchedCharactersCount];
                }
            }
        }

        public static IReadOnlyList<int> ComputePrefixesLengthOfLongestProperSuffixThatIsItselfAPrefix(string pattern)
        {
            int[] prefixesLengthOfLongestProperSuffixThatIsItselfAPrefix = new int[pattern.Length + 1];
            int lengthOfLongestProperSuffixThatIsItselfAPrefix = prefixesLengthOfLongestProperSuffixThatIsItselfAPrefix[1] = 0;

            for (int i = 2; i <= pattern.Length; ++i)
            {
                while (lengthOfLongestProperSuffixThatIsItselfAPrefix > 0
                    && pattern[(lengthOfLongestProperSuffixThatIsItselfAPrefix + 1) - 1] != pattern[(i) - 1])
                {
                    lengthOfLongestProperSuffixThatIsItselfAPrefix =
                        prefixesLengthOfLongestProperSuffixThatIsItselfAPrefix[lengthOfLongestProperSuffixThatIsItselfAPrefix];
                }

                if (pattern[(lengthOfLongestProperSuffixThatIsItselfAPrefix + 1) - 1] == pattern[(i) - 1])
                {
                    ++lengthOfLongestProperSuffixThatIsItselfAPrefix;
                }

                prefixesLengthOfLongestProperSuffixThatIsItselfAPrefix[i] = lengthOfLongestProperSuffixThatIsItselfAPrefix;
            }

            return Array.AsReadOnly(prefixesLengthOfLongestProperSuffixThatIsItselfAPrefix);
        }
    }
}
