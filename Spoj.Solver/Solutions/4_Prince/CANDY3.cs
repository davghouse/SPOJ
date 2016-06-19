using System;
using System.Collections.Generic;
using System.Linq;

// Candy III
// 2148 http://www.spoj.com/problems/CANDY3/
// Determines if N bags full of candies can have their contents redistributed equally amongst N children.
public static class TEMPLATE
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
            Console.ReadLine(); // Remove blank line.

            int backpackCount = int.Parse(Console.ReadLine());
            ulong[] backpackCandyCounts = new ulong[backpackCount];

            for (int i = 0; i < backpackCount; ++i)
            {
                backpackCandyCounts[i] = ulong.Parse(Console.ReadLine());
            }

            Console.WriteLine(
                TEMPLATE.Solve(backpackCandyCounts));
        }
    }
}