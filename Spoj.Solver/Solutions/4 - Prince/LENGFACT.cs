using System;

// https://www.spoj.com/problems/LENGFACT/ #formula #math
// Calculates the number of digits in a factorial.
public static class LENGFACT
{
    // Not sure how to rate this problem. Kind of reminds me of FCTRL. I gave up trying
    // to figure out a smart way and BigInteger was clearly going to be too slow. So:
    // https://oeis.org/A034886, https://mathoverflow.net/q/19170
    public static long Solve(long n)
        => n < 2 ? 1 : (long)Math.Ceiling(Math.Log10(2 * Math.PI * n) / 2 + n * Math.Log10(n / Math.E));
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            long n = long.Parse(Console.ReadLine());

            Console.WriteLine(
                LENGFACT.Solve(n));
        }
    }
}
