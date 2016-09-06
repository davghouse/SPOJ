using System;

namespace Spoj.Library.Primes
{
    public sealed class TrialDivisionDecider : PrimeDecider
    {
        private readonly SieveOfEratosthenesProvider _sieve;

        public TrialDivisionDecider(int limit)
            : base(limit)
        {
            _sieve = new SieveOfEratosthenesProvider((int)Math.Sqrt(Limit));
        }

        public override bool IsPrime(int n)
        {
            if (n <= _sieve.Limit)
                return _sieve.IsPrime(n);

            foreach (int prime in _sieve.Primes)
            {
                if (prime > Math.Sqrt(n))
                    break;

                if (n % prime == 0)
                    return false;
            }

            return true;
        }
    }
}