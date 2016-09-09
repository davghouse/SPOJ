using System;
using System.Collections.Generic;

// 740 http://www.spoj.com/problems/TRT/ Treats for the Cows
// Finds the optimal order to sell cow treats that become more valuable over time.
public static class TRT // v1, top-down, recursion with memoization
{
    private struct IndexRange
    {
        public int Start { get; set; }
        public int End { get; set; }

        public static IndexRange Create(int startIndex, int endIndex)
            => new IndexRange
            {
                Start = startIndex,
                End = endIndex,
            };
    }

    // Observation: for a given range, the starting age is always the same. That's because the starting age
    // corresponds to the number of treats chosen before arriving at the range, and to arrive at a range
    // we must choose whatever's to the left of it and whatever's to the right of it, and that's the only way.
    // The order those treats outside the range were chosen can differ, but that doesn't affect the range's starting age.
    // So the memoization doesn't need to worry about age (this was realized after studying the non-memoized recursive sol'n).
    // Unfortunately memoization gives a TLE, but it helps motivate a bottom-up DP in TRT_v2.
    public static int Solve(int[] treatValues)
        => SolveWithMemoization(
            treatValues,
            IndexRange.Create(0, treatValues.Length - 1),
            age: 1,
            memoizer: new Dictionary<IndexRange, int>());

    private static int SolveWithMemoization(
        int[] treatValues, IndexRange indexRange, int age, Dictionary<IndexRange, int> memoizer)
    {
        if (indexRange.Start == indexRange.End)
            return age * treatValues[indexRange.Start];

        int result;
        if (memoizer.TryGetValue(indexRange, out result))
            return result;

        result = Math.Max(
            age * treatValues[indexRange.Start]
            + SolveWithMemoization(treatValues, IndexRange.Create(indexRange.Start + 1, indexRange.End), age + 1, memoizer),
            age * treatValues[indexRange.End]
            + SolveWithMemoization(treatValues, IndexRange.Create(indexRange.Start, indexRange.End - 1), age + 1, memoizer));

        memoizer.Add(indexRange, result);

        return result;
    }
}

public static class Program
{
    private static void Main()
    {
        int treatCount = int.Parse(Console.ReadLine());
        var treatValues = new int[treatCount];

        for (int i = 0; i < treatCount; ++i)
        {
            treatValues[i] = int.Parse(Console.ReadLine());
        }

        Console.WriteLine(TRT.Solve(treatValues));
    }
}
