using System;
using System.Collections.Generic;

// https://www.spoj.com/problems/COINS/ #dynamic-programming-1d #recursion
// Figures out if it's better to exchange a coin of value n directly for USD at a 1:1 rate,
// or split the coins up as Byteland banks allow.
public static class COINS
{
    private const int _cachedLimit = 15258; // 1 billion / 2^16

    // Cache some of the first 500 million values necessary to do this without
    // recursive calls. The limit for the cache was picked through experimentation;
    // doesn't take too long to create the cache, and trims the exponentially growing
    // recursion enough to be fast for the problem's input.
    private static readonly IReadOnlyList<long> _exchangeValues;

    static COINS()
    {
        long[] exchangeValues = new long[_cachedLimit + 1];
        exchangeValues[0] = 0;

        for (int n = 1; n <= _cachedLimit; ++n)
        {
            exchangeValues[n] = Math.Max(n,
                exchangeValues[n / 2] + exchangeValues[n / 3] + exchangeValues[n / 4]);
        }

        _exchangeValues = exchangeValues;
    }

    public static long Solve(int n)
    {
        if (n <= _cachedLimit)
            return _exchangeValues[n];

        return Math.Max(n, Solve(n / 2) + Solve(n / 3) + Solve(n / 4));
    }
}

public static class Program
{
    private static void Main()
    {
        string line;
        while ((line = Console.ReadLine()) != null)
        {
            Console.WriteLine(
                COINS.Solve(int.Parse(line)));
        }
    }
}
