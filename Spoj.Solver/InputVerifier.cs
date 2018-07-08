using System;
using System.Linq;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/KGSS/
public static class InputVerifier
{
    private static void Main()
    {
        int arrayLength = int.Parse(Console.ReadLine());
        if (arrayLength < 2 || arrayLength > 100000)
            throw new FormatException();

        int[] sourceArray = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        if (sourceArray.Length != arrayLength)
            throw new FormatException();
        if (sourceArray.Any(v => v < 0 || v > 100000000))
            throw new FormatException();

        int operationCount = int.Parse(Console.ReadLine());
        if (operationCount < 0 || operationCount > 100000)
            throw new FormatException();

        for (int o = 0; o < operationCount; ++o)
        {
            string[] operation = Console.ReadLine().Split();
            if (operation[0] == "Q")
            {
                int startIndex = int.Parse(operation[1]) - 1;
                if (startIndex < 0 || startIndex >= arrayLength)
                    throw new FormatException();

                int endIndex = int.Parse(operation[2]) - 1;
                if (endIndex < 0 || endIndex >= arrayLength)
                    throw new FormatException();

                if (startIndex >= endIndex)
                    throw new FormatException();
            }
            else if (operation[0] == "U")
            {
                int index = int.Parse(operation[1]) - 1;
                if (index < 0 || index >= arrayLength)
                    throw new FormatException();

                int value = int.Parse(operation[2]);
                if (value < 0 || value > 100000000)
                    throw new FormatException();
            }
            else throw new FormatException();
        }
    }
}
