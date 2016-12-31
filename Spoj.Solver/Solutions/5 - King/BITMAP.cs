using System;
using System.Collections.Generic;
using System.Text;

// http://www.spoj.com/problems/BITMAP/ #bfs #experiment #multi-source
// For all black and white pixels in a grid, finds the distance to the closest white pixel.
public static class BITMAP
{
    // White pixels are within zero of a white pixel. All black pixels adjacent to a white pixel
    // are within 1. All black pixels adjacent to those black pixels are within 2 (unless we already
    // know they're within 1). Easy to visualize as flood filling, numbers spreading out in waves first
    // from the white pixels, then from the previous set of black pixels visited. In terms of
    // implementation, it'll be like a BFS starting from the set of all the white pixels (multisource).
    public static int?[,] Solve(int rowCount, int columnCount, string[] zeroOneRows)
    {
        int?[,] nearestWhitePixelDistances = new int?[rowCount, columnCount];
        var pixelsToFloodFrom = new Queue<Tuple<int, int>>();

        // First pass to initialize distance for white pixels (represented by 1) to zero, and enqueue them.
        for (int r = 0; r < rowCount; ++r)
        {
            for (int c = 0; c < columnCount; ++c)
            {
                if (zeroOneRows[r][c] == '1')
                {
                    nearestWhitePixelDistances[r, c] = 0;
                    pixelsToFloodFrom.Enqueue(Tuple.Create(r, c));
                }
            }
        }

        while (pixelsToFloodFrom.Count > 0)
        {
            int waveSize = pixelsToFloodFrom.Count;
            for (int i = 0; i < waveSize; ++i)
            {
                Tuple<int, int> floodPixel = pixelsToFloodFrom.Dequeue();
                int floodPixelRow = floodPixel.Item1;
                int floodPixelColumn = floodPixel.Item2;
                int nearestWhitePixelDistance = nearestWhitePixelDistances[floodPixelRow, floodPixelColumn].Value + 1;

                // Consider the pixel above the pixel we're flooding from.
                if (floodPixelRow - 1 >= 0 && !nearestWhitePixelDistances[floodPixelRow - 1, floodPixelColumn].HasValue)
                {
                    nearestWhitePixelDistances[floodPixelRow - 1, floodPixelColumn] = nearestWhitePixelDistance;
                    pixelsToFloodFrom.Enqueue(Tuple.Create(floodPixelRow - 1, floodPixelColumn));
                }
                // Consider the pixel below the pixel we're flooding from.
                if (floodPixelRow + 1 < rowCount && !nearestWhitePixelDistances[floodPixelRow + 1, floodPixelColumn].HasValue)
                {
                    nearestWhitePixelDistances[floodPixelRow + 1, floodPixelColumn] = nearestWhitePixelDistance;
                    pixelsToFloodFrom.Enqueue(Tuple.Create(floodPixelRow + 1, floodPixelColumn));
                }
                // Consider the pixel to the left of the pixel we're flooding from.
                if (floodPixelColumn - 1 >= 0 && !nearestWhitePixelDistances[floodPixelRow, floodPixelColumn - 1].HasValue)
                {
                    nearestWhitePixelDistances[floodPixelRow, floodPixelColumn - 1] = nearestWhitePixelDistance;
                    pixelsToFloodFrom.Enqueue(Tuple.Create(floodPixelRow, floodPixelColumn - 1));
                }
                // Consider the pixel to the right of the pixel we're flooding from.
                if (floodPixelColumn + 1 < columnCount && !nearestWhitePixelDistances[floodPixelRow, floodPixelColumn + 1].HasValue)
                {
                    nearestWhitePixelDistances[floodPixelRow, floodPixelColumn + 1] = nearestWhitePixelDistance;
                    pixelsToFloodFrom.Enqueue(Tuple.Create(floodPixelRow, floodPixelColumn + 1));
                }
            }
        }

        return nearestWhitePixelDistances;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int rowCount = line[0];
            int columnCount = line[1];

            string[] zeroOneRows = new string[rowCount];
            for (int r = 0; r < rowCount; ++r)
            {
                zeroOneRows[r] = Console.ReadLine();
            }

            var output = new StringBuilder();
            int?[,] nearestWhitePixelDistances = BITMAP.Solve(rowCount, columnCount, zeroOneRows);
            for (int r = 0; r < rowCount; ++r)
            {
                output.Append(nearestWhitePixelDistances[r, 0]);
                for (int c = 1; c < columnCount; ++c)
                {
                    output.Append($" {nearestWhitePixelDistances[r, c]}");
                }
                output.AppendLine();
            }

            Console.Write(output);
            Console.ReadLine();
        }
    }
}
