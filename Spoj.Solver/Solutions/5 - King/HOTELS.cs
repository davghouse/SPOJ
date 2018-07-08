using System;

// https://www.spoj.com/problems/HOTELS/ #greedy #optimization #subarray #window
// Finds the consecutive hotels that maximize the money spent (without going over).
public static class HOTELS
{
    // Gonna have a start hotel and an end hotel. The end hotel for a given start
    // hotel is the one immediately before the hotel that would put us over the spend limit.
    // Once we find that, we move the start hotel to the next hotel, and try pushing the
    // end hotel again. Whenever moving the start hotel is necessary, we compare the
    // spend for the window of hotels to the maximum spend for a window found so far.
    // This effectively makes two passes over the hotels (and we stop trying to move the start
    // index once the end index can't be moved, since that'd just be decreasing the money spend).
    public static long Solve(int hotelCount, int costLimit, int[] hotelCosts)
    {
        int startIndex = 0, endIndex = 0;
        long costFromStartToEnd = hotelCosts[0];
        long maximumCostFromStartToEnd = 0;

        while (true)
        {
            // While the next hotel exists and doesn't put us over the limit...
            while (endIndex + 1 < hotelCount
                && costFromStartToEnd + hotelCosts[endIndex + 1] <= costLimit)
            {
                costFromStartToEnd += hotelCosts[++endIndex];
            }

            // The next hotel doesn't exist or puts us over the limit, so mark what we've found so far...
            // Have to be careful in case there are single hotels more costly than our costLimit.
            if (costFromStartToEnd < costLimit)
            {
                maximumCostFromStartToEnd = Math.Max(maximumCostFromStartToEnd, costFromStartToEnd);
            }
            else if (costFromStartToEnd == costLimit)
                return costLimit; // Can't do better than this, so stop trying.

            // If the next hotel doesn't exist, stop. Increasing start only decreases cost from here on.
            if (endIndex + 1 == hotelCount)
                break;

            // Prepare for the next iteration by bumping the start and removing its cost.
            // But have to be careful--could've bumped start past end if window was a single hotel!
            costFromStartToEnd -= hotelCosts[startIndex++];
            if (startIndex > endIndex)
            {
                endIndex = startIndex;
                costFromStartToEnd = hotelCosts[startIndex];
            }
        }

        return maximumCostFromStartToEnd;
    }
}

public static class Program
{
    private static void Main()
    {
        int[] line = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
        int hotelCount = line[0];
        int costLimit = line[1];

        int[] hotelCosts = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);

        Console.WriteLine(
            HOTELS.Solve(hotelCount, costLimit, hotelCosts));
    }
}
