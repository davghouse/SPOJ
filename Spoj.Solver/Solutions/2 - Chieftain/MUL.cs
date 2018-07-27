using System;
using System.Numerics;
using System.Text;

// https://www.spoj.com/problems/MUL/ #big-numbers
// Multiplies big numbers.
public static class MUL
{
    public static BigInteger Solve(string a, string b)
        => BigInteger.Parse(a) * BigInteger.Parse(b);
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();

            output.Append(
                MUL.Solve(line[0], line[1]));
            output.AppendLine();
        }

        Console.Write(output);
    }
}
