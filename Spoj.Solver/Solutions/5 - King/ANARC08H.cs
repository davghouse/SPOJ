using System;

// https://www.spoj.com/problems/ANARC08H/ #formula #game
// Solves the Josephus problem in the easiest way.
public static class ANARC08H
{
    // Seems like kind of a niche problem so I just looked up a guide:
    // https://www.geeksforgeeks.org/josephus-problem-set-1-a-on-solution/
    // https://medium.com/@rrfd/explaining-the-josephus-algorithm-11d0c02e7212
    // Their solution is top down, this one is bottom up, but the same.
    public static int Solve(int n, int d)
    {
        int lastExecuted = 1; // If just 1 person, that person is last.
        for (int p = 2; p <= n; ++p)
        {
            lastExecuted = (lastExecuted + d - 1) % p + 1;
        }

        return lastExecuted;
    }
}

public static class Program
{
    private static void Main()
    {
        while (true)
        {
            string[] line = Console.ReadLine().Split();
            int n = int.Parse(line[0]);
            int d = int.Parse(line[1]);
            if (n == 0 && d == 0)
                return;

            Console.WriteLine(
                $"{n} {d} {ANARC08H.Solve(n, d)}");
        }
    }
}
