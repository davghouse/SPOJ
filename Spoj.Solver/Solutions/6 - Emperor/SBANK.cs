using System;
using System.Collections.Generic;
using System.Text;

// 27 http://www.spoj.com/problems/SBANK/ Sorting Bank Accounts
// Sorts bank accounts (all #s are the same length so we don't have to worry about how).
public static class SBANK
{
    // TLE is an issue so I do the I/O inside of the Solve this time. At first I tried a
    // SortedDictionary and that was too slow. Then I tried a SortedList, too slow, then
    // just sorting an array of strings and iterating to get the counts, too slow. Then
    // I tried wrapping the int in a reference to reduce lookups, but still too slow...
    public static void SolveTooSlowly(int accountCount, StringBuilder output)
    {
        var accountOccurrenceCounts = new SortedDictionary<string, int>();

        while (accountCount-- > 0)
        {
            string account = Console.ReadLine();

            int occurrenceCount;
            if (accountOccurrenceCounts.TryGetValue(account, out occurrenceCount))
            {
                accountOccurrenceCounts[account] = occurrenceCount + 1;
            }
            else
            {
                accountOccurrenceCounts[account] = 1;
            }
        }
        Console.ReadLine();

        foreach (var accountOccurrenceCount in accountOccurrenceCounts)
        {
            output.AppendLine($"{accountOccurrenceCount.Key} {accountOccurrenceCount.Value}");
        }
        output.AppendLine();
    }

    // ...Then I tried a Dictionary and sorted the dictionary by key a single time during output
    // and that was fast enough. That makes sense because we only need to sort once at the end;
    // the overhead of reading from the SortedDictionary wasn't worth the benefit of maintaining sorted
    // order. Then instead of sorting the Dictionary by key at the end I added on a BST
    // to maintain the sort order--this time the BST worked because I was only inserting
    // new accounts into it, never reading from it (like the SortedDictionary case), and inserts
    // are relatively rare(?), so it had less overhead than sorting from the hash set all at once...
    public static void SolveFastEnough(int accountCount, StringBuilder output)
    {
        var sortedAccounts = new SortedSet<string>();
        var accountOccurrenceCounts = new Dictionary<string, int>();

        while (accountCount-- > 0)
        {
            string account = Console.ReadLine();

            int occurrenceCount;
            if (accountOccurrenceCounts.TryGetValue(account, out occurrenceCount))
            {
                accountOccurrenceCounts[account] = occurrenceCount + 1;
            }
            else
            {
                accountOccurrenceCounts[account] = 1;
                sortedAccounts.Add(account);
            }
        }
        Console.ReadLine();

        foreach (var account in sortedAccounts)
        {
            output.AppendLine($"{account} {accountOccurrenceCounts[account]}");
        }
        output.AppendLine();
    }

    // ...And finally here's a least-significant-digit radix sort. There are effectively only
    // 10 input characters to consider, since the whitespace characters are always at the same
    // position. And the strings are all the same length at 31, but once the whitespace is
    // ignored, only 26. The following helpful article explains LSD sorting (go back a page too):
    // http://www.informit.com/articles/article.aspx?p=2180073&seqNum=2.
    // Comparison-based sorts are O(nlogn) operations, but the operations here are string comparisons,
    // and these strings have such large prefixes in common, it's going to be like O(nWlogn). But
    // radix sort is like O(2Wn), and n is large so that logn above will matter (W is word length).
    public static void SolveEvenFaster(int accountCount, StringBuilder output)
    {
        var accountsCurrent = new string[accountCount];
        var accountsPrevious = new string[accountCount];
        for (int a = 0; a < accountCount; ++a)
        {
            accountsCurrent[a] = Console.ReadLine();
        }
        Console.ReadLine();

        for (int digit = 30; digit >= 0; --digit)
        {
            if (digit == 26 || digit == 21 || digit == 16 || digit == 11 || digit == 2)
                continue; // It's a whitespace character (from inspecting the form of the input).

            // ASCII value for '9' is 57, we don't want to have to perform subtraction (- '0') to reference
            // array positions, so though there are only 10 characters we care about, make it convenient.
            int[] characterCounts = new int['9' + 2];

            // Compute character frequency counts, for the characters '0' to '9' (but stored one index after).
            for (int a = 0; a < accountCount; ++a)
                ++characterCounts[accountsCurrent[a][digit] + 1];

            // For those 10 characters, transform their counts to indices: where their matching accounts begin.
            for (int c = '0'; c <= '9'; ++c)
                characterCounts[c + 1] += characterCounts[c];

            // Distribute the accounts using the computed character counts (which are now indices/running sums).
            for (int a = 0; a < accountCount; ++a)
                accountsPrevious[characterCounts[accountsCurrent[a][digit]]++] = accountsCurrent[a];

            // Now accountsPrevious has the current sorted progress, so swap with accountsCurrent.
            var accountsTemp = accountsPrevious;
            accountsPrevious = accountsCurrent;
            accountsCurrent = accountsTemp;
        }

        // Kind of tricky to do this (calculate the occurrenceCounts from the sorted accountsCurrent)
        // without dereferencing the accountsCurrent array more than accountCount times. Separating
        // out the append would be best for performance (to avoid the implicit string.Format stuff).
        string account = accountsCurrent[0];
        int occurrenceCount = 1;
        for (int a = 1; a < accountCount; ++a)
        {
            if (accountsCurrent[a] == account)
            {
                ++occurrenceCount;
            }
            else // Gotten to a new account, so print out info for the previous one, and reset.
            {
                output.AppendLine($"{account} {occurrenceCount}");

                account = accountsCurrent[a];
                occurrenceCount = 1;
            }
        }
        output.AppendLine($"{account} {occurrenceCount}");
        output.AppendLine();
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        var output = new StringBuilder();

        while (remainingTestCases-- > 0)
        {
            SBANK.SolveEvenFaster(
                accountCount: int.Parse(Console.ReadLine()),
                output: output);
        }

        Console.Write(output);
    }
}
