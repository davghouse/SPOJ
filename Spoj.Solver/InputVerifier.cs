using System;
using System.Linq;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/ABCPATH/
public static class InputVerifier
{
    private static void Main()
    {
        string[] line;
        while ((line = Console.ReadLine().Split())[0] != "0")
        {
            int height = int.Parse(line[0]);
            if (height < 1 || height > 50)
                throw new ArgumentOutOfRangeException();

            int width = int.Parse(line[1]);
            if (width < 1 || width > 50)
                throw new ArgumentOutOfRangeException();

            for (int r = 0; r < height; ++r)
            {
                string row = Console.ReadLine();
                if (row.Length != width)
                    throw new ArgumentException();
                if (!row.All(Char.IsUpper))
                    throw new ArgumentException();
            }
        }
    }
}
