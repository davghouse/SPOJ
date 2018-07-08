using System;
using System.Text;

// https://www.spoj.com/problems/FCTRL/ #experiment #factorial #factors #inspection #math #trap
// Returns the number of trailing zeros of n!, for very large n.
public static class FCTRL
{
    // n! has as many zeros as it has factors of 10. n! has as many factors of 10 as it has
    // min(factors of 2, factors of 5). Looking at some numbers, it's clear n! is picking up
    // factors of 2 a lot faster than factors of 5. So we need to find the number of factors
    // of 5. Here's an example of the procedure used, for n = 5:
    // 1 * 2 * 3 * 4 * 5 => (ignore everything that isn't contributing factors of 5)
    // 5
    // 1 (tally the factors of 5).
    // => 1 total.
    // And for n = 50:
    // 1 * 2 * ... * 50 =>
    // 5 * 10 * 15 * 20 * 25 * 30 * 35 * 40 * 45 * 50
    // 1   1    1    1    1    1    1    1    1    1
    //                    1                        1
    // => 10 + 2 total. Note the pattern that starts to emerge, a row of tallies for 5, for 25, for 125, etc,
    // with a constant difference between tallyies on the same row (1, 5, 25, etc). To find the tally count on
    // the first row for n, divide n by 5. On the second row, divide n by 25. On the third by 125, etc.
    public static int Solve(int n)
    {
        int count = 0;
        while (n != 0)
        {
            count += n /= 5;
        }

        return count;
    }
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            output.Append(
                FCTRL.Solve(int.Parse(Console.ReadLine())));
            output.AppendLine();
        }

        Console.Write(output);
    }
}
