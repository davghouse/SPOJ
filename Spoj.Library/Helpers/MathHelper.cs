using System;

namespace Spoj.Library.Helpers
{
    public static class MathHelper
    {
        // http://stackoverflow.com/a/600306
        public static bool IsPowerOfTwo(long n)
            => n <= 0 ? false : (n & (n - 1)) == 0;

        public static int FirstPowerOfTwoAtOrAfter(int value)
        {
            int result = 1;
            while (result < value)
            {
                result <<= 1;
            }

            return result;
        }

        // C(n, k)
        // = [n * (n - 1) * ... * (n - k + 1)] / [k * (k - 1) * ... * 1]
        // = (n / 1) * ((n - 1) / 2) * ... * ((n - k + 1) / k).
        // As long as we multiply by the next term's numerator first it'll be an integer every step
        // of the way, since it'll correspond to a different combination (for a k equal to the denominator).
        // And C(n, k) = C(n, n - k), so choose whichever is smaller.
        public static int NumberOfCombinations(int n, int k)
        {
            k = Math.Min(k, n - k);
            if (k == 0) return 1;

            int result = n;
            for (int denominator = 2; denominator <= k; ++denominator)
            {
                result *= (n - denominator + 1);
                result /= denominator;
            }

            return result;
        }
    }
}
