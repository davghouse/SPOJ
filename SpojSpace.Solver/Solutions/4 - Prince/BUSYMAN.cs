using System;

// http://www.spoj.com/problems/BUSYMAN/ #ad-hoc #greedy #sorting
// Finds the maximum number of activities that can fit into a schedule.
public static class BUSYMAN
{
    // We start off needing to choose the activity that'll occupy us from t=0
    // to t=activity's end time. Best to greedily choose the activity ending the
    // soonest. That activity is at least as good as any other since it keeps
    // as much of the remaining schedule open as possible. Whatever a different
    // activity allows us to fit, the activity ending the soonest will too.
    // Then just employ the same strategy on the remaining unscheduled block.
    public static int Solve(int activityCount, int[] startTimes, int[] endTimes)
    {
        Array.Sort(endTimes, startTimes);

        int maxActivityCount = 0;
        int previousActivityEndTime = 0;
        for (int a = 0; a < activityCount; ++a)
        {
            if (startTimes[a] >= previousActivityEndTime)
            {
                ++maxActivityCount;
                previousActivityEndTime = endTimes[a];
            }
        }

        return maxActivityCount;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int activityCount = int.Parse(Console.ReadLine());
            var startTimes = new int[activityCount];
            var endTimes = new int[activityCount];
            for (int a = 0; a < activityCount; ++a)
            {
                int[] startAndEndTime = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                startTimes[a] = startAndEndTime[0];
                endTimes[a] = startAndEndTime[1];
            }

            Console.WriteLine(
                BUSYMAN.Solve(activityCount, startTimes, endTimes));
        }
    }
}
