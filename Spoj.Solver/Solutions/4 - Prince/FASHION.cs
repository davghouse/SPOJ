using System;
using System.Linq;

// http://www.spoj.com/problems/FASHION/: ad hoc, experiment, sorting, trap
// Given parallel arrays of hotness levels, calculate the sum of the values of the hotness bonds.
public static class FASHION
{
    // Problem statement leads one to believe these arrays are already in parallel;
    // each pair already chosen. That's not the case, so it's necessary to figure out the
    // pairings that produces the maximum sum of hotness bonds. Sorting and pairing the highest
    // with the highest does that. For example, consider [10, 5], [9, 6]. Think of it from the
    // perspective of the first array. 10 needs to get multiplied by something, and it's the
    // highest value in that array, so we should mutliply it by the highest value in the other
    // array, as having more of it is more useful than having more of anything else.
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
