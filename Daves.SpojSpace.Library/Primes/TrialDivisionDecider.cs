using System;

namespace Daves.SpojSpace.Library.Primes
{
    public sealed class TrialDivisionDecider : IPrimeDecider
    {
        private readonly SieveOfEratosthenesProvider _sieveProvider;

        public TrialDivisionDecider(int limit)
        {
            Limit = limit;

            _sieveProvider = new SieveOfEratosthenesProvider(Convert.ToInt32(Math.Sqrt(Limit)));
        }

        public int Limit { get; }

        public bool IsPrime(int n)
        {
            if (n <= _sieveProvider.Limit)
                return _sieveProvider.IsPrime(n);

            foreach (int prime in _sieveProvider.Primes)
            {
                // Check for factors up to sqrt(n), as non-primes with a factor larger than that must also have
                // a factor less than that, otherwise they'd multiply together to make a number greater than n.
                if (prime * prime > n)
                    break;

                if (n % prime == 0)
                    return false;
            }

            return true;
        }
    }
}
