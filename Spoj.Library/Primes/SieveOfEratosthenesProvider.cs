using System.Collections.Generic;

namespace Spoj.Library.Primes
{
    public class SieveOfEratosthenesProvider : IPrimeProvider, IPrimeDecider
    {
        private readonly SieveOfEratosthenesDecider _decider;

        public SieveOfEratosthenesProvider(int limit)
        {
            Limit = limit;

            _decider = new SieveOfEratosthenesDecider(Limit);

            var primes = new List<int>();
            for (int n = 2; n <= Limit; ++n)
            {
                if (_decider.IsPrime(n))
                {
                    primes.Add(n);
                }
            }

            Primes = primes.AsReadOnly();
        }

        public int Limit { get; }

        public bool IsPrime(int n)
            => _decider.IsPrime(n);

        public IReadOnlyList<int> Primes { get; }
    }
}