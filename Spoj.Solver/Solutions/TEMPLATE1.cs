using System;

// https://www.spoj.com/problems/TEMPLATE1/ <tags>
// <description>
public static class TEMPLATE1
{
    public static int Solve(int a, int b)
        => a * b;
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
                TEMPLATE1.Solve(line[0], line[1]));
        }
    }
}
