using System;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/DOTAA/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        while (remainingTestCases-- > 0)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            if (line.Length != 3)
            {
                throw new FormatException();
            }

            for (int i = 0; i < line[0]; ++i)
            {
                int a = int.Parse(Console.ReadLine());
            }
            Console.ReadLine(); // Don't need this; there's actually no newline between test cases.
        }
    }
}
