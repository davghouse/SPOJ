using System;
using System.Collections.Generic;

// https://www.spoj.com/problems/TRT/ #memoization #optimization #recursion
// Finds the optimal order to sell cow treats that become more valuable over time.
public static class TRT // v1, top-down, recursion with memoization
{
    // Observation: for a given range, the starting age is always the same. That's
    // because the starting age corresponds to the number of treats chosen before
    // arriving at the range, and to arrive at a range we must choose whatever's
    // to the left of it and whatever's to the right of it, and that's the only way.
    // The order those treats outside the range were chosen can differ, but that
    // doesn't affect the range's starting age. So the memoization doesn't need to
    // worry about age (this was realized after studying the non-memoized recursive sol'n).
    // This gives TLE, but it helps motivate a bottom-up DP in TRT_v2.
    public static int Solve(int[] treatValues)
        => SolveWithMemoization(
            treatValues,
            indexRange: new IndexRange(0, treatValues.Length - 1),
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
                + SolveWithMemoization(
                    treatValues,
                    new IndexRange(indexRange.Start + 1, indexRange.End),
                    age + 1, memoizer),
            age * treatValues[indexRange.End]
                + SolveWithMemoization(
                    treatValues,
                    new IndexRange(indexRange.Start, indexRange.End - 1),
                    age + 1, memoizer));
        memoizer.Add(indexRange, result);

        return result;
    }

    private struct IndexRange
    {
        public IndexRange(int startIndex, int endIndex)
        {
            Start = startIndex;
            End = endIndex;
        }

        public int Start { get; }
        public int End { get; }
    }
}

public static class Program
{
    private static void Main()
    {
        int treatCount = int.Parse(Console.ReadLine());
        int[] treatValues = new int[treatCount];
        for (int i = 0; i < treatCount; ++i)
        {
            treatValues[i] = int.Parse(Console.ReadLine());
        }

        Console.WriteLine(
            TRT.Solve(treatValues));
    }
}
