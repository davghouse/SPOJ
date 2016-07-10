using System;
using System.Collections.Generic;

// 1724 http://www.spoj.com/problems/TRICOUNT/ Counting Triangles
// Counts triangles (of all sizes) for different levels of the given construction.
public static class TRICOUNT
{
    private const int _maxLevel = 1000000;
    private static readonly IReadOnlyList<long> _triangleCounts;

    // See image for details: http://imgur.com/Sa2yh7R.
    static TRICOUNT()
    {
        var triangleCounts = new long[_maxLevel + 1];
        triangleCounts[0] = 0;

        // Important that n starts out as a long here as intermediate calculations will exceed int.MaxValue.
        for (long n = 1; n <= _maxLevel; ++n)
        {
            triangleCounts[n] = triangleCounts[n - 1] + (n + 1) * n / 2 + (long)Math.Ceiling(0.25 * (n * n - 1));
        }

        _triangleCounts = triangleCounts;
    }

    public static long Solve(int level)
        => _triangleCounts[level];
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        while (remainingTestCases-- > 0)
        {
            Console.WriteLine(
                TRICOUNT.Solve(int.Parse(Console.ReadLine())));
        }
    }
}
