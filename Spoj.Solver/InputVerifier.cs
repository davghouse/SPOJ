using System;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/TWOSQRS/
public static class InputVerifier
{
    private const long _oneTrillion = 1000000000000;

    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        if (remainingTestCases < 1 || remainingTestCases > 100)
            throw new FormatException();

        while (remainingTestCases-- > 0)
        {
            long n = long.Parse(Console.ReadLine());

            if (n < 0 || n > _oneTrillion)
                throw new FormatException();
        }
    }
}
