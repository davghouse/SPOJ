using System.Collections.Generic;

namespace Spoj.Library.Primes
{
    public sealed class SieveOfEratosthenesProvider : PrimeProvider
    {
        private readonly SieveOfEratosthenesDecider _decider;

        public SieveOfEratosthenesProvider(int limit)
            : base(limit)
        {
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

        public override bool IsPrime(int n)
            => _decider.IsPrime(n);

        public override IReadOnlyList<int> Primes { get; }
    }
}