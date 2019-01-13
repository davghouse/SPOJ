using System;

// https://www.spoj.com/problems/NY10E/ #dynamic-programming-2d
// Calculates how many non-decreasing digit strings exist for the given length.
public static class NY10E
{
    private static long[,] _endingDigitCounts = new long[65, 10];

    // Note that for any non-decreasing digit string of length n, it's prefixed
    // by a non-decreasing digit string of length n - 1. We need to figure out a way
    // to build up the non-decreasing digit strings of length n using the digit
    // strings of length n - 1.
    // For strings of length 1, obviously there are 9 non-decreasing digit strings,
    // one for each digit. For strings of length 2, consider the ending digit 0. It
    // can only be appended to 0. The ending digit 1 can be appended to 0 or 1. The
    // ending digit 3 can be appened to 0, 1, or 2.
    // To create digit strings of length n, the ending digit d can be appened to
    // strings of length n - 1 with ending digits 0 through d. If we know the
    // ending digit counts for n - 1, we can figure out the ending digit counts for
    // n, and summing the ending digit counts gives us the total string count.
    static NY10E()
    {
        for (int digit = 0; digit <= 9; ++digit)
        {
            _endingDigitCounts[1, digit] = 1;
        }

        for (int stringLength = 2; stringLength <= 64; ++stringLength)
        {
            for (int digit = 0; digit <= 9; ++digit)
            {
                for (int d = 0; d <= digit; ++d)
                {
                    _endingDigitCounts[stringLength, digit] += _endingDigitCounts[stringLength - 1, d];
                }
            }
        }
    }

    public static long Solve(int stringLength)
    {
        long stringCount = 0;
        for (int digit = 0; digit <= 9; ++digit)
        {
            stringCount += _endingDigitCounts[stringLength, digit];
        }

        return stringCount;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();
            int testNumber = int.Parse(line[0]);
            int stringLength = int.Parse(line[1]);

            Console.WriteLine(
                $"{testNumber} {NY10E.Solve(stringLength)}");
        }
    }
}
