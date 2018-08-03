using System;
using System.Text;

// https://www.spoj.com/problems/QUADAREA/ #formula
// Compute the max area possible for any quadrilateral with the given side lengths.
public static class QUADAREA
{
    // Some facts on the Wikipedia page for cyclic quadrilateral mention they have the
    // most area of any quadrilateral with the same sequence of side lengths. Not sure
    // but I guess no matter the side lengths, we can assume a cyclic quadrilteral with
    // those side lengths exists => https://en.wikipedia.org/wiki/Brahmagupta%27s_formula.
    public static double Solve(double a, double b, double c, double d)
    {
        double s = (a + b + c + d) / 2;

        return Math.Sqrt((s - a) * (s - b) * (s - c) * (s - d));
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
            double[] line = Array.ConvertAll(Console.ReadLine().Split(), double.Parse);

            output.AppendLine(
                QUADAREA.Solve(line[0], line[1], line[2], line[3]).ToString());
        }

        Console.Write(output);
    }
}
