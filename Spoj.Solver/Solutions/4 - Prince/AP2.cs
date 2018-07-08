using System;

// https://www.spoj.com/problems/AP2/ #experiment #math #sequence
// Given the third term, third-to-last term, and sum of an arithmetic progression, finds the whole series.
public static class AP2
{
    public static long[] Solve(long thirdTerm, long thirdToLastTerm, long sum)
    {
        // In an AP, note that pairs like (a_1, a_n), (a_2, a_n-1) always add up to the same thing.
        // A little more work shows (a_b + a_n-b) * (n/2) = sum, so n = 2 * sum / (a_b + a_n-b).
        int n = (int)(2 * sum / (thirdTerm + thirdToLastTerm));

        // Now we know the third to last term's index is n - 2, and the third term's index is 3.
        int progressionsBetweenGivenTerms = (n - 2) - 3;
        long termDifference = (thirdToLastTerm - thirdTerm) / progressionsBetweenGivenTerms;
        long firstTerm = thirdTerm - 2 * termDifference;

        long[] series = new long[n];
        series[0] = firstTerm;
        for (int i = 1; i < n; ++i)
        {
            series[i] = series[i - 1] + termDifference;
        }

        return series;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            long[] line = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);
            long[] series = AP2.Solve(line[0], line[1], line[2]);

            Console.WriteLine(series.Length);
            Console.WriteLine(string.Join(" ", series));
        }
    }
}
