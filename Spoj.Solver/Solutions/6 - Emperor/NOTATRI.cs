using System;
using System.Collections.Generic;

// https://www.spoj.com/problems/NOTATRI/ #binary-search #sliding-window #sorting
// Figures out how many combinations of 3 sticks can't form a triangle.
public static class NOTATRI
{
    // Imagine how we would brute force this. Three for loops, i=0, j=i+1, k=j+1. This considers
    // every combination (set of three sticks, order not important) exactly once. If we sort the
    // sticks by length first, then once we've picked the short and medium-length sticks, we can
    // do a predicate-based binary search for the first stick longer than their sum. Every stick
    // after that stick is also longer, so they contribute to the non-triangle count as well.
    // This gets us to O(n^2 * logn). But we actually only need to binary search once for a given
    // short stick and the initial medium stick, to get an initial long stick. Then while the
    // short stick stays the same, we have a sliding window on the medium and long sticks. Bump
    // the medium stick, and then proceed from the previous long stick index until the new long
    // stick is long enough again. This is O(n^2).
    public static int Solve(int stickCount, int[] stickLengths)
    {
        Array.Sort(stickLengths);

        int nonTriangleCombinations = 0;

        for (int smallStickIndex = 0;
            smallStickIndex < stickCount - 2;
            ++smallStickIndex)
        {
            int smallStickLength = stickLengths[smallStickIndex];

            int initialMediumStickIndex = smallStickIndex + 1;
            int initialMediumStickLength = stickLengths[initialMediumStickIndex];

            int? initialLongStickIndex = BinarySearch.Search(
                start: initialMediumStickIndex + 1,
                end: stickCount - 1,
                verifier: i => stickLengths[i] > smallStickLength + initialMediumStickLength,
                mode: BinarySearch.Mode.FalseToTrue);
            if (!initialLongStickIndex.HasValue)
                return nonTriangleCombinations;

            int mediumStickIndex = initialMediumStickIndex;
            int mediumStickLength = initialMediumStickLength;
            int longStickIndex = initialLongStickIndex.Value;
            int longStickLength = stickLengths[longStickIndex];

            while (longStickIndex < stickCount)
            {
                nonTriangleCombinations += stickCount - longStickIndex;

                ++mediumStickIndex;
                mediumStickLength = stickLengths[mediumStickIndex];

                while (longStickLength <= smallStickLength + mediumStickLength
                    && longStickIndex < stickCount)
                {
                    ++longStickIndex;

                    if (longStickIndex < stickCount)
                    {
                        longStickLength = stickLengths[longStickIndex];
                    }
                }
            }
        }

        return nonTriangleCombinations;
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

    public static int? Search(int start, int end, Predicate<int> verifier, Mode mode)
        => mode == Mode.FalseToTrue
        ? SearchFalseToTrue(start, end, verifier)
        : SearchTrueToFalse(start, end, verifier);

    // When given an array, the verifier should be built against the values in the array, not its
    // indices. To satisfy all needs, the index of the found value (rather than the value), is returned.
    public static int? Search(IReadOnlyList<int> values, Predicate<int> verifier, Mode mode)
        => Search(0, values.Count - 1, i => verifier(values[i]), mode);

    private static int? SearchFalseToTrue(int start, int end, Predicate<int> verifier)
    {
        if (start > end) return null;

        int initialEnd = end;
        int mid;

        while (start != end)
        {
            mid = start + (end - start) / 2;

            if (verifier(mid))
            {
                end = mid;
            }
            else
            {
                start = mid + 1;
            }
        }

        // This avoids a redundant verification when a solution can be found:
        // If start(==end) isn't at initialEnd, there's a solution, since end only moves after a verify.
        // If start is at intialEnd, we still need to try verifying it.
        return start != initialEnd ? start
            : verifier(start) ? start
            : (int?)null;
    }

    private static int? SearchTrueToFalse(int start, int end, Predicate<int> verifier)
    {
        if (start > end) return null;

        int initialStart = start;
        int mid;

        while (start != end)
        {
            mid = start + (end - start + 1) / 2;

            if (verifier(mid))
            {
                start = mid;
            }
            else
            {
                end = mid - 1;
            }
        }

        // This avoids a redundant verification when a solution can be found:
        // If start isn't at initialStart, there's a solution, since start only moves after a verify.
        // If start is at initialStart, we still need to try verifying it.
        return start != initialStart ? start
            : verifier(start) ? start
            : (int?)null;
    }
}

public static class Program
{
    private static void Main()
    {
        int stickCount;
        while ((stickCount = int.Parse(Console.ReadLine())) != 0)
        {
            int[] stickLengths = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            Console.WriteLine(
                NOTATRI.Solve(stickCount, stickLengths));
        }
    }
}
