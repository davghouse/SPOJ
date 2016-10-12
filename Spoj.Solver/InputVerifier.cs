using System;
using System.Linq;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/PIR/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        if (remainingTestCases < 0 || remainingTestCases > 100000)
            throw new FormatException();

        while (remainingTestCases-- > 0)
        {
            int[] wvuUVW = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            if (wvuUVW.Length != 6)
                throw new FormatException();

            if (wvuUVW.Any(n => n < 1 || n > 1000))
                throw new FormatException();
        }
    }
}
