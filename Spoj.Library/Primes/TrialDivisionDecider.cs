using System;

namespace Spoj.Library.Primes
{
    public class TrialDivisionDecider : IPrimeDecider
    {
        private readonly SieveOfEratosthenesProvider _sieveProvider;

        public TrialDivisionDecider(int limit)
        {
            Limit = limit;

            _sieveProvider = new SieveOfEratosthenesProvider((int)Math.Sqrt(Limit));
        }

        public int Limit { get; }

        public bool IsPrime(int n)
        {
            if (n <= _sieveProvider.Limit)
                return _sieveProvider.IsPrime(n);

            foreach (int prime in _sieveProvider.Primes)
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