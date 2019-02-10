using System;
using System.Collections.Generic;

// https://www.spoj.com/problems/ELEVTRBL/ #bfs
// Finds the minimum number of button pushes to ride a weird elevator.
public static class ELEVTRBL
{
    public static int? Solve(
        int floorCount,
        int startFloor, int goalFloor,
        int upInterval, int downInterval)
    {
        if (startFloor == goalFloor)
            return 0;

        bool[] discoveredFloors = new bool[floorCount + 1];
        var floorsToVisit = new Queue<int>();
        discoveredFloors[startFloor] = true;
        floorsToVisit.Enqueue(startFloor);

        int distance = 1;
        while (floorsToVisit.Count > 0)
        {
            int waveSize = floorsToVisit.Count;
            for (int i = 0; i < waveSize; ++i)
            {
                int floor = floorsToVisit.Dequeue();

                int upFloor = floor + upInterval;
                if (upFloor <= floorCount && !discoveredFloors[upFloor])
                {
                    if (upFloor == goalFloor)
                        return distance;

                    discoveredFloors[upFloor] = true;
                    floorsToVisit.Enqueue(upFloor);
                }

                int downFloor = floor - downInterval;
                if (downFloor >= 1 && !discoveredFloors[downFloor])
                {
                    if (downFloor == goalFloor)
                        return distance;

                    discoveredFloors[downFloor] = true;
                    floorsToVisit.Enqueue(downFloor);
                }
            }
            ++distance;
        }

        return null;
    }
}

public static class Program
{
    private static void Main()
    {
        int[] line = Array.ConvertAll(
            Console.ReadLine().Trim().Split(),
            int.Parse);
        int? result = ELEVTRBL.Solve(
            floorCount: line[0],
            startFloor: line[1],
            goalFloor: line[2],
            upInterval: line[3],
            downInterval: line[4]);

        Console.Write(result?.ToString() ?? "use the stairs");
    }
}
