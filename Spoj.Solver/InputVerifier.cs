using System;
using System.Linq;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/HISTOGRA/
public static class InputVerifier
{
    private static void Main()
    {
        string[] line;
        while((line = Console.ReadLine().Split())[0] != "0")
        {
            int rectangleCount = int.Parse(line[0]);

            if (rectangleCount != line.Length - 1)
                throw new FormatException();

            if (rectangleCount < 1 || rectangleCount > 100000)
                throw new FormatException();

            int[] rectangleHeights = new int[rectangleCount];
            for (int i = 0; i < rectangleCount; ++i)
            {
                rectangleHeights[i] = int.Parse(line[i + 1]);
            }

            if (rectangleHeights.Any(h => h < 0 || h > 1000000000))
                throw new FormatException();
        }
    }
}
