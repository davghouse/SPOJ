using System;
using System.Collections.Generic;

// Bytelandian gold coins
// 346 http://www.spoj.com/problems/COINS/
// Figures out if it's better to exchange a coin of value n directly for USD at a 1:1 rate,
// or split the coins up as Byteland banks allow (n/2, n/3, n/4 coins and then recursively on those...).
public static class COINS
{
    private const int _cachedLimit = 15258; // 1 billion / 2^16

    // Cache some of the first 500 million values necessary to do this without recursive calls.
    // The limit for the cache was picked through experimentation; doesn't take too long to
    // create the cache, and trims the exponentially growing recursion enough to be fast for the problem's input.
    private static IReadOnlyList<long> _exchangeValues;

    static COINS()
    {
        var exchangeValues = new long[_cachedLimit + 1];
        exchangeValues[0] = 0;

        for (int n = 1; n <= _cachedLimit; ++n)
        {
            exchangeValues[n] = Math.Max(n, GetExchangeValue(n / 2) + GetExchangeValue(n / 3) + GetExchangeValue(n / 4));
        }

        _exchangeValues = exchangeValues;
    }

    private static long GetExchangeValue(int n)
    {
        if (n <= _cachedLimit)
            return _exchangeValues[n];

        return Math.Max(n, GetExchangeValue(n / 2) + GetExchangeValue(n / 3) + GetExchangeValue(n / 4));
    }

    public static long Solve(int n)
        => GetExchangeValue(n);
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