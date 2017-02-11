using System;

// 8785 http://www.spoj.com/problems/SILVER/ Cut the Silver Bar
// Finds the way to pay someone a silver a day with the fewest cuts.
public static class SILVER
{
    // First, note there could be a lot going on here (cuts on what's been cut) so DP or recursion
    // is probably out and there's a better solution. Then try a few examples  by hand, progressing
    // naturally/greedily, following certain guidelines that make sense like: only cut when needed,
    // always take back as many cuts from the creditor as possible so you have more options to work
    // with next day, etc. Here's a run I did for 10 days:
    // 1: 1        | 9, cut a 1
    // 2: 2        | 1 7, cut a 2
    // 3: 2 1      | 7,
    // 4: 4        | 1 2 3, cut a 4
    // 5: 4 1      | 2 3,
    // 6: 4 2      | 1 3,
    // 7: 4 3      | 1 2,
    // 8: 4 3 1    | 2,
    // 9: 4 3 2    | 1,
    // 10: 4 3 2 1.
    // Then I did 15 and 16 which brought out the binary pattern even more clearly. For example,
    // for 10 we need to get 3 'bits' of silver, 1, 2, 4, then, thinking in binary, we can represent
    // any number from 001 (1) to 111 (7). That is, by giving the creditor the appropriate
    // combination of 3 bits, we can satisfy him through the first 7 days. The 3 cuts needed for 1, 2
    // and 4 leave a 3 leftover bit, so we can use that too, in combination with the 7 combo, to satisfy from
    // day 3 to 10. 3 cuts for 1, 2, 4 will satify up to a 15 day debt. For 15, there'd be an 8 leftover bit.
    // We use the bits for days 1 to 7, then 8 for day 8, and then use 1 to 7 again for the remainder. Pattern:
    // 1         = 0 cuts
    // 2 to 3    = 1 cuts (1)
    // 4 to 7    = 2 cuts (1, 2)
    // 8 to 15   = 3 cuts (1, 2, 4)
    // 16 to 31  = 4 cuts (1, 2, 4, 8) (...leftover can be 1 more than what we can represent with our cut bits.)
    // So that's a new upper bound, down from what we know would've worked, which is cutting a new piece every day.
    // To prove it's optimal, say we only needed 2 cuts for 8. Then with 3 bits total (one from the leftover),
    // we'd have to represent the numbers 1 through 8 somehow. That is, we'd have to give the creditor a certain
    // combination of those 3 bits each day (like first and third bit on the 3rd day), with each day's combination's
    // value corresponding to the day number. The creditor either has a bit or doesn't, so we're seeing why binary
    // is correct. If he either has a bit or doesn't, there are 2 * 2 * 2 - 1 possible combinations with 3 bits, the
    // minus 1 because he has to have at least one bit, otherwise we wouldn't have given him anything. But that's only 7,
    // which is less than the 8 distinct values we need for each different day.
    public static int Solve(int n)
        // Just worried about rounding errors, don't know enough about how those work. But:
        // Limit is 20k, 16384 largest power of 2 < 20k, log2(16383) = 13.99991..., so this epsilon is small enough.
        => (int)(Math.Log(n, 2) + .000000001);
}

public static class Program
{
    private static void Main()
    {
        int n;
        while ((n = int.Parse(Console.ReadLine())) != 0)
        {
            Console.WriteLine(
                SILVER.Solve(n));
        }
    }
}
