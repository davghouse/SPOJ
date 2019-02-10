using System;
using System.Linq;

// https://www.spoj.com/problems/ANARC05H/ #dynamic-programming-2d
// Finds the number of ways to create increasing groups of digits.
public static class ANARC05H
{
    public static int Solve(int[] digits)
    {
        int totalDigitSum = digits.Sum();

        // groupCounts(s, d) is the number of ways to group digits starting with the
        // digit at index d, given that the group to the left has a sum of s. Groups
        // of digits must be non-decreasing (by the sum of their digits), left to right.
        int[,] groupCounts = new int[totalDigitSum + 1, digits.Length];

        // We initialize the table starting with the last digit. There's only one way
        // to group the last digit--just the digit itself. As long as the previous
        // group is <= the last digit's value, the non-decreasing property will be satisfied.
        // After that point, the previous group is too big and the column is left as zeros.
        for (int previousGroupSum = 0; previousGroupSum <= digits[digits.Length - 1]; ++previousGroupSum)
        {
            groupCounts[previousGroupSum, digits.Length - 1] = 1;
        }

        for (int startDigit = digits.Length - 2; startDigit >= 0; --startDigit)
        {
            // Sum from the startDigit to the last digit.
            int fullRangeDigitSum = digits.Skip(startDigit).Sum();

            // Only go up to fullRangeDigitSum, because anything higher we can't satisfy.
            for (int previousGroupSum = 0; previousGroupSum <= fullRangeDigitSum; ++previousGroupSum)
            {
                // Increment once because grouping startDigit to the last digit all together
                // satisfies the non-decreasing property (this is the loop's condition).
                ++groupCounts[previousGroupSum, startDigit];

                int partialRangeDigitSum = fullRangeDigitSum;
                for (int endDigit = digits.Length - 2; endDigit >= startDigit; --endDigit)
                {
                    // Remove the previous end digit.
                    partialRangeDigitSum -= digits[endDigit + 1];

                    // DP time: here's the scenario...
                    // {previous group} {group from start to end index} {remaining digits}
                    // The group from the start to the end index is big enough to satisfy the
                    // non-decreasing property, so we can use it. Once we use it we look up
                    // how many ways there are to group the remaining digits--their previous group
                    // group is this group, and their start index is 1 past this group's end index.
                    if (previousGroupSum <= partialRangeDigitSum)
                    {
                        groupCounts[previousGroupSum, startDigit]
                            += groupCounts[partialRangeDigitSum, endDigit + 1];
                    }
                    // No longer big enough to satisfy the non-decreasing property, so stop.
                    else break;
                }
            }
        }

        // [0, 0] is the number of ways to group digits when the previous digit sum is
        // 0 (empty), starting from the first digit. Which is what we're looking for.
        return groupCounts[0, 0];
    }
}

public static class Program
{
    private static void Main()
    {
        string line;
        int testCounter = 0;
        while ((line = Console.ReadLine()) != "bye")
        {
            int[] digits = line
                .Select(c => c - '0')
                .ToArray();

            Console.WriteLine(
                $"{++testCounter}. {ANARC05H.Solve(digits)}");
        }
    }
}
