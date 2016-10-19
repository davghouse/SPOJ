using System;
using System.Linq;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/CPRMT/
public static class InputVerifier
{
    private static void Main()
    {
        string a, b;

        while ((a = Console.ReadLine()) != null)
        {
            b = Console.ReadLine();

            if (a.Any(c => c < 'a' || c > 'z'))
                throw new FormatException();

            if (b.Any(c => c < 'a' || c > 'z'))
                throw new FormatException();
        }
    }
}
