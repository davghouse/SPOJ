using System;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/PERMUT1/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();

            int n = int.Parse(line[0]);
            if (n < 1 || n > 12)
                throw new Exception();

            int k = int.Parse(line[1]);
            if (k < 0 || k > 98)
                throw new Exception();
        }
    }
}
