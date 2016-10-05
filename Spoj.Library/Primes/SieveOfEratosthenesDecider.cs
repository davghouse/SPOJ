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

            // Check for n up to sqrt(Limit), as any non-primes <= Limit with a factor > sqrt(Limit)
            // must also have a factor < sqrt(Limit) (otherwise they'd be > Limit), and so already sieved.
            for (int n = 2; n * n <= Limit; ++n)
            {
                // If true then n hasn't been sieved yet, so it's prime; sieve its multiples.
                if (_sieve[n])
                {
                    // Multiples of n less than n * n were already sieved from lower primes.
                    int nextPotentiallyUnsievedMultiple = n * n;
                    while (nextPotentiallyUnsievedMultiple <= Limit)
                    {
                        _sieve[nextPotentiallyUnsievedMultiple] = false;
                        // Room for optimization here; could do += 2n except in the case where n is 2.
                        nextPotentiallyUnsievedMultiple += n; 
                    }
                }
            }
        }

        public int Limit { get; }

        public bool IsPrime(int n)
            => _sieve[n];
    }
}
