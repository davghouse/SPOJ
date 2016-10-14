using System;
using System.Text;

// 2716 http://www.spoj.com/problems/QUADAREA/ Maximal Quadrilateral Area
// Compute the max area possible for any quadrilateral with the given side lengths.
public static class QUADAREA
{
    // Some facts on the Wikipedia page for cycli quadrilateral mention the cyclic
    // quadrilateral has the most area of any quadrilateral with the same side lengths.
    // Not sure but I guess not matter the side lengths a cyclic quadrilteral always exists,
    // so we can use: https://en.wikipedia.org/wiki/Brahmagupta%27s_formula.
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
        int remainingTestCases = int.Parse(Console.ReadLine());
        var output = new StringBuilder();

        while (remainingTestCases-- > 0)
        {
            double[] line = Array.ConvertAll(Console.ReadLine().Split(), double.Parse);

            output.AppendLine(
                QUADAREA.Solve(line[0], line[1], line[2], line[3]).ToString());
        }

        Console.Write(output);
    }
}
