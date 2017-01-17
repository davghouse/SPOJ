using System;
using System.Collections.Generic;
using System.Text;

// http://www.spoj.com/problems/FIBOSUM/ #formula #math #memoization #mod-math #research #sequence
// Finds the sum of the fibonacci numbers in a range.
public static class FIBOSUM
{
    private const int _mod = 1000000007;
    private const int _precomputedLimit = 1000;
    private static readonly IReadOnlyList<int> _precomputedFibNums;
    private static readonly Dictionary<int, int> _fibNumMemoizer = new Dictionary<int, int>();

    static FIBOSUM()
    {
        int[] precomputedFibNums = new int[_precomputedLimit + 1];
        precomputedFibNums[0] = 0;
        precomputedFibNums[1] = 1;

        for (int i = 2; i <= _precomputedLimit; ++i)
        {
            precomputedFibNums[i] = (precomputedFibNums[i - 1] + precomputedFibNums[i - 2]) % _mod;
        }

        _precomputedFibNums = precomputedFibNums;
    }

    // https://en.wikipedia.org/wiki/Fibonacci_number
    // https://en.wikipedia.org/wiki/Modular_arithmetic
    // http://stackoverflow.com/questions/1082917/mod-of-negative-number-is-melting-my-brain
    // Lots of formulas on the Wikipedia page for Fibonacci numbers. One of them
    // tells us fib sum 1 to n equals fib(n + 2) - 1, so we just need to calculate
    // a single really large Fibonacci number. There's a form with phi to a power
    // but there's no way to mod intermediate results (necessary, so they fit into a C# data type)
    // when we're dealing with non-integers. Normal fib calculation will be way too slow,
    // but somewhere in the article there's two formulas for fib n in terms of n / 2, so
    // it'll be log(n) with memoization and it allows us to mod intermediate results so
    // we'll be dealing with at most int64s in the intermediate (squaring 1 billion and 7 fits into long!).
    // F(2n - 1) = F(n)^2 + F(n - 1)^2, F(2n) = (2F(n - 1) + F(n))F(n), mods applied liberally.
    // Throughout what follows I say 'fib nums' but I really mean 'fib nums mod 1 billion and 7.'
    public static int Solve(int rangeStart, int rangeEnd)
    {
        // Sum through the end, minus sum up to the start that we shouldn't have counted:
        int rangeSum = GetFibNum(rangeEnd + 2) - GetFibNum((rangeStart - 1) + 2);

        // The above result could've been negative if the 2nd term was larger (due to mods),
        // but what we want is the range sum and that should always be positive after mods, so adjust.
        return rangeSum >= 0 ? rangeSum : rangeSum + _mod;
    }

    private static int GetFibNum(int n)
    {
        if (n <= _precomputedLimit)
            return _precomputedFibNums[n];

        int nFibNum;
        if (_fibNumMemoizer.TryGetValue(n, out nFibNum))
            return nFibNum;

        if (n % 2 == 0)
        {
            long nDiv2Minus1FibNum = GetFibNum(n / 2 - 1);
            long nDiv2FibNum = GetFibNum(n / 2);
            nFibNum = (int)(((((2 * nDiv2Minus1FibNum % _mod) + nDiv2FibNum) % _mod) * nDiv2FibNum) % _mod);
        }
        else // n % 2 == 1
        {
            long nPlus1Div2FibNum = GetFibNum((n + 1) / 2);
            long nDiv2FibNum = GetFibNum(n / 2);
            nFibNum = (int)((nPlus1Div2FibNum * nPlus1Div2FibNum % _mod + nDiv2FibNum * nDiv2FibNum % _mod) % _mod);
        }

        _fibNumMemoizer[n] = nFibNum;

        return nFibNum;
    }
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int[] range = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            output.Append(FIBOSUM.Solve(range[0], range[1]));
            output.AppendLine();
        }

        Console.Write(output);
    }
}
