using System;
using System.Collections;
using System.Collections.Generic;

// 2 http://www.spoj.com/problems/PRIME1/ Prime Generator
// Returns all the primes between two numbers m and n (inclusive), where 1 <= m <= n <= 1,000,000,000.
public static class PRIME1
{
    private const int _limit = 1000000000;
    private static readonly TrialDivisionDecider _decider;

    static PRIME1()
    {
        _decider = new TrialDivisionDecider(_limit);
    }

    public static IEnumerable<int> Solve(int m, int n)
    {
        for (int i = m; i <= n; ++i)
        {
            if (_decider.IsPrime(i))
            {
                yield return i;
            }
        }
    }
}

public abstract class PrimeDecider
{
    public PrimeDecider(int limit)
    {
        Limit = limit;
    }

    public int Limit { get; }

    public abstract bool IsPrime(int n);
}

public abstract class PrimeProvider : PrimeDecider
{
    public PrimeProvider(int limit)
        : base(limit)
    { }

    public abstract IReadOnlyList<int> Primes { get; }
}

public sealed class SieveOfEratosthenesDecider : PrimeDecider
{
    private readonly BitArray _sieve;

    public SieveOfEratosthenesDecider(int limit)
        : base(limit)
    {
        _sieve = new BitArray(Limit + 1, true);
        _sieve[0] = false;
        _sieve[1] = false;

        for (int n = 2; n <= (int)Math.Sqrt(Limit); ++n)
        {
            if (_sieve[n]) // Then n hasn't been sieved yet, so it's prime; sieve its multiples.
            {
                int nextPotentiallyUnsievedMultiple = n * n; // Multiples of n less than this were already sieved from lower primes.
                while (nextPotentiallyUnsievedMultiple <= Limit)
                {
                    _sieve[nextPotentiallyUnsievedMultiple] = false;
                    nextPotentiallyUnsievedMultiple += n; // Room for optimization here; could do += 2n except in the case where n is 2.
                }
            }
        }
    }

    public override bool IsPrime(int n)
        => _sieve[n];
}

public sealed class SieveOfEratosthenesProvider : PrimeProvider
{
    private readonly SieveOfEratosthenesDecider _decider;

    public SieveOfEratosthenesProvider(int limit)
        : base(limit)
    {
        _decider = new SieveOfEratosthenesDecider(Limit);

        var primes = new List<int>();
        for (int n = 2; n <= Limit; ++n)
        {
            if (_decider.IsPrime(n))
            {
                primes.Add(n);
            }
        }

        Primes = primes.AsReadOnly();
    }

    public override bool IsPrime(int n)
        => _decider.IsPrime(n);

    public override IReadOnlyList<int> Primes { get; }
}

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

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        while (remainingTestCases-- > 0)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            foreach (int prime in PRIME1.Solve(line[0], line[1]))
            {
                Console.WriteLine(prime);
            }
            Console.WriteLine();
        }
    }
}