using System;
using System.Collections.Generic;
using System.Linq;

// https://www.spoj.com/problems/ARITH2/ #ad-hoc #parsing #strings
// Parses a math expression and computes the result as if it were streamed in, ignoring precedence.
public static class ARITH2
{
    public static long Solve(string expression)
    {
        string[] spacelessSubexpressions = expression.Split(
            default(char[]), StringSplitOptions.RemoveEmptyEntries);
        string[] tokens = spacelessSubexpressions
            .SelectMany(s => s.SplitAndKeep(new[] { '+', '-', '*', '/', '=' }))
            .ToArray();

        // No negative integers simplifies things a bit. The input must be a number followed by
        // (an operator and a number) and so on, ending with the equals operator.
        long result = long.Parse(tokens[0]);
        for (int i = 1; i < tokens.Length - 2; ++i)
        {
            char @operator = tokens[i][0];
            long value = long.Parse(tokens[++i]);

            switch (@operator)
            {
                case '+': result += value; break;
                case '-': result -= value; break;
                case '*': result *= value; break;
                case '/': result /= value; break;
            }
        }

        return result;
    }
}

public static class StringHelper
{
    // The StringSplitOptions.RemoveEmptyEntries is implicit here.
    public static IEnumerable<string> SplitAndKeep(this string s, char[] delimiters)
    {
        int nextSubstringStartIndex = 0;
        while (nextSubstringStartIndex < s.Length)
        {
            int nextDelimiterIndex = s.IndexOfAny(delimiters, nextSubstringStartIndex);

            if (nextDelimiterIndex == nextSubstringStartIndex)
            {
                yield return s[nextSubstringStartIndex].ToString();
                ++nextSubstringStartIndex;
            }
            else if (nextDelimiterIndex > nextSubstringStartIndex)
            {
                yield return s.Substring(nextSubstringStartIndex,
                    length: nextDelimiterIndex - nextSubstringStartIndex);
                nextSubstringStartIndex = nextDelimiterIndex;
            }
            else // No next delimiter found; nextDelimiterIndex = -1.
            {
                yield return s.Substring(nextSubstringStartIndex);
                nextSubstringStartIndex = s.Length;
            }
        }
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            Console.ReadLine();

            Console.WriteLine(
                ARITH2.Solve(Console.ReadLine()));
        }
    }
}
