using System;

// https://www.spoj.com/problems/ONP/ #parsing #recursion #stack #strings
// Returns the RPN form of the expression with binary operators and parentheses.
public static class ONP
{
    public static string Solve(string expression)
    {
        // expression is some constant.
        if (expression.Length == 1)
            return expression;

        // expression is of the form (firstSubexpression@secondSubexpression), where @ represents an arbitrary operator.
        // Now RPN(expression) = RPN(firstSubexpression)RPN(secondSubexpression)@, so our job is just to identify
        // the substrings of the two subexpressions, and solve recursively.

        // Starts at index 1, immediately after the ( at the 0th index, and has some calculated length.
        string firstSubexpression = expression.Substring(
            startIndex: 1,
            length: GetSubexpressionLength(expression, subexpressionStartIndex: 1));

        // Starts at the index after (firstSubexpression@, and runs up to but not including the ending ).
        string secondSubexpression = expression.Substring(
            startIndex: 1 + firstSubexpression.Length + 1,
            length: expression.Length - (1 + firstSubexpression.Length + 1) - 1);

        char @operator = expression[1 + firstSubexpression.Length];

        return Solve(firstSubexpression) + Solve(secondSubexpression) + @operator;
    }

    private static int GetSubexpressionLength(string expression, int subexpressionStartIndex)
    {
        int currentIndex = subexpressionStartIndex;
        int unmatchedParentheses = 0;
        do
        {
            if (expression[currentIndex] == '(')
            {
                ++unmatchedParentheses;
            }
            else if (expression[currentIndex] == ')')
            {
                --unmatchedParentheses;
            }
            ++currentIndex;
        } while (unmatchedParentheses != 0);

        return currentIndex - subexpressionStartIndex;
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
                ONP.Solve(Console.ReadLine()));
        }
    }
}
