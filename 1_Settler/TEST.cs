using System;

// Life, the Universe, and Everything
// 1 http://www.spoj.com/problems/TEST/
// Reads and prints console input until the answer to life, the universe, and everything is found.
public static class TEST
{
    private const string AnswerToEverything = "42";

    public static void Solve()
    {
        string line;
        while((line = Console.ReadLine()) != AnswerToEverything)
        {
            Console.WriteLine(line);
        }
    }
}

public static class Program
{
    private static void Main()
    {
        TEST.Solve();
    }
}