using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 27 http://www.spoj.com/problems/SBANK/ Sorting Bank Accounts
// Sorts bank accounts (all #s are the same length so we don't have to worry about how).
public static class SBANK
{
    // I failed real hard with this problem, all started by using a SortedDictionary.
    // Wasn't fast enough so wrapped my int in a reference type to lessen lookups, tried
    // lots of other little tweaks, but in the end I should've realized I only need to sort
    // it once at the end, so paying the overhead of SortedDictionary isn't worth it.
    public static Dictionary<string, int> Solve(int accountCount)
    {
        var accountOccurrenceCounts = new Dictionary<string, int>();
        while (accountCount-- > 0)
        {
            string account = Console.ReadLine().Trim();

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

        return accountOccurrenceCounts;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        while (remainingTestCases-- > 0)
        {
            int accountCount = int.Parse(Console.ReadLine());
            var accountOccurrenceCounts = SBANK.Solve(accountCount);

            var output = new StringBuilder();
            foreach (var accountOccurrenceCount in accountOccurrenceCounts.OrderBy(aoc => aoc.Key))
            {
                output.AppendLine($"{accountOccurrenceCount.Key} {accountOccurrenceCount.Value.ToString()}");
            }

            Console.WriteLine(output);
        }
    }
}
