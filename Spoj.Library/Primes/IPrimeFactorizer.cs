using System.Collections.Generic;

namespace Spoj.Library.Primes
{
    public interface IPrimeFactorizer
    {
        IEnumerable<int> GetPrimeFactors(int n);
        IEnumerable<int> GetDistinctPrimeFactors(int n);
    }
}
