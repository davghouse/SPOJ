using System;

// https://www.spoj.com/problems/TRT/ #dynamic-programming-2d #optimization
// Finds the optimal order to sell cow treats that become more valuable over time.
public static class TRT // v2, bottom-up, dynamic programming with tabulation
{
    // See TRT_v1 for the memoized solution that helps motivate this DP.
    // A 2D array is used for the DP, where the first index corresponds to a range's
    // start index and the second index corresponds to that range's end index. The
    // value in the array isn't just the maximum revenue of range, it's the maximum
    // revenue given that the requisite number of days has passed to allow us to
    // choose all the treats outside the range. Details:
    // For the range from r (row) to c (column), r <= c (below the diagonal is N/A,
    // as not valid ranges), the maximum revenue for that range (given the starting
    // age is r + ((treatCount.Length - 1) - c) + 1) is
    // = maximumRevenues[r, c]
    // = Max(
    //    age * treatValues[r] + maximumRevenues[r + 1, c],
    //    age * treatValues[c] + maximumRevenues[r, c - 1]).
    // First corresponds to choosing the first treat in the range, the second to
    // choosing the last treat, our only options. So we need to fill the maximumRevenues
    // table such that the value underneath and the value to the left are filled in.
    public static int Solve(int treatCount, int[] treatValues)
    {
        int[,] maximumRevenues = new int[treatCount, treatCount];

        // Initialize along the diagonal.
        for (int d = 0; d < treatCount; ++d)
        {
            // Starting age of a single treat is always treatCount; call method to be explicit.
            maximumRevenues[d, d] = StartingAgeOfRange(treatCount, d, d) * treatValues[d];
        }

        // While moving right across the columns, solve up from the diagonal.
        for (int c = 1; c < treatCount; ++c)
        {
            for (int r = c - 1; r >= 0; --r)
            {
                int startingAgeOfRange = StartingAgeOfRange(treatCount, r, c);

                maximumRevenues[r, c] = Math.Max(
                    startingAgeOfRange * treatValues[r] + maximumRevenues[r + 1, c],
                    startingAgeOfRange * treatValues[c] + maximumRevenues[r, c - 1]);
            }
        }

        // Revenue for the full range is in the top right corner of the table.
        return maximumRevenues[0, treatCount - 1];
    }

    // Note the full range has an age of 1 (the multiplier for the first treat sold is 1).
    // Counting treats outside of the range, as that corresponds to days until the range
    // is arrived at, i.e.: ----[ ... ]--- => 7 treats outside of range, 4 before, 3 after.
    private static int StartingAgeOfRange(int treatCount, int startIndex, int endIndex)
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
