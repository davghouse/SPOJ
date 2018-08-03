using System;
using System.Text;

// https://www.spoj.com/problems/ENIGMATH/ #gcd #math
// Solves Ax - By = 0 with x and y the smallest possible positive integers.
public static class ENIGMATH
{
    // Ax = By => y = (A/B)x. A and B might not be in lowest terms, but we can divide each by
    // their GCD so that they are. So assume they are in lowest terms, A', B'. y must be an
    // integer, so x has to be a multiple of B' to make (A'/B')x an integer (since B' has
    // no factors in common with A'). Choosing x = B' then minimizes y as A', and also minimizes
    // x. Therefore, x = B' = B / GCD(A,B) and y = A' = A / GCD(A,B). Consuming the array for convenience.
    public static int[] Solve(int[] ab)
    {
        int a = ab[0];
        int b = ab[1];
        int gcd = GreatestCommonDivisor(a, b);
        ab[0] = b / gcd; // mapping to x
        ab[1] = a / gcd; // mapping to y

        return ab; // returning x and y
    }

    // This is a good article (first section): http://www.cut-the-knot.org/blue/Euclid.shtml.
    // One point to note, for a = bt + r, the gcd(a, b) divides a so it divides bt + r.
    // And it divides b, so it divides bt, which means for bt + r to be divisible by it,
    // r also needs to be divisible by it. So it divides both b and r. And the article
    // notes the importance of showing not only does it divide b and r, it's also their gcd.
    public static int GreatestCommonDivisor(int a, int b)
    {
        int temp;
        while (b != 0)
        {
            temp = b;
            b = a % b;
            a = temp;
        }

        return a;
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
            int[] ab = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
            int[] xy = ENIGMATH.Solve(ab);

            output.Append(
                $"{xy[0]} {xy[1]}");
            output.AppendLine();
        }

        Console.Write(output);
    }
}
