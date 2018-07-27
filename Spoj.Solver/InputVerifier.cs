using System;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/MUL/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        if (remainingTestCases < 1 || remainingTestCases > 1000)
            throw new ArgumentOutOfRangeException();

        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();
            if (line.Length != 2)
                throw new ArgumentException();
            if (line[0].Length > 10000 || line[1].Length > 10000)
                throw new ArgumentException();
        }
    }
}
