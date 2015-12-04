using System;

// 1 Life, the Universe, and Everything
// http://www.spoj.com/problems/TEST/
public class TEST
{
    private static void Main()
    {
        TEST.Solve();
    }

    public static void Solve()
    {
        string line;
        while((line = Console.ReadLine()) != "42")
        {
            Console.WriteLine(line);
        }
    }
}