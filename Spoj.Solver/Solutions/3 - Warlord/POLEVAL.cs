using System;
using System.Text;

// https://www.spoj.com/problems/POLEVAL/ #ad-hoc
// Evaluates a polynomial of high degree.
public static class POLEVAL
{
    // The polynomial is like: c_n * x^n + c_n-1 * x^n-1 + … + c_2 * x^2 + c_1 * x + c_0
    // We start from the right-hand side and perform 2 multiplications and 1 addition for each
    // term. Importantly, we use x^m-1 from the previous coefficient to compute x^m = x*x^m-1.
    public static long Solve(int degree, int[] coefficients, int point)
    {
        int coefficientIndex = degree;
        long result = coefficients[coefficientIndex];
        int pointToPower = 1;
        while (--coefficientIndex >= 0)
        {
            pointToPower *= point;
            result += coefficients[coefficientIndex] * pointToPower;
        }

        return result;
    }
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int caseNumber = 0;
        int degree;
        while ((degree = int.Parse(Console.ReadLine())) != -1)
        {
            int[] coefficients = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            int pointCount = int.Parse(Console.ReadLine());
            int[] points = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            output.AppendLine($"Case {++caseNumber}:");
            for (int p = 0; p < pointCount; ++p)
            {
                output.Append(
                    POLEVAL.Solve(degree, coefficients, points[p]));
                output.AppendLine();
            }
        }

        Console.Write(output);
    }
}
