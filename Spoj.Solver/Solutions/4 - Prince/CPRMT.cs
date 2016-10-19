using System;
using System.Text;

// 1728 http://www.spoj.com/problems/CPRMT/ Common Permutation
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
    public static string Solve(string a, string b)
    {
        var x = new StringBuilder();

        // Gonna take some care to do this in a single pass, rather than 26 passes with LINQ's Count().
        // We know they're lowercase so buckets are convenient. For unbucketable we might need to sort both,
        // then emulate a merge while counting up, or O(n) w/ overhead from a HashSet may be worth it.
        int[] aCharacterCounts = new int[26];
        int[] bCharacterCounts = new int[26];

        foreach (char c in a)
        {
            ++aCharacterCounts[c - 'a'];
        }

        foreach (char c in b)
        {
            ++bCharacterCounts[c - 'a'];
        }

        for (char c = 'a'; c <= 'z'; ++c)
        {
            x.Append(c, Math.Min(aCharacterCounts[c - 'a'], bCharacterCounts[c - 'a']));
        }

        return x.ToString();
    }
}

public static class Program
{
    private static void Main()
    {
        string a, b;
        var output = new StringBuilder();

        while ((a = Console.ReadLine()) != null)
        {
            b = Console.ReadLine();

            output.AppendLine(
                CPRMT.Solve(a, b));
        }

        Console.Write(output);
    }
}
