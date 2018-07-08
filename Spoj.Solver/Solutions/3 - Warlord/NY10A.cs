using System;
using System.Collections.Generic;
using System.Linq;

// https://www.spoj.com/problems/NY10A/ #buckets #sorting #strings
// Counts the 8 different length 3 H/T substrings in a string of coin flips.
public static class NY10A
{
    public static IEnumerable<int> Solve(string flips)
    {
        // The sequence counts happen to be wanted in reverse alphabetical order,
        // so it's useful to have a sorted dictionary here, but we won't bother with a comparer.
        var threeFlipSequenceCounts = new SortedDictionary<string, int>
        {
            { "TTT", 0 }, { "TTH", 0 }, { "THT", 0 }, { "THH", 0 },
            { "HTT", 0 }, { "HTH", 0 }, { "HHT", 0 }, { "HHH", 0 }
        };

        for (int i = 0; i < flips.Length - 2; ++i)
        {
            ++threeFlipSequenceCounts[flips.Substring(i, 3)];
        }

        return threeFlipSequenceCounts.Values.Reverse();
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int testNumber = int.Parse(Console.ReadLine());

            Console.WriteLine($"{testNumber} {string.Join(" ", NY10A.Solve(Console.ReadLine()))}");
        }
    }
}
