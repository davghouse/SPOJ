using System;
using System.Numerics;

// http://www.spoj.com/problems/GCD2/ #gcd #math #research #trap
// Finds the GCD of two numbers, one of which can be really big.
public static class GCD2
{
    public static int Solve(int a, int b)
        => GreatestCommonDivisor(a, b);

    // This is a good article (first section): http://www.cut-the-knot.org/blue/Euclid.shtml.
    // One point to note, for a = bt + r, the gcd(a, b) divides a so it divides bt + r.
    // And it divides b, so it divides bt, which means for bt + r to be divisible by it,
    // r also needs to be divisible by it. So it divides both b and r. And the article
    // notes the importance of showing not only does it divide b and r, it's also their gcd.
    private static int GreatestCommonDivisor(int a, int b)
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
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();

            int a = int.Parse(line[0]);
            BigInteger b = BigInteger.Parse(line[1]);

            if (a == 0)
            {
                Console.WriteLine(b);
            }
            else
            {
                Console.WriteLine(
                    // a is guaranteed to be an integer. The trick of the problem is being able to do
                    // the b % a the first time, which gets the big integer small enough to work with
                    // as an int. Easy for us since we have BigInteger built-in.
                    GCD2.Solve(a, (int)(b % a)));
            }
        }
    }
}
