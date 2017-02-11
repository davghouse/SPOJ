using System;
using System.Text;

// http://www.spoj.com/problems/TEMPLATE2/ <tags>
// <description>
public static class TEMPLATE2
{
    public static int Solve(int a, int b)
        => a * b;
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            output.Append(
                TEMPLATE2.Solve(line[0], line[1]));
            output.AppendLine();
        }

        Console.Write(output);
    }
}
