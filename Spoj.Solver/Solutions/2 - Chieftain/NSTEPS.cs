using System;

// https://www.spoj.com/problems/NSTEPS/ #ad-hoc
// Given an x and y coordinate, returns the value on the plane at that point.
public static class NSTEPS
{
    public static string Solve(int x, int y)
    {
        if (x == y || x == y + 2)
            return x % 2 == 0
                ? (x + y).ToString()
                : (x + y - 1).ToString();

        return "No Number";
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
                NSTEPS.Solve(line[0], line[1]));
        }
    }
}
