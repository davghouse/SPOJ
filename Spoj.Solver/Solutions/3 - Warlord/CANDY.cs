using System;
using System.Linq;

// 2123 http://www.spoj.com/problems/CANDY/ Candy I
// Given packets containing different numbers of candies,
// count the fewest moves needed to split them up fairly, if possible.
public static class CANDY
{
    public static int Solve(int[] packetCandyCounts)
    {
        int packetCount = packetCandyCounts.Length;
        int totalCandies = packetCandyCounts.Sum();

        if (totalCandies % packetCount != 0)
            return -1; // Splitting the candies up fairly isn't possible.

        int fairCandyCount = totalCandies / packetCount;

        // It's necessary to remove the extra candies from each packet exceeding the fair candy count.
        // One move is required for each of these removals, so it's easy to find the total required moves.
        return packetCandyCounts
            .Where(c => c > fairCandyCount)
            .Select(c => c - fairCandyCount)
            .Sum();
    }
}

public static class Program
{
    private static void Main()
    {
        int packetCount;
        while ((packetCount = int.Parse(Console.ReadLine())) != -1)
        {
            int[] packetCandyCounts = new int[packetCount];
            for (int i = 0; i < packetCount; ++i)
            {
                packetCandyCounts[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine(
                CANDY.Solve(packetCandyCounts));
        }
    }
}
