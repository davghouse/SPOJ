using System;
using System.Collections.Generic;
using System.Linq;

// https://www.spoj.com/problems/COMDIV/ #division #factors #io #math #primes #sieve
// Finds the number of common divisors shared by two numbers.
// See COMDIV.cpp--this solution was submitted using C++ because C# I/O is too slow.
public static class COMDIV
{
    private const int _limit = 1000000;
    private static readonly TrialDivisionFactorizer _factorizer;

    static COMDIV()
    {
        _factorizer = new TrialDivisionFactorizer(_limit);
    }

    // If it's a common divisor, it must divide the GCD. Therefore, if we find the number of divisors
    // of the GCD, we find the number of common divisors. To find the number of divisors of a number,
    // we need to find its prime factorization. Each factor can be chosen a certain number of times,
    // from 0 up to its power in the factorization, independently of all other factors.
    // n = p1^e1 * p2^e2 * ... * pk^ek => (e1 + 1) * (e2 + 1) * ... * (ek + 1) different combinations
    // of prime factors. This corresponds to the number of divisors, since each different combination
    // has a different prime factorization and is therefore a different number. The case where no
    // factors are chosen corresponds to the divisor 1, which divides everything.
    public static int Solve(int a, int b)
        => _factorizer
        .GetPrimeFactors(MathHelper.GreatestCommonDivisor(a, b))
        .GroupBy(factor => factor)
        .Select(factorGroup => factorGroup.Count() + 1)
        .Aggregate(1, (current, next) => current * next);
}

public static class MathHelper
{
    // This is a good article (first section): http://www.cut-the-knot.org/blue/Euclid.shtml.
    // One point to note, for a = bt + r, the gcd(a, b) divides a so it divides bt + r.
    // And it divides b, so it divides bt, which means for bt + r to be divisible by it,
    // r also needs to be divisible by it. So it divides both b and r. And the article
    // notes the importance of showing not only does it divide b and r, it's also their gcd.
    public static int GreatestCommonDivisor(int a, int b)
    {
        int temp;
        while (b != 0)
        {
            temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}

public sealed class SieveOfEratosthenesFactorizer
{
    // This sieve is slightly different, rather than storing false for prime (unsieved) and true for not
    // prime (sieved), it stores null for prime and some prime factor (doesn't matter which) that divides
    // the number for not prime. And has entries for evens. Knowing some prime factor that divides n, we
    // can divide by that factor, and then divide the result by its own factor, and so on, until we reach one.
    private readonly IReadOnlyList<int?> _sieveWithSomePrimeFactor;

    // TODO: A bool controlling proper inteface implementation seems bad, but providing isn't always needed...
    public SieveOfEratosthenesFactorizer(int limit, bool needsToProvide = false)
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
        _sieveWithSomePrimeFactor = Array.AsReadOnly(sieveWithSomePrimeFactor);

        if (needsToProvide)
        {
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
            Primes = primes.AsReadOnly();
        }
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

public sealed class TrialDivisionFactorizer
{
    private readonly SieveOfEratosthenesFactorizer _sieveFactorizer;

    public TrialDivisionFactorizer(int limit)
    {
        Limit = limit;

        _sieveFactorizer = new SieveOfEratosthenesFactorizer(
            limit: Convert.ToInt32(Math.Sqrt(Limit)),
            needsToProvide: true);
    }

    public int Limit { get; }

    public IEnumerable<int> GetPrimeFactors(int n)
    {
        if (n <= _sieveFactorizer.Limit)
        {
            foreach (int primeFactor in _sieveFactorizer.GetPrimeFactors(n))
            {
                yield return primeFactor;
            }
        }
        else
        {
            foreach (int prime in _sieveFactorizer.Primes)
            {
                // Check for factors up to sqrt(n), as non-primes with a factor larger than that must also have a factor
                // less than that, otherwise they'd multiply together to make a number greater than n. The fact that n
                // is getting smaller doesn't matter. If this condition stops the loop, what remains of n is a single
                // prime factor. All primes less than 'prime' were already divided out, so for n to have multiple prime
                // factors they'd have to all be greater than 'prime', but in that case the loop wouldn't stop here.
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

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            Console.WriteLine(
                COMDIV.Solve(line[0], line[1]));
        }
    }
}
