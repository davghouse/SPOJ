using System;
using System.Collections.Generic;

// https://www.spoj.com/problems/ALLIZWEL/ #dfs #recursion
// Determines if ALLIZZWELL can be found in a grid of letters.
public sealed class ALLIZWEL
{
    private readonly int _rowCount;
    private readonly int _columnCount;
    private readonly char[,] _letterGrid;

    public ALLIZWEL(int rowCount, int columnCount, char[,] letterGrid)
    {
        _rowCount = rowCount;
        _columnCount = columnCount;
        _letterGrid = letterGrid;
    }

    public bool Solve(string word)
    {
        bool[,] usedCells = new bool[_rowCount, _columnCount];

        for (int r = 0; r < _rowCount; ++r)
        {
            for (int c = 0; c < _columnCount; ++c)
            {
                if (_letterGrid[r, c] != word[0])
                    continue;

                if (SearchFrom(new Cell(r, c), usedCells, word, wordIndex: 0))
                    return true;
            }
        }

        return false;
    }

    // Cell satisfies the letter @ wordIndex. The most important part here is how the
    // used cells are tracked. Mark the cell as used, then search from all of its
    // neighbors that match the next letter. If none of the searches from this cell
    // return true, unmark it so it becomes available for the searches following it.
    private bool SearchFrom(Cell cell, bool[,] usedCells, string word, int wordIndex)
    {
        if (wordIndex == word.Length - 1)
            return true;

        usedCells[cell.Row, cell.Column] = true;

        foreach (var neighbor in GetExtendingNeighbors(cell, usedCells, word[wordIndex + 1]))
        {
            if (SearchFrom(neighbor, usedCells, word, wordIndex + 1))
                return true;
        }

        usedCells[cell.Row, cell.Column] = false;

        return false;
    }

    private IEnumerable<Cell> GetExtendingNeighbors(Cell cell, bool[,] usedCells, char nextLetter)
    {
        for (int r = cell.Row - 1; r <= cell.Row + 1; ++r)
        {
            for (int c = cell.Column - 1; c <= cell.Column + 1; ++c)
            {
                if (r >= 0 && r < _rowCount
                    && c >= 0 && c < _columnCount
                    && !usedCells[r, c]
                    && _letterGrid[r, c] == nextLetter)
                    yield return new Cell(r, c);
            }
        }
    }

    private struct Cell
    {
        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; set; }
        public int Column { get; set; }
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();
            int rowCount = int.Parse(line[0]);
            int columnCount = int.Parse(line[1]);

            char[,] letterGrid = new char[rowCount, columnCount];
            for (int r = 0; r < rowCount; ++r)
            {
                string row = Console.ReadLine();
                for (int c = 0; c < columnCount; ++c)
                {
                    letterGrid[r, c] = row[c];
                }
            }
            Console.ReadLine();

            var solver = new ALLIZWEL(rowCount, columnCount, letterGrid);

            Console.WriteLine(
                solver.Solve("ALLIZZWELL") ? "YES" : "NO");
        }
    }
}
