using System;
using System.Linq;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/MCOINS/
public static class InputVerifier
{
    private static void Main()
    {
        int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int[] coinCounts = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        int k = line[0];
        int l = line[1];
        int m = line[2];

        if (k <= 1 || k >= 10)
            throw new FormatException();

        if (l <= 1 || l >= 10)
            throw new FormatException();

        if (k == l)
            throw new FormatException();

        if (m <= 3 || m >= 50)
            throw new FormatException();

        if (coinCounts.Length != m)
            throw new FormatException();

        if (coinCounts.Any(c => c < 1 || c > 1000000))
            throw new FormatException();
    }
}
