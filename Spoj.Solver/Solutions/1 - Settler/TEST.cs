using System;

// https://www.spoj.com/problems/TEST/ #io
// Reads and prints input until the answer to life, the universe, and everything is found.
public static class TEST
{
    public static void Solve()
    {
        string line;
        while ((line = Console.ReadLine()) != "42")
        {
            Console.WriteLine(line);
        }
    }
}

public static class Program
{
    private static void Main()
        => TEST.Solve();
}
