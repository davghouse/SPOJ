using System;
using System.Linq;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/ABA12C/ (it has issues)
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();

            int friendCount = int.Parse(line[0]);
            if (friendCount <= 0 || friendCount > 100)
                throw new Exception();

            int appleCount = int.Parse(line[1]);
            if (appleCount <= 0 || appleCount > 100)
                throw new Exception();

            int[] applePackCosts = Array.ConvertAll(
                Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
                int.Parse);
            if (applePackCosts.Any(p => p != -1 && (p < 0 || p > 1000)))
                throw new Exception();
        }
    }
}
