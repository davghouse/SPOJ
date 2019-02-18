using System;

// https://www.spoj.com/problems/STAMPS/ #extrema #sorting
// Figures out if Lucy can borrow enough stamps from a certain number of her friends.
public static class STAMPS
{
    public static string Solve(int neededStamps, int[] friendsStampCounts)
    {
        Array.Sort(friendsStampCounts);

        int borrowedStamps = 0;
        // Borrow stamps from the remaining friend with the most stamps until we get enough.
        for (int i = friendsStampCounts.Length - 1; i >= 0; --i)
        {
            borrowedStamps += friendsStampCounts[i];

            if (borrowedStamps >= neededStamps)
                return (friendsStampCounts.Length - i).ToString();
        }

        return "impossible";
    }
}

public static class Program
{
    private static void Main()
    {
        int testCount = int.Parse(Console.ReadLine());
        for (int t = 0; t < testCount; ++t)
        {
            int[] line1 = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int[] line2 = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            Console.WriteLine($"Scenario #{t + 1}:");
            Console.WriteLine(
                STAMPS.Solve(line1[0], line2));
            Console.WriteLine();
        }
    }
}
