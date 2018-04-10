using System;

// http://www.spoj.com/problems/TEST/ #io
// Reads and prints console input until the answer to life, the universe, and everything is found.
public static class TEST
{
    private const string _answerToEverything = "42";

    public static void Solve()
    {
        string line;
        while ((line = Console.ReadLine()) != _answerToEverything)
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
