using System;
using System.Linq;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/BYECAKES/ (it's improperly formatted)
public static class InputVerifier
{
    private static void Main()
    {
        int[] line;
        while ((line = Array.ConvertAll(
            Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
            int.Parse))[0] != -1)
        {
            if (line.Length != 8)
                throw new FormatException();

            for (int i = 0; i < 4; i++)
            {
                if (line[i] < 0 || line[i] > 1000)
                    throw new FormatException();
            }

            for (int i = 4; i < line.Length; i++)
            {
                if (line[i] < 1 || line[i] > 1000)
                    throw new FormatException();
            }
        }
    }
}
