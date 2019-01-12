using System;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/ROOTCIPH/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        if (remainingTestCases < 1 || remainingTestCases > 1000)
            throw new Exception();

        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();
            long a = long.Parse(line[0]);
            long b = long.Parse(line[1]);
            // This will throw an OverflowException, but that's okay once you know the solution.
            // I personally think that it's pretty dumb to do that, but knowing the solution one
            // could argue that it isn't an oversight. Unfortunately, depending on your input
            // parsing method you might not have to make this realization--scanf will just work
            // when attempting to parse c into a long, cin and long.Parse will fail.
            //long c = long.Parse(line[2]);
        }
    }
}
