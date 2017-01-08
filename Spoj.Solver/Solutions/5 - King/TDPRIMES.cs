using System;
using System.Text;

// 6471 http://www.spoj.com/problems/TDPRIMES/ Printing some primes
// Prints some of the primes up to 100 million.
public static class TDPRIMES
{
    private const int _100Million = 100000000;

    public static string Solve()
    {
        var decider = new SieveOfEratosthenesDecider(_100Million);
        var output = new StringBuilder();

        output.Append(2);
        output.AppendLine();
        int count = 1;

        for (int n = 3; n <= _100Million; n += 2)
        {
            if (decider.IsOddPrime(n) && ++count == 101)
            {
                output.Append(n);
                output.AppendLine();
                count = 1;
            }
        }

        return output.ToString();
    }
}

public class SieveOfEratosthenesDecider
{
    private readonly bool[] _sieve;

    public SieveOfEratosthenesDecider(int limit)
    {
        _sieve = new bool[(limit + 1) >> 1];
        _sieve[0] = true; // 1 (which maps to index [1 / 2] == [0]) is not a prime.

        // Check for n up to sqrt(Limit), as any non-primes <= Limit with a factor > sqrt(Limit)
        // must also have a factor < sqrt(Limit) (otherwise they'd be > Limit), and so already sieved.
        for (int n = 3; n * n <= limit; n += 2)
        {
            // It's a prime if we haven't sieve it yet, so sieve its multiples.
            if (IsOddPrime(n))
            {
                // Multiples of n less than n * n were already sieved from lower primes.
                for (int nextPotentiallyUnsievedMultiple = n * n;
                    nextPotentiallyUnsievedMultiple <= limit;
                    nextPotentiallyUnsievedMultiple += n << 1)
                {
                    _sieve[nextPotentiallyUnsievedMultiple >> 1] = true;
                }
            }
        }
    }

    public bool IsPrime(int n)
        => (n & 1) == 0 ? n == 2 : IsOddPrime(n);

    public bool IsOddPrime(int n)
        => !_sieve[n >> 1];
}

public static class Program
{
    private static void Main()
    {
        Console.Write(
            TDPRIMES.Solve());
    }
}
