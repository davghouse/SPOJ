using System;

// 8061 http://www.spoj.com/problems/AMR10G/ Christmas Play
// Finds a set of K students from N total having the minimum height difference between tallest and shortest.
public static class AMR10G
{
    // There are potentially many subsets of size costumeCount, but it doesn't make sense
    // to consider all of them. Ignoring non-distinct heights (because it won't matter), every
    // set of size costumeCount has a tallest student. There must be at least costumeCount - 1
    // students shorter than that student, since it's tallest. So the potential tallest students
    // are the last (studentCount - costumeCount) + 1 in an array of students sorted by height.
    // We want the minimum difference though, so given we have the tallest student, we'd never go
    // further back into the shorter students than absolutely necessary. So there'll be
    // (studentCount - costumeCount) + 1 pairs of students to consider from the sorted array of heights.
    // [ s s s s s s S S S S], 10 students, 7 costumes needed => 4 tallest students to consider.
    public static int Solve(int studentCount, int costumeCount, int[] studentHeights)
    {
        if (costumeCount == 1)
            return 0;

        // Don't bother sorting in this case; easy to find the only possibility for tallest and shortest.
        if (costumeCount == studentCount)
        {
            int heightOfShortest = studentHeights[0];
            int heightOfTallest = studentHeights[0];
            for (int s = 1; s < studentCount; ++s)
            {
                heightOfShortest = Math.Min(heightOfShortest, studentHeights[s]);
                heightOfTallest = Math.Max(heightOfTallest, studentHeights[s]);
            }

            return heightOfTallest - heightOfShortest;
        }

        Array.Sort(studentHeights);

        // Now consider the shortest/tallest pairs from the (studentCount - costumeCount) + 1 relevant subsets.
        int minimumDifference = int.MaxValue;
        for (int shortest = 0, tallest = costumeCount - 1; tallest < studentCount; ++shortest, ++tallest)
        {
            int heightOfShortest = studentHeights[shortest];
            int heightOfTallest = studentHeights[tallest];

            minimumDifference = Math.Min(minimumDifference, heightOfTallest - heightOfShortest);
        }

        return minimumDifference;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        while (remainingTestCases-- > 0)
        {
            int[] line = Array.ConvertAll(
                Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
                int.Parse);
            int studentCount = line[0];
            int costumeCount = line[1];

            int[] studentHeights = Array.ConvertAll(
                Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
                int.Parse);

            Console.WriteLine(
                AMR10G.Solve(studentCount, costumeCount, studentHeights));
        }
    }
}
