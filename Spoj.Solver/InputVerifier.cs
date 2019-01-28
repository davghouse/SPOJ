using System;

// Too many problems have improperly formatted input, like random whitespace, missing newlines
// and other things that C# doesn't deal well with by default. It seems fine to allow verifying
// the input format on an alt account to prevent frustration and misrepresentative profile stats.
// Currently verifying for: https://www.spoj.com/problems/CHOCOLA/
public static class InputVerifier
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            Console.ReadLine();

            string[] line = Console.ReadLine().Split();
            int m = int.Parse(line[0]);
            int n = int.Parse(line[1]);
            if (m < 2 || m > 1000 || n < 2 || n > 1000)
                throw new Exception();

            int[] columnCosts = new int[m];
            for (int c = 0; c < m - 1; ++c)
            {
                columnCosts[c] = int.Parse(Console.ReadLine());
            }

            int[] rowCosts = new int[n];
            for (int r = 0; r < n - 1; ++r)
            {
                rowCosts[r] = int.Parse(Console.ReadLine());
            }
        }
    }
}
