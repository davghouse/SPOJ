using System;
using System.Text;

// http://www.spoj.com/problems/ALICESIE/ #division #experiment #sieve
// Finds how many numbers remaining after sieving like Alice.
public static class ALICESIE
{
    // Just look at four different examples to understand what's happening:
    // 13 12 11 10 9 8 7 6 5 4 3 2
    //                   X X X X X
    // 10 9 8 7 6 5 4 3 2
    //            X X X X
    // 5 4 3 2
    //       X
    // 3 2
    //
    // The numbers that get crossed out are those that can divide other numbers in the list.
    // This is always the tail of the list, since eventually we reach the point where 2 * m
    // is greater than n, so m can't be a divisor of anything left in the list (because the
    // smallest number having m as a proper divisor is 2 * m). To find the largest m, divide
    // n by 2 (and round down if necessary). Then count from 2 to that m to find the total
    // removed. And subtract from the total that exist (n - 1) to find the total remaining.
    public static int Solve(int n)
    {
        int numbersRemoved = n / 2 >= 2
            ? n / 2 - 1
            : 0;

        return (n - 1) - numbersRemoved;
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
            output.Append(ALICESIE.Solve(
                int.Parse(Console.ReadLine())));
            output.AppendLine();
        }

        Console.Write(output);
    }
}
