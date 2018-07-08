using System;

// https://www.spoj.com/problems/AE00/ #division #experiment #math
// Counts the number of rectangles that can be constructed from n 1x1 squares (using any number of those squares), 1 <= n <= 10000.
// More info here: http://mathschallenge.net/library/number/number_of_divisors, https://en.wikipedia.org/wiki/Divisor_function
public static class AE00
{
    public static int Solve(int n)
    {
        // Rectangles from n squares can use any number of squares, that is, n squares can make (recursively)
        // as many rectangles as n - 1 squares make, plus however many rectangles from exactly n squares.
        // So we'll keep a cumulative count of rectangles made from s = 1 squares up to s = n squares.
        int cumulativeRectangleCount = 0;
        for (int s = 1; s <= n; ++s)
        {
            // There's always at least one rectangle made from s squares, with dimensions 1 x s.
            cumulativeRectangleCount += 1;

            // After that, look for how many ways there are to represent s as a product of two integers > 1.
            for (int d = 2; d <= Math.Sqrt(s); ++d)
            {
                // If d divides s evenly, d x (s / d) are the dimensions of a rectangle made up from s squares.
                if (s % d == 0)
                {
                    cumulativeRectangleCount += 1;
                }
            }
        }

        return cumulativeRectangleCount;
    }
}

public static class Program
{
    private static void Main()
    {
        Console.WriteLine(
            AE00.Solve(int.Parse(Console.ReadLine())));
    }
}
