using System;
using System.Linq;

// Too many problems have improperly formatted input... random whitespace,
// missing newlines... things that C# doesn't deal well with by default.
// It's good to verify the input format on an alt account so you don't get frustrated.
// Currently verifying for: http://www.spoj.com/problems/ANARC05B/ (it's improperly formatted)
public static class InputVerifier
{
    private static void Main()
    {
        int[] firstSequence;
        int[] secondSequence;

        while ((firstSequence = Array.ConvertAll(
            Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
            int.Parse)).Length != 1)
        {
            secondSequence = Array.ConvertAll(
                Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
                int.Parse);

            if (firstSequence[0] != firstSequence.Length - 1
                || firstSequence[0] > 10000)
                throw new FormatException();

            if (secondSequence[0] != secondSequence.Length - 1
                || secondSequence[0] > 10000)
                throw new FormatException();

            if (firstSequence.Any(v => v < -10000 || v > 10000))
                throw new FormatException();

            if (secondSequence.Any(v => v < -10000 || v > 10000))
                throw new FormatException();

            for (int i = 2; i < firstSequence.Length; ++i)
            {
                if (firstSequence[i - 1] >= firstSequence[i])
                    throw new FormatException();
            }

            for (int i = 2; i < secondSequence.Length; ++i)
            {
                if (secondSequence[i - 1] >= secondSequence[i])
                    throw new FormatException();
            }
        }
    }
}
