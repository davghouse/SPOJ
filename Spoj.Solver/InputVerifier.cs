using System;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/ARRAYSUB/ (it's improperly formatted).
public static class InputVerifier
{
    private static void Main()
    {
        int arraySize = int.Parse(Console.ReadLine());

        if (arraySize < 1 || arraySize > 100000)
            throw new FormatException();

        int[] array = Array.ConvertAll(
            Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
            int.Parse);

        if (array.Length != arraySize)
            throw new FormatException();

        int k = int.Parse(Console.ReadLine());

        if (k < 1 || k > arraySize)
            throw new FormatException();
    }
}