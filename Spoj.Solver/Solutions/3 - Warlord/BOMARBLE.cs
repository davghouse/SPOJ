using System;

// https://www.spoj.com/problems/BOMARBLE/ #math
// Calculates the number of marbles in nested pentagons.
// See BOMARBLE.cpp--this solution was submitted using C++ because C# was unavailable.
public static class BOMARBLE
{
    // I don't know what's going on with the intersecting lines or whatever,
    // but n=4 is 35 I guess? If that's the case, then the second difference
    // (https://www.purplemath.com/modules/nextnumb2.htm) is always 3, so the
    // sequence is defined by a quadratic. Some calculation shows it's this one:
    // (3/2)n^2 + (5/2)n + 1 = (3n^2 + 5n + 2)/2. Easy to prove the numerator
    // is always even (so answer is always an integer), as we'd expect.
    public static int Solve(int n)
        => (3 * n * n + 5 * n + 2) / 2;
}

public static class Program
{
    private static void Main()
    {
        int n;
        while ((n = int.Parse(Console.ReadLine())) != 0)
        {
            Console.WriteLine(
                BOMARBLE.Solve(n));
        }
    }
}
