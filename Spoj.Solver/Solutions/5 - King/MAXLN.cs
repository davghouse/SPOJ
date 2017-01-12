using System;

// http://www.spoj.com/problems/MAXLN/ #math #proof
// Finds the max value of a formula given in terms of the side lengths of an inscribed triangle.
public static class MAXLN
{
    // Note by drawing a line from the center to the off-diameter corner we can create two isoceles
    // triangles, one with angle x and one with angle y. Then x + (x + y) + y = 180 => x + y = 90 (angle A)
    // => the inscribed triangle is always a right triangle. Then AB^2 + AC^2 = (2r)^2 => AB^2 = 4r^2 - AC^2
    // => s = 4r^2 - AC^2 + AC, 0 < AC < 1. By inspection or differentiation, s is maximized at
    // AC = 1/2 => s_max = 4r^2 + 0.25.
    public static decimal Solve(int radius)
        => 4m * radius * radius + 0.25m;
}

public static class Program
{
    private static void Main()
    {
        int totalTestCases = int.Parse(Console.ReadLine());
        for (int i = 1; i <= totalTestCases; ++i)
        {
            Console.WriteLine($"Case {i}: {MAXLN.Solve(int.Parse(Console.ReadLine()))}");
        }
    }
}
