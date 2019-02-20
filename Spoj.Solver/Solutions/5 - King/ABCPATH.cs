using System;
using System.Collections.Generic;
using System.Text;

// https://www.spoj.com/problems/ABCPATH/ #dag #dfs #greedy #memoization
// Finds the longest sequential path in a grid of letters (starting at A).
public static class ABCPATH
{
    // Unlike an arbitrary DAG, no matter how we get to a letter the path length to it is the
    // same. So once we get to a letter we can throw it out, only exploring to/from it once.
    public static int Solve(int height, int width, char[,] letterGrid)
    {
        var cellsToVisit = new Stack<Tuple<int, int>>();
        for (int r = 0; r < height; ++r)
        {
            for (int c = 0; c < width; ++c)
            {
                if (letterGrid[r, c] == 'A')
                {
                    cellsToVisit.Push(Tuple.Create(r, c));
                }
            }
        }

        if (cellsToVisit.Count == 0)
            return 0;

        char maxLetterFound = 'A';
        while (cellsToVisit.Count > 0)
        {
            var cell = cellsToVisit.Pop();
            int row = cell.Item1;
            int column = cell.Item2;
            char letter = letterGrid[row, column];
            letterGrid[row, column] = '@'; // Effectively throw the letter out.

            if (letter > maxLetterFound)
            {
                maxLetterFound = letter;
            }

            char nextLetter = (char)(letter + 1);

            for (int r = row - 1; r <= row + 1; ++r)
            {
                for (int c = column - 1; c <= column + 1; ++c)
                {
                    if (r >= 0 && r < height
                        && c >= 0 && c < width
                        && letterGrid[r, c] == nextLetter)
                    {
                        cellsToVisit.Push(Tuple.Create(r, c));
                    }
                }
            }
        }

        return maxLetterFound - 'A' + 1;
    }
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int caseNumber = 1;
        string[] line;
        char[,] letterGrid = new char[50, 50];
        while ((line = Console.ReadLine().Split())[0] != "0")
        {
            int height = int.Parse(line[0]);
            int width = int.Parse(line[1]);
            for (int r = 0; r < height; ++r)
            {
                string row = Console.ReadLine();
                for (int c = 0; c < width; ++c)
                {
                    letterGrid[r, c] = row[c];
                }
            }

            output.Append(
                $"Case {caseNumber++}: {ABCPATH.Solve(height, width, letterGrid)}");
            output.AppendLine();
        }

        Console.Write(output);
    }
}
