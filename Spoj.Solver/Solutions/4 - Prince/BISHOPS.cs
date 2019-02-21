using System;
using System.Numerics;

// https://www.spoj.com/problems/BISHOPS/ #ad-hoc #math
// Finds the maximum number of bishops that can be (safely) placed on a given-size chessboard.
public static class BISHOPS
{
    // I didn't prove optimality but it's easy to show at least 2n - 2 bishops is always
    // possible by placing n along the top and n - 2 along the base (none in the corners).
    // Might be easy to show 2n isn't possible.
    public static BigInteger Solve(BigInteger size)
        => size == 1 ? 1 : 2 * size - 2;
}

public static class Program
{
    private static void Main()
    {
        string line;
        while ((line = Console.ReadLine()) != null)
        {
            Console.WriteLine(
                BISHOPS.Solve(BigInteger.Parse(line)));
        }
    }
}
