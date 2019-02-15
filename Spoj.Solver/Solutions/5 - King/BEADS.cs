using System;

// https://www.spoj.com/problems/BEADS/ #ad-hoc #sorting
// Finds the start index of the minimum string rotation (lexicographically).
public static class BEADS
{
    public static int Solve(string @string)
    {
        // Create this to make the comparison much more convenient.
        string doubleString = @string + @string;

        int minimumStartIndex = 0;
        for (int s = 1; s < @string.Length; ++s)
        {
            // For perf, important to not actually create substrings, and to use compare ordinal.
            if (string.CompareOrdinal(
                doubleString, minimumStartIndex, doubleString, s, @string.Length) > 0)
            {
                minimumStartIndex = s;
            }
        }

        return minimumStartIndex + 1;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string @string = Console.ReadLine().Trim();

            Console.WriteLine(
                BEADS.Solve(@string));
        }
    }
}
