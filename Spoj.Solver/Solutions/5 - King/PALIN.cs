using System;
using System.Linq;

// https://www.spoj.com/problems/PALIN/ #ad-hoc #experiment #inspection
// Outputs the smallest palindrome larger than the given integer.
public static class PALIN
{
    public static string Solve(string k)
    {
        // Need to handle even-length and odd-length arrays a bit differently,
        // since odds have a middle index.
        int? middleIndex = k.Length % 2 == 0 ? (int?)null : k.Length / 2;
        int leftHalfStartIndex = middleIndex - 1 ?? k.Length / 2 - 1;
        int rightHalfStartIndex = middleIndex + 1 ?? leftHalfStartIndex + 1;
        char[] ret;

        // Handle the edge case where the integer is all nines; this is the only case
        // where it's necessary to add an extra digit to find the smallest larger palindrome.
        // To see this, note if it's not all nines then there's at least the all nines
        // palindrome larger than it, with the same number of digits.
        if (k.All(c => c == '9'))
        {
            ret = new char[k.Length + 1];
            ret[0] = ret[ret.Length - 1] = '1';

            for (int i = 1; i < ret.Length - 1; ++i)
            {
                ret[i] = '0';
            }

            return new string(ret);
        }

        // Solve knowing the result has the same number of digits as the input. Only one
        // palindrome can be made from the input without increasing the left half of the
        // number; by mirroring the left half to the right. That number isn't necessarily
        // larger than the input number though.
        ret = k.ToCharArray();
        for (int l = leftHalfStartIndex, r = rightHalfStartIndex; l >= 0; --l, ++r)
        {
            if (k[l] > k[r])
            {
                // The left half mirrored is larger than the right, so the palindrome made from
                // the left half mirrored to the right is larger than the input, and hence valid.
                for (l = leftHalfStartIndex, r = rightHalfStartIndex; l >= 0; --l, ++r)
                {
                    ret[r] = ret[l];
                }

                return new string(ret);
            }
            if (k[l] < k[r])
                break;
        }

        // The left half mirrored isn't larger than the right, so make the left half minimally
        // larger so the number as a whole is larger than the input, and then turn it into a palindrome.
        for (int l = middleIndex ?? leftHalfStartIndex; l >= 0; --l)
        {
            bool keepCarryingTheOne = ret[l] == '9';
            ret[l] = keepCarryingTheOne ? '0' : (char)(ret[l] + 1);

            if (!keepCarryingTheOne)
                break;
        }
        for (int l = leftHalfStartIndex, r = rightHalfStartIndex; l >= 0; --l, ++r)
        {
            ret[r] = ret[l];
        }

        return new string(ret);
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
                PALIN.Solve(Console.ReadLine()));
        }
    }
}
