using System;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/FIBOSUM/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        while (remainingTestCases-- > 0)
        {
            int[] range = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int rangeStart = range[0];
            int rangeEnd = range[1];

            if (rangeStart > rangeEnd)
                throw new FormatException();

            if (rangeEnd > 100000)
                throw new FormatException();
        }
    }
}
