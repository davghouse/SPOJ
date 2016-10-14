using System;
using System.Collections.Generic;
using System.Linq;

// 4452 http://www.spoj.com/problems/ARITH2/ Simple Arithmetics II
// Parses a math expression and computes the result as if it were streamed in, ignoring precedence.
public static class ARITH2
{
    public static long Solve(string expression)
    {
        string[] spacelessSubexpressions = expression.Split(default(char[]), StringSplitOptions.RemoveEmptyEntries);
        string[] tokens = spacelessSubexpressions
            .SelectMany(s => s.SplitAndKeep(new[] { '+', '-', '*', '/', '=' }))
            .ToArray();

        // No negative integers simplifies things a bit. The input must be a number followed by
        // (an operator and a number) and so on, ending with the equals operator.
        long result = long.Parse(tokens[0]);
        for (int i = 1; i < tokens.Length - 2; ++i)
        {
            string @operator = tokens[i];
            long value = long.Parse(tokens[++i]);

            if (@operator == "+") result += value;
            else if (@operator == "-") result -= value;
            else if (@operator == "*") result *= value;
            else if (@operator == "/") result /= value;
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
                yield return s.Substring(nextSubstringStartIndex, nextDelimiterIndex - nextSubstringStartIndex);
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
