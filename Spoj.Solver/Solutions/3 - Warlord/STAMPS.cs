using System;

// http://www.spoj.com/problems/STAMPS/ #extrema #sorting
// Figures out if Lucy can borrow enough stamps from a certain number of her friends.
public static class STAMPS
{
    public static string Solve(int neededStamps, int[] friendsStampCounts)
    {
        // This sorts in ascending order and there's not a great way to get descending, so we'll traverse backwards.
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
        int totalTestCases = int.Parse(Console.ReadLine());
        for (int i = 0; i < totalTestCases; ++i)
        {
            int[] line1 = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int[] line2 = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            Console.WriteLine($"Scenario #{i + 1}:");
            Console.WriteLine(
                STAMPS.Solve(line1[0], line2));
            Console.WriteLine();
        }
    }
}
