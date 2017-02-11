using System.Collections.Generic;

namespace Daves.SpojSpace.Library.Primes
{
    public static class NaivePrimeDeciderProviderFactorizer
    {
        public static bool IsPrime(int n)
        {
            if (n == 0 || n == 1)
                return false;

            for (int i = 2; i * i <= n; ++i)
            {
                if (n % i == 0)
                    return false;
            }

            return true;
        }

        public static IReadOnlyList<int> GetPrimes(int limit)
        {
            var primes = new List<int>();
            for (int n = 2; n <= limit; ++n)
            {
                if (IsPrime(n))
                {
                    primes.Add(n);
                }
            }

            return primes;
        }

        public static IEnumerable<int> GetPrimeFactors(int n)
        {
            for (int i = 2; i <= n; ++i)
            {
                if (IsPrime(i))
                {
                    while (n % i == 0)
                    {
                        yield return i;
                        n /= i;
                    }
                }
            }
        }

        public static IEnumerable<int> GetDistinctPrimeFactors(int n)
        {
            for (int i = 2; i <= n; ++i)
            {
                if (IsPrime(i))
                {
                    if (n % i == 0)
                        yield return i;

                    while (n % i == 0)
                    {
                        n /= i;
                    }
                }
            }
        }
    }
}
