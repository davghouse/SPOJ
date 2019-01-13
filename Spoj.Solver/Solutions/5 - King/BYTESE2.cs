using System;

// https://www.spoj.com/problems/BYTESE2/ #ad-hoc #sorting
// Finds out the maximum number of students in the Great Hall at once.
public static class BYTESE2
{
    // Students enter and students exit. The specific entrance and exit time pairs don't
    // matter: 2 4 and 3 5 might as well be 2 5 and 3 4 for our counting purposes.
    // Someone enters at 2, another at 3, someone exits at 4, and another exits at 5.
    // We just need to sort the entrance and exit times and traverse them in order,
    // incrementing the count when someone enters and decrementing it when someone exits.
    public static int Solve(int[] entranceTimes, int[] exitTimes)
    {
        Array.Sort(entranceTimes);
        Array.Sort(exitTimes);

        int maxStudentCount = 0;
        int currentStudentCount = 0;
        int entranceIndex = 0;
        int exitIndex = 0;
        // The entrances certainly finish up before the exits, and once the entrances
        // are done the current count will only get smaller, so ignore later exits.
        while (entranceIndex < entranceTimes.Length)
        {
            int nextEntranceTime = entranceTimes[entranceIndex];
            int nextExitTime = exitTimes[exitIndex];
            if (nextEntranceTime < nextExitTime)
            {
                ++currentStudentCount;
                if (currentStudentCount > maxStudentCount)
                {
                    maxStudentCount = currentStudentCount;
                }
                ++entranceIndex;
            }
            else
            {
                --currentStudentCount;
                ++exitIndex;
            }
        }

        return maxStudentCount;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int entryExitCount = int.Parse(Console.ReadLine());
            int[] entranceTimes = new int[entryExitCount];
            int[] exitTimes = new int[entryExitCount];

            for (int i = 0; i < entryExitCount; ++i)
            {
                string[] line = Console.ReadLine().Split();
                entranceTimes[i] = int.Parse(line[0]);
                exitTimes[i] = int.Parse(line[1]);
            }

            Console.WriteLine(
                BYTESE2.Solve(entranceTimes, exitTimes));
        }
    }
}
