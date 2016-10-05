using System;
using System.Collections.Generic;

namespace Spoj.Library.Primes
{
    public sealed class SieveOfEratosthenesFactorizer : IPrimeDecider, IPrimeProvider, IPrimeFactorizer
    {
        // The only thing different about this sieve is that rather than storing true for prime
        // and false for not prime, it stores null for prime and some prime factor (doesn't matter which)
        // that divides the number for not prime. Knowing some prime factor that divides n, we can divide
        // by that factor, and then divide the result by its factor, and so on, until we reach one.
        private readonly IReadOnlyList<int?> _sieveWithSomePrimeFactor;

        // This bool seems kind of bad but for now, whatever. Providing from here is convenient but may not be needed.
        public SieveOfEratosthenesFactorizer(int limit, bool needsToProvide = false)
        {
            Limit = limit;

            int?[] sieveWithSomePrimeFactor = new int?[Limit + 1];
            sieveWithSomePrimeFactor[0] = 0;
            sieveWithSomePrimeFactor[1] = 1;

            // Check for n up to sqrt(Limit), as any non-primes <= Limit with a factor > sqrt(Limit)
            // must also have a factor < sqrt(Limit) (otherwise they'd be > Limit), and so already sieved.
            for (int n = 2; n*n <= Limit; ++n)
            {
                // If true then n hasn't been sieved yet, so it's prime; sieve its multiples.
                if (!sieveWithSomePrimeFactor[n].HasValue)
                {
                    // Multiples of n less than n * n were already sieved from lower primes.
                    int nextPotentiallyUnsievedMultiple = n * n;
                    while (nextPotentiallyUnsievedMultiple <= Limit)
                    {
                        sieveWithSomePrimeFactor[nextPotentiallyUnsievedMultiple] = n;
                        // Room for optimization here; could do += 2n except in the case where n is 2.
                        nextPotentiallyUnsievedMultiple += n;
                    }
                }
            }

            _sieveWithSomePrimeFactor = Array.AsReadOnly(sieveWithSomePrimeFactor);

            if (needsToProvide)
            {
                var primes = new List<int>();
                for (int n = 2; n <= Limit; ++n)
                {
                    if (IsPrime(n))
                    {
                        primes.Add(n);
                    }
                }

                Primes = primes.AsReadOnly();
            }
        }

        public int Limit { get; }

        public bool IsPrime(int n)
            => !_sieveWithSomePrimeFactor[n].HasValue;

        public IReadOnlyList<int> Primes { get; }

        public IEnumerable<int> GetPrimeFactors(int n)
        {
            while (n > 1)
            {
                int somePrimeFactor = _sieveWithSomePrimeFactor[n] ?? n;
                yield return somePrimeFactor;

                n /= somePrimeFactor;
            }
        }

        public IEnumerable<int> GetDistinctPrimeFactors(int n)
        {
            while (n > 1)
            {
                int somePrimeFactor = _sieveWithSomePrimeFactor[n] ?? n;
                yield return somePrimeFactor;

                while (n % somePrimeFactor == 0)
                {
                    n /= somePrimeFactor;
                }
            }
        }
    }
}
