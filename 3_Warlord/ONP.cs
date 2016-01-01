using System;

// Transform the Expression
// 4 http://www.spoj.com/problems/ONP/
// Returns the RPN form of the expression with binary operators and parentheses.
public static class ONP
{
    public static string Solve(string expression)
    {
        // expression is of the form a.
        if (expression.Length == 1)
            return expression;

        // expression is of the form (a_exp*b_exp). Transform to a_expb_exp*
        int lengthOfFirstSubexpression = LengthOfSubexpression(expression, 1);

        return Solve(expression.Substring(1, lengthOfFirstSubexpression)) // a_exp
            + Solve(expression.Substring(
                lengthOfFirstSubexpression + 2,
                expression.LengthStartingAt(lengthOfFirstSubexpression + 2) - 1)) // b_exp
            + expression.Substring(lengthOfFirstSubexpression + 1, 1); // *
    }

    private static int LengthOfSubexpression(string expression, int startIndex)
    {
        int currentIndex = startIndex;
        int unmatchedParentheses = 0;
        do
        {
            if (expression[currentIndex] == '(')
                ++unmatchedParentheses;
            else if (expression[currentIndex] == ')')
                --unmatchedParentheses;
            ++currentIndex;
        } while (unmatchedParentheses != 0);

        return currentIndex - startIndex;
    }

    private static int LengthStartingAt(this string value, int startIndex)
        => value.Length - startIndex;
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        while (remainingTestCases-- > 0)
        {
            Console.WriteLine(ONP.Solve(Console.ReadLine()));
        }
    }
}