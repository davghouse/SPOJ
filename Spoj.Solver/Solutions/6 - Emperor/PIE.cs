using System;
using System.Linq;

// https://www.spoj.com/problems/PIE/ #binary-search #optimization
// Finds out how much pie each person can get when they all need the same amount.
public static class PIE
{
    public static double Solve(int pieCount, int personCount, int[] pieRadii)
    {
        double[] orderedPieVolumes = pieRadii
            // Pies are cylinders of height 1.
            .Select(r => Math.PI * r * r * 1)
            // Order so that we can speed up the verifier a bit--unrelated to the binary search.
            .OrderByDescending(v => v)
            .ToArray();
        double maxPieVolume = orderedPieVolumes.First();

        return BinarySearch.Search(
            start: 0.00001,
            // Can't combine volume from multiple pies, so this is the max possible slice size.
            end: maxPieVolume,
            // Epsilon can be as low as 0.0001 to get AC, but this guarantees same output as example.
            epsilon: 0.00001,
            verifier: sv => CanDistributeSlices(pieCount, personCount, orderedPieVolumes, sv),
            mode: BinarySearch.Mode.TrueToFalse) ?? 0;
    }

    private static bool CanDistributeSlices(
        int pieCount, int personCount, double[] orderedPieVolumes, double sliceVolume)
    {
        int unfedPersonCount = personCount;

        for (int p = 0; p < pieCount && unfedPersonCount > 0; ++p)
        {
            int piesSliceCount = (int)(orderedPieVolumes[p] / sliceVolume);

            // The volumes are ordered. If this pie doesn't contribute any slices, no
            // future pies will either. Since we haven't fed everyone yet, we never will.
            if (piesSliceCount == 0) return false;

            unfedPersonCount -= piesSliceCount;
        }

        return unfedPersonCount <= 0;
    }
}

// This facilitates predicate-based binary searching, where the values being searched on
// satisfy the predicate in an ordered manner, in one of two ways:
// [false false false ... false true ... true true true] (true => anything larger is true)
// [true true true ... true false ... false false false] (true => anything smaller is true)
// In the first, the goal of the search is to locate the smallest value satisfying the predicate.
// In the second, the goal of the search is to locate the largest value satisfying the predicate.
// For more info, see: https://www.topcoder.com/community/data-science/data-science-tutorials/binary-search/.
public static class BinarySearch
{
    public enum Mode
    {
        FalseToTrue,
        TrueToFalse
    };

    public static double? Search(
        double start, double end, double epsilon, Predicate<double> verifier, Mode mode)
        => mode == Mode.FalseToTrue
        ? SearchFalseToTrue(start, end, epsilon, verifier)
        : SearchTrueToFalse(start, end, epsilon, verifier);

    private static double? SearchFalseToTrue(
        double start, double end, double epsilon, Predicate<double> verifier)
    {
        if (start > end) return null;

        double mid;

        while (end - start > epsilon)
        {
            mid = start + (end - start) / 2;

            if (verifier(mid))
            {
                end = mid;
            }
            else
            {
                start = mid + epsilon / 2;
            }
        }

        return verifier(start) ? start : (double?)null;
    }

    private static double? SearchTrueToFalse(
        double start, double end, double epsilon, Predicate<double> verifier)
    {
        if (start > end) return null;

        double mid;

        while (end - start > epsilon)
        {
            mid = start + (end - start) / 2;

            if (verifier(mid))
            {
                start = mid;
            }
            else
            {
                end = mid - epsilon / 2;
            }
        }

        return verifier(start) ? start : (double?)null;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();
            int pieCount = int.Parse(line[0]);
            int friendCount = int.Parse(line[1]);
            int[] pieRadii = Array.ConvertAll(
                Console.ReadLine().Trim().Split(),
                int.Parse);

            Console.WriteLine(
                PIE.Solve(pieCount, friendCount + 1, pieRadii).ToString("F4"));
        }
    }
}
