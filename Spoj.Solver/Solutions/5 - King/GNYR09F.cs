using System;

// https://www.spoj.com/problems/GNYR09F/ #binary #dynamic-programming-3d
// Finds the number of n-bit strings with k adjacent bits.
public static class GNYR09F
{
    // The first index is n, number of bits.
    // The second index is k, number of adjacent bits.
    // The third index is 0 or 1, the bit the string ends with.
    private static int[,,] _bitStringCounts = new int[101, 100, 2];

    static GNYR09F()
    {
        // For n = 0, there are no bits, so no strings for any k.

        // For n = 1, initialize the row. There are only two strings for n = 1, "0"
        // and "1". Both have 0 for their number of adjacent bits.
        _bitStringCounts[1, 0, 0] = 1; // "0"
        _bitStringCounts[1, 0, 1] = 1; // "1"

        // For n = 1, initialize the two columns for k = 0.
        for (int n = 2; n <= 100; ++n)
        {
            // For n, how many k = 0 strings end in 0? Imagine adding a 0 onto an n - 1
            // length string. The n - 1 length string must have k = 0 as well, as adding
            // a bit to the end can only increase the number of adjacencies. Since we're
            // adding 0, it doesn't matter if the n - 1 length string ends in 0 or ends
            // in 1, neither leads to a new adjacency.
            _bitStringCounts[n, 0, 0]
                = _bitStringCounts[n - 1, 0, 0] + _bitStringCounts[n - 1, 0, 1];

            // For n, how many k = 0 strings end in 1? Similar to above, we can add 1
            // to n - 1 length strings with k = 0 ending in 0, as 01 doesn't form an
            // adjacency. But can't add 1 for the strings ending in 1.
            _bitStringCounts[n, 0, 1] = _bitStringCounts[n - 1, 0, 0];
        }

        for (int n = 2; n <= 100; ++n)
        {
            // Note that the max number of adjacencies is n - 1, so we only go up to 99.
            for (int k = 1; k <= 99; ++k)
            {
                // For length n strings ending in 0, the n - 1 bits before the new bit
                // being added must have k adjacencies already, since adding a 0 to
                // the end can't add any new ones.
                _bitStringCounts[n, k, 0]
                    = _bitStringCounts[n - 1, k, 0] + _bitStringCounts[n - 1, k, 1];

                // For length n strings ending in 1, the n - 1 bits before the new one
                // might already have k adjacencies. If so, they must end in 0, otherwise
                // we'd add a new adjacency when we add the 1. If they end in 1, they'll
                // need to have k - 1 adjacencies so we end up with k total.
                _bitStringCounts[n, k, 1]
                    = _bitStringCounts[n - 1, k, 0] + _bitStringCounts[n - 1, k - 1, 1];
            }
        }
    }

    public static int Solve(int n, int k)
        => k >= n ? 0 // n can have at most k - 1 adjacencies.
        : _bitStringCounts[n, k, 0] + _bitStringCounts[n, k, 1];
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split(); ;
            int testCase = int.Parse(line[0]);
            int n = int.Parse(line[1]);
            int k = int.Parse(line[2]);

            Console.WriteLine(
                $"{testCase} {GNYR09F.Solve(n, k)}");
        }
    }
}
