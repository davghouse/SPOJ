using System;
using System.Collections.Generic;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/PPATH/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        if (remainingTestCases < 1 || remainingTestCases > 100)
            throw new FormatException();

        while (remainingTestCases-- > 0)
        {
            int[] primes = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            if (primes[0] < 1000 || primes[0] > 9999)
                throw new FormatException();

            if (primes[1] < 1000 || primes[1] > 9999)
                throw new FormatException();

            // I'll trust they're primes.
        }
    }
}
