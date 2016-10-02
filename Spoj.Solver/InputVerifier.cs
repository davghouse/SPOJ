using System;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/BUGLIFE/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        while (remainingTestCases-- > 0)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            if (line.Length != 2)
                throw new FormatException();

            int bugCount = line[0];
            int interactionCount = line[1];

            for (int i = 0; i < interactionCount; ++i)
            {
                line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

                if (line.Length != 2)
                    throw new FormatException();

                if (line[0] > bugCount || line[1] > bugCount)
                    throw new FormatException();
            }
        }
    }
}