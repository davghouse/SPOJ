using System;
using System.Linq;

// https://www.spoj.com/problems/BWIDOW/ #ad-hoc
// Determines if there's a ring through which all other rings can pass.
public static class BWIDOW
{
    // Choose the ring with the largest inner radii. There might be multiple rings
    // with this radius, but that doesn't matter--the outer radius is necessarily
    // larger than the inner radius, so in that case a ring can't be found even
    // if we choose the max inner radius ring with the largest outer radius.
    public static int? Solve(int[] innerRadii, int[] outerRadii)
    {
        int maxInnerRadius = innerRadii.Max();
        int indexOfMaxInnerRadius = Array.IndexOf(innerRadii, maxInnerRadius);
        // The chosen ring doesn't need to fit through itself--exclude it from the outers.
        outerRadii[indexOfMaxInnerRadius] = int.MinValue;
        int maxRemainingOuterRadius = outerRadii.Max();

        return maxRemainingOuterRadius < maxInnerRadius
            ? indexOfMaxInnerRadius + 1 // Make the index 1-based.
            : (int?)null;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int ringCount = int.Parse(Console.ReadLine());
            int[] innerRadii = new int[ringCount];
            int[] outerRadii = new int[ringCount];
            for (int r = 0; r < ringCount; ++r)
            {
                string[] line = Console.ReadLine().Split();
                innerRadii[r] = int.Parse(line[0]);
                outerRadii[r] = int.Parse(line[1]);
            }

            Console.WriteLine(
                BWIDOW.Solve(innerRadii, outerRadii) ?? -1);
        }
    }
}
