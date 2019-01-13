using System;
using System.Collections.Generic;

// https://www.spoj.com/problems/MRECAMAN/ #ad-hoc #memoization
// Computes a special sequence with an interesting recursive definition.
public static class MRECAMAN
{
    private const int _limit = 500000;
    private static readonly IReadOnlyList<int> _sequence;

    static MRECAMAN()
    {
        int[] sequence = new int[_limit + 1];
        var sequenceSet = new HashSet<int> { 0 };

        for (int i = 1; i <= _limit; ++i)
        {
            int firstOption = sequence[i - 1] - i;
            int secondOption = sequence[i - 1] + i;
            int chosenOption = firstOption > 0 && !sequenceSet.Contains(firstOption)
                ? firstOption : secondOption;

            sequence[i] = chosenOption;
            sequenceSet.Add(chosenOption);
        }

        _sequence = sequence;
    }

    public static int Solve(int k)
        => _sequence[k];
}

public static class Program
{
    private static void Main()
    {
        int k;
        while ((k = int.Parse(Console.ReadLine())) != -1)
        {
            Console.WriteLine(
                MRECAMAN.Solve(k));
        }
    }
}
