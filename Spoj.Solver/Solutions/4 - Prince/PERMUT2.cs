using System;

// http://www.spoj.com/problems/PERMUT2/: ad hoc
// Figures out if a permutation of n integers is the same as its inverse permutation.
public static class PERMUT2
{
    public static string Solve(int[] permutation)
    {
        // Given is a permutation like 5, 1, 2, 3, 4.
        // This (obviously) tells us the value 5 is at index 1, the value 1 is at index 2, and so on.
        // We can also interpret the permutation as specifying an inverse permutation, where it's
        // telling us the value 1 is at index 5, the value 2 is at index 1, and so on. The permutation
        // is ambiguous if the permutation and the inverse permutation are equal.
        for (int i = 0; i < permutation.Length; ++i)
        {
            // For example, from the above permutation, the inverse value of 1...
            int inverseValue = i + 1;
            // Is at index 5 (but actually 4 because we're zero-indexed, sigh)...
            int indexOfInverseValue = permutation[i] - 1;
            // And the value at that index in the normal permutation is 4...
            int valueAtThatIndexInNormalPermutation = permutation[indexOfInverseValue];

            // So they differ, and the permutations aren't ambiguous.
            if (inverseValue != valueAtThatIndexInNormalPermutation)
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
