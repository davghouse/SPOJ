using System;

// https://www.spoj.com/problems/M3TILE/ #ad-hoc #dynamic-programming #recursion
// Calculates the number of tilings for a 3xn grid using 2x1 rectangles.
public static class M3TILE
{
    private static readonly int[] _tilingCounts = new int[31];

    static M3TILE()
    {
        // Empty grid is considered to have 1 tiling.
        _tilingCounts[0] = 1;

        // >>      ^^      >>
        // >>  or  ^^  or  ^^
        // >>      >>      ^^
        _tilingCounts[2] = 3;

        // Now's the tricky part. How do we use previous tiling counts to calculate
        // the tiling count for n? Say we know n - 2. There are 3 ways to tile a 3x2
        // grid as the base case shows. The tilings for n - 2 and the 3 for the new
        // 3x2 part of the grid are independent, so we'll have at least c(n - 2) * 3
        // tilings for n. But the tilings for n - 2 are all smooth, none of their
        // tiles stick out into the new 3x2 part for n. Play around on paper a bit
        // and you can see there are only 2 ways for tiles to stick into the 3x2
        // part while still allowing us to tile it exactly (and while not being
        // redundant with the tilings we've already found). They're like this:
        // |>       |
        // |>  and  |>
        // |        |>
        // There's only 1 way to fill in the remaining 4 tiles for those (L-shape).
        // Call those two top and bottom, c(n) = c(n - 2) * 3 + c(top) + c(bottom).
        // It's important to understand that the tilings from those 3 sets are all
        // different, and that the new tilings we're creating for n are too.

        // The base case for the top and bottom jaggeds before n = 4 looks like:
        // ^>>   >>
        // ^>>   ^>>
        // >>    ^>>
        int topJaggedCount = 1;
        int bottomJaggedCount = 1;

        // Note that +=2 because it's not possible to tile grids where n is odd.
        // Such grids have an odd number of cells, and 2x1s can only make even.
        for (int n = 4; n <= 30; n += 2)
        {
            _tilingCounts[n] = _tilingCounts[n - 2] * 3
                + topJaggedCount
                + bottomJaggedCount;

            // Any tilings that are already jagged can be easily extended to be jagged
            // for the next iteration. But we can also get brand new jaggeds by appending
            // the jagged base cases above to the n - 2 non-jagged tilings.
            topJaggedCount += _tilingCounts[n - 2];
            bottomJaggedCount += _tilingCounts[n - 2];
        }
    }

    public static int Solve(int n)
        => _tilingCounts[n];
}

public static class Program
{
    private static void Main()
    {
        int n;
        while ((n = int.Parse(Console.ReadLine())) != -1)
        {
            Console.WriteLine(
                M3TILE.Solve(n));
        }
    }
}
