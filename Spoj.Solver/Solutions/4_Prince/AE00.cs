using System;
using System.Collections.Generic;

// 4300 http://www.spoj.com/problems/AE00/ Rectangles
// Counts the number of rectangles that can be constructed from n 1x1 squares (using any number of those squares), 1 <= n <= 10000.
// More info here: http://mathschallenge.net/library/number/number_of_divisors, https://en.wikipedia.org/wiki/Divisor_function
public static class AE00
{
    private static readonly IReadOnlyList<int> _rectangleCounts;

    static AE00()
    {
        // Rectangles from n squares can use any number of squares, that is, n squares can make
        // as many rectangles as n - 1 squares make, plus however many rectangles from exactly n squares.
        var rectangleCounts = new int[10001];
        rectangleCounts[0] = 0;
        rectangleCounts[1] = 1;

        for (int n = 2; n <= 10000; ++n)
        {
            rectangleCounts[n] = rectangleCounts[n - 1];

            // Calculate how many rectangles can be created from exactly n squares.
            // There's always at least one, with dimensions 1 x n.
            // After that, look for how many ways there are to represent n as a product of two integers (dim 1 x dim 2).
            rectangleCounts[n] += 1;
            for (int i = 2; i <= Math.Sqrt(n); ++i)
            {
                // If i divides n evenly, i times some other number equals n.
                if (n % i == 0)
                {
                    rectangleCounts[n] += 1;
                }
            }
        }

        _rectangleCounts = rectangleCounts;
    }

    public static int Solve(int n)
        => _rectangleCounts[n];
}

public static class Program
{
    private static void Main()
    {
        Console.WriteLine(
            AE00.Solve(int.Parse(Console.ReadLine())));
    }
}
