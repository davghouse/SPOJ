using System;

// https://www.spoj.com/problems/TSHOW1/ #binary #experiment #math
// Finds the kth number composed of only the digits 5 and 6.
public static class TSHOW1
{
    // Group 1: (1 digit)
    //  1:   5 | 0
    //  2:   6 | 1
    // Group 2: (2 digits)
    //  3:  55 | 00
    //  4:  56 | 01
    //  5:  65 | 10
    //  6:  66 | 11
    // Group 3: (3 digits)
    //  7: 555 | 000
    //  8: 556 | 001
    //  9: 565 | 010
    // 10: 566 | 011
    // 11: 655 | 100
    // 12: 656 | 101
    // 13: 665 | 110
    // 14: 666 | 111
    // Group n has n digits, and 2^n numbers in it. There are 2^(n + 1) - 2 numbers
    // through the first n groups. Given k, we can calculate the group it falls in.
    // We need the lowest n such that 2^(n + 1) - 2 >= k. After some experimentation,
    // that's apparently floor(log(k + 1)). Then it's simple to calculate k's index
    // in its group. Imagine the binary representation of its index in the group.
    // As shown above, the binary representation maps to the 5/6 number by converting
    // 0s into 5s and 1s into 6s. We get all binary #s of a group's length, so we must
    // also get all 5/6 numbers of a group's length (since the mapping is 1:1).
    // Example: Take k = 12. It's in group 3. It's index in that group is 5. The binary
    // representation of 5 is 101. This maps to 656.
    public static string Solve(long k)
    {
        int groupNumber = (int)Math.Log(k + 1, 2);
        long totalSizeOfPreviousGroups = (long)Math.Pow(2, groupNumber) - 2;
        long indexInGroup = k - totalSizeOfPreviousGroups - 1;

        return Convert.ToString(indexInGroup, toBase: 2)
            .PadLeft(groupNumber, '0')
            .Replace('0', '5')
            .Replace('1', '6');
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            long k = long.Parse(Console.ReadLine());

            Console.WriteLine(
                TSHOW1.Solve(k));
        }
    }
}
