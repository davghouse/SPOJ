using System.Collections.Generic;

namespace Spoj.Library.Primes
{
    public abstract class PrimeProvider : PrimeDecider
    {
        public PrimeProvider(int limit)
            : base(limit)
        { }

        public abstract IReadOnlyList<int> Primes { get; }
    }
}