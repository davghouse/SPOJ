using System;
using System.Runtime.CompilerServices;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/HORRIBLE/
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

            int arraySize = line[0];
            int commandCount = line[1];

            if (arraySize > 100000)
                throw new FormatException();

            if (commandCount > 100000)
                throw new FormatException();

            for (int c = 0; c < commandCount; ++c)
            {
                int[] commandLine = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

                if (commandLine.Length != 3 && commandLine.Length != 4)
                    throw new FormatException();

                if (commandLine[0] != 0 && commandLine[0] != 1)
                    throw new FormatException();

                if ((commandLine[0] == 0 && commandLine.Length != 4)
                    || (commandLine[0] == 1 && commandLine.Length != 3))
                    throw new FormatException();

                int p = commandLine[1];
                int q = commandLine[2];

                if (p < 1 || p > 100000)
                    throw new FormatException();

                if (q < 1 || q > 100000)
                    throw new FormatException();

                if (commandLine.Length == 4)
                {
                    int v = commandLine[3];

                    if (v < 1 || v > 10000000)
                        throw new FormatException();
                }
            }
        }
    }
}