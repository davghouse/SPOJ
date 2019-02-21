using System;

// https://www.spoj.com/problems/EDIST/ #dynamic-programming-2d #strings
// Finds the minimum number of edits (inserts, deletes, replacements) to make two strings equal.
public static class EDIST
{
    // This is just solved in the standard way using the Wagner-Fischer algorithm.
    // Note that proceeds from the perspective of transforming a into b rather than
    // transforming a and b into a common third string c as this problem permits.
    // Sufficient though, as assume second way is faster, so a -> c in n, b -> c in m,
    // n + m total. But then a -> b in n + m, since a -> c in n, to c -> b in m (each
    // transformation is reversible).
    public static int Solve(string from, string to)
    {
        // Haven't found/created a proof for the WF algorithm being correct (that is, optimal).
        // We'll just assume it is, and do the space-efficient version as we don't need to
        // backtrace. The from string is being built downward as the row increases, and being
        // made to equal the substring of the to string, being built out to the right as the
        // column increases for a row.
        int[] previousRowCounts = new int[to.Length + 1];
        int[] currentRowCounts = new int[to.Length + 1];
        int row = 1;

        for (int col = 0; col <= to.Length; ++col)
        {
            previousRowCounts[col] = col;
        }

        while (row <= from.Length)
        {
            currentRowCounts[0] = row;

            for (int column = 1; column <= to.Length; ++column)
            {
                int countFromDeletion = previousRowCounts[column] + 1;
                int countFromInsertion = currentRowCounts[column - 1] + 1;
                int countFromReplacement = previousRowCounts[column - 1]
                    + (from[row - 1] == to[column - 1] ? 0 : 1);

                currentRowCounts[column] = Math.Min(countFromDeletion,
                    Math.Min(countFromInsertion, countFromReplacement));
            }

            var temp = previousRowCounts;
            previousRowCounts = currentRowCounts;
            currentRowCounts = temp;
            ++row;
        }

        return previousRowCounts[to.Length];
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
                EDIST.Solve(Console.ReadLine(), Console.ReadLine()));
        }
    }
}
