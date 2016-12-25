using System;
using System.Collections.Generic;

namespace Spoj.Library.Primes
{
    public sealed class SieveOfEratosthenesFactorizer : IPrimeDecider, IPrimeProvider, IPrimeFactorizer
    {
        // This sieve is slightly different, rather than storing false for prime (unsieved) and true for not
        // prime (sieved), it stores null for prime and some prime factor (doesn't matter which) that divides
        // the number for not prime. And has entries for evens. Knowing some prime factor that divides n, we
        // can divide by that factor, and then divide the result by its own factor, and so on, until we reach one.
        private readonly IReadOnlyList<int?> _sieveWithSomePrimeFactor;

        // TODO: A bool controlling proper inteface implementation seems bad, but providing isn't always needed...
        public SieveOfEratosthenesFactorizer(int limit, bool needsToProvide = false)
        {
            Limit = limit;

            var sieveWithSomePrimeFactor = new int?[Limit + 1];
            sieveWithSomePrimeFactor[0] = 0;
            sieveWithSomePrimeFactor[1] = 1;

            // Check for n up to sqrt(Limit), as any non-primes <= Limit with a factor > sqrt(Limit)
            // must also have a factor < sqrt(Limit) (otherwise they'd be > Limit), and so already sieved.
            for (int n = 2; n * n <= Limit; ++n)
            {
                // If we haven't sieved it yet then it's a prime, so sieve its multiples.
                if (!sieveWithSomePrimeFactor[n].HasValue)
                {
                    // Multiples of n less than n * n were already sieved from lower primes.
                    for (int nextPotentiallyUnsievedMultiple = n * n;
                        nextPotentiallyUnsievedMultiple <= Limit;
                        nextPotentiallyUnsievedMultiple += n)
                    {
                        sieveWithSomePrimeFactor[nextPotentiallyUnsievedMultiple] = n;
                    }
                }
            }
            _sieveWithSomePrimeFactor = Array.AsReadOnly(sieveWithSomePrimeFactor);

            if (needsToProvide)
            {
                var primes = 2 <= Limit
                    ? new List<int> { 2 }
                    : new List<int>();

                for (int n = 3; n <= Limit; n += 2)
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
