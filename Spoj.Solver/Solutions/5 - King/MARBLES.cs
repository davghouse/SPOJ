using System;
using System.Numerics;

// https://www.spoj.com/problems/MARBLES/ #big-numbers #combinatorics #math
// From k differently colored, infinite sets of marbles, counts the number of ways to choose n marbles s.t.
// there's always at least one marble of each color chosen.
public static class MARBLES
{
    // Knowing how to solve the problem without the requirement of a marble for each color is useful
    // (and it's a common problem). Effectively we need to distribute the marbles across colorCount
    // number of bins. Say there's 10 marbles and 4 bins, then one configuration is: m m m | m m | m | m m m m.
    // Configurations represented like that map 1-1 to all possible. Here's another: | | | m m m m m m m m m m.
    // To count those configurations, note a configuration is gotten by choosing the placement of 3 bars across
    // 10 + 3 positions => there are C(13, 3) = 286 ways to place 10 marbles in 4 bins. The problem's additional
    // requirement forces us to place colorCount marbles by hand to begin with, one in each color bin. Then we
    // place marbleCount - colorCount remaining marbles across colorCount bins =>
    // C(positions, bars)
    // = C(remaining marbles + (bins - 1), (bins - 1))
    // = C((marbleCount - colorCount) + (colorCount - 1), (colorCount - 1))
    // = C(marbleCount - 1, colorCount - 1).
    // See https://en.wikipedia.org/wiki/Combination#Number_of_combinations_with_repetition.
    public static BigInteger Solve(int marbleCount, int colorCount)
        => NumberOfCombinations(marbleCount - 1, colorCount - 1);

    // C(n, k)
    // = [n * (n - 1) * ... * (n - k + 1)] / [k * (k - 1) * ... * 1]
    // = (n / 1) * ((n - 1) / 2) * ... * ((n - k + 1) / k).
    // As long as we multiply by the next term's numerator first it'll be an integer every step
    // of the way, since it'll correspond to a different combination (for a k equal to the denominator).
    // And C(n, k) = C(n, n - k), so choose whichever is smaller.
    private static BigInteger NumberOfCombinations(int n, int k)
    {
        k = Math.Min(k, n - k);
        if (k == 0)
            return 1;

        BigInteger result = n;
        for (int denominator = 2; denominator <= k; ++denominator)
        {
            // This is the right idea but still overflows long/ulong in the intermediate for a test case like
            // C(63, 29). So rather than try to figure out how to work around that any better, just used BigInteger.
            result *= n - denominator + 1;
            result /= denominator;
        }

        return result;
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
                MARBLES.Solve(line[0], line[1]));
        }
    }
}
