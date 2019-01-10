using System;
using System.Linq;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/ABSP1/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int numberCount = int.Parse(Console.ReadLine());
            if (numberCount > 10000)
                throw new Exception();

            int[] numbers = Array.ConvertAll(
                Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
                int.Parse);
            // This throws?
            if (numbers.Any(n => n < 1 || n > 1000000000))
                throw new Exception();
        }
    }
}
