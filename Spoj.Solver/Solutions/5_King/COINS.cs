using System;
using System.Collections.Generic;

// COINS
// 346 http://www.spoj.com/problems/COINS/
// Figures out if it's better to exchange a coin of value n directly for USD at a 1:1 rate,
// or split the coins up as Byteland banks allow (n/2, n/3, n/4 coins and then recursively on those...).
public static class COINS
{
    private const int _limit = 1000000000;
    private const int _cachedLimit = 976562; // 1 billion / 2^10

    // Roughly, given a coin of value n, look up its exchange value index in the indices list.
    // Then, use the index to find the exchange value from the values list. Not enough memory
    // to store longs for each n, so store shorts that map to longs (turns out the number of
    // distinct values is on the order of only 1000). The mapper facilitates working with these
    // two lists; after calculating the value, look up the short which indexes the value in the values list.
    // And on top of that, to calculate all solutions only need up to limit/2 (for the n/2 split).
    // ** Actually, doing it up to limit/2 is way too slow. Do it partially on some handpicked value,
    // which doesn't take long but trims out enough of the exponentially growing recursion to make it fast.
    private static short[] _exchangeValueIndices;
    private static List<long> _exchangeValues;
    private static Dictionary<long, short> _exchangeValueIndexMapper;

    static COINS()
    {
        _exchangeValueIndices = new short[_cachedLimit + 1];
        _exchangeValues = new List<long>();
        _exchangeValueIndexMapper = new Dictionary<long, short>();

        _exchangeValueIndices[0] = 0;
        _exchangeValues.Add(0);
        _exchangeValueIndexMapper.Add(0, 0);

        for (int n = 1; n <= _cachedLimit; ++n)
        {
            long exchangeValue = Math.Max(n, GetExchangeValue(n / 2) + GetExchangeValue(n / 3) + GetExchangeValue(n / 4));

            short exchangeValueIndex;
            if (!_exchangeValueIndexMapper.TryGetValue(exchangeValue, out exchangeValueIndex))
            {
                // Haven't seen this exchange value before, so add it to the list and store its index
                // in the value index mapper so it's easy to find for other n's with this value.
                _exchangeValues.Add(exchangeValue);
                exchangeValueIndex = (short)(_exchangeValues.Count - 1);
                _exchangeValueIndexMapper.Add(exchangeValue, exchangeValueIndex);
            }

            _exchangeValueIndices[n] = exchangeValueIndex;
        }
    }

    private static long GetExchangeValue(int n)
    {
        if (n <= _cachedLimit)
            return _exchangeValues[_exchangeValueIndices[n]];

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