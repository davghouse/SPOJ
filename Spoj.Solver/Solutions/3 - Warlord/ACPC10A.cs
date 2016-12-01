using System;
using System.Linq;

// http://www.spoj.com/problems/ACPC10A/: sequence
// Given three successive numbers in a sequence, returns the type of sequence and the next number.
public static class ACPC10A
{
    public static string Solve(int first, int second, int third)
    {
        if (SequenceHelper.IsArithmeticSequence(first, second, third))
            return $"AP {third + (third - second)}";

        return $"GP {third * (third / second)}";
    }
}

public static class SequenceHelper
{
    public static bool IsArithmeticSequence(params int[] sequence)
    {
        int firstDifference = sequence[1] - sequence[0];

        for (int i = 2; i < sequence.Length; ++i)
        {
            if (sequence[i] - sequence[i - 1] != firstDifference)
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
