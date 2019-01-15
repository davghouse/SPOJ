using System;

namespace Spoj.Library.Helpers
{
    public static class MathHelper
    {
        // http://stackoverflow.com/a/600306
        public static bool IsPowerOfTwo(long n)
            => n <= 0 ? false : (n & (n - 1)) == 0;

        public static int FirstPowerOfTwoEqualOrGreater(int value)
        {
            int result = 1;
            while (result < value)
            {
                result <<= 1;
            }

            return result;
        }

        public static int GreatestPowerOfTwoEqualOrLess(int value)
        {
            int result = 2;
            while (result <= value)
            {
                result <<= 1;
            }

            return result >> 1;
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

        // https://en.wikipedia.org/wiki/Exponentiation_by_squaring
        // https://stackoverflow.com/a/383596
        public static int Pow(int @base, int exponent)
        {
            int result = 1;
            while (exponent != 0)
            {
                if ((exponent & 1) == 1)
                {
                    result *= @base;
                }

                @base *= @base;
                exponent >>= 1;
            }

            return result;
        }

        // https://en.wikipedia.org/wiki/Exponentiation_by_squaring
        // https://stackoverflow.com/a/383596
        // https://en.wikipedia.org/wiki/Modular_exponentiation#Right-to-left_binary_method
        public static int ModularPow(int @base, int exponent, int modulus)
        {
            if (modulus == 1)
                return 0;

            int result = 1;
            @base = @base % modulus;
            while (exponent != 0)
            {
                if ((exponent & 1) == 1)
                {
                    result = result * @base % modulus;
                }

                @base = @base * @base % modulus;
                exponent >>= 1;
            }

            return result;
        }
    }
}
