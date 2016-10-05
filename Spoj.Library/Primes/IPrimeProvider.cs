using System.Collections.Generic;

namespace Spoj.Library.Primes
{
    public interface IPrimeProvider
    {
        // List of primes in ascending order.
        IReadOnlyList<int> Primes { get; }
    }
}
