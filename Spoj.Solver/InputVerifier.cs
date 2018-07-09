using System;
using System.Linq;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/HACKRNDM/ (it's malformed)
public static class InputVerifier
{
    private static void Main()
    {
        string[] line = Console.ReadLine().Split();
        int numberCount = int.Parse(line[0]);
        if (numberCount < 1 || numberCount > 100000)
            throw new ArgumentOutOfRangeException();
        int difference = int.Parse(line[1]);
        if (difference < 1)
            throw new ArgumentOutOfRangeException();

        int[] numbers = new int[numberCount];
        for (int i = 0; i < numberCount; ++i)
        {
            numbers[i] = int.Parse(Console.ReadLine());
        }
        if (numbers.Length != numbers.Distinct().Count())
            throw new ArgumentException();
    }
}
