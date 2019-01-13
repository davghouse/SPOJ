using System;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/BYTESE2/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        if (remainingTestCases < 1 || remainingTestCases > 100)
            throw new Exception();

        while (remainingTestCases-- > 0)
        {
            int entryExitCount = int.Parse(Console.ReadLine());
            if (entryExitCount < 1 || entryExitCount > 100)
                throw new Exception();

            for (int i = 0; i < entryExitCount; ++i)
            {
                string[] line = Console.ReadLine().Split();
                int entryTime = int.Parse(line[0]);
                int exitTime = int.Parse(line[1]);
                if (entryTime > exitTime)
                    throw new Exception();
            }
        }
    }
}
