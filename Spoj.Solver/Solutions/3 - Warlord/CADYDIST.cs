using System;
using System.Linq;

// https://www.spoj.com/problems/CADYDIST/ #ad-hoc #experiment #sorting
// Given class sizes and candy prices, feed all students in the cheapest way possible.
public static class CADYDIST
{
    // Similar to FASHION (which is ranked higher because it's confusing). One type of
    // candy is bought per class. In order to minimize cost, buy the cheapest candy for
    // the biggest class, and so on.
    public static long Solve(int[] classSizes, int[] candyPrices)
    {
        Array.Sort(classSizes);
        Array.Sort(candyPrices);

        return classSizes.Reverse()
            .Zip(candyPrices, (s, p) => s * (long)p)
            .Sum();
    }
}

public static class Program
{
    private static void Main()
    {
        int classCount;
        while ((classCount = int.Parse(Console.ReadLine())) != 0)
        {
            int[] classSizes = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int[] candyPrices = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            Console.WriteLine(
                CADYDIST.Solve(classSizes, candyPrices));
        }
    }
}
