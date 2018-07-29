using System;
using System.Linq;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/LABYR1/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();
            int columnCount = int.Parse(line[0]);
            int rowCount = int.Parse(line[1]);

            if (columnCount < 3 || rowCount < 3)
                throw new ArgumentOutOfRangeException();

            if (columnCount > 1000 || rowCount > 1000)
                throw new ArgumentOutOfRangeException();

            for (int r = 1; r <= rowCount; ++r)
            {
                string row = Console.ReadLine();
                if (row.Length != columnCount)
                    throw new ArgumentException();
                if (row.Any(b => b != '#' && b != '.'))
                    throw new ArgumentException();
            }
        }
    }
}
