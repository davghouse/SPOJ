using System;

// https://www.spoj.com/problems/EASYPROB/ #binary #experiment #recursion
// Transforms a number into its base 2 representation in a weird recursive way.
public static class EASYPROB
{
    public static string Solve(int n)
        => $"{n}={SolveRecursively(n)}";

    // Say n = 137. The binary representation of n is 10001001, or in other words:
    // 2^7 + 2^3 + 2^0, but those exponents get defined recursively using the same procedure:
    // 2^(2^2 + 2 + 2^0) + 2^(2 + 2^0) + 2^0, where there's a weird base case for 2^1 (it's just 2).
    // Oh and we're not using exponents, parenthesis stand in for them so it's like:
    // 2(2(2)+2+2(0))+2(2+2(0))+2(0).
    private static string SolveRecursively(int n)
    {
        // Normal base case.
        if (n == 0) return "0";

        string nInBinary = Convert.ToString(n, toBase: 2);
        string result = string.Empty;

        for (int i = 0; i < nInBinary.Length; ++i)
        {
            if (nInBinary[i] == '0') continue;

            int exponentForThisPowerOfTwo = nInBinary.Length - i - 1;

            if (result != string.Empty)
            {
                // This isn't the first part we're adding, so we need a plus.
                result += "+";
            }

            // Weird base case because we can't recurse to get 2(2(0)).
            if (exponentForThisPowerOfTwo == 1)
            {
                result += "2";
            }
            else
            {
                result += $"2({SolveRecursively(exponentForThisPowerOfTwo)})";
            }
        }

        return result;
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
                EASYPROB.Solve(int.Parse(Console.ReadLine())));
        }
    }
}
