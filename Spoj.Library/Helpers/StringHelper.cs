using System.Collections.Generic;

namespace Spoj.Library.Helpers
{
    public static class StringHelper
    {
        // The StringSplitOptions.RemoveEmptyEntries is implicit here.
        public static IEnumerable<string> SplitAndKeep(this string s, char[] delimiters)
        {
            int nextSubstringStartIndex = 0;
            while (nextSubstringStartIndex < s.Length)
            {
                int nextDelimiterIndex = s.IndexOfAny(delimiters, nextSubstringStartIndex);

                if (nextDelimiterIndex == nextSubstringStartIndex)
                {
                    yield return s[nextSubstringStartIndex].ToString();
                    ++nextSubstringStartIndex;
                }
                else if (nextDelimiterIndex > nextSubstringStartIndex)
                {
                    yield return s.Substring(nextSubstringStartIndex, nextDelimiterIndex - nextSubstringStartIndex);
                    nextSubstringStartIndex = nextDelimiterIndex;
                }
                else // No next delimiter found; nextDelimiterIndex = -1.
                {
                    yield return s.Substring(nextSubstringStartIndex);
                    nextSubstringStartIndex = s.Length;
                }
            }
        }
    }
}
