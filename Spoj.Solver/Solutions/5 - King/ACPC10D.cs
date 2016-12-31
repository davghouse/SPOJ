using System;
using System.Text;

// http://www.spoj.com/problems/ACPC10D/ #dynamic-programming-2d
// Finds the shortest path between top and bottom middle vertices in a 'tri graph.'
public static class ACPC10D
{
    // Really similar to BYTESM2. Need to be a little careful to consider all moves,
    // not just downward and diagonal, since vertices can have negative values. And we start
    // at the top middle, so can't let impossible moves from the top left impact results.
    // For example, recursively the answer for the bottom middle vertex is its vertex
    // cost plus the minimum cost of getting to any one of the four vertices from which
    // we can travel to it. For convenience, just going to consume the input array.
    public static int Solve(int rowCount, int[,] vertexCosts)
    {
        vertexCosts[0, 0] = int.MaxValue; // Can't use this vertex as we start from middle top.
        // vertexCosts[0, 1] = vertexCosts[0, 1]; Cost of arriving at start already taken into account.
        vertexCosts[0, 2] += vertexCosts[0, 1]; // Only one way to reach top right.

        for (int r = 1; r < rowCount; ++r)
        {
            int upLeftCost = vertexCosts[r - 1, 0];
            int upMiddleCost = vertexCosts[r - 1, 1]; 
            int upRightCost = vertexCosts[r - 1, 2];
            int bestUpLeftMiddleCost = Math.Min(upLeftCost, upMiddleCost);
            int bestUpLeftMiddleRightCost = Math.Min(bestUpLeftMiddleCost, upRightCost);
            int bestUpMiddleRightCost = Math.Min(upMiddleCost, upRightCost);

            vertexCosts[r, 0] += bestUpLeftMiddleCost;
            vertexCosts[r, 1] += Math.Min(bestUpLeftMiddleRightCost, vertexCosts[r, 0]);
            vertexCosts[r, 2] += Math.Min(bestUpMiddleRightCost, vertexCosts[r, 1]);
        }

        return vertexCosts[rowCount - 1, 1];
    }
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int[,] vertexCosts = new int[100000, 3];
        int testCase = 1;
        int rowCount;
        while ((rowCount = int.Parse(Console.ReadLine())) != 0)
        {
            for (int r = 0; r < rowCount; ++r)
            {
                string[] vertices = Console.ReadLine().Split();
                for (int c = 0; c < 3; ++c)
                {
                    vertexCosts[r, c] = int.Parse(vertices[c]);
                }
            }

            output.AppendLine(
                $"{testCase++}. {ACPC10D.Solve(rowCount, vertexCosts)}");
        }

        Console.Write(output);
    }
}
