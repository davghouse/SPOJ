using System;

// https://www.spoj.com/problems/WORDCNT/ #ad-hoc
// Finds the longest contiguous sequence of words of the same length.
public static class WORDCNT
{
    public static int Solve(string[] words)
    {
        int longestSequenceLength = 0;
        int currentSequenceLength = 0;
        int previousWordLength = -1;
        foreach (string word in words)
        {
            if (word.Length == previousWordLength)
            {
                ++currentSequenceLength;
            }
            else
            {
                if (currentSequenceLength > longestSequenceLength)
                {
                    longestSequenceLength = currentSequenceLength;
                }

                currentSequenceLength = 1;
                previousWordLength = word.Length;
            }
        }

        return Math.Max(longestSequenceLength, currentSequenceLength);
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] words = Console.ReadLine().Split(
                default(char[]),StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine(
                WORDCNT.Solve(words));
        }
    }
}
