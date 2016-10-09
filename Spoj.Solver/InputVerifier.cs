using System;
using System.Collections.Generic;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/MAJOR/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        if (remainingTestCases < 1 || remainingTestCases > 100)
            throw new FormatException();

        while (remainingTestCases-- > 0)
        {
            int transmissionCount = int.Parse(Console.ReadLine());
            string[] transmissions = Console.ReadLine().Split();

            if (transmissionCount != transmissions.Length)
                throw new FormatException();

            if (transmissionCount < 1 || transmissionCount > 1000000)
                throw new FormatException();
        }
    }
}
