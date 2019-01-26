using System;

// https://www.spoj.com/problems/WILLITST/ #experiment #game #math
// Determines if the specified algorithm ever stops for a given input.
public static class WILLITST
{
    // If the second branch gets hit, the number maps to 3(n + 1) and definitely has a factor of 3.
    // That means that second branch eventually gets hit again, even if a lot of factors of 2 get divided out.
    // And when it's hit again, it still has a factor of 3 after mapping to 3(n + 1), and the process repeats...
    // So, only powers of two will ever stop.
    public static bool Solve(long n)
        => n <= 1 || IsPowerOfTwo(n);

    // http://stackoverflow.com/a/600306
    private static bool IsPowerOfTwo(long n)
        => n <= 0 ? false : (n & (n - 1)) == 0;
}

public static class Program
{
    private static void Main()
    {
        Console.WriteLine(
            WILLITST.Solve(long.Parse(Console.ReadLine())) ? "TAK" : "NIE");
    }
}
