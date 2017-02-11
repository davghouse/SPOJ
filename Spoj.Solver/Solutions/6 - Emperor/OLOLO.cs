using System;

// http://www.spoj.com/problems/OLOLO/ #binary #io #trap
// Identifies the sole integer that occurs in the input once rather than twice.
// See OLOLO.cpp--this solution was submitted using C++ because C# I/O is too slow.
public static class OLOLO
{
    public static int Solve(int pyaniCount)
    {
        int remainingPyanis = pyaniCount;
        int result = 0;

        while (remainingPyanis-- > 0)
        {
            // It's easy to see XOR'ing will work if all integer pairs arrive adjacent to each other.
            // Since XOR is commutative and associative though, the order the integers arrive doesn't matter.
            // For intuition, all columns of 1s and 0s are independent, and anything XOR'd with 0 is the same
            // thing, so the 0s in a column don't matter, and then it's just a bunch of 1s, independent
            // of the order in which the numbers arrive. The paired 1s cancel, leaving only the 1s from the unique.
            result ^= int.Parse(Console.ReadLine());
        }

        return result;
    }
}

public static class Program
{
    private static void Main()
    {
        int pyaniCount = int.Parse(Console.ReadLine());

        Console.WriteLine(
            OLOLO.Solve(pyaniCount));
    }
}
