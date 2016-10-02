using System;
using System.Numerics;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/GCD2/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();

            if (line.Length != 2)
                throw new FormatException();

            int a = int.Parse(line[0]);
            BigInteger b = BigInteger.Parse(line[1]);

            if (a < 0 || a > b)
                throw new FormatException();
        }
    }
}