using System.Collections.Generic;

namespace SpojSpace.Library.Primes
{
    public interface IPrimeProvider
    {
        // List of primes in ascending order.
        IReadOnlyList<int> Primes { get; }
    }
}
