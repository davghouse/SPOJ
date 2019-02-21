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
                limit: Convert.ToInt32(Math.Sqrt(Limit)),
                needsToProvide: true);
        }

        public int Limit { get; }

        public IEnumerable<int> GetPrimeFactors(int n)
        {
            if (n <= _sieveFactorizer.Limit)
            {
                foreach (int primeFactor in _sieveFactorizer.GetPrimeFactors(n))
                {
                    yield return primeFactor;
                }
            }
            else
            {
                foreach (int prime in _sieveFactorizer.Primes)
                {
                    // Check for factors up to sqrt(n), as non-primes with a factor larger than that
                    // must also have a factor less than that, otherwise they'd multiply together to
                    // make a number greater than n. The fact that n is getting smaller doesn't matter.
                    // If this condition stops the loop, what remains of n is a single prime factor.
                    // All primes less than 'prime' were already divided out, so for n to have multiple
                    // prime factors they'd have to all be >= 'prime', but in that case the loop
                    // wouldn't stop here.
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

                // The loop above was broken out of (before n == 1), so the original n, or what
                // remains of it, is prime.
                yield return n;
            }
        }

        public IEnumerable<int> GetDistinctPrimeFactors(int n)
        {
            if (n <= _sieveFactorizer.Limit)
            {
                foreach (int primeFactor in _sieveFactorizer.GetDistinctPrimeFactors(n))
                {
                    yield return primeFactor;
                }
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
