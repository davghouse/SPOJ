using System;
using System.Collections.Generic;
using System.Linq;

// 91 http://www.spoj.com/problems/TWOSQRS/ Two squares or not two squares
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
    public static string Solve(long n)
        => _factorizer.GetPrimeFactors(n)
        .Where(f => f % 4 == 3)
        .GroupBy(f => f)
        .All(fg => fg.Count() % 2 == 0) ? "Yes" : "No";
}

public class TrialDivisionFactorizer
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
                yield return primeFactor;
        }
        else
        {
            foreach (long prime in _sieveFactorizer.Primes)
            {
                // Check for factors up to sqrt(n), as non-primes with such a factor must've had
                // a factor seen earlier < sqrt(n) (otherwise multiplied together they'd be > n).
                // The fact that n is getting smaller doesn't matter. If this condition makes the
                // loop stop, the current value of n must be a prime greater than 'prime', since
                // n's only other option (multiple prime factors > 'prime') doesn't stop the loop.
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

public class SieveOfEratosthenesFactorizer
{
    // The only thing different about this sieve is that rather than storing true for prime
    // and false for not prime, it stores null for prime and some prime factor (doesn't matter which)
    // that divides the number for not prime. Knowing some prime factor that divides n, we can divide
    // by that factor, and then divide the result by its factor, and so on, until we reach one.
    private readonly IReadOnlyList<int?> _sieveWithSomePrimeFactor;

    public SieveOfEratosthenesFactorizer(int limit)
    {
        Limit = limit;

        int?[] sieveWithSomePrimeFactor = new int?[Limit + 1];
        sieveWithSomePrimeFactor[0] = 0;
        sieveWithSomePrimeFactor[1] = 1;

        // Check for n up to sqrt(Limit), as any non-primes <= Limit with a factor > sqrt(Limit)
        // must also have a factor < sqrt(Limit) (otherwise they'd be > Limit), and so already sieved.
        for (int n = 2; n*n <= Limit; ++n)
        {
            // If true then n hasn't been sieved yet, so it's prime; sieve its multiples.
            if (!sieveWithSomePrimeFactor[n].HasValue)
            {
                // Multiples of n less than n * n were already sieved from lower primes.
                int nextPotentiallyUnsievedMultiple = n * n;
                while (nextPotentiallyUnsievedMultiple <= Limit)
                {
                    sieveWithSomePrimeFactor[nextPotentiallyUnsievedMultiple] = n;
                    // Room for optimization here; could do += 2n except in the case where n is 2.
                    nextPotentiallyUnsievedMultiple += n;
                }
            }
        }

        _sieveWithSomePrimeFactor = Array.AsReadOnly(sieveWithSomePrimeFactor);

        var primes = new List<int>();
        for (int n = 2; n <= Limit; ++n)
        {
            if (IsPrime(n))
            {
                primes.Add(n);
            }
        }

        Primes = primes.AsReadOnly();
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
                TWOSQRS.Solve(n));
        }
    }
}
