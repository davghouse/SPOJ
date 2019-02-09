using System;
using System.Collections.Generic;

// https://www.spoj.com/problems/CATM/ #bfs-multi-source #simulation
// Figures out if a mouse can escape from two cats.
public class CATM
{
    private readonly int _rowCount;
    private readonly int _columnCount;

    public CATM(int rowCount, int columnCount)
    {
        _rowCount = rowCount;
        _columnCount = columnCount;
    }

    // The cats are establishing a threshold of squares that the mouse can't reach before
    // they do. The mouse can't travel to any of these squares under the cats' control.
    // He will keep travelling until all the squares for him to visit next are within the
    // cats' threshold (so he dies), or until he reaches the edge (so he lives).
    // Note: we can solve this mathematically, but BFS is more fun/educational.
    public bool Solve(Square mouseStartSquare, Square cat1StartSquare, Square cat2StartSquare)
    {
        bool[,] catThreshold = new bool[_rowCount, _columnCount];
        catThreshold[cat1StartSquare.Row, cat1StartSquare.Column] = true;
        catThreshold[cat2StartSquare.Row, cat2StartSquare.Column] = true;
        var catSquaresToVisit = new Queue<Square>();
        catSquaresToVisit.Enqueue(cat1StartSquare);
        catSquaresToVisit.Enqueue(cat2StartSquare);

        bool[,] mouseThreshold = new bool[_rowCount, _columnCount];
        mouseThreshold[mouseStartSquare.Row, mouseStartSquare.Column] = true;
        var mouseSquaresToVisit = new Queue<Square>();
        mouseSquaresToVisit.Enqueue(mouseStartSquare);

        while (mouseSquaresToVisit.Count > 0)
        {
            int catWaveSize = catSquaresToVisit.Count;
            for (int i = 0; i < catWaveSize; ++i)
            {
                var catSquare = catSquaresToVisit.Dequeue();
                foreach (var neighbor in GetNeighborSquares(catSquare))
                {
                    if (!catThreshold[neighbor.Row, neighbor.Column])
                    {
                        catThreshold[neighbor.Row, neighbor.Column] = true;
                        catSquaresToVisit.Enqueue(neighbor);
                    }
                }
            }

            int mouseWaveSize = mouseSquaresToVisit.Count;
            for (int i = 0; i < mouseWaveSize; ++i)
            {
                var mouseSquare = mouseSquaresToVisit.Dequeue();
                foreach (var neighbor in GetNeighborSquares(mouseSquare))
                {
                    if (!catThreshold[neighbor.Row, neighbor.Column]
                        && !mouseThreshold[neighbor.Row, neighbor.Column])
                    {
                        if (neighbor.Row == 0
                            || neighbor.Column + 1 == _columnCount
                            || neighbor.Row + 1 == _rowCount
                            || neighbor.Column == 0)
                            return true;

                        mouseThreshold[neighbor.Row, neighbor.Column] = true;
                        mouseSquaresToVisit.Enqueue(neighbor);
                    }
                }
            }
        }

        return false;
    }

    private IEnumerable<Square> GetNeighborSquares(Square square)
    {
        if (square.Row - 1 >= 0)
            yield return new Square(square.Row - 1, square.Column);
        if (square.Column + 1 < _columnCount)
            yield return new Square(square.Row, square.Column + 1);
        if (square.Row + 1 < _rowCount)
            yield return new Square(square.Row + 1, square.Column);
        if (square.Column - 1 >= 0)
            yield return new Square(square.Row, square.Column - 1);
    }
}

public struct Square
{
    public Square(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public int Row { get; set; }
    public int Column { get; set; }
}

public static class Program
{
    private static void Main()
    {
        string[] line = Console.ReadLine().Split();
        int rowCount = int.Parse(line[0]);
        int columnCount = int.Parse(line[1]);
        var solver = new CATM(rowCount, columnCount);

        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            line = Console.ReadLine().Split();
            var mouseStartSquare = new Square(
                row: int.Parse(line[0]) - 1,
                column: int.Parse(line[1]) - 1);
            var cat1StartSquare = new Square(
                row: int.Parse(line[2]) - 1,
                column: int.Parse(line[3]) - 1);
            var cat2StartSquare = new Square(
                row: int.Parse(line[4]) - 1,
                column: int.Parse(line[5]) - 1);
            bool result = solver.Solve(mouseStartSquare, cat1StartSquare, cat2StartSquare);

            Console.WriteLine(result ? "YES" : "NO");
        }
    }
}
