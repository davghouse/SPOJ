using System;
using System.Linq;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/PHONELST/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        if (remainingTestCases < 1 || remainingTestCases > 40)
            throw new FormatException();

        while(remainingTestCases-- > 0)
        {
            int phoneNumberCount = int.Parse(Console.ReadLine());

            if (phoneNumberCount < 1 || phoneNumberCount > 10000)
                throw new FormatException();

            for (int p = 0; p < phoneNumberCount; ++p)
            {
                string phoneNumber = Console.ReadLine();

                if (phoneNumber.Any(d => d < '0' || d > '9'))
                    throw new FormatException();

                if (phoneNumber.Length < 1 || phoneNumber.Length > 10)
                    throw new FormatException();
            }
        }
    }
}
