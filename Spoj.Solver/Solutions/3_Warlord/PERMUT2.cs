using System;

// Ambiguous Permutations
// 379 http://www.spoj.com/problems/PERMUT2/
// Figures out if a permutation of n integers is the same as its inverse permutation.
public static class PERMUT2
{
    public static string Solve(int[] permutation)
    {
        // Given is a permutation like 5, 1, 2, 3, 4.
        // This tells us the value 5 is at index 1, the value 1 is at index 2, and so on.
        // We can also interpret the permutation as specifying an inverse permutation,
        // where the value 1 is at index 5, the value 2 is at index 1, and so on.
        // The permutation is ambiguous if the permutation and the inverse permutation are equal.
        for (int i = 0; i < permutation.Length; ++i)
        {
            int value = i + 1;
            int indexOfValueInInversePermutation = permutation[i] - 1;
            int valueAtTheSameIndexInNormalPermutation = permutation[indexOfValueInInversePermutation];

            if (value != valueAtTheSameIndexInNormalPermutation)
                return "not ambiguous";
        }

        return "ambiguous";
    }
}

public static class Program
{
    private static void Main()
    {
        int n;
        while ((n = int.Parse(Console.ReadLine())) != 0)
        {
            int[] permutation = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            Console.WriteLine(
                PERMUT2.Solve(permutation));
        }
    }
}
