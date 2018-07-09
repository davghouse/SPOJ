using System;
using System.Collections.Generic;
using System.Linq;

// https://www.spoj.com/problems/HACKRNDM/ #hash-table
// Finds the number of pairs of numbers that are a certain distance away from each other.
public static class HACKRNDM // v2, using a hash table.
{
    // Given a number we know the number that would create the desired difference with it,
    // so we can just check to see if that number exists in the set.
    public static int Solve(int difference, HashSet<int> numbers)
        => numbers.Count(n => numbers.Contains(n - difference));
}

public static class Program
{
    private static void Main()
    {
        string[] line = Console.ReadLine().Split();
        int numberCount = int.Parse(line[0]);
        int difference = int.Parse(line[1]);

        var numbers = new HashSet<int>();
        for (int i = 0; i < numberCount; ++i)
        {
            numbers.Add(int.Parse(Console.ReadLine()));
        }

        Console.Write(HACKRNDM.Solve(difference, numbers));
    }
}
