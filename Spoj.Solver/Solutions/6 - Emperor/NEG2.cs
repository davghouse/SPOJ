using System;

// https://www.spoj.com/problems/NEG2/ #ad-hoc #binary #experiment
// Converts decimal integers to base -2.
public static class NEG2
{
    // Observe the following after playing around on paper for a while:
    // The positive powers of 2 are: 2^0 = 1, 2^2 = 4, 2^4 = 16, etc, so for example, we can
    // represent up to 5 (1 + 4) by using powers 2^2 and below, up to 21 (1 + 4 + 16) by using
    // powers 2^4 and below, and so on. Negatives are similar with 2^1 = -2, 2^3 = -8, where
    // for example we can represent up to -10 (-2 + -8) by using powers 2^3 and below.
    // Example for 13:
    // It's greater than 5 but less than 21, so we use 2^4 in its representation. We are trying
    // to get 13 total, so now we need -3, because 16 + -3 = 13. -3 is less than -2 but greater
    // than -10, so we need to use 2^3 in its representation. The total is now 16 - 8 = 8, so
    // we need 5 because 8 + 5 = 13. 5 uses 2^2, then we need 1, 1 uses 2^0, then we're done.
    // We used 2^4, 2^3, 2^2, and 2^0 for 16-8+4+1=13, and binary of 11101.
    public static string Solve(long n)
    {
        long nToBaseNeg2 = 0;

        while (n != 0)
        {
            if (n > 0)
            {
                // Start at 2^0, increment to 2^2, 2^4, etc, and stop once the running sum
                // shows we've reached a high enough power of 2 to represent what remains of n.
                long nextPositivePowerOf2 = 1; 
                long positivePowerOf2Sum = 1;
                while (positivePowerOf2Sum < n)
                {
                    nextPositivePowerOf2 <<= 2;
                    positivePowerOf2Sum |= nextPositivePowerOf2;
                }

                n -= nextPositivePowerOf2;
                nToBaseNeg2 |= nextPositivePowerOf2;
            }
            else
            {
                long nextNegativePowerOf2 = 2;
                long negativePowerOf2Sum = 2;
                while (negativePowerOf2Sum < -n)
                {
                    nextNegativePowerOf2 <<= 2;
                    negativePowerOf2Sum |= nextNegativePowerOf2;
                }

                n += nextNegativePowerOf2;
                nToBaseNeg2 |= nextNegativePowerOf2;
            }
        }

        return Convert.ToString(nToBaseNeg2, toBase: 2);
    }
}

public static class Program
{
    private static void Main()
        => Console.Write(
            NEG2.Solve(long.Parse(Console.ReadLine())));
}
