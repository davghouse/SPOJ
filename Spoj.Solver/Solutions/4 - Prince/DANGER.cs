using System;

// 1786 http://www.spoj.com/problems/DANGER/ In Danger
// Finds the last survivor for n people in a circle, where every second person dies.
public static class DANGER
{
    // 1
    // 
    // 1 2
    // 1
    // 
    // 1 2 3
    // 1   3
    //     3
    // 
    // 1 2 3 4
    // 1   3
    // 1
    // And so on, like:
    // n: 1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17...
    // s: 1 1 3 1 3 5 7 1 3 5  7  9  11 13 15 1  3...
    // For a proof that this pattern holds, see Wikipedia.
    // So it's like  2*(n - 2^(floor(log(n)))) + 1
    public static int Solve(string nEncoded)
    {
        int mantissa = (nEncoded[0] - '0') * 10 + (nEncoded[1] - '0');
        int exponent = nEncoded[3] - '0';

        int n = mantissa;
        for (int e = 0; e < exponent; ++e)
        {
            n *= 10;
        }

        return 2 * (n - MathHelper.FirstPowerOfTwoEqualOrLess(n)) + 1;
    }
}

public static class MathHelper
{
    public static int FirstPowerOfTwoEqualOrLess(int value)
    {
        int result = 2;
        while (result <= value)
        {
            result <<= 1;
        }

        return result >> 1;
    }
}

public static class Program
{
    private static void Main()
    {
        string nEncoded;
        while (!(nEncoded = Console.ReadLine()).StartsWith("00e0"))
        {
            Console.WriteLine(
                DANGER.Solve(nEncoded));
        }
    }
}
