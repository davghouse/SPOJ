using System;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/GLJIVE/
public static class InputVerifier
{
    private static void Main()
    {
        int[] points = new int[10];

        for (int i = 0; i < 10; ++i)
        {
            points[i] = int.Parse(Console.ReadLine());

            if (points[i] < 0 || points[i] > 100)
                throw new FormatException();
        }
    }
}
