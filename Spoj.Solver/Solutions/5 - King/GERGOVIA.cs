using System;

// https://www.spoj.com/problems/GERGOVIA/ #greedy
// Determines the work needed to distribute wine between houses.
public static class GERGOVIA
{
    // Doesn't matter the order in which you distribute the wine to houses. Could do furthest house
    // first or closest house first, because you pay for either choice equally when moving to the right.
    // When you move to the right, you know you're gonna have to travel through the house you're leaving
    // from for each existing demand to the left. So each existing demand contributes an extra work unit
    // as we move through that house in order to satisfy it in some undetermined selling/buying process.
    public static long Solve(int houseCount, int[] demands)
    {
        long minimumWork = 0;
        int demandBalance = 0;

        for (int h = 0; h < houseCount; ++h)
        {
            minimumWork += Math.Abs(demandBalance);
            demandBalance += demands[h];
        }

        return minimumWork;
    }
}

public static class Program
{
    private static void Main()
    {
        int houseCount;
        while ((houseCount = int.Parse(Console.ReadLine())) != 0)
        {
            int[] demands = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            Console.WriteLine(
                GERGOVIA.Solve(houseCount, demands));
        }
    }
}
