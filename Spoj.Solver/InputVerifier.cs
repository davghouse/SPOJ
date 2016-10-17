using System;
using System.Linq;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/ACPC10D/
public static class InputVerifier
{
    private static void Main()
    {
        int rowCount;
        while((rowCount = int.Parse(Console.ReadLine())) != 0)
        {
            if (rowCount < 2 || rowCount > 100000)
                throw new FormatException();

            for (int r = 0; r < rowCount; ++r)
            {
                int[] vertexCosts = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

                if (vertexCosts.Length != 3)
                    throw new FormatException();

                if (vertexCosts.Any(v => v >= 1000))
                    throw new FormatException();
            }
        }
    }
}
