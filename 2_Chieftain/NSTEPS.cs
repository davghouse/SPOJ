using System;

// Number Steps
// 1112 http://www.spoj.com/problems/NSTEPS/
// Given an x and y coordinate, returns the value on the plane at that point.
public static class NSTEPS
{
    public static string Solve(int x, int y)
    {
        if (x == y || x == y + 2)
        {
            if (x % 2 == 0) return (x + y).ToString();
            else return (x + y - 1).ToString();
        }

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
            string[] line = Console.ReadLine().Split(' ');

            Console.WriteLine(
                NSTEPS.Solve(int.Parse(line[0]), int.Parse(line[1])));
        }
    }
}