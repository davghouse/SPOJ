using System;
using System.Linq;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/GSS3/ (it's improperly formatted).
public static class InputVerifier
{
    private static void Main()
    {
        int arrayLength = int.Parse(Console.ReadLine());

        if (arrayLength < 1 || arrayLength > 50000)
            throw new FormatException();

        int[] array = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);

        if (array.Length != arrayLength)
            throw new FormatException();

        if (array.Any(a => a < -10000 || a > 10000))
            throw new FormatException();

        int operationCount = int.Parse(Console.ReadLine());

        if (operationCount < 1 || operationCount > 50000)
            throw new FormatException();

        int[] operation;
        for (int o = 0; o < operationCount; ++o)
        {
            operation = Array.ConvertAll(Console.ReadLine().Trim().Split(), int.Parse);
            int type = operation[0];

            if (type == 0) // Update
            {
                int index = operation[1];
                int newValue = operation[2];

                if (index < 1 || index > arrayLength)
                    throw new FormatException();

                if (newValue < -10000 || newValue > 10000)
                    throw new FormatException();
            }
            else if (type == 1) // Query
            {
                int startIndex = operation[1];
                int endIndex = operation[2];

                if (startIndex < 1 || startIndex > arrayLength)
                    throw new FormatException();

                if (endIndex < 1 || endIndex > arrayLength)
                    throw new FormatException();
            }
            else throw new FormatException();
        }
    }
}