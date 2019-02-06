using System;
using System.Collections.Generic;

// https://www.spoj.com/problems/APS/ #factors #primes #sieve
// Calculates a sequence defined in terms of a number's smallest prime factor.
public static class APS
{
    private const int _limit = 10000000;
    private static readonly SieveOfEratosthenesFactorizer _factorizer;
    private static readonly long[] _sequence;

    static APS()
    {
        _factorizer = new SieveOfEratosthenesFactorizer(_limit);
        _sequence = new long[_limit + 1];
        for (int n = 2; n <= _limit; ++n)
        {
            _sequence[n] = _sequence[n - 1] + _factorizer.GetFirstPrimeFactor(n);
        }
    }

    public static long Solve(int n)
        => _sequence[n];
}

public sealed class SieveOfEratosthenesFactorizer
{
    // This sieve is slightly different, rather than storing false for prime (unsieved) and true for not
    // prime (sieved), it stores null for prime and the first prime factor that divides the number for not
    // prime. And has entries for evens.
    private readonly IReadOnlyList<int?> _sieveWithFirstPrimeFactor;

    public SieveOfEratosthenesFactorizer(int limit)
    {
        Limit = limit;

        int?[] sieveWithFirstPrimeFactor = new int?[Limit + 1];
        sieveWithFirstPrimeFactor[0] = 0;
        sieveWithFirstPrimeFactor[1] = 1;

        // Check for n up to sqrt(Limit), as any non-primes <= Limit with a factor > sqrt(Limit)
        // must also have a factor < sqrt(Limit) (otherwise they'd be > Limit), and so already sieved.
        for (int n = 2; n * n <= Limit; ++n)
        {
            // If we haven't sieved it yet then it's a prime, so sieve its multiples.
            if (!sieveWithFirstPrimeFactor[n].HasValue)
            {
                // Multiples of n less than n * n were already sieved from lower primes.
                for (int nextPotentiallyUnsievedMultiple = n * n;
                    nextPotentiallyUnsievedMultiple <= Limit;
                    nextPotentiallyUnsievedMultiple += n)
                {
                    if (!sieveWithFirstPrimeFactor[nextPotentiallyUnsievedMultiple].HasValue)
                    {
                        sieveWithFirstPrimeFactor[nextPotentiallyUnsievedMultiple] = n;
                    }
                }
            }
        }
        _sieveWithFirstPrimeFactor = Array.AsReadOnly(sieveWithFirstPrimeFactor);
    }

    public int Limit { get; }

    public bool IsPrime(int n)
        => !_sieveWithFirstPrimeFactor[n].HasValue;

    public int GetFirstPrimeFactor(int n)
        => _sieveWithFirstPrimeFactor[n] ?? n;
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            Console.WriteLine(
                APS.Solve(int.Parse(Console.ReadLine())));
        }
    }
}
