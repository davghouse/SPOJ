using System;
using System.Text;

// https://www.spoj.com/problems/DISUBSTR/ #sorting #strings #suffixes
// Counts the number of distinct substrings in a string.
public static class DISUBSTR
{
    // This uses the well-known concept of a suffix array. The string we're given is pretty short and there
    // aren't many test cases, so it's fine to create the sorted suffix start indices in O(nWlogn). I don't
    // actually create the substrings though. We don't need to create the LCP array, but the same concept
    // is used to count distinct substrings.
    public static int Solve(string s)
    {
        int[] sortedSuffixStartIndices = new int[s.Length];
        for (int i = 0; i < s.Length; ++i)
        {
            sortedSuffixStartIndices[i] = i;
        }
        // Important to compare ordinal and not actually create the substrings for performance reasons.
        Array.Sort(sortedSuffixStartIndices, (i, j) => string.CompareOrdinal(s, i, s, j, s.Length));

        // Every substring is the prefix of a suffix, the suffix starting at the substring's start index,
        // the prefix as long as the substring. Having the suffixes ordered allows us to count distinct
        // substrings by comparing adjacent sorted suffixes. The ith sorted suffix has some prefix length
        // in common with the ith + 1, so those substrings are repeated in the string. We wait to count
        // the ones in common until they become not in common for some j > i in a later iteration. For the
        // length that's not in common with the ith + 1, we count it now. Distinct substrings are only
        // counted once because they're not counted until the first time they're not in common with the
        // next suffix, and once they're not in common with the next suffix, they never happen again,
        // since the suffixes are ordered.
        int distinctSubstringCount = 0;
        for (int i = 0; i < s.Length - 1; ++i)
        {
            int currentSuffixStartIndex = sortedSuffixStartIndices[i];
            int nextSuffixStartIndex = sortedSuffixStartIndices[i + 1];
            int currentSuffixLength = s.Length - currentSuffixStartIndex;
            int nextSuffixLength = s.Length - nextSuffixStartIndex;
            int maxPrefixLengthInCommon = Math.Min(currentSuffixLength, nextSuffixLength);

            int prefixLengthInCommon = 0;
            while (prefixLengthInCommon < maxPrefixLengthInCommon
                && s[currentSuffixStartIndex + prefixLengthInCommon] == s[nextSuffixStartIndex + prefixLengthInCommon])
            {
                ++prefixLengthInCommon;
            }

            distinctSubstringCount += currentSuffixLength - prefixLengthInCommon;
        }
        // Everything in the last suffix still needs to be counted, since anything in common with it hasn't been counted yet.
        distinctSubstringCount += s.Length - sortedSuffixStartIndices[s.Length - 1];

        return distinctSubstringCount;
    }
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            output.Append(DISUBSTR.Solve(Console.ReadLine()));
            output.AppendLine();
        }

        Console.Write(output);
    }
}
