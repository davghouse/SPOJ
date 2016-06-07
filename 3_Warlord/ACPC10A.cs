using System;
using System.Linq;

// What's Next
// 7974 http://www.spoj.com/problems/ACPC10A/
// Given three successive numbers in a sequence, returns the type of sequence and the next number.
public static class ACPC10A
{
    public static string Solve(int first, int second, int third)
    {
        if (NumberSequence.IsArithmeticSequence(first, second, third))
            return $"AP {third + (third - second)}";

        return $"GP {third * (third / second)}";
    }
}

// TODO: Create classes for arithmetic and geometric sequences.
public static class NumberSequence
{
    public static bool IsArithmeticSequence(params int[] numbers)
    {
        int firstDifference = numbers[1] - numbers[0];

        for (int i = 2; i < numbers.Length; ++i)
        {
            if (numbers[i] - numbers[i - 1] != firstDifference)
                return false;
        }

        return true;
    }
}

public static class Program
{
    private static void Main()
    {
        while (true)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            if (line.All(i => i == 0)) return;

            Console.WriteLine(
                ACPC10A.Solve(line[0], line[1], line[2]));
        }
    }
}