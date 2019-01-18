using System;
using System.Linq;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/CRSCNTRY/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int[] agnesCheckpoints = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            if (agnesCheckpoints.Last() != 0)
                throw new Exception();

            int[] raceCardCheckpoints;
            while ((raceCardCheckpoints = Array.ConvertAll(
                Console.ReadLine().Split(), int.Parse))[0] != 0)
            {
                if (raceCardCheckpoints.Last() != 0)
                    throw new Exception();
            }
        }
    }
}
