using System;
using System.Collections.Generic;

// https://www.spoj.com/problems/SUMFOUR/ #ad-hoc #binary-search #combinatorics #hash-table
// Counts all 4-tuples adding up to zero, a + b + c + d = 0.
public static class SUMFOUR
{
    // The arrays don't necessarily contain distinct elements, but duplicate
    // tuples (across different indices) must be counted separately anyway.
    // a + b + c + d = 0 => a + b = -(c + d). The size of the arrays is limited
    // to 2500, and 2500 * 2500 isn't too big, so we can store all the possible
    // (c + d)'s in a dictionary, then loop over a + b looking for matches.
    public static long Solve(int[] a, int[] b, int[] c, int[] d)
    {
        int size = a.Length;

        // Will at worst need size * size capacity if every c + d is different.
        var cdResultCounts = new Dictionary<int, int>(capacity: size * size);
        for (int i = 0; i < size; ++i)
        {
            for (int j = 0; j < size; ++j)
            {
                int result = c[i] + d[j];
                int currentResultCount;

                if (cdResultCounts.TryGetValue(result, out currentResultCount))
                {
                    cdResultCounts[result] = currentResultCount + 1;
                }
                else
                {
                    cdResultCounts[result] = 1;
                }
            }
        }

        // Will at worst have 2500^4 tuples if all zeros, so we need a long.
        long validTupleCount = 0;
        for (int i = 0; i < size; ++i)
        {
            for (int j = 0; j < size; ++j)
            {
                int cdResultCount;
                if (cdResultCounts.TryGetValue(-(a[i] + b[j]), out cdResultCount))
                {
                    validTupleCount += cdResultCount;
                }
            }
        }

        return validTupleCount;
    }
}

public static class Program
{
    private static void Main()
    {
        int size = int.Parse(Console.ReadLine());
        int[] a = new int[size];
        int[] b = new int[size];
        int[] c = new int[size];
        int[] d = new int[size];

        for (int i = 0; i < size; ++i)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            a[i] = line[0];
            b[i] = line[1];
            c[i] = line[2];
            d[i] = line[3];
        }

        Console.WriteLine(
            SUMFOUR.Solve(a, b, c, d));
    }
}
