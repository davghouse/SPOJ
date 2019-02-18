using System;
using System.Linq;

// https://www.spoj.com/problems/EC_CONB/ #ad-hoc #binary
// Reverses the binary representation of even numbers.
public static class EC_CONB
{
    public static int Solve(int num)
    {
        if (num % 2 == 1)
            return num;

        string binaryRepresentation = Convert.ToString(num, toBase: 2);
        string reversedbinaryRepresentation
            = new string(binaryRepresentation.Reverse().ToArray());

        return Convert.ToInt32(reversedbinaryRepresentation, fromBase: 2);
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            Console.WriteLine(
                EC_CONB.Solve(int.Parse(Console.ReadLine())));
        }
    }
}
