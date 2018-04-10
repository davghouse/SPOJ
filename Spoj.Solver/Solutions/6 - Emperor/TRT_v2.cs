using System;

// http://www.spoj.com/problems/TRT/ #dynamic-programming-2d #experiment #optimization
// Finds the optimal order to sell cow treats that become more valuable over time.
public static class TRT // v2, bottom-up, dynamic programming with tabulation
{
    // See TRT_v1 for the memoized solution that helps motivate this DP.
    // A 2D array is used for the DP, where the first index corresponds to a range's start index
    // and the second index corresponds to that range's end index. The value in the array isn't just
    // the maximum revenue of range, it's the maximum revenue given that the requisite number of days has
    // passed to allow us to choose all the treats outside the range. Details:
    // For the range from i (row) to j (column), i <= j (below the diagonal is N/A, as not valid ranges),
    // the maximum revenue for that range (given the starting age is i + ((treatCount.Length - 1) - j) + 1) is
    // = maximumRevenues[i, j]
    // = Max(age * treatValues[i] + maximumRevenues[i + 1, j], age * treatValues[j] + maximumRevenues[i, j - 1]).
    // First corresponds to choosing the first treat in the range, the second to choosing the last treat, our only options.
    // So we need to fill the maximumRevenues table such that the value underneath and the value to the left are already calculated.
    public static int Solve(int treatCount, int[] treatValues)
    {
        int[,] maximumRevenues = new int[treatCount, treatCount];

        // Initialize along the diagonal.
        for (int d = 0; d < treatCount; ++d)
        {
            // Starting age of a single treat is always treatCount, but call the method here to be explicit.
            maximumRevenues[d, d] = StartingAgeOfRange(d, d, treatCount) * treatValues[d];
        }

        // While moving right across the columns, solve up from the diagonal.
        for (int cj = 1; cj < treatCount; ++cj)
        {
            for (int ri = cj - 1; ri >= 0; --ri)
            {
                int startingAgeOfRange = StartingAgeOfRange(ri, cj, treatCount);

                maximumRevenues[ri, cj] = Math.Max(
                    startingAgeOfRange * treatValues[ri] + maximumRevenues[ri + 1, cj],
                    startingAgeOfRange * treatValues[cj] + maximumRevenues[ri, cj - 1]);
            }
        }

        // Return the revenue for the full range, which is in the top right corner of the table.
        return maximumRevenues[0, treatCount - 1];
    }

    // Note the full range has an age of 1 (the multiplier for the first treat sold is 1).
    // Counting treats outside of the range, as that corresponds to days until the range is arrived at: ----[ ... ]--- => 7.
    private static int StartingAgeOfRange(int startIndex, int endIndex, int treatCount)
        // startIndex + ((treatCount - 1) - endIndex) + 1 =>
        => startIndex + treatCount - endIndex;
}

public static class Program
{
    private static void Main()
    {
        int treatCount = int.Parse(Console.ReadLine());
        int[] treatValues = new int[treatCount];
        for (int i = 0; i < treatCount; ++i)
        {
            treatValues[i] = int.Parse(Console.ReadLine());
        }

        Console.WriteLine(
            TRT.Solve(treatCount, treatValues));
    }
}
