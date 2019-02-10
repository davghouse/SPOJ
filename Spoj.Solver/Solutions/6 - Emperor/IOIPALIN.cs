using System;

// https://www.spoj.com/problems/IOIPALIN/ #dynamic-programming-2d #optimization
// Finds the minimum number of character insertions needed to turn a string into a palindrome.
public static class IOIPALIN // Same problem as AIBOHP.
{
    // If the string starts and ends in the same characters, the insertion count is whatever
    // the count is for the substring excluding those two characters. It wouldn't make sense
    // to add characters before the start or after the end since (ignoring the case where they're
    // the same character as the start/end--because then just interpret the insert as inside,
    // not outside) a complementary character would have to be added on the other side. Once
    // the palindrome is found, all of characters before/after the original start/end could
    // be cutoff without any problem, so we know to never try adding them.
    // If the start/end aren't the same... we'll necessarily have to add a character on the
    // start or end at some point--that's the only way to reconcile the difference. We can
    // either add the start character to the end or the end character to the start. Once that
    // happens, the same reasoning shows we'd never want to add any characters before the
    // start or after the end, so the solution can be gotten by recursing on what's inside,
    // depending on where the insert happens. For example, take some string (the interior whatever):
    // a___b =>
    // Case where start inserted on the end: a___ba, so 1 + (solution for ___b) inserts needed.
    // Case where end inserted at the start: ba___b, so 1 + (solution for a___) inserts needed.
    // This recursive solution lends itself pretty naturally to dynamic programming, in a similar
    // way as TRT. That is, a 2D table with indices [r, c] corresponding to substrings solution,
    // the answer in the top right.
    public static int Solve(string s)
    {
        // Memory trick: we only need 2 columns, the one we're filling in and the previous.
        int[,] minimumInsertionCounts = new int[s.Length, 2];

        // The diagonal(s) are already initialized to zero properly; single/zero length strings
        // are palindromes. While moving right across the columns, solve up from the diagonal.
        for (int c = 1; c < s.Length; ++c)
        {
            int cNew = c % 2;
            int cPrev = (c + 1) % 2;

            for (int r = c - 1; r >= 0; --r)
            {
                if (s[r] == s[c])
                {
                    minimumInsertionCounts[r, cNew] = minimumInsertionCounts[r + 1, cPrev];
                }
                else
                {
                    minimumInsertionCounts[r, cNew] = 1 + Math.Min(
                        // Visualize inserting the start character at the end.
                        minimumInsertionCounts[r + 1, cNew],
                        // Visualize inserting the end character at the start.
                        minimumInsertionCounts[r, cPrev]);
                }
            }
        }

        // Insertion count for the full string is in the top right corner.
        return minimumInsertionCounts[0, (s.Length - 1) % 2];
    }
}

public static class Program
{
    private static void Main()
    {
        Console.ReadLine();

        Console.Write(
            IOIPALIN.Solve(Console.ReadLine()));
    }
}
