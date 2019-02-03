using System;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/WACHOVIA/ (it's bad)
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();
            int maxWeight = int.Parse(line[0]);
            int bagCount = int.Parse(line[1]);

            int[,] bags = new int[bagCount, 2];
            for (int b = 0; b < bagCount; ++b)
            {
                line = Console.ReadLine().Split();
                bags[b, 0] = int.Parse(line[0]); // bag weight
                bags[b, 1] = int.Parse(line[1]); // bag value
            }
        }
    }
}
