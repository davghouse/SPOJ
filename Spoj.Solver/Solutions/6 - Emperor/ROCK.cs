using System;

// https://www.spoj.com/problems/ROCK/ #dynamic-programming-2d
// Finds out how much length of a sweet/sour stick can be sold.
public static class ROCK
{
    // Subproblems are the usable length for a range of stick [r, c]. We can solve [r, c]
    // by noting that if there are more sweet than sour in the range, the whole range is
    // usable. Otherwise, it's necessary to break the range up at least once. So consider
    // all first break points, [r, r] U [r + 1, c] to [r, c - 1] U [c, c]. Choose the
    // breakpoint that maximizes sum of the usable lengths across the two parts. We will
    // have all the subranges necessary by solving across the columns, up from the diagonal.
    public static int Solve(string stick)
    {
        int[,] usableLengths = new int[stick.Length, stick.Length];

        // Initialize along the diagonal.
        for (int d = 0; d < stick.Length; ++d)
        {
            usableLengths[d, d] = stick[d] == '1' ? 1 : 0;
        }

        // While moving right across the columns, solve up from the diagonal.
        for (int c = 1; c < stick.Length; ++c)
        {
            for (int r = c - 1; r >= 0; --r)
            {
                int sweetSourDifference = 0;
                for (int i = r; i <= c; ++i)
                {
                    if (stick[i] == '1') ++sweetSourDifference;
                    else --sweetSourDifference;
                }

                int usableLength = 0;
                if (sweetSourDifference > 0)
                {
                    // The whole range is usable.
                    usableLength = c - r + 1;
                }
                else
                {
                    // The range must have at least one break point--find the best,
                    // knowing that we already have the answer to the subranges.
                    for (int i = r; i < c; ++i)
                    {
                        usableLength = Math.Max(
                            usableLength,
                            usableLengths[r, i] + usableLengths[i + 1, c]);
                    }
                }

                usableLengths[r, c] = usableLength;
            }
        }

        return usableLengths[0, stick.Length - 1];
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            Console.ReadLine();

            Console.WriteLine(
                ROCK.Solve(Console.ReadLine()));
        }
    }
}
