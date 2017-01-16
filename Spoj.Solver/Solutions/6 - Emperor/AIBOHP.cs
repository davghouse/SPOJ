using System;

// http://www.spoj.com/problems/AIBOHP/ #dynamic-programming-2d #optimization
// Finds the minimum number of character insertions needed to turn a string into a palindrome.
public static class AIBOHP // bottom-up, dynamic programming with tabulation
{
    private static int[,] minimumInsertionCounts = new int[6100, 6100];

    // If the string starts and ends in the same characters, the insertion count is whatever
    // the count is for the substring excluding those two characters. It wouldn't make sense
    // to add characters before the start or after the end since (ignoring the case where they're
    // the same character as the start/end--because then just interpret the insert as inside, not outside)
    // a complementary character would have to be added on the other side. Once the palindrome is found,
    // all of characters before/after the original start/end could be cutoff without any problem, so we
    // know to never try adding them.
    // If the start/end aren't the same... we'll necessarily have to add a character on the start or end
    // at some point--that's the only way to reconcile the difference. We can either add the start character
    // to the end or the end character to the start. Once that happens, the same reasoning shows we'd never
    // want to add any characters before the start or after the end, so the solution can be gotten by recursing
    // on what's inside, depending on where the insert happens. For example, take some string (the interior whatever):
    // a___b =>
    // Case where start inserted on the end: a___ba, so 1 + (solution for ___b) inserts needed.
    // Case where end inserted at the start: ba___b, so 1 + (solution for a___) inserts needed.
    // This recursive solution lends itself pretty naturally to dynamic programming, in a similar way as TRT.
    // That is, a 2D table with indices [i, j] corresponding to substrings solution, the answer in the top right.
    public static int Solve(string s)
    {
        // The diagonal(s) are already initialized to zero properly; single/zero length strings are palindromes.
        // While moving right across the columns, solve up from the diagonal.
        for (int cj = 1; cj < s.Length; ++cj)
        {
            for (int ri = cj - 1; ri >= 0; --ri)
            {
                if (s[ri] == s[cj])
                {
                    minimumInsertionCounts[ri, cj] = minimumInsertionCounts[ri + 1, cj - 1];
                }
                else
                {
                    minimumInsertionCounts[ri, cj] = 1 + Math.Min(
                        minimumInsertionCounts[ri + 1, cj],  // Visualize inserting the start character at the end.
                        minimumInsertionCounts[ri, cj - 1]); // Visualize inserting the end character at the start.
                }
            }
        }

        // Return the insertion count for the full string, which is in the top right corner of the table.
        return minimumInsertionCounts[0, s.Length - 1];
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
                AIBOHP.Solve(Console.ReadLine()));
        }
    }
}
