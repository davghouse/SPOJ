using System.Collections.Generic;

namespace Daves.SpojSpace.Library.Primes
{
    public interface IPrimeProvider
    {
        // List of primes in ascending order.
        IReadOnlyList<int> Primes { get; }
    }
}
