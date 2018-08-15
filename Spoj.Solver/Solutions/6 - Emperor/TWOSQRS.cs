using System;
using System.Collections.Generic;
using System.Linq;

// https://www.spoj.com/problems/TWOSQRS/ #math #mod-math #primes #sieve
// Determines if an integer can be expressed as the sum of two squared integers.
public static class TWOSQRS
{
    private const long _oneTrillion = 1000000000000;
    private static readonly TrialDivisionFactorizer _factorizer;

    static TWOSQRS()
    {
        _factorizer = new TrialDivisionFactorizer(_oneTrillion);
    }

    // See https://en.wikipedia.org/wiki/Fermat%27s_theorem_on_sums_of_two_squares,
    // specifically "if all the prime factors of n congruent to 3 modulo 4 occur to
    // an even exponent, then n is expressible as a sum of two squares." But yeah
    // I had to google that or brute force in O(sqrt(n)).
    public static bool Solve(long n)
        => _factorizer.GetPrimeFactors(n)
        .Where(f => f % 4 == 3)
        .GroupBy(f => f)
        .All(fg => fg.Count() % 2 == 0);
}

public sealed class TrialDivisionFactorizer
{
    private readonly SieveOfEratosthenesFactorizer _sieveFactorizer;

    public TrialDivisionFactorizer(long limit)
    {
        Limit = limit;

        _sieveFactorizer = new SieveOfEratosthenesFactorizer(Convert.ToInt32(Math.Sqrt(Limit)));
    }

    public long Limit { get; }

    public IEnumerable<long> GetPrimeFactors(long n)
    {
        if (n <= _sieveFactorizer.Limit)
        {
            foreach (long primeFactor in _sieveFactorizer.GetPrimeFactors((int)n))
            {
                yield return primeFactor;
            }
        }
        else
        {
            foreach (long prime in _sieveFactorizer.Primes)
            {
                // Check for factors up to sqrt(n), as non-primes with a factor larger than that must also have a factor
                // less than that, otherwise they'd multiply together to make a number greater than n. The fact that n
                // is getting smaller doesn't matter. If this condition stops the loop, what remains of n is a single
                // prime factor. All primes less than 'prime' were already divided out, so for n to have multiple prime
                // factors they'd have to all be >= 'prime', but in that case the loop wouldn't stop here.
                if (prime * prime > n)
                    break;

                while (n % prime == 0)
                {
                    yield return prime;
                    n /= prime;
                }

                // All the prime factors have been extracted, so stop looking.
                if (n == 1)
                    yield break;
            }

            // The loop above was broken out of (before n == 1), so the original n, or what remains of it, is prime.
            yield return n;
        }
    }
}

public sealed class SieveOfEratosthenesFactorizer
{
    // This sieve is slightly different, rather than storing false for prime (unsieved) and true for not
    // prime (sieved), it stores null for prime and some prime factor (doesn't matter which) that divides
    // the number for not prime. And has entries for evens. Knowing some prime factor that divides n, we
    // can enumerate all its prime factors by dividing it by that factor, the quotient by its factor, etc.
    private readonly IReadOnlyList<int?> _sieveWithSomePrimeFactor;

    public SieveOfEratosthenesFactorizer(int limit)
    {
        Limit = limit;

        int?[] sieveWithSomePrimeFactor = new int?[Limit + 1];
        sieveWithSomePrimeFactor[0] = 0;
        sieveWithSomePrimeFactor[1] = 1;

        // Check for n up to sqrt(Limit), as any non-primes <= Limit with a factor > sqrt(Limit)
        // must also have a factor < sqrt(Limit) (otherwise they'd be > Limit), and so already sieved.
        for (int n = 2; n * n <= Limit; ++n)
        {
            // If we haven't sieved it yet then it's a prime, so sieve its multiples.
            if (!sieveWithSomePrimeFactor[n].HasValue)
            {
                // Multiples of n less than n * n were already sieved from lower primes.
                for (int nextPotentiallyUnsievedMultiple = n * n;
                    nextPotentiallyUnsievedMultiple <= Limit;
                    nextPotentiallyUnsievedMultiple += n)
                {
                    sieveWithSomePrimeFactor[nextPotentiallyUnsievedMultiple] = n;
                }
            }
        }
        _sieveWithSomePrimeFactor = sieveWithSomePrimeFactor;

        var primes = 2 <= Limit
            ? new List<int> { 2 }
            : new List<int>();

        for (int n = 3; n <= Limit; n += 2)
        {
            if (IsPrime(n))
            {
                primes.Add(n);
            }
        }
        Primes = primes;
    }

    public int Limit { get; }

    public bool IsPrime(int n)
        => !_sieveWithSomePrimeFactor[n].HasValue;

    public IReadOnlyList<int> Primes { get; }

    public IEnumerable<int> GetPrimeFactors(int n)
    {
        while (n > 1)
        {
            int somePrimeFactor = _sieveWithSomePrimeFactor[n] ?? n;
            yield return somePrimeFactor;

            n /= somePrimeFactor;
        }
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            long n = long.Parse(Console.ReadLine());

            Console.WriteLine(
                TWOSQRS.Solve(n) ? "Yes" : "No");
        }
    }
}
