using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// 4141 http://www.spoj.com/problems/ETF/ Euler Totient Function
// Calculates the value of the totient function for the given n, 1 <= n <= 10^6.
public static class ETF
{
    private const int _limit = 1000000;
    private static readonly SieveOfEratosthenesFactorizer _factorizer;

    static ETF()
    {
        _factorizer = new SieveOfEratosthenesFactorizer(_limit);
    }

    // We know a sieve is pretty fast, especially only up to a million. And it can be modified easily
    // to allow retrieving the prime factors of a given number. So combine that with Euler's product formula
    // (see https://en.wikipedia.org/wiki/Euler%27s_totient_function#Euler.27s_product_formula). That's a lot
    // better than my first idea for using a sieve, which was getting the prime factorization of every
    // number below n and comparing it to n's prime factorization (and I only got it by browsing Wikipedia).
    // I feel like I might've gotten lucky in thinking of a sieve first and then finding that formula.
    public static int Solve(int n)
    {
        int[] distinctPrimeFactors = _factorizer.GetDistinctPrimeFactors(n).ToArray();

        double totient = n;
        foreach (int primeFactor in distinctPrimeFactors)
        {
            totient *= (1 - 1 / (double)primeFactor);
        }

        return (int)Math.Round(totient);
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

        for (int n = 2; n <= (int)Math.Sqrt(Limit); ++n)
        {
            if (!sieveWithSomePrimeFactor[n].HasValue) // Then n hasn't been sieved yet, so it's prime; sieve its multiples.
            {
                int nextPotentiallyUnsievedMultiple = n * n; // Multiples of n less than this were already sieved from lower primes.
                while (nextPotentiallyUnsievedMultiple <= Limit)
                {
                    sieveWithSomePrimeFactor[nextPotentiallyUnsievedMultiple] = n;
                    nextPotentiallyUnsievedMultiple += n; // Room for optimization here; could do += 2n except in the case where n is 2.
                }
            }
        }

        _sieveWithSomePrimeFactor = Array.AsReadOnly(sieveWithSomePrimeFactor);
    }

    public int Limit { get; }

    public bool IsPrime(int n)
        => !_sieveWithSomePrimeFactor[n].HasValue;

    public IEnumerable<int> GetPrimeFactors(int n)
    {
        while (n > 1)
        {
            int somePrimeFactor = _sieveWithSomePrimeFactor[n] ?? n;
            yield return somePrimeFactor;

            n /= somePrimeFactor;
        }
    }

    public IEnumerable<int> GetDistinctPrimeFactors(int n)
    {
        while (n > 1)
        {
            int somePrimeFactor = _sieveWithSomePrimeFactor[n] ?? n;
            yield return somePrimeFactor;

            while (n % somePrimeFactor == 0)
            {
                n /= somePrimeFactor;
            }
        }
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
            int n = int.Parse(Console.ReadLine());
            output.AppendLine(ETF.Solve(n).ToString());
        }

        Console.Write(output);
    }
}
