using System;

// http://www.spoj.com/problems/GLJIVE/ #ad-hoc #binary #sequence #sets #trap
// Finds the contiguous subsequence starting from 1 that most closely adds to 100.
public static class GLJIVE
{
    // Actually, the work below is wrong. We're looking for a contiguous subsequence starting
    // from the first index, but not necessarily spanning all 10.
    public static int SolveCorrectly(int[] points)
    {
        int result = 0;

        for (int i = 0; i < 10; ++i)
        {
            if (result + points[i] <= 100)
            {
                result += points[i];
            }
            else // points[i] pushes us over 100...
            {
                // So if the distance now is worse than (or equal to) the distance after adding it, add it.
                if (100 - result >= result + points[i] - 100)
                {
                    result += points[i];
                }

                // And then it's time to stop, since we'll only get further away by adding more.
                break;
            }
        }

        return result;
    }

    // This is like subset sum but not sure I want to use DP to solve it, since
    // the numbers we're working with are so well-defined. Instead, I'll try to
    // brute force over the 2^10 - 1 subsets, - 1 since at least one value is needed.
    public static int SolveIncorrectly(int[] points)
    {
        var powersOfTwoTo512 = new[]
        {
            1, 1 << 1, 1 << 2, 1 << 3, 1 << 4, 1 << 5, 1 << 6, 1 << 7, 1 << 8, 1 << 9
        };

        int bestResult = 0;
        int bestDistance = 100;

        // Each i corresponds to a different subset, based upon its binary representation
        // For example, 17 = 0000010001, 0th and 4th points included, 1023 = 1111111111,
        // all points included.
        for (int i = 1; i <= 1023 && bestDistance != 0; ++i)
        {
            int result = 0;

            for (int j = 0; j <= 9; ++j)
            {
                if ((i & powersOfTwoTo512[j]) != 0) // The jth bit is turned on.
                {
                    result += points[j];
                }
            }

            int distance = 100 >= result ? 100 - result : result - 100;

            // If it's closer, or it's the same distance but on the greater-than side of 100...
            if (distance < bestDistance || distance == bestDistance && result > bestResult)
            {
                bestResult = result;
                bestDistance = distance;
            }
        }

        return bestResult;
    }
}

public static class Program
{
    private static void Main()
    {
        var points = new int[10];
        for (int i = 0; i < 10; ++i)
        {
            points[i] = int.Parse(Console.ReadLine());
        }

        Console.WriteLine(
            GLJIVE.SolveCorrectly(points));
    }
}
