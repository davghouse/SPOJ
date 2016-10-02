using System;
using System.Collections.Generic;

namespace Spoj.Library.Primes
{
    public sealed class SieveOfEratosthenesFactorizer : IPrimeFactorizer, IPrimeDecider
    {
        // The only thing different about this sieve is that rather than storing true for prime
        // and false for not prime, it stores null for prime and some prime factor (doesn't matter which)
        // that divides the number for not prime. Knowing some prime factor that divides n, we can divide
        // by that factor, and then divide the result by its factor, and so on, until we reach one.
        private readonly IReadOnlyList<int?> _sieveWithSomePrimeFactor;

        public SieveOfEratosthenesFactorizer(int limit)
        {
            Limit = limit;

            var sieveWithSomePrimeFactor = new int?[Limit + 1];
            sieveWithSomePrimeFactor[0] = 0;
            sieveWithSomePrimeFactor[1] = 1;

            for (int n = 2; n <= (int)Math.Sqrt(Limit); ++n)
            {
                if (!sieveWithSomePrimeFactor[n].HasValue) // Then n hasn't been sieved yet, so it's prime; sieve its multiples.
                {
                    int nextPotentiallyUnsievedMultiple = n * n; // Multiples of n less than this were already sieved from lower primes.
                    while (nextPotentiallyUnsievedMultiple <= Limit)
                    {
                        sieveWithSomePrimeFactor[nextPotentiallyUnsievedMultiple] = n;
                        nextPotentiallyUnsievedMultiple += n; // Room for optimization here; could do += 2n except in the case where n is 2.
                    }
                }
            }

            _sieveWithSomePrimeFactor = Array.AsReadOnly(sieveWithSomePrimeFactor);
        }

        public int Limit { get; }

        public bool IsPrime(int n)
            => !_sieveWithSomePrimeFactor[n].HasValue;

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
