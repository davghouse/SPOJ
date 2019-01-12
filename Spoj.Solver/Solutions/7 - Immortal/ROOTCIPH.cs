using System;
using System.Collections.Generic;
using System.Linq;

// https://www.spoj.com/problems/ROOTCIPH/ #factors #formula #math
// Finds the sum of squared roots of an integer polynomial of the form x³ + ax² + bx + c.
public static class ROOTCIPH
{
    private static readonly TrialDivisionFactorizer _factorizer;

    static ROOTCIPH()
    {
        _factorizer = new TrialDivisionFactorizer(int.MaxValue);
    }

    // Knowing the coefficients of the polynomial x³ + ax² + bx + c, we want to find the sum
    // of its squared roots, r₁² + r₂² + r₃². This didn't seem as easy as the comments/ranking
    // implied it would be. First, remind yourself about the factor theorem:
    // https://en.wikipedia.org/wiki/Factor_theorem
    // https://proofwiki.org/wiki/Polynomial_Factor_Theorem. So to begin:
    //   x³ + ax² + bx + c
    // = (x - r₁)(x - r₂)(x - r₃) (from the factor theorem)
    // = (x - r₁)(x² - xr₃ - xr₂ + r₂r₃)
    // = x³ - x²r₃ - x²r₂ + xr₂r₃ - x²r₁ + xr₃r₁ + xr₂r₁ - r₁r₂r₃
    // = x³ - (r₁ + r₂ + r₃)x² + (r₁r₂ + r₁r₃ + r₂r₃)x - r₁r₂r₃
    // Setting x = 0 shows that c = -r₁r₂r₃. Eliminating c from both sides and dividing by
    // x and then setting x = 0 shows that b = r₁r₂ + r₁r₃ + r₂r₃, and similarly we can show
    // that a = -(r₁ + r₂ + r₃). Now consider the following (I thought to try this because
    // I knew it would include the sum of squared roots):
    //   (r₁ + r₂ + r₃)²
    // = r₁² + r₁r₂ + r₁r₃ + r₂r₁ + r₂² + r₂r₃ + r₃r₁ + r₃r₂ + r₃²
    // = (r₁² + r₂² + r₃²) + 2(r₁r₂ + r₁r₃ + r₂r₃).
    // Therefore:
    //   r₁² + r₂² + r₃²
    // = (r₁ + r₂ + r₃)² - 2(r₁r₂ + r₁r₃ + r₂r₃)
    // = (-a)² - 2b (from above)
    // = a² - 2b.
    // The solution is independent of the c coefficient, so the 'trick' to getting AC (if you
    // don't have access to scanf-like handling), is to just avoid parsing c. It seems like
    // a lot of people aren't realizing that--rather, they're just seeking of scanf-like handling.
    public static long SolveCorrectly(long a, long b)
        => a * a - 2 * b;

    // So I read this https://en.wikipedia.org/wiki/Rational_root_theorem and thought that
    // it applied to this problem. Specifically, I thought that because the leading coefficient
    // is 1, every root of the polynomial must be a fact;or of the constant term c. That's wrong,
    // it's not every root, it's only every *rational* root. Some of the roots might not be
    // rational, and that's fine, the final answer can still be an integer since we square
    // the roots. This solution enumerates all the factors of c and tests to see if they're
    // roots, and then builds up the distance (sum of squared roots).
    // NOTE: This doesn't bother handling the large numbers present in the actual input.
    public static int SolveIncorrectly(int a, int b, int c)
    {
        var primeFactorCounts = _factorizer
            .GetPrimeFactors(Math.Abs(c))
            .GroupBy(f => f)
            .Select(fg => new KeyValuePair<int, int>(fg.Key, fg.Count()))
            .ToArray();

        var roots = new List<int>();

        foreach (int factor in GetPositiveFactors(primeFactorCounts)
            .SelectMany(f => new[] { f, -f }))
        {
            if (factor * factor * factor + a * factor * factor + b * factor + c == 0)
            {
                roots.Add(factor);

                if (roots.Count == 3)
                    break;
            }
        }

        // The sole root must have a multiplicity of 3.
        if (roots.Count == 1)
        {
            roots.Add(roots[0]);
            roots.Add(roots[0]);
        }
        // One of the roots must have a multiplicity 2.
        else if (roots.Count == 2)
        {
            if (-roots[0] * -roots[0] * -roots[1] == c)
            {
                roots.Add(roots[0]);
            }
            else
            {
                roots.Add(roots[1]);
            }
        }

        return roots.Sum(r => r * r);
    }

    // The prime factor counts is the prime factorization of c, like 2^3 * 3^4 * 5^1.
    // We can find all the factors by taking all the combinations of the prime factors.
    // So 2^0 * 3^0 * 5^0 is one, 2^0 * 3^0 * 5^1 is another, 4 possible powers for 2,
    // 5 for 3, 2 for 5, for a total of 4 * 5 * 2 = 40 factors.
    private static IEnumerable<int> GetPositiveFactors(
        KeyValuePair<int, int>[] primeFactorCounts, int recursiveStartIndex = 0)
    {
        if (recursiveStartIndex == primeFactorCounts.Length)
        {
            yield return 1;
            yield break;
        }

        int primeFactor = primeFactorCounts[recursiveStartIndex].Key;
        int factorCount = primeFactorCounts[recursiveStartIndex].Value;

        for (int power = 0; power <= factorCount; ++power)
        {
            int primeFactorToPower = MathHelper.IntPow(primeFactor, power);

            foreach (int factor in GetPositiveFactors(primeFactorCounts, recursiveStartIndex + 1))
                yield return primeFactorToPower * factor;
        }
    }
}

public sealed class TrialDivisionFactorizer
{
    private readonly SieveOfEratosthenesFactorizer _sieveFactorizer;

    public TrialDivisionFactorizer(int limit)
    {
        Limit = limit;

        _sieveFactorizer = new SieveOfEratosthenesFactorizer(Convert.ToInt32(Math.Sqrt(Limit)));
    }

    public long Limit { get; }

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

public static class MathHelper
{
    // https://en.wikipedia.org/wiki/Exponentiation_by_squaring
    // https://stackoverflow.com/questions/383587/how-do-you-do-integer-exponentiation-in-c
    public static int IntPow(int n, int pow)
    {
        int ret = 1;
        while (pow != 0)
        {
            if ((pow & 1) == 1)
            {
                ret *= n;
            }

            n *= n;
            pow >>= 1;
        }

        return ret;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();
            long a = long.Parse(line[0]);
            long b = long.Parse(line[1]);
            // Don't parse c, we don't need it. Parsing c will result in an OverflowException.

            Console.WriteLine(
                ROOTCIPH.SolveCorrectly(a, b));
        }
    }
}
