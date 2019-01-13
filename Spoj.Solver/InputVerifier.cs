using System;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/TRGRID/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();
            int h = int.Parse(line[0]);
            int l = int.Parse(line[1]);

            if (h < 1 || l < 1)
                throw new Exception();
        }
    }
}
