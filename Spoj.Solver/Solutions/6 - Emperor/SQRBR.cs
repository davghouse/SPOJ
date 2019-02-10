using System;
using System.Linq;

// https://www.spoj.com/problems/SQRBR/ #dynamic-programming-2d
// Calculates how many valid bracket expressions there are.
public static class SQRBR
{
    // First I solved the problem where there are no brackets that must be open.
    // Full solution is easy from that, just throw in mustBeOpeningBracket checks.
    // Given a range r to c where we want to find the total expression count,
    // observe that r must be an open bracket. Then, the closing bracket matching
    // to r can be anywhere from r + 1 to c, like this:
    // [ ] _ _ _ _ _ _
    // [ _ _ ] _ _ _ _
    // [ _ _ _ _ ] _ _
    // [ _ _ _ _ _ _ ]
    // The contribution from the first is however many expressions exist for
    // the trailing range, for the second it's the inner range count * trailing
    // range count (the two ranges are independent), same for third, and for
    // fourth it's the count for the inner range.
    public static int Solve(int n, int[] openingBrackets)
    {
        n = 2 * n;

        int[,] expressionCounts = new int[n + 1, n + 1];
        bool[] mustBeOpeningBracket = Enumerable.Range(0, n + 1)
            .Select(openingBrackets.Contains)
            .ToArray();

        // Initialize along the diagonal--these are the expressions of length 2,
        // the first one being at index 1 and 2, and the next at index 2 and 3.
        for (int r = 1; r < n; ++r)
        {
            expressionCounts[r, r + 1] =
                mustBeOpeningBracket[r + 1] ? 0 : 1;
        }

        // Fill the table in, travelling across the columns and up from the diagonal.
        // For a given column c, we've already filled in r = c - 1, so we start from
        // c - 3 and travel up until we reach r = 1. (We know odd length ranges have
        // no valid expressions, so don't bother computing--decrement r by 2 each time.)
        for (int c = 1; c <= n; ++c)
        {
            for (int r = c - 3; r >= 1; r -= 2)
            {
                // Now we're solving the range r to c. There has to be an open bracket
                // at r matched to some closing bracket. It could be matched to any
                // closing bracket from r + 1 to c. If it's matched to r + 1, we get
                // this contribution from the range to the right, [ ] _ _ _ _:
                expressionCounts[r, c] +=
                    mustBeOpeningBracket[r + 1] ? 0 : expressionCounts[r + 2, c];
                // If it's matched to c, we get this from the inner range, [ _ _ _ _ ]:
                expressionCounts[r, c] +=
                    mustBeOpeningBracket[c] ? 0 : expressionCounts[r + 1, c - 1];

                // Otherwise, we have a situation like [ _ _ ] _ _, where there are two
                // ranges to consider, one within the brackets, and one to the right.
                // These ranges are independent, so we multiply their counts together.
                // Also, we increment by 2 for the same reason as above--odd ranges have 0.
                for (int b = r + 3; b <= c - 2; b += 2)
                {
                    if (mustBeOpeningBracket[b])
                        continue;

                    expressionCounts[r, c] +=
                        expressionCounts[r + 1, b - 1] * expressionCounts[b + 1, c];
                }
            }
        }

        return expressionCounts[1, n];
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            string[] line = Console.ReadLine().Split();
            int n = int.Parse(line[0]);
            int k = int.Parse(line[1]);
            int[] openingBrackets = Array.ConvertAll(
                Console.ReadLine().Trim().Split(),
                int.Parse);

            Console.WriteLine(
                SQRBR.Solve(n, openingBrackets));
        }
    }
}
