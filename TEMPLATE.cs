using System;

// <Name>
// <Number> http://www.spoj.com/problems/TEMPLATE/
// <Description>
public static class TEMPLATE
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
                TEMPLATE.Solve(line[0], line[1]));
        }
     }
}