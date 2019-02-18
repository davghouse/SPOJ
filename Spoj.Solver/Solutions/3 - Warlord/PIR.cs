using System;
using System.Text;

// https://www.spoj.com/problems/PIR/ #formula #math
// Calculates the volume of a triangular pyramid, given its side lengths.
public static class PIR
{
    // See https://en.wikipedia.org/wiki/Tetrahedron#Heron-type_formula_for_the_volume_of_a_tetrahedron.
    // References a paper talking about a lot of numerical analysis issues, but for
    // us the main problem will be making sure we line up the input to match the
    // side lengths needed by the formula. Inspect sides listed by the problem,
    // compare to U, V, W, u, v, w needed by formula, to see wvuUVW is input order.
    public static double Solve(int[] wvuUVW)
    {
        double U = wvuUVW[3], V = wvuUVW[4], W = wvuUVW[5];
        double u = wvuUVW[2], v = wvuUVW[1], w = wvuUVW[0];

        double X = (w - U + v) * (U + v + w);
        double x = (U - v + w) * (v - w + U);
        double Y = (u - V + w) * (V + w + u);
        double y = (V - w + u) * (w - u + V);
        double Z = (v - W + u) * (W + u + v);
        double z = (W - u + v) * (u - v + W);

        double a = Math.Sqrt(x * Y * Z);
        double b = Math.Sqrt(y * Z * X);
        double c = Math.Sqrt(z * X * Y);
        double d = Math.Sqrt(x * y * z);

        double termUnderSqrt =
            (-a + b + c + d)
            * (a - b + c + d)
            * (a + b - c + d)
            * (a + b + c - d);
        double numerator = Math.Sqrt(termUnderSqrt);
        double denominator = 192 * u * v * w;

        return numerator / denominator;
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
            int[] wvuUVW = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            output.AppendLine(
                PIR.Solve(wvuUVW).ToString("F4"));
        }

        Console.Write(output);
    }
}
