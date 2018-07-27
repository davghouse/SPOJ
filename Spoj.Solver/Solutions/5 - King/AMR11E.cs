using System;
using System.Text;

// https://www.spoj.com/problems/AMR11E/ #factors #math #primes #sieve
// Finds the first 1000 numbers with at least 3 distinct prime factors ('lucky' numbers).
public static class AMR11E
{
    // Calculated beforehand (via exploration) that 2664 is the 1000th lucky number.
    private static readonly int[] _sieve = new int[2665];
    private static readonly int[] _luckyNumbers = new int[1001];

    static AMR11E()
    {
        int n = 0;

        // Store the number of distinct prime factors in the sieve. A number is prime if when
        // we get to it, its distinct prime factor count is still zero. If it's a prime, mark
        // off its multiples in the sieve by incrementing their distinct prime factor count.
        // By the time we get to i, we've checked it against all the prime factors less than it,
        // which is all the prime factors it can possibly have. So we know if it has at least 3.
        for (int i = 2; i <= 2664; ++i)
        {
            if (_sieve[i] == 0) // i is prime.
            {
                for (int j = i; j <= 2664; j += i)
                {
                    ++_sieve[j]; // i is one of j's distinct prime factors.
                }
            }
            else if (_sieve[i] >= 3) // i has at least 3 distinct prime factors.
            {
                _luckyNumbers[++n] = i; // i is the nth lucky number.
            }
        }
    }

    public static int Solve(int n)
        => _luckyNumbers[n];
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int n = int.Parse(Console.ReadLine());

            output.Append(AMR11E.Solve(n));
            output.AppendLine();
        }

        Console.Write(output);
    }
}
