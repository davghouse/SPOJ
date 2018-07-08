using System;
using System.Collections.Generic;
using System.Text;

// https://www.spoj.com/problems/NHAY/ #research #strings
// Finds all occurrences of a given pattern in a string.
public static class NHAY
{
    public static IEnumerable<int> Solve(string text, string pattern)
        => KmpStringMatcher.GetMatchIndices(text, pattern);
}

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

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int patternLength;
        while (int.TryParse(Console.ReadLine(), out patternLength))
        {
            string pattern = Console.ReadLine();
            string text = Console.ReadLine();

            int outputLengthBefore = output.Length;
            foreach (int matchIndex in NHAY.Solve(text, pattern))
            {
                output.Append(matchIndex);
                output.AppendLine();
            }

            if (output.Length == outputLengthBefore)
            {
                output.AppendLine(); // Empty line indicates no matches found.
            }
        }

        Console.Write(output);
    }
}
