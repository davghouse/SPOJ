using System;

// https://www.spoj.com/problems/CANTON/ #ad-hoc #math #sequence
// Finds the nth term in Cantor's enumeration of the rational numbers.
public static class CANTON
{
    public static Tuple<int, int> Solve(int n)
    {
        // The enumeration progresses along diagonals of the rectangle. The first diagonal has
        // one term, the second two, the third three, and so on. Hence, the total number of terms
        // in the first d diagonals is 1 + 2 + ... + d = (d + 1)d/2. The nth term is found in the
        // first diagonal d such that (d + 1)d/2 >= n => (d + 1/2)^2 >= 2n + 1/4 =>
        int diagonal = (int)Math.Ceiling((Math.Sqrt(2 * n + 0.25) - 0.5));
        int positionOfFurthestTermAlongDiagonal = ((diagonal + 1) * diagonal) / 2;
        int termsBackFromFurthestTermAlongDiagonal = positionOfFurthestTermAlongDiagonal - n;
        var diagonalEndPosition
            = diagonal % 2 == 0 ? DiagonalEndPosition.SideEdge : DiagonalEndPosition.TopEdge;
        int numeratorOfFurthestTermAlongDiagonal
            = diagonalEndPosition == DiagonalEndPosition.SideEdge ? diagonal : 1;
        int denominatorOfFurthestTermAlongDiagonal
            = diagonalEndPosition == DiagonalEndPosition.TopEdge ? diagonal : 1;

        int numeratorOfNthTerm;
        int denominatorOfNthTerm;
        if (diagonalEndPosition == DiagonalEndPosition.SideEdge)
        {
            // If the last term of the diagonal ends along the side, we traverse up and to the right
            // to find the nth term, the numerator decreasing and the denominator increasing.
            numeratorOfNthTerm
                = numeratorOfFurthestTermAlongDiagonal - termsBackFromFurthestTermAlongDiagonal;
            denominatorOfNthTerm
                = denominatorOfFurthestTermAlongDiagonal + termsBackFromFurthestTermAlongDiagonal;
        }
        else
        {
            // If the last term of the diagonal ends along the top, we traverse down and to the left
            // to find the nth term, the numerator increasing and the denominator decreasing.
            numeratorOfNthTerm
                = numeratorOfFurthestTermAlongDiagonal + termsBackFromFurthestTermAlongDiagonal;
            denominatorOfNthTerm
                = denominatorOfFurthestTermAlongDiagonal - termsBackFromFurthestTermAlongDiagonal;
        }

        return Tuple.Create(numeratorOfNthTerm, denominatorOfNthTerm);
    }

    private enum DiagonalEndPosition
    {
        SideEdge,
        TopEdge
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int n = int.Parse(Console.ReadLine());
            Tuple<int, int> nthTerm = CANTON.Solve(n);

            Console.WriteLine(
                $"TERM {n} IS {nthTerm.Item1}/{nthTerm.Item2}");
        }
    }
}
