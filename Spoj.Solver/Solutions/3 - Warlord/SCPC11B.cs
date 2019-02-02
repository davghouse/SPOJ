using System;
using System.Linq;

// https://www.spoj.com/problems/SCPC11B/ #ad-hoc #sorting
// Determines if charging stations are frequent enough to make a trip.
public static class SCPC11B
{
    // The distance between consecutive stations can't be more than 200. And from
    // the last station we have to travel to mile 1422, turn around, and still have
    // enough charge to make it back to the last station.
    public static bool Solve(int[] stationDistances)
    {
        Array.Sort(stationDistances);

        for (int s = 1; s < stationDistances.Length; ++s)
        {
            if (stationDistances[s] - stationDistances[s - 1] > 200)
                return false;
        }

        int distanceFromLastStationToTheEndAndBack = 2 * (1422 - stationDistances.Last());
        return distanceFromLastStationToTheEndAndBack <= 200;
    }
}

public static class Program
{
    private static void Main()
    {
        int stationCount;
        while ((stationCount = int.Parse(Console.ReadLine())) != 0)
        {
            int[] stationDistances = new int[stationCount];
            for (int s = 0; s < stationCount; ++s)
            {
                stationDistances[s] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine(
                SCPC11B.Solve(stationDistances) ? "POSSIBLE" : "IMPOSSIBLE");
        }
    }
}
