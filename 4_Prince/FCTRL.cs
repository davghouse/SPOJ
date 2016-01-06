using System;

// Factorial
// 11 http://www.spoj.com/problems/FCTRL/
// Returns the number of trailing zeros of n! (the number of factors of 10), where 1 <= n <= 1,000,000,000.
public static class FCTRL
{
    private const int _limit = 1000000000;
    private static FactorCounter _fivesCounter;
    // Cumulative count of the factors of five for 0 <= i <= _limit / 5, where i represents the ith multiple of 5.
    private static int[] _fivesCounterCumulative;

    static FCTRL()
    {
        _fivesCounter = new FactorCounter(factor: 5, limit: _limit);
        _fivesCounterCumulative = new int[_limit / 5 + 1];
        _fivesCounterCumulative[0] = 0;

        for (int i = 1; i <= _limit / 5; ++i)
        {
            _fivesCounterCumulative[i] = _fivesCounterCumulative[i - 1] + _fivesCounter.Count(i * 5);
        }
    }

    // n! has as many zeros as it has factors of 10.
    // n! has as many factors of 10 as it has min(factors of 2, factors of 5).
    // Looking at some numbers, it seems like the number of factors of 2 will always be more than the number of factors of 5.
    // So, find the number of factors of 5 for n! by accumulating the factors of 5 for the numbers 1 to n.
    public static int Solve(int n)
        => _fivesCounterCumulative[n / 5]; // Find the cumulative count for the greatest multiple of 5 less than n (corresponding to index n / 5).
}

// Constructed with a factor and a limit. Used to find how many times that factor evenly divides any input <= limit.
public sealed class FactorCounter
{
    // Stores the count we're interested in for 0 <= i <= Limit / (Factor * Factor), where i represents the ith multiple of Factor * Factor (squared for memory concerns).
    private int[] _counts;

    public int Factor { get; private set; }
    public int Limit { get; private set; }

    public FactorCounter(int factor, int limit)
    {
        Factor = factor;
        Limit = limit;

        BuildCounts();
    }

    private void BuildCounts()
    {
        _counts = new int[Limit / (Factor * Factor) + 1];
        _counts[0] = 0; // 0 * (Factor * Factor) has no factors equal to Factor.
        _counts[1] = 2; // 1 * (Factor * Factor) has two factors equal to Factor.

        for (int i = 2; i <= Limit / (Factor * Factor); ++i)
        {
            _counts[i] = Count(i) + 2; // i * (Factor * Factor) has Count(i) Factor factors, plus two.
        }
    }

    public int Count(int a)
    {
        if (a % Factor != 0)
            return 0; // a isn't a multiple of Factor, so it has no Factor factors.
        else if ((a / Factor) % Factor != 0)
            return 1; // a is a multiple of Factor, with one Factor factor.

        return _counts[a / (Factor * Factor)]; // a is a multiple of (Factor * Factor), so look up its count at the (a / (Factor * Factor))th multiple of (Factor * Factor).
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
                FCTRL.Solve(int.Parse(Console.ReadLine())));
        }
    }
}
