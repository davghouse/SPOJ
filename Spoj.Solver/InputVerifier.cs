using System;
using System.Linq;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/SUMFOUR/
public static class InputVerifier
{
    private static void Main()
    {
        int size = int.Parse(Console.ReadLine());
        if (size < 1 || size > 2500)
            throw new ArgumentOutOfRangeException();

        for (int i = 0; i < size; ++i)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            if (line.Length != 4)
                throw new ArgumentException();
            if (line.Any(n => Math.Abs(n) > Math.Pow(2, 28)))
                throw new ArgumentOutOfRangeException();
        }
    }
}
