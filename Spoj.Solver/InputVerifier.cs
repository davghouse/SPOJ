using System;
using System.Linq;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/NOTATRI/
public static class InputVerifier
{
    private static void Main()
    {
        int stickCount;
        while ((stickCount = int.Parse(Console.ReadLine())) != 0)
        {
            if (stickCount < 3 || stickCount > 2000)
                throw new Exception();

            int[] stickLengths = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            if (stickLengths.Any(l => l < 1 || l > 1000000))
                throw new Exception();
        }
    }
}
