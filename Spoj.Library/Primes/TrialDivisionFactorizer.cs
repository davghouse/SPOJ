using System;
using System.Collections.Generic;

namespace Spoj.Library.Primes
{
    public sealed class TrialDivisionFactorizer : IPrimeFactorizer
    {
        private readonly SieveOfEratosthenesFactorizer _sieveFactorizer;

        public TrialDivisionFactorizer(int limit)
        {
            Limit = limit;

            _sieveFactorizer = new SieveOfEratosthenesFactorizer(
                Convert.ToInt32(Math.Sqrt(Limit)), needsToProvide: true);
        }

        public int Limit { get; }

        public IEnumerable<int> GetPrimeFactors(int n)
        {
            if (n <= _sieveFactorizer.Limit)
            {
                foreach (int primeFactor in _sieveFactorizer.GetPrimeFactors(n))
                    yield return primeFactor;
            }
            else
            {
                foreach (int prime in _sieveFactorizer.Primes)
                {
                    // Check for factors up to sqrt(n), as non-primes with such a factor must've had
                    // a factor seen earlier < sqrt(n) (otherwise multiplied together they'd be > n).
                    // The fact that n is getting smaller doesn't matter. If this condition makes the
                    // loop stop, the current value of n must be a prime greater than 'prime', since
                    // n's only other option (multiple prime factors > 'prime') doesn't stop the loop.
                    if (prime * prime > n)
                        break;

                    while (n % prime == 0)
                    {
                        yield return prime;
                        n /= prime;
                    }

                    // All the prime factors have been extracted, so stop looking.
                    if (n == 1)
                        yield break;
                }

                // The loop above was broken out of (before n == 1), so the original n, or what remains of it, is prime.
                yield return n;
            }
        }

        public IEnumerable<int> GetDistinctPrimeFactors(int n)
        {
            if (n <= _sieveFactorizer.Limit)
            {
                foreach (int primeFactor in _sieveFactorizer.GetDistinctPrimeFactors(n))
                    yield return primeFactor;
            }
            else
            {
                // See comments above in GetPrimeFactors.
                foreach (int prime in _sieveFactorizer.Primes)
                {
                    if (prime * prime > n)
                        break;

                    if (n % prime == 0)
                    {
                        yield return prime;

                        while (n % prime == 0)
                        {
                            n /= prime;
                        }
                    }

                    if (n == 1)
                        yield break;
                }

                yield return n;
            }
        }
    }
}
