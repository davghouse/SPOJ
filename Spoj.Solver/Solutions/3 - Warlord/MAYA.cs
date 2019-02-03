using System;
using System.Collections.Generic;

// https://www.spoj.com/problems/MAYA/ #ad-hoc
// Converts Mayan numbers to decimal numbers.
public static class MAYA
{
    private static readonly IReadOnlyDictionary<string, int> _mayanDigitsToInts = new Dictionary<string, int>
    {
        ["S"] = 0,      ["."] = 1,        [".."] = 2,        ["..."] = 3,        ["...."] = 4,
        ["-"] = 5,      [". -"] = 6,      [".. -"] = 7,      ["... -"] = 8,      [".... -"] = 9,
        ["- -"] = 10,   [". - -"] = 11,   [".. - -"] = 12,   ["... - -"] = 13,   [".... - -"] = 14,
        ["- - -"] = 15, [". - - -"] = 16, [".. - - -"] = 17, ["... - - -"] = 18, [".... - - -"] = 19,
    };

    private static readonly IReadOnlyDictionary<int, int> _mayanPowersToInts = new Dictionary<int, int>
    {
        [0] = 1,
        [1] = 20,
        [2] = 360,
        [3] = 360 * 20,
        [4] = 360 * 20 * 20,
        [5] = 360 * 20 * 20 * 20,
        [6] = 360 * 20 * 20 * 20 * 20
    };

    public static int Solve(int digitCount, string[] digits)
    {
        int result = 0;
        for (int d = 0; d < digitCount; ++d)
        {
            result += _mayanDigitsToInts[digits[d]] * _mayanPowersToInts[digitCount - d - 1];
        }

        return result;
    }
}

public static class Program
{
    private static void Main()
    {
        int digitCount;
        while ((digitCount = int.Parse(Console.ReadLine())) != 0)
        {
            string[] digits = new string[digitCount];
            for (int d = 0; d < digitCount; ++d)
            {
                digits[d] = Console.ReadLine().Trim();
            }
            Console.ReadLine();

            Console.WriteLine(
                MAYA.Solve(digitCount, digits));
        }
    }
}
