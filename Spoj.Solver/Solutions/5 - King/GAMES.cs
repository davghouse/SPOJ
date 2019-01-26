using System;
using System.Text;

// https://www.spoj.com/problems/GAMES/ #ad-hoc #gcd #proof
// Finds the minimum number of integer scores that could've produced the given average.
public static class GAMES
{
    // We're given the average, where average = sum of integer scores / # of games.
    // We want to find the fewest # of games possible. average * # of games has to
    // equal an integer. The non-fractional part of average can be ignored, since it
    // times an integer always contributes an integer amount to the sum. So we need
    // fractional part of average * # of games = integer. We're told the fractional
    // part has no more than 4 decimal places, so its smallest value is .0001. Hence,
    // it can be represented as f / 10000, where f is an integer.
    // By finding the GCD of f and 10000 we can write it in lowest terms as p / q.
    // For p / q * # of games to equal an integer, the # of games must be a multiple
    // of q. Otherwise there'd be some factor of q left in the denominator that
    // neither p (because it has no divisor in common with q) or the # of games can
    // cancel out, meaning p / q * that number of games wouldn't be an integer.
    // Smallest number of games is then the smallest multiple of q--just q itself.
    public static int Solve(decimal average)
    {
        int fractionalPartToInt = (int)(10000 * (average - (int)average));
        if (fractionalPartToInt == 0)
            return 1; // If the average is already an integer, they could've only played 1 game.

        int numerator = fractionalPartToInt;
        int denominator = 10000;
        int gcd = GreatestCommonDivisor(numerator, denominator);
        int reducedDenominator = denominator / gcd;

        return reducedDenominator;
    }

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
        var output = new StringBuilder();
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            output.Append(
                GAMES.Solve(decimal.Parse(Console.ReadLine())));
            output.AppendLine();
        }

        Console.Write(output);
    }
}
