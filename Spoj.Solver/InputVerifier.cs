using System;
using System.Linq;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/ACPC11B/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        if (remainingTestCases < 1 || remainingTestCases > 100)
            throw new FormatException();

        while (remainingTestCases-- > 0)
        {
            string[] firstLine = Console.ReadLine().Split();

            int firstAltitudeCount = int.Parse(firstLine[0]);
            if (firstAltitudeCount < 1 || firstAltitudeCount > 1000)
                throw new FormatException();

            if (firstAltitudeCount != firstLine.Length - 1)
                throw new FormatException();

            int[] firstAltitudes = new int[firstAltitudeCount];
            for (int a = 0; a < firstAltitudeCount; ++a)
            {
                firstAltitudes[a] = int.Parse(firstLine[a + 1]);
            }
            if (firstAltitudes.Any(a => a < 1 || a > 1000000))
                throw new FormatException();

            string[] secondLine = Console.ReadLine().Split();

            int secondAltitudeCount = int.Parse(secondLine[0]);
            if (secondAltitudeCount < 1 || secondAltitudeCount > 1000)
                throw new FormatException();

            if (secondAltitudeCount != secondLine.Length - 1)
                throw new FormatException();

            int[] secondAltitudes = new int[secondAltitudeCount];
            for (int a = 0; a < secondAltitudeCount; ++a)
            {
                secondAltitudes[a] = int.Parse(secondLine[a + 1]);
            }
            if (secondAltitudes.Any(a => a < 1 || a > 1000000))
                throw new FormatException();
        }
    }
}
