using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

// http://www.spoj.com/problems/ABCDEF/ #ad-hoc #combinatorics #hash-table #math
// Counts all sextuples satisfying the equation (a * b + c) / d - e = f.
public static class ABCDEF
{
    // Naively we can generates all sextuples and test each. Generating them
    // is simple because repetitions are allowed (and nums are already distinct).
    public static long SolveSlowly(int[] nums)
    {
        long validSextupleCount = 0;
        foreach (int a in nums)
        {
            foreach (int b in nums)
            {
                foreach (int c in nums)
                {
                    foreach (int d in nums.Where(n => n != 0 && (a * b + c) % n == 0))
                    {
                        foreach (int e in nums)
                        {
                            foreach (int f in nums)
                            {
                                if ((a * b + c) / d - e == f)
                                {
                                    ++validSextupleCount;
                                }
                            }
                        }
                    }
                }
            }
        }

        return validSextupleCount;
    }

    // Equivalently we're trying to find: (a * b + c) / d == e + f. We can make use of
    // multiplicative commutativity to search through all of a's nums but about half of b's.
    // Only some d's need searched through; non-zero, and ones that evenly divide a * b + c,
    // as to equal e + f the result must be an integer. And I guess the possible results of
    // e + f can be stored in a dictionary with the # of ways to produce them, using commutativity
    // here too to cut down the search space a little bit. So looping 4-ish deep with an O(1)
    // lookup, instead of the naive approach which loops 6-ish deep.
    public static long SolveFaster(int[] nums)
    {
        // n numbers, so at most (n choose 2) + n (for equal pairs) = n * (n - 1) / 2 + n different results.
        var efResultCounts = new Dictionary<int, int>(
            capacity: (nums.Length * (nums.Length - 1)) / 2 + nums.Length);
        for (int ei = 0; ei < nums.Length; ++ei)
        {
            // Take advantage of commutativity here and in gainedResultCount.
            for (int fi = ei; fi < nums.Length; ++fi)
            {
                int currentResultCount;
                int gainedResultCount = (ei == fi ? 1 : 2);
                int sum = nums[ei] + nums[fi];

                if (efResultCounts.TryGetValue(sum, out currentResultCount))
                {
                    efResultCounts[sum] = currentResultCount + gainedResultCount;
                }
                else
                {
                    efResultCounts[sum] = gainedResultCount;
                }
            }
        }

        int[] dNums = nums.Where(n => n != 0).ToArray();

        long validSextupleCount = 0;
        for (int ai = 0; ai < nums.Length; ++ai)
        {
            // Take advantage of commutativity here and down below when incrementing the sextuple count.
            for (int bi = ai; bi < nums.Length; ++bi)
            {
                foreach (int c in nums)
                {
                    foreach (int d in dNums)
                    {
                        int abc = nums[ai] * nums[bi] + c;
                        if (abc % d != 0) continue;

                        int efResultCount;
                        if (efResultCounts.TryGetValue(abc / d, out efResultCount))
                        {
                            // For this unique combination of a, b, c, d, there are efResultCount ways
                            // to get an e and f such that (a * b + c) / d == e + f. And take into account
                            // commutativity on a and b.
                            validSextupleCount += ai == bi ? efResultCount : 2 * efResultCount;
                        }
                    }
                }
            }
        }

        return validSextupleCount;
    }

    // Similar ideas as above, except now trying (a * b + c) == d * (e + f) so we can turn
    // the 2-ish deep preprocess and 4-ish deep process into 3-ish deep each.
    public static long SolveEvenFaster(int[] nums)
    {
        int[] dNums = nums.Where(n => n != 0).ToArray();

        // n numbers, so at most (n choose 2) + n (for equal pairs) = n * (n - 1) / 2 + n different sums
        // for e + f, and then * n (maybe minus one if nums includes a zero) for the possible d factors.
        var defResultCounts = new Dictionary<int, int>(
            capacity: dNums.Length * ((nums.Length * (nums.Length - 1)) / 2 + nums.Length));
        foreach (int d in dNums)
        {
            for (int ei = 0; ei < nums.Length; ++ei)
            {
                // Take advantage of commutativity here and in gainedResultCount.
                for (int fi = ei; fi < nums.Length; ++fi)
                {
                    int currentResultCount;
                    int gainedResultCount = (ei == fi ? 1 : 2);
                    int result = d * (nums[ei] + nums[fi]);

                    if (defResultCounts.TryGetValue(result, out currentResultCount))
                    {
                        defResultCounts[result] = currentResultCount + gainedResultCount;
                    }
                    else
                    {
                        defResultCounts[result] = gainedResultCount;
                    }
                }
            }
        }

        long validSextupleCount = 0;
        for (int ai = 0; ai < nums.Length; ++ai)
        {
            // Take advantage of commutativity here and down below when incrementing the sextuple count.
            for (int bi = ai; bi < nums.Length; ++bi)
            {
                foreach (int c in nums)
                {
                    int defResultCount;
                    if (defResultCounts.TryGetValue(nums[ai] * nums[bi] + c, out defResultCount))
                    {
                        // For this unique combination of a, b, and c there are defResultCount ways
                        // to get an d, e, and f such that (a * b + c) == d * (e + f). And take into account
                        // commutativity on a and b.
                        validSextupleCount += ai == bi ? defResultCount : 2 * defResultCount;
                    }
                }
            }
        }

        return validSextupleCount;
    }
}

public static class Program
{
    private static void Main()
    {
        int[] nums = new int[int.Parse(Console.ReadLine())];
        for (int i = 0; i < nums.Length; ++i)
        {
            nums[i] = int.Parse(Console.ReadLine());
        }

        Console.WriteLine(
            ABCDEF.SolveEvenFaster(nums));
    }
}
