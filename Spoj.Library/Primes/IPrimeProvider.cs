using System.Collections.Generic;

namespace Spoj.Library.Primes
{
    public interface IPrimeProvider
    {
        IReadOnlyList<int> Primes { get; }
    }
}
