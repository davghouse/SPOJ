using System;
using System.Collections.Generic;

// Feynman
// 3410 http://www.spoj.com/problems/SAMER08F/
// Returns the number of squares in a grid of n x n squares, for 1 <= n <= 100.
public static class SAMER08F
{
    private const int _limit = 100;
    private static readonly IReadOnlyList<int> _squareCounts;

    static SAMER08F()
    {
        var squareCounts = new int[_limit + 1];
        squareCounts[0] = 0;
        squareCounts[1] = 1;
        squareCounts[2] = 5;

        for (int n = 3; n <= _limit; ++n)
        {
            // Add the parent square as a whole.
            squareCounts[n] += 1;
            // Add the squares the touch only one of the parent square's four corners.
            // For example, growing from the top left for n = 3 there's square n = 1, and square n = 2, and this repeats for each corner.
            squareCounts[n] += 4 * (n - 1);
            // Add the squares that touch the parent square's edge but don't touch one of the parent square's four corners.
            // For example, for n = 4 there's two unused edge squares on each side, from the first there's two (n = 1, n = 2) second there's one (n = 1).
            // In general, for each side there's n - 2 squares to start from, the first gives n - 2 squares, the second n - 3 squares, etc, to one.
            squareCounts[n] += 4 * SumFromOneUntil(n - 2);
            // Add the counts from interior square (touching no edges or corners) of dimension n - 2.
            squareCounts[n] += squareCounts[n - 2];
        }

        _squareCounts = Array.AsReadOnly(squareCounts);
    }

    private static int SumFromOneUntil(int n)
        => n * (n + 1) / 2;

    public static int Solve(int n)
        => _squareCounts[n];
}

public static class Program
{
    private static void Main()
    {
        int n;
        while ((n = int.Parse(Console.ReadLine())) != 0)
        {
            Console.WriteLine(
                SAMER08F.Solve(n));
        }
    }
}