using System;
using System.Collections.Generic;

// https://www.spoj.com/problems/AGGRCOW/ #binary-search #greedy #optimization #research
// Places some cows in stalls in a way that maximizes: the shortest distance between any two cows.
public static class AGGRCOW
{
    // Given a potential shortest distance between stalls, it's easy to verify if the cows can be placed
    // with at least that much distance between any two of them. To do this:
    // Put a cow in the first stall, put the next cow in the 𝐟𝐢𝐫𝐬𝐭 stall to the right at least the given
    // distance away from previous stall, and repeat, until no more cows or no more room to place them.
    // If we can place the cows for a given distance, then we can place them for any shorter distance,
    // since we'd have even more room/stalls to work with. If we can't place the cows for some distance,
    // we can't place them for any longer distance, since we'd have even less room/stalls to work with.
    // This fits the criteria for predicate-based binary searching on all potential shortest distances.
    public static int Solve(int cowCount, int[] stallLocations)
    {
        // The stall locations don't do us much good unless they're sorted.
        Array.Sort(stallLocations);

        int shortestDistanceBetweenAdjacentStalls = int.MaxValue;
        for (int i = 0; i < stallLocations.Length - 1; ++i)
        {
            int distanceBetweenAdjacentStalls = stallLocations[i + 1] - stallLocations[i];
            shortestDistanceBetweenAdjacentStalls = Math.Min(
                shortestDistanceBetweenAdjacentStalls, distanceBetweenAdjacentStalls);
        }

        // We know there are enough stalls for all the cows, so there's always a solution. If we have to
        // place cows in the two stalls closest to each other, that would be the worst solution.
        int worstShortestDistance = shortestDistanceBetweenAdjacentStalls;
        // It would be best if the stalls were such that all the cows could be placed an equal distance
        // from each other, with a cow at the first stall and a cow at the last--so we use the full stall distance.
        int fullStallDistance = stallLocations[stallLocations.Length - 1] - stallLocations[0];
        int bestPotentialShortestDistance = fullStallDistance / (cowCount - 1);

        // The preamble isn't necessary, it just helps us understand the problem. We could search from 1 to int.MaxValue.
        return BinarySearch.Search(
            worstShortestDistance,
            bestPotentialShortestDistance,
            d => VerifyShortestDistanceIsAttainable(d, cowCount, stallLocations),
            BinarySearch.Mode.TrueToFalse).Value;
    }

    private static bool VerifyShortestDistanceIsAttainable(int potentialShortestDistance, int cowCount, int[] stallLocations)
    {
        int cowsPlacedInStallsCount = 1;
        int previousStall = 0;

        for (int currentStall = 1;
            cowsPlacedInStallsCount < cowCount && currentStall < stallLocations.Length;
            ++currentStall)
        {
            if (stallLocations[currentStall] - stallLocations[previousStall] >= potentialShortestDistance)
            {
                ++cowsPlacedInStallsCount;
                previousStall = currentStall;
            }
        }

        return cowsPlacedInStallsCount == cowCount;
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
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int stallCount = line[0];
            int cowCount = line[1];

            int[] stallLocations = new int[stallCount];
            for (int i = 0; i < stallCount; ++i)
            {
                stallLocations[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine(
                AGGRCOW.Solve(cowCount, stallLocations));
        }
    }
}
