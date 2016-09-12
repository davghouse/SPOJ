using System;

// 14930 http://www.spoj.com/problems/FARIDA/ Princess Farida
// Takes the most coins from a series of monsters, where it's not possible to take from adjacent monsters.
public static class FARIDA
{
    private static long[] _maxCoinsCollectable = new long[10000];

    // The max coins collected through the first k monsters is max of: the coin count for the kth monster
    // plus the solution through k - 2 monsters (so the (k - 1)th monster definitely isn't chosen),
    // or the solution through the first k - 1 monsters. So 1D dynamic programming where the array's index
    // is the solution when travelling only past monsters [0 ... index].
    public static long Solve(int monsterCount, int[] coinCounts)
    {
        if (monsterCount == 0) return 0;
        if (monsterCount == 1) return coinCounts[0];

        _maxCoinsCollectable[0] = coinCounts[0];
        _maxCoinsCollectable[1] = Math.Max(coinCounts[0], coinCounts[1]);
        for (int m = 2; m < monsterCount; ++m)
        {
            _maxCoinsCollectable[m] = Math.Max(
                coinCounts[m] + _maxCoinsCollectable[m - 2],
                _maxCoinsCollectable[m - 1]);
        }

        return _maxCoinsCollectable[monsterCount - 1];
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        for (int t = 1; t <= remainingTestCases; ++t)
        {
            int monsterCount = int.Parse(Console.ReadLine());
            int[] coinCounts = Array.ConvertAll(
                Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
                int.Parse);

            Console.WriteLine(
                $"Case {t}: {FARIDA.Solve(monsterCount, coinCounts)}");
        }
    }
}
