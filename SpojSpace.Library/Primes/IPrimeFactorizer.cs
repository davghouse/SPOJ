using System.Collections.Generic;

namespace SpojSpace.Library.Primes
{
    public interface IPrimeFactorizer
    {
        IEnumerable<int> GetPrimeFactors(int n);
        IEnumerable<int> GetDistinctPrimeFactors(int n);
    }
}
