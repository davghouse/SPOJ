using System;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/POLEVAL/
public static class InputVerifier
{
    private static void Main()
    {
        int degree;
        while ((degree = int.Parse(Console.ReadLine())) != -1)
        {
            int[] coefficients = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            if (coefficients.Length != degree + 1)
                throw new Exception();

            int pointCount = int.Parse(Console.ReadLine());
            int[] points = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            if (points.Length != pointCount)
                throw new Exception();
        }
    }
}
