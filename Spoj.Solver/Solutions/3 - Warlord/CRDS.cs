using System;

// http://www.spoj.com/problems/CRDS/ #inspection #sequence
// Counts cards in a typical card pyramid, modulo 1000007.
public static class CRDS
{
    public static int Solve(int n)
    {
        long m = n;
        // There are m - 1 floors with card counts 1, 2, ... m - 1, for:
        long cardsFromFloors = m * (m - 1) / 2;
        // There are m levels with card counts of 2, 4, ..., 2m, for (cancelling the 2s):
        long cardsFromWalls = (m + 1) * m;

        return (int)((cardsFromFloors + cardsFromWalls) % 1000007);
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            Console.WriteLine(
                CRDS.Solve(int.Parse(Console.ReadLine())));
        }
    }
}
