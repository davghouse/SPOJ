using System;

// https://www.spoj.com/problems/ATOMS/ #math
// Finds the largest T such that N*K^T <= M.
public static class ATOMS
{
    // N*K^T <= M => logK(K^T) <= logK(M/N) => T <= logK(M/N).
    public static long Solve(long n, long k, long m)
    {
        if (n >= m)
            return 0;

        // I'm not confident that this solution is robust, because double doesn't
        // have enough precision to exactly represent the numbers being worked with.
        return (long)Math.Log(m / (double)n, k);
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            long[] line = Array.ConvertAll(Console.ReadLine().Split(), long.Parse);

            Console.WriteLine(
                ATOMS.Solve(line[0], line[1], line[2]));
        }
    }
}
