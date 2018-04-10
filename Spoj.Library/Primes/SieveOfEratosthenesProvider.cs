using System.Collections.Generic;

namespace Spoj.Library.Primes
{
    public sealed class SieveOfEratosthenesProvider : IPrimeDecider, IPrimeProvider
    {
        private readonly SieveOfEratosthenesDecider _decider;

        public SieveOfEratosthenesProvider(int limit)
        {
            Limit = limit;

            _decider = new SieveOfEratosthenesDecider(Limit);

            var primes = 2 <= Limit
                ? new List<int> { 2 }
                : new List<int>();

            for (int n = 3; n <= Limit; n += 2)
            {
                if (IsOddPrime(n))
                {
                    primes.Add(n);
                }
            }
            Primes = primes;
        }

        public int Limit { get; }

        public bool IsPrime(int n)
            => _decider.IsPrime(n);

        public bool IsOddPrime(int n)
            => _decider.IsOddPrime(n);

        public IReadOnlyList<int> Primes { get; }
    }
}
