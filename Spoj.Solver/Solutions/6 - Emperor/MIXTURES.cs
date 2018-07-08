using System;

// https://www.spoj.com/problems/MIXTURES/ #dynamic-programming-2d #memoization #optimization
// Helps Harry mix potions in the way that produces the least amount of smoke.
public static class MIXTURES
{
    // Observation: the order the mixtures are mixed won't matter in the end. Adding up a bunch of numbers
    // mod 100 doesn't depend on the order it's done (associativity), so the color of the final mixture will be
    // the same regardless (which makes sense, the colors are adding together like paint does). So a recursive
    // solution: minimize--over each pair of adjacent mixtures--the (amount of smoke the pair produces)
    // + (amount of smoke necessary to mix the resultant sequence of mixtures, one fewer than before).
    // But after experimenting this doesn't lend itself to DP, as each subarray of the same size might be
    // different from each other. So another observation: eventually Harry will be left with two mixtures
    // that need mixing. Only adjacent mixtures can be mixed, so these two mixtures must've come from
    // two contiguous subarrays of the original array of mixtures, like [1...k][k + 1...n]. Remember,
    // the mixed color of those subarrays is the same no matter what, and it's those mixed colors that'll
    // determine the smoke amount (added to the smoke already produced) when mixing the final two mixtures.
    // So we can find the minimum smoke amount for [1...k] and [k + 1...n], add them together, add that to
    // the smoke amount of the final mixing, and know that's the minimum smoke amount possible. We don't
    // have to worry about doing [1...k] suboptimally to see if it ends up mixing more efficienly with what's next,
    // as it'll always mix in the same way. In order to find the optimal two subarrays we need to consider each
    // pair of them, and at this point the 2D DP in O(n^3) time is easy to see.
    // (After solving, the comments revealed this problem is similar to matrix chain multiplication--see Wikipedia).

    // [r, c, 0] = the minimum smoke amount for mixing the subarray [r, c]. [r, c, 1] = the resultant color.
    // To compute [0...n - 1], we need the answer for every subarray starting at zero ending before n - 1, and
    // ending at n - 1 starting before zero. So for a given cell [r, c], need all ← cells and all ↓ cells.
    private static int[,,] _minimumSmokeAmountAndResultantColor = new int[100, 100, 2];

    public static int Solve(int mixtureCount, int[] mixtureColors)
    {
        // Initialize along the diagonal (the smoke amount is already set to zero).
        for (int d = 0; d < mixtureCount; ++d)
        {
            _minimumSmokeAmountAndResultantColor[d, d, 1] = mixtureColors[d];
        }

        // While moving right across the columns, solve up from the diagonal.
        for (int c = 1; c < mixtureCount; ++c)
        {
            for (int r = c - 1; r >= 0; --r)
            {
                // Minimize over each pair of left-starting and right-ending subarrays.
                int minimumSmokeAmount = int.MaxValue;
                for (int rightStartIndex = r + 1; rightStartIndex <= c; ++rightStartIndex)
                {
                    int leftSmokeAmount = _minimumSmokeAmountAndResultantColor[r, rightStartIndex - 1, 0];
                    int leftColor = _minimumSmokeAmountAndResultantColor[r, rightStartIndex - 1, 1];
                    int rightSmokeAmount = _minimumSmokeAmountAndResultantColor[rightStartIndex, c, 0];
                    int rightColor = _minimumSmokeAmountAndResultantColor[rightStartIndex, c, 1];

                    minimumSmokeAmount = Math.Min(
                        minimumSmokeAmount,
                        leftSmokeAmount + rightSmokeAmount + leftColor * rightColor);
                }

                _minimumSmokeAmountAndResultantColor[r, c, 0] = minimumSmokeAmount;
                _minimumSmokeAmountAndResultantColor[r, c, 1] =
                    // Arbitrary the subarrays used here, just need to know what [r, c]'s color adds up to (same for all pairs).
                    (_minimumSmokeAmountAndResultantColor[r, c - 1, 1] + _minimumSmokeAmountAndResultantColor[c, c, 1]) % 100;
            }
        }

        return _minimumSmokeAmountAndResultantColor[0, mixtureCount - 1, 0];
    }
}

public static class Program
{
    private static void Main()
    {
        int mixtureCount;
        while (int.TryParse(Console.ReadLine(), out mixtureCount))
        {
            int[] mixtureColors = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            Console.WriteLine(
                MIXTURES.Solve(mixtureCount, mixtureColors));
        }
    }
}
