using System;
using System.Linq;

// 1025 http://www.spoj.com/problems/FASHION/ Fashion Shows
// Given parallel arrays of hotness levels, calculate the sum of the values of the hotness bonds.
public static class FASHION
{
    // Problem statement leads one to believe these arrays are already in parallel;
    // each pair already chosen. That's not the case, so it's necessary to figure out the
    // pairings that lead the maximum hotness bonds. It's easy to see sorting the arrays does this..
    // The hottest gets the hottest; not even necessary to think about maximizing the sum here.
    public static int Solve(int[] maleHotnessLevels, int[] femaleHotnessLevels)
    {
        Array.Sort(maleHotnessLevels);
        Array.Sort(femaleHotnessLevels);

        return maleHotnessLevels
            .Zip(femaleHotnessLevels, (m, f) => m * f)
            .Sum();
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        while (remainingTestCases-- > 0)
        {
            int perShowParticipantCount = int.Parse(Console.ReadLine()); // Don't need to use this.
            int[] maleHotnessLevels = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int[] femaleHotnessLevels = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            Console.WriteLine(
                FASHION.Solve(maleHotnessLevels, femaleHotnessLevels));
        }
    }
}