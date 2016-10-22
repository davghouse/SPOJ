using System;
using System.Linq;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/DISUBSTR/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        if (remainingTestCases < 1 || remainingTestCases > 20)
            throw new FormatException();

        while (remainingTestCases-- > 0)
        {
            string s = Console.ReadLine();

            if (s.Length < 1 || s.Length > 1000)
                throw new FormatException();
        }
    }
}
