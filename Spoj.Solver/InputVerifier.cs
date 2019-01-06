using System;
using System.Linq;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/CADYDIST/
public static class InputVerifier
{
    private static void Main()
    {
        int classCount;
        while ((classCount = int.Parse(Console.ReadLine())) != 0)
        {
            if (classCount > 100000)
                throw new Exception();

            int[] classSizes = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int[] candyPrices = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            if (classSizes.Length != classCount)
                throw new Exception();
            if (candyPrices.Length != classCount)
                throw new Exception();
            if (classSizes.Any(s => s > 100000))
                throw new Exception();
            if (candyPrices.Any(p => p > 100000))
                throw new Exception();
        }
    }
}
