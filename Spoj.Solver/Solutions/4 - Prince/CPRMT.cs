using System;
using System.Text;

// https://www.spoj.com/problems/CPRMT/ #ad-hoc #buckets #strings
// From two strings finds the longest string with some permutation being a subsequence of both.
public static class CPRMT
{
    // Note the subsequence doesn't have to be contiguous. Since we can take any permutation of
    // the string, the string from all common characters between the two strings is the
    // best possible. Each string has all the characters, so in whatever order they appear
    // defines the necessary permutation. We can't do any better, since if the characters
    // aren't in common between the two, at least one doesn't have all the characters and
    // therefore no subsequence containing those characters could exist. The problem
    // says if there's more than one x print in alphabetical order, and of course there will be
    // if they have more than one unique character in common, since any permutation of x is an x.
    public static string Solve(string first, string second)
    {
        var common = new StringBuilder();

        // Gonna take some care to do this in a single pass, rather than 26 passes with LINQ's
        // Count(). We know they're lowercase so buckets are convenient. For unbucketable we
        // might need to sort both, then emulate a merge while counting up, or O(n) w/ overhead
        // from a HashSet may be worth it.
        int[] firstCharacterCounts = new int[26];
        int[] secondCharacterCounts = new int[26];

        foreach (char c in first)
        {
            ++firstCharacterCounts[c - 'a'];
        }

        foreach (char c in second)
        {
            ++secondCharacterCounts[c - 'a'];
        }

        for (char c = 'a'; c <= 'z'; ++c)
        {
            common.Append(c, repeatCount: Math.Min(
                firstCharacterCounts[c - 'a'],
                secondCharacterCounts[c - 'a']));
        }

        return common.ToString();
    }
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        string first, second;
        while ((first = Console.ReadLine()) != null)
        {
            second = Console.ReadLine();

            output.AppendLine(
                CPRMT.Solve(first, second));
        }

        Console.Write(output);
    }
}
