using System;
using System.Collections.Generic;
using System.Text;

// https://www.spoj.com/problems/LABYR1/ #bfs #graph-theory #longest-path #proof #tree
// Finds the longest path between 'free' blocks in a labyrinth (a tree).
public static class LABYR1
{
    private static readonly Tuple<int, int>[] _neighborBlockTransformations = new[]
    {
        Tuple.Create(-1, 0), Tuple.Create(1, 0),
        Tuple.Create(0, -1), Tuple.Create(0, 1),
    };

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
                int column = vertex.Item2;

                foreach (var neighborBlockTransformation in _neighborBlockTransformations)
                {
                    var neighborBlockRow = row + neighborBlockTransformation.Item1;
                    var neighborBlockColumn = column + neighborBlockTransformation.Item2;

                    if (neighborBlockRow >= 0 && neighborBlockRow < labyrinth.Length
                        && neighborBlockColumn >= 0 && neighborBlockColumn < labyrinth[0].Length
                        && !discoveredVertices[neighborBlockRow, neighborBlockColumn]
                        && labyrinth[neighborBlockRow][neighborBlockColumn] == '.')
                    {
                        discoveredVertices[neighborBlockRow, neighborBlockColumn] = true;
                        verticesToVisit.Enqueue(Tuple.Create(neighborBlockRow, neighborBlockColumn));
                    }
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
