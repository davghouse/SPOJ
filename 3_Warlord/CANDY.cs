using System;
using System.Linq;

// Candy I
// 2123 http://www.spoj.com/problems/CANDY/
// Given packets containing different numbers of candies,
// count the fewest moves needed to split them up fairly, if possible.
public static class CANDY
{
    public static int Solve(int[] packetCandyCounts)
    {
        int numberOfPackets = packetCandyCounts.Length;
        int totalCandies = packetCandyCounts.Sum();

        if (totalCandies % numberOfPackets != 0)
            return -1; // Splitting the candies up fairly isn't possible.

        int fairCandyCount = totalCandies / numberOfPackets;

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
        int numberOfPackets;
        while ((numberOfPackets = int.Parse(Console.ReadLine())) != -1)
        {
            int[] packetCandyCounts = new int[numberOfPackets];
            for (int i = 0; i < numberOfPackets; ++i)
            {
                packetCandyCounts[i] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine(
                CANDY.Solve(packetCandyCounts));
        }
    }
}