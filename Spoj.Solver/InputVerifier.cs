using System;
using System.Linq;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/MISERMAN/ (it's improperly formatted)
public static class InputVerifier
{
    private static void Main()
    {
        string[] line = Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries);
        int cityCount = int.Parse(line[0]);
        int busCount = int.Parse(line[1]);

        if (cityCount < 1 || cityCount > 100)
            throw new FormatException();

        if (busCount < 1 || busCount > 100)
            throw new FormatException();

        for (int c = 0; c < cityCount; ++c)
        {
            line = Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries);

            for (int b = 0; b < busCount; ++b)
            {
                int busFare = int.Parse(line[b]);

                if (busFare < 1 || busFare > 100)
                    throw new FormatException();
            }
        }
    }
}
