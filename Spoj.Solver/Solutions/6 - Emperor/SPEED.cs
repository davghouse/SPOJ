using System;

// https://www.spoj.com/problems/SPEED/ #ad-hoc #gcd #math
// Finds at how many distinct points a runner passes a slower run on a circular track.
public static class SPEED
{
    // The pattern of meeting points will begin repeating once the two runners both
    // end up at the start position at the same time, since that's how the race began.
    // Imagine they met at a point for a second time before meeting up at start... their
    // speeds are constants so you could run time backwards to show they must've already
    // met @ start. Say the faster runner has speed f and the slower runner has speed s.
    // When the faster runner has completed a lap, the slower runner has completed s/f
    // of a lap. Say the faster runner has completed n laps. Then the slower runner has
    // completed n*s/f laps. We want to find the lowest n such that n*s/f is an integer,
    // meaning they'd both be back at the start. Write s/f is lowest terms as p/q (by
    // dividing out their GCD). Then we see after q laps by the faster runner, the
    // slower runner has run p laps, and they're both back at start. The slower runner
    // got lapped q-p times, each one contributing a meeting point. For when they're
    // travelling in opposite directions, it's similar. But instead of the slower runner's
    // p laps preventing him from getting lapped more, they're now contributing to it, so q+p.
    public static int Solve(int s1, int s2)
    {
        bool goingInSameDirection = Math.Sign(s1) == Math.Sign(s2);
        int fastSpeed = Math.Max(Math.Abs(s1), Math.Abs(s2));
        int slowSpeed = Math.Min(Math.Abs(s1), Math.Abs(s2));
        int gcd = GreatestCommonDivisor(fastSpeed, slowSpeed);

        return goingInSameDirection
            ? fastSpeed / gcd - slowSpeed / gcd
            : fastSpeed / gcd + slowSpeed / gcd;
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
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            Console.WriteLine(
                SPEED.Solve(line[0], line[1]));
        }
    }
}
