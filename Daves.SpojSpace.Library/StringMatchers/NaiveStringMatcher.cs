using System.Collections.Generic;

namespace Daves.SpojSpace.Library.StringMatchers
{
    public static class NaiveStringMatcher
    {
        public static IEnumerable<int> GetMatchIndices(string text, string pattern)
        {
            int matchIndex = -1;
            while ((matchIndex = text.IndexOf(pattern,
                // Note that IndexOf doesn't throw an exception for matchIndex == text.Length.
                startIndex: matchIndex + 1)) != -1)
            {
                yield return matchIndex;
            }
        }
    }
}
