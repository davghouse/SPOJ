using System;
using System.Collections.Generic;

// http://www.spoj.com/problems/EIGHTS/: digits, numbers
// Finds the kth cube that ends in 888 (the first such cube is 192).
public static class EIGHTS
{
    // Only part that affects the last three digits of a multiplied number are the last three
    // digits of the numbers being multipled. These are all the three digit numbers whose
    // cubes end in 888. Any other, higher numbers whose cubes end in 888 must themselves
    // end in one of these four numbers (and it's an if and only if).
    public static readonly IReadOnlyList<int> threeDigitCubesEndingIn888 = new[] { 192, 442, 692, 942 };

    public static long Solve(long k)
    {
        // k is indexed from 1, subtracting by 1 here makes the modulo result map directly to a list index.
        int finalThreeDigits = threeDigitCubesEndingIn888[(int)((k - 1) % 4)];
        // There are four cubes for each 1000 numbers as seen above, so calculate how many 1000s need to get added before the final three digits.
        long countOf1000s = (k - 1) / 4;

        // This is going to be a really big number, but it's guaranteed to be < 2^63 - 1.
        return countOf1000s * 1000 + finalThreeDigits;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            Console.WriteLine(
                EIGHTS.Solve(long.Parse(Console.ReadLine())));
        }
    }
}
