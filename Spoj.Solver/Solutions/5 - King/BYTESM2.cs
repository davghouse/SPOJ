using System;
using System.Collections.Generic;

// http://www.spoj.com/problems/BYTESM2/ #dynamic-programming-2d
// Finds the (downward) path of most stones in a grid of stones.
public static class BYTESM2
{
    // The value of the best path to a square is the square's value plus the max
    // of the values of the best paths of the squares in the previous row it's reachable from.
    public static int Solve(int height, int width, int[,] stones)
    {
        // We could use the stones array to hold these values, but let's not.
        int[,] bestPathValues = new int[height, width];

        // Initialize the first row of best path values to the corresponding square values.
        for (int c = 0; c < width; ++c)
        {
            bestPathValues[0, c] = stones[0, c];
        }

        for (int r = 1; r < height; ++r)
        {
            for (int c = 0; c < width; ++c)
            {
                int leftPathValue = c > 0 ? bestPathValues[r - 1, c - 1] : 0;
                int middlePathValue = bestPathValues[r - 1, c];
                int rightPathValue = c < width - 1 ? bestPathValues[r - 1, c + 1] : 0;

                bestPathValues[r, c] = stones[r, c]
                    + Math.Max(Math.Max(leftPathValue, middlePathValue), rightPathValue);
            }
        }

        // Find the overall best path value by taking the max of the last row's values.
        int bestPathValue = 0;
        for (int c = 0; c < width; ++c)
        {
            bestPathValue = Math.Max(bestPathValue, bestPathValues[height - 1, c]);
        }

        return bestPathValue;
    }
}

public static class Program
{
    private static void Main()
    {
        // The input is malformed which forces us to do some weird handling.
        var input = new List<int>();
        string line;
        while ((line = Console.ReadLine()) != null)
        {
            int[] ints = Array.ConvertAll(
                line.Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
                int.Parse);
            input.AddRange(ints);
        }

        int inputIndex = 0;
        int remainingTestCases = input[inputIndex++];
        while (remainingTestCases-- > 0)
        {
            int height = input[inputIndex++]; // row count
            int width = input[inputIndex++]; // column count

            int[,] stones = new int[height, width];
            for (int r = 0; r < height; ++r)
            {
                for (int c = 0; c < width; ++c)
                {
                    stones[r, c] = input[inputIndex++];
                }
            }

            Console.WriteLine(
                BYTESM2.Solve(height, width, stones));
        }
    }
}
