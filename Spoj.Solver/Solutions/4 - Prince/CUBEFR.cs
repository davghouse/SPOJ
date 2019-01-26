using System;
using System.Text;

// https://www.spoj.com/problems/CUBEFR/ #sieve
// Determines if a number is cube free, and its index amongst the others if so.
public static class CUBEFR
{
    private const int _limit = 1000000;
    private const int _cubedRootOfLimit = 100;
    private static readonly int[] _sieve = new int[_limit + 1];

    static CUBEFR()
    {
        int cubeFreeIndex = 1;
        _sieve[1] = cubeFreeIndex;

        for (int n = 2; n <= _limit; ++n)
        {
            // If sieve at n is still 0, then n is cube free. Mark its index amongst the
            // other cube free numbers, and then sieve out all multiples of its cube.
            if (_sieve[n] == 0)
            {
                _sieve[n] = ++cubeFreeIndex;

                // If n is above 100, the multiples of its cube are all larger than the limit.
                if (n <= _cubedRootOfLimit)
                {
                    int nCubed = n * n * n;

                    for (int m = nCubed; m <= _limit; m += nCubed)
                    {
                        _sieve[m] = -1;
                    }
                }
            }
        }
    }

    public static int Solve(int n)
        => _sieve[n];
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int testCount = int.Parse(Console.ReadLine());
        for (int t = 1; t <= testCount; ++t)
        {
            int n = int.Parse(Console.ReadLine());
            int result = CUBEFR.Solve(n);

            if (result != -1)
            {
                output.Append($"Case {t}: {result}");
            }
            else
            {
                output.Append($"Case {t}: Not Cube Free");
            }

            output.AppendLine();
        }

        Console.Write(output);
    }
}
