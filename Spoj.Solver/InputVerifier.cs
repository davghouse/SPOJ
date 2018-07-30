using System;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/MICEMAZE/
public static class InputVerifier
{
    private static void Main()
    {
        int cellCount = int.Parse(Console.ReadLine());

        int exitCell = int.Parse(Console.ReadLine());
        if (exitCell < 1 || exitCell > cellCount)
            throw new ArgumentOutOfRangeException();

        int timeLimit = int.Parse(Console.ReadLine());
        int connectionCount = int.Parse(Console.ReadLine());

        for (int c = 0; c < connectionCount; ++c)
        {
            string[] line = Console.ReadLine().Split();
            if (line.Length != 3)
                throw new ArgumentException();

            int firstCell = int.Parse(line[0]);
            if (firstCell < 1 || firstCell > cellCount)
                throw new ArgumentOutOfRangeException();

            int secondCell = int.Parse(line[1]);
            if (secondCell < 1 || secondCell > cellCount)
                throw new ArgumentOutOfRangeException();

            int timeCost = int.Parse(line[2]);
        }
    }
}
