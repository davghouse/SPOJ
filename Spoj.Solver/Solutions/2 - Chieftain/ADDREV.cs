using System;

// http://www.spoj.com/problems/ADDREV/ #ad-hoc #digits
// Returns the reversed sum of two reversed integers.
public static class ADDREV
{
    public static int Solve(int a, int b)
        => (a.Reverse() + b.Reverse()).Reverse();

    private static int Reverse(this int a)
    {
        int reverse = 0;
        while (a != 0)
        {
            reverse = reverse * 10 + a % 10; // Make room for the next digit, and then add it.
            a = a / 10; // Remove the digit just added.
        }

        return reverse;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            Console.WriteLine(
                ADDREV.Solve(line[0], line[1]));
        }
    }
}
