using System;
using System.Collections.Generic;
using System.Text;

// https://www.spoj.com/problems/LABYR1/ #bfs #graph-theory #longest-path #proof #tree
// Finds the longest path between 'free' blocks in a labyrinth (a tree).
public static class LABYR1
{
    // See image for details: http://i.imgur.com/hWnw1N9.jpg.
    public static int Solve(string[] labyrinth)
    {
        var startBlock = FindAnyFreeBlock(labyrinth);
        if (startBlock == null) return 0;

        int distance;
        var furthestFromStartBlock = FindFurthestFreeBlockFrom(startBlock, labyrinth, out distance);
        var furthestFromThatBlock = FindFurthestFreeBlockFrom(furthestFromStartBlock, labyrinth, out distance);

        return distance;
    }

    private static Tuple<int, int> FindAnyFreeBlock(string[] labyrinth)
    {
        for (int r = 0; r < labyrinth.Length; ++r)
        {
            for (int c = 0; c < labyrinth[0].Length; ++c)
            {
                if (labyrinth[r][c] == '.')
                    return Tuple.Create(r, c);
            }
        }

        return null;
    }

    // We could use a graph like PT07Z, but whatever. Just determining edges mid-BFS.
    public static Tuple<int, int> FindFurthestFreeBlockFrom(
        Tuple<int, int> startBlock, string[] labyrinth, out int distance)
    {
        bool[,] discoveredVertices = new bool[labyrinth.Length, labyrinth[0].Length];
        var verticesToVisit = new Queue<Tuple<int, int>>();
        discoveredVertices[startBlock.Item1, startBlock.Item2] = true;
        verticesToVisit.Enqueue(startBlock);

        Tuple<int, int> furthestVertex = null;
        int furthestDistance = -1;

        // We visit vertices in waves, where all vertices in the same wave are the same distance
        // from the start vertex, which BFS makes convenient. This allows us to avoid storing
        // distances to the start vertex at the level of individual vertices.
        while (verticesToVisit.Count > 0)
        {
            // We don't care which furthest vertex we get from this wave, so we just choose the first.
            furthestVertex = verticesToVisit.Peek();
            ++furthestDistance;

            int waveSize = verticesToVisit.Count;
            for (int i = 0; i < waveSize; ++i)
            {
                var vertex = verticesToVisit.Dequeue();
                int row = vertex.Item1;
                int col = vertex.Item2;

                if (row - 1 >= 0 && labyrinth[row - 1][col] == '.'
                    && !discoveredVertices[row - 1, col])
                {
                    discoveredVertices[row - 1, col] = true;
                    verticesToVisit.Enqueue(Tuple.Create(row - 1, col));
                }

                if (col + 1 < labyrinth[0].Length && labyrinth[row][col + 1] == '.'
                    && !discoveredVertices[row, col + 1])
                {
                    discoveredVertices[row, col + 1] = true;
                    verticesToVisit.Enqueue(Tuple.Create(row, col + 1));
                }

                if (row + 1 < labyrinth.Length && labyrinth[row + 1][col] == '.'
                    && !discoveredVertices[row + 1, col])
                {
                    discoveredVertices[row + 1, col] = true;
                    verticesToVisit.Enqueue(Tuple.Create(row + 1, col));
                }

                if (col - 1 >= 0 && labyrinth[row][col - 1] == '.'
                    && !discoveredVertices[row, col - 1])
                {
                    discoveredVertices[row, col - 1] = true;
                    verticesToVisit.Enqueue(Tuple.Create(row, col - 1));
                }
            }
        }

        distance = furthestDistance;
        return furthestVertex;
    }
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();
            int columnCount = int.Parse(line[0]);
            int rowCount = int.Parse(line[1]);

            string[] labyrinth = new string[rowCount];
            for (int r = 0; r < rowCount; ++r)
            {
                labyrinth[r] = Console.ReadLine();
            }

            output.Append(
                $"Maximum rope length is {LABYR1.Solve(labyrinth)}.");
            output.AppendLine();
        }

        Console.Write(output);
    }
}
