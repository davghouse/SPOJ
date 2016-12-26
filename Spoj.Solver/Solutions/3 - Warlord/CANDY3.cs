using System;
using System.Collections.Generic;
using System.Linq;

// http://www.spoj.com/problems/CANDY3/ #division #mod-math #trap
// Determines if N bags full of candies can have their contents redistributed equally amongst N children.
public static class CANDY3
{
    // I guess the numbers will be too big if summed directly, so need to use property of modular arithmetic:
    // (a + b) mod m == (a mod m + b mod m) mod m
    public static string Solve(ulong[] backpackCandyCounts)
    {
        uint N = (uint)backpackCandyCounts.Count;
        ulong modSum = 0;

        for (int i = 0; i < N; ++i)
        {
            modSum = (modSum + (backpackCandyCounts[i] % N)) % N;
        }

        return modSum == 0 ? "YES" : "NO";
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            Console.ReadLine();

            int backpackCount = int.Parse(Console.ReadLine());
            var backpackCandyCounts = new ulong[backpackCount];
            for (int i = 0; i < backpackCount; ++i)
            {
                backpackCandyCounts[i] = ulong.Parse(Console.ReadLine());
            }

            Console.WriteLine(
                CANDY3.Solve(backpackCandyCounts));
        }
    }
}
