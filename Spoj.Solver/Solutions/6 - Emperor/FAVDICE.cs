using System;

// https://www.spoj.com/problems/FAVDICE/ #experiment #math #probability #proof
// Finds the expected number of rolls before rolling every side of an n-sided die.
public static class FAVDICE
{
    // See image for details: http://i.imgur.com/O1rtLnX.jpg.
    public static double Solve(int n)
    {
        double expectedRolls = 0;
        for (int i = 0; i <= n - 1; ++i)
        {
            expectedRolls += n / (double)(n - i);
        }

        return expectedRolls;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int n = int.Parse(Console.ReadLine());

            Console.WriteLine(
                $"{FAVDICE.Solve(n):F2}");
        }
    }
}
