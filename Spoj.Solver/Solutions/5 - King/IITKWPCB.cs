using System;

// https://www.spoj.com/problems/IITKWPCB/ #factors #math #proof
// Finds the largest number from N/2 that's coprime with N.
public static class IITKWPCB
{
    public static long Solve(long n)
        // Consider odd n. n - 1 is coprime with n because any factor f divides n but
        // doesn't divide 1, so n/f - 1/f is definitely not an integer. Hence, (n - 1)/2
        // is also not coprime with n, because it has the same insufficient factors as
        // n - 1, except for missing a factor of 2.
        => n % 2 == 1 ? (n - 1) / 2
        // Consider even n = 2m. 2m/2 = m is a factor of 2m, so they aren't coprime. The
        // next highest option is m - 1. m - 1 is coprime with m as shown above, but is
        // it coprime with 2m (is it divisible by 2)? If m is even, m - 1 isn't, so
        // m - 1 would be coprime with 2m. If m is odd, m - 1 is even, so m - 1 would
        // not be coprime with 2m. But m - 2 would be, since it's odd (and again, going
        // back to the logic above, any other factors of n=2m won't divide the -2).
        : n / 2 % 2 == 0 ? n / 2 - 1
        : n / 2 - 2;
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
                IITKWPCB.Solve(n));
        }
    }
}
