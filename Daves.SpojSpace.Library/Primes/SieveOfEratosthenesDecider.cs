using System;
using System.Collections.Generic;

namespace Daves.SpojSpace.Library.Primes
{
    // This sieve has some optimizations to avoid storing results for even integers; the result for an odd
    // integer n is stored at index n / 2. IsOddPrime is supplied for convenience (input n assumed to be odd).
    public sealed class SieveOfEratosthenesDecider : IPrimeDecider
    {
        private readonly IReadOnlyList<bool> _sieve;

        public SieveOfEratosthenesDecider(int limit)
        {
            Limit = limit;

            bool[] sieve = new bool[(Limit + 1) >> 1];
            sieve[0] = true; // 1 (which maps to index [1 / 2] == [0]) is not a prime, so sieve it out.

            // Check for n up to sqrt(Limit), as any non-primes <= Limit with a factor > sqrt(Limit)
            // must also have a factor < sqrt(Limit) (otherwise they'd be > Limit), and so already sieved.
            for (int n = 3; n * n <= Limit; n += 2)
            {
                // If we haven't sieved it yet then it's a prime, so sieve its multiples.
                if (!sieve[n >> 1])
                {
                    // Multiples of n less than n * n were already sieved from lower primes. Add twice
                    // n for each iteration, as otherwise it's odd + odd = even.
                    for (int nextPotentiallyUnsievedMultiple = n * n;
                        nextPotentiallyUnsievedMultiple <= Limit;
                        nextPotentiallyUnsievedMultiple += (n << 1))
                    {
                        sieve[nextPotentiallyUnsievedMultiple >> 1] = true;
                    }
                }
            }
            _sieve = Array.AsReadOnly(sieve);
        }

        public int Limit { get; }

        public bool IsPrime(int n)
            => (n & 1) == 0 ? n == 2 : IsOddPrime(n);

        public bool IsOddPrime(int n)
            => !_sieve[n >> 1];
    }
}
