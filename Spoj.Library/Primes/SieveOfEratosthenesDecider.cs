using System;
using System.Collections;

namespace Spoj.Library.Primes
{
    public sealed class SieveOfEratosthenesDecider : IPrimeDecider
    {
        private readonly BitArray _sieve;

        public SieveOfEratosthenesDecider(int limit)
        {
            Limit = limit;

            _sieve = new BitArray(Limit + 1, true);
            _sieve[0] = false;
            _sieve[1] = false;

            for (int n = 2; n <= (int)Math.Sqrt(Limit); ++n)
            {
                if (_sieve[n]) // Then n hasn't been sieved yet, so it's prime; sieve its multiples.
                {
                    int nextPotentiallyUnsievedMultiple = n * n; // Multiples of n less than this were already sieved from lower primes.
                    while (nextPotentiallyUnsievedMultiple <= Limit)
                    {
                        _sieve[nextPotentiallyUnsievedMultiple] = false;
                        nextPotentiallyUnsievedMultiple += n; // Room for optimization here; could do += 2n except in the case where n is 2.
                    }
                }
            }
        }

        public int Limit { get; }

        public bool IsPrime(int n)
            => _sieve[n];
    }
}
