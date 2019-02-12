using System;
using System.Collections.Generic;

// https://www.spoj.com/problems/CPCRC1C/ #ad-hoc #digits
// Sums the digits of all the numbers in a range.
public static class CPCRC1C
{
    public static long Solve(int start, int end)
        => SumOfDigitsFrom1To(end) - SumOfDigitsFrom1To(start - 1);

    private static long SumOfDigitsFrom1To(int n)
    {
        if (n <= 0) return 0;

        // Say n = 37468. Create a stack looking like this: 3|7|4|6|8.
        var digits = BuildDigitsStack(n);

        long result = 0;
        while (digits.Count > 0)
        {
            // Say this is the first iteration of the loop for n = 37468. The digit is 3 @ pos 5.
            int digitPosition = digits.Count;
            int digit = digits.Pop();

            // We now want to add the contribution from 1 to 30000. Suppose we know the answer
            // for 1 to 10000. This happens 3 times, the second time with an extra 1 out front
            // like 1[xxxx], the third time with an extra 2 out front like 2[xxxx], up to 30000.
            // So we get 1 and 2 an extra 10000 times each, plus a final 3 from 3[0000]
            int exponent = digitPosition - 1;
            long powerOf10 = Pow(10, exponent);
            long sumOfDigitsFrom1ToPowerOf10 = SumOfDigitsFrom1ToPowerOf10(powerOf10, exponent);
            result += digit * sumOfDigitsFrom1ToPowerOf10;
            for (int i = 1; i < digit; ++i)
            {
                result += i * powerOf10;
            }
            result += digit;

            // We've summed up to 30000, but 7468 still remains. For each of those 7468, 3 is
            // out in front, so we need to add that part. Then we'll have added everything from
            // the leading 3, and can continue the loop in the same way for the remaining 7468.
            n -= (int)(digit * powerOf10); // Subtract 30000.
            result += digit * n;           // Add the 7468 3s.
        }

        return result;
    }

    private static Stack<int> BuildDigitsStack(int n)
    {
        var digits = new Stack<int>();
        while (n != 0)
        {
            digits.Push(n % 10);
            n /= 10;
        }

        return digits;
    }

    // Say the power of 10 = 1000. Then we're summing the digits of the numbers from 1 to 1000.
    // Each position in [000] contributes 100 1s, 100 2s, and so on. This is easy to see for
    // the highest position: 100 to 199, 200 to 299, and so on. For the lowest position, it's
    // constantly changing as we go from 1 to 1000, so it contributes evenly to each one as well.
    // Others work similarly, the digit changing more slowly as the position increases.
    private static long SumOfDigitsFrom1ToPowerOf10(long powerOf10, int exponent)
    {
        long sumFrom1to9 = 45;
        long digitOccurrencesPerPosition = powerOf10 / 10;
        long positionCount = exponent;

        return sumFrom1to9 * digitOccurrencesPerPosition * positionCount;
    }

    // https://en.wikipedia.org/wiki/Exponentiation_by_squaring
    // https://stackoverflow.com/a/383596
    private static long Pow(int @base, int exponent)
    {
        long result = 1;
        while (exponent != 0)
        {
            if ((exponent & 1) == 1)
            {
                result *= @base;
            }

            @base *= @base;
            exponent >>= 1;
        }

        return result;
    }
}

public static class Program
{
    private static void Main()
    {
        string[] line;
        while ((line = Console.ReadLine().Trim().Split())[0] != "-1")
        {
            Console.WriteLine(
                CPCRC1C.Solve(int.Parse(line[0]), int.Parse(line[1])));
        }
    }
}
