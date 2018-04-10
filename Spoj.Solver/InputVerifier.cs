using System;
using System.Linq;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It's seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: http://www.spoj.com/problems/BUSYMAN/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        if (remainingTestCases < 1 || remainingTestCases > 10)
            throw new FormatException();

        while (remainingTestCases-- > 0)
        {
            int activityCount = int.Parse(Console.ReadLine());
            if (activityCount < 1 || activityCount > 100000)
                throw new FormatException();

            for (int a = 0; a < activityCount; ++a)
            {
                int[] startAndEndTime = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

                if (startAndEndTime.Length != 2)
                    throw new FormatException();

                if (startAndEndTime.Any(n => n < 0 || n > 1000000))
                    throw new FormatException();

                if (startAndEndTime[0] >= startAndEndTime[1])
                    throw new FormatException();
            }
        }
    }
}
