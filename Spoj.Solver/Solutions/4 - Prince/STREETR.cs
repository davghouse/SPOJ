using System;

// https://www.spoj.com/problems/STREETR/ #ad-hoc #experiment #gcd
// Finds how many new trees need to be planted to make all trees an equal distance apart.
public static class STREETR
{
    // Whatever the largest distance possible between trees is, it has to evenly divide
    // each range between the existing trees. If it didn't, we'd start trying to plant
    // trees in a range and eventually not be able to place the last tree properly. So
    // compute the GCD of all the ranges. Then plant ((range length)/GCD) - 1 trees
    // in each range (draw a picture to see why - 1).
    public static int Solve(int treeCount, int[] treeCoordinates)
    {
        int gcd = treeCoordinates[1] - treeCoordinates[0];
        for (int t = 2; t < treeCount; ++t)
        {
            gcd = GreatestCommonDivisor(gcd, treeCoordinates[t] - treeCoordinates[t - 1]);
        }

        int newTreeCount = 0;
        for (int t = 1; t < treeCount; ++t)
        {
            newTreeCount += ((treeCoordinates[t] - treeCoordinates[t - 1]) / gcd) - 1;
        }

        return newTreeCount;
    }

    // This is a good article (first section): http://www.cut-the-knot.org/blue/Euclid.shtml.
    // One point to note, for a = bt + r, the gcd(a, b) divides a so it divides bt + r.
    // And it divides b, so it divides bt, which means for bt + r to be divisible by it,
    // r also needs to be divisible by it. So it divides both b and r. And the article
    // notes the importance of showing not only does it divide b and r, it's also their gcd.
    private static int GreatestCommonDivisor(int a, int b)
    {
        int temp;
        while (b != 0)
        {
            temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}

public static class Program
{
    private static void Main()
    {
        int treeCount = int.Parse(Console.ReadLine());
        int[] treeCoordinates = new int[treeCount];
        for (int t = 0; t < treeCount; ++t)
        {
            treeCoordinates[t] = int.Parse(Console.ReadLine());
        }

        Console.Write(
            STREETR.Solve(treeCount, treeCoordinates));
    }
}
