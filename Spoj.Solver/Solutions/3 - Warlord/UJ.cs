using System;
using System.Numerics;

// https://www.spoj.com/problems/UJ/ #big-numbers
// Finds the number of ways to distribute discs amongst nephews.
public static class UJ
{
    public static BigInteger Solve(int nephewCount, int discCount)
        => BigInteger.Pow(nephewCount, discCount);
}

public static class Program
{
    private static void Main()
    {
        while (true)
        {
            int[] line = line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            if (line[0] == 0 && line[1] == 0)
                return;

            Console.WriteLine(
                UJ.Solve(line[0], line[1]));
        }
    }
}
