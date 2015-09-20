using System;

public class TEST
{
    public static void Main()
    {
        string line;
        while((line = Console.ReadLine()) != "42" && line != null)
        {
            Console.WriteLine(line);
        }
    }
}