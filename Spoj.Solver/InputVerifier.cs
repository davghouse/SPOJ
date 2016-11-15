using System;
using System.Linq;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/DQUERY/ (it's improperly formatted)
public static class InputVerifier
{
    private static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        if (n < 1 || n > 30000)
            throw new FormatException();

        int[] sourceArray = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);

        if (sourceArray.Length != n)
            throw new FormatException();

        if (sourceArray.Any(a => a < 1 || a > 1000000))
            throw new FormatException();

        int queryCount = int.Parse(Console.ReadLine());

        if (queryCount < 1 || queryCount > 200000)
            throw new FormatException();

        for (int q = 1; q <= queryCount; ++q)
        {
            int[] query = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            if (query[0] < 1 || query[0] > n)
                throw new FormatException();

            if (query[1] < 1 || query[1] > n)
                throw new FormatException();

            if (query[0] > query[1])
                throw new FormatException();
        }
    }
}
