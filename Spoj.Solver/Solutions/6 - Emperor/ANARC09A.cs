using System;
using System.Collections.Generic;

// https://www.spoj.com/problems/ANARC09A/ #dynamic-programming-x #greedy #recursion #stack
// Finds the minimum number of replacements needed to balance a string of brackets.
public static class ANARC09A
{
    public static int Solve(string s)
        => SolveUsingAStack(s);

    // This relies on a greedy strategy (I gave up and read the comments). Immediately
    // when a } is found, pair it off with a { if one exists (by removing that { from
    // the stack). {'s can only exist above all }'s on the stack. At the end we'll have
    // one of the following that can be dealt with while popping & replacing til empty:
    // {{{... or }}}... or }}}...}{...{{{ (note the transition point). The problem with
    // this approach is that I haven't shown it's optimal.
    private static int SolveUsingAStack(string s)
    {
        var unmatchedBrackets = new Stack<char>();

        foreach (char c in s)
        {
            if (c == '{')
            {
                unmatchedBrackets.Push('{');
            }
            else // '}'
            {
                if (unmatchedBrackets.Count > 0 && unmatchedBrackets.Peek() == '{')
                {
                    // Match the stack's { with the } being considered.
                    unmatchedBrackets.Pop();
                }
                else
                {
                    unmatchedBrackets.Push('}');
                }
            }
        }

        // We've matched everything we can greedily, so the stack is in one of the
        // three scenarios mentioned above. Match brackets with as few replacements
        // as possible from the top of the stack down.
        int replacementCount = 0;
        while (unmatchedBrackets.Count != 0)
        {
            char rightUnmatchedBracket = unmatchedBrackets.Pop();
            char leftUnmatchedBracket = unmatchedBrackets.Pop();

            replacementCount += ReplacementCountForAPairOfBrackets(
                leftUnmatchedBracket, rightUnmatchedBracket);
        }

        return replacementCount;
    }

    // Once we balance s it'll either be of the form s={t} or the form s=uv (there are
    // lots of possibilities). I think those parts can be treated independently from
    // the perspective of balancing... balancing within a part can't affect the balancing
    // outside the part. So a solution is to consider the min count for every possible form...
    // This could be turned into DP but it's O(n^3) so it's probably not what the problem
    // setter was looking for. It's here to sanity check the greedy solution for small test cases.
    private static int SolveRecursively(string s, int startIndex, int endIndex)
    {
        // Base case for a range of two characters.
        if (endIndex - startIndex == 1)
            return ReplacementCountForAPairOfBrackets(s[startIndex], s[endIndex]);

        // Otherwise, we need to try s={t}, and all the possibilies for s=uv, like
        // -u-|---v--- and  --u--|--v--.
        int countForSubstringForm = ReplacementCountForAPairOfBrackets(s[startIndex], s[endIndex])
            + SolveRecursively(s, startIndex + 1, endIndex - 1);

        int countForConcatenationForm = int.MaxValue;
        for (int rightSubstringStartIndex = startIndex + 2;
            rightSubstringStartIndex <= endIndex - 1;
            rightSubstringStartIndex += 2)
        {
            countForConcatenationForm = Math.Min(countForConcatenationForm,
                SolveRecursively(s, startIndex, rightSubstringStartIndex - 1)
                + SolveRecursively(s, rightSubstringStartIndex, endIndex));
        }

        return Math.Min(countForSubstringForm, countForConcatenationForm);
    }

    private static int ReplacementCountForAPairOfBrackets(char leftBracket, char rightBracket)
    {
        if (leftBracket == rightBracket) return 1; // Of the form {{ or }}.
        if (leftBracket == '}') return 2;          // Of the form }{.
        return 0;                                  // Of the form {}.
    }
}

public static class Program
{
    private static void Main()
    {
        int lineCounter = 1;
        string line;
        while ((line = Console.ReadLine())[0] != '-')
        {
            Console.WriteLine(
                $"{lineCounter++}. {ANARC09A.Solve(line.Trim())}");
        }
    }
}
