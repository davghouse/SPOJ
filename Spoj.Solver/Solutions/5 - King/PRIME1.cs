using System;
using System.Collections.Generic;
using System.Text;

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

    // This is still pretty slow, looks like we might want a segmented sieve?
    public static IEnumerable<int> Solve(int m, int n)
    {
        // Check for the only even prime manually, and move m and n to odds.
        if (m <= 2) yield return 2;
        if (m % 2 == 0) ++m;
        if (n % 2 == 0) --n;

        for (int i = m; i <= n; i += 2)
        {
            if (_decider.IsPrime(i))
            {
                yield return i;
            }
        }
    }
}

public interface IPrimeDecider
{
    bool IsPrime(int n);
}

public interface IPrimeProvider
{
    // List of primes in ascending order.
    IReadOnlyList<int> Primes { get; }
}

public class SieveOfEratosthenesDecider : IPrimeDecider
{
    private readonly IReadOnlyList<bool> _sieve;

    public SieveOfEratosthenesDecider(int limit)
    {
        Limit = limit;

        bool[] sieve = new bool[(Limit + 1) >> 1];
        sieve[0] = true; // 1 (which maps to index [1 / 2] == [0]) is not a prime, so sieve it out.

        // Check for n up to sqrt(Limit), as any non-primes <= Limit with a factor > sqrt(Limit)
        // must also have a factor < sqrt(Limit) (otherwise they'd be > Limit), and so already sieved.
        for (int n = 3; n * n <= Limit; n += 2)
        {
            // If we haven't sieved it yet then it's a prime, so sieve its multiples.
            if (!sieve[n >> 1])
            {
                // Multiples of n less than n * n were already sieved from lower primes. Add twice
                // n for each iteration, as otherwise it's odd + odd = even.
                for (int nextPotentiallyUnsievedMultiple = n * n;
                    nextPotentiallyUnsievedMultiple <= Limit;
                    nextPotentiallyUnsievedMultiple += (n << 1))
                {
                    sieve[nextPotentiallyUnsievedMultiple >> 1] = true;
                }
            }
        }
        _sieve = Array.AsReadOnly(sieve);
    }

    public int Limit { get; }

    public bool IsPrime(int n)
        => (n & 1) == 0 ? n == 2 : IsOddPrime(n);

    public bool IsOddPrime(int n)
        => !_sieve[n >> 1];
}

public class SieveOfEratosthenesProvider : IPrimeDecider, IPrimeProvider
{
    private readonly SieveOfEratosthenesDecider _decider;

    public SieveOfEratosthenesProvider(int limit)
    {
        Limit = limit;

        _decider = new SieveOfEratosthenesDecider(Limit);

        var primes = 2 <= Limit
            ? new List<int>() { 2 }
            : new List<int>();

        for (int n = 3; n <= Limit; n += 2)
        {
            if (IsOddPrime(n))
            {
                primes.Add(n);
            }
        }
        Primes = primes.AsReadOnly();
    }

    public int Limit { get; }

    public bool IsPrime(int n)
        => _decider.IsPrime(n);

    public bool IsOddPrime(int n)
        => _decider.IsOddPrime(n);

    public IReadOnlyList<int> Primes { get; }
}

public class TrialDivisionDecider : IPrimeDecider
{
    private readonly SieveOfEratosthenesProvider _sieve;

    public TrialDivisionDecider(int limit)
    {
        _sieve = new SieveOfEratosthenesProvider(Convert.ToInt32(Math.Sqrt(limit)));
    }

    public bool IsPrime(int n)
    {
        if (n <= _sieve.Limit)
            return _sieve.IsPrime(n);

        foreach (int prime in _sieve.Primes)
        {
            // Check for factors up to sqrt(n), as non-primes with such a factor must've had
            // a factor seen earlier < sqrt(n) (otherwise multiplied together they'd be > n).
            if (prime * prime > n)
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
        var output = new StringBuilder();

        while (remainingTestCases-- > 0)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            foreach (int prime in PRIME1.Solve(line[0], line[1]))
            {
                output.Append(prime);
                output.AppendLine();
            }
            output.AppendLine();
        }

        Console.Write(output);
    }
}
