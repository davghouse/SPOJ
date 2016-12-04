using System;

// http://www.spoj.com/problems/EGYPIZZA/ #ad-hoc #division
// Orders pizza for people who need 1/4, 1/2, or 3/4 size slices (all from the same pie, single slices).
public static class EGYPIZZA
{
    public static int Solve(int quarterSliceCount, int halfSliceCount, int threeQuarterSliceCount)
    {
        // One whole pizza for the host.
        int pizzaCount = 1;

        // Every 3/4 slice demands a whole pizza, but can be paired off with a 1/4 slice.
        pizzaCount += threeQuarterSliceCount;
        quarterSliceCount = Math.Max(quarterSliceCount - threeQuarterSliceCount, 0);

        // Each 1/2 slice can be paired off with another 1/2 slice.
        pizzaCount += halfSliceCount / 2;
        halfSliceCount = halfSliceCount % 2;

        // The potentially one remaining 1/2 slice can be paired with up to two 1/4 slices.
        if (halfSliceCount == 1)
        {
            pizzaCount += 1;
            quarterSliceCount = Math.Max(quarterSliceCount - 2, 0);
        }

        // All that remains are some 1/4 slices, which may or may not fit into an exact number of pizzas.
        if (quarterSliceCount % 4 == 0)
        {
            pizzaCount += quarterSliceCount / 4;
        }
        else
        {
            pizzaCount += (quarterSliceCount / 4) + 1;
        }

        return pizzaCount;
    }
}

public static class Program
{
    private static void Main()
    {
        int friendCount = int.Parse(Console.ReadLine());

        int quarterSizedCount = 0, halfSizedCount = 0, threeQuarterSizedCount;
        for (int f = 0; f < friendCount; ++f)
        {
            string preference = Console.ReadLine();

            if (preference.Contains("1/4"))
            {
                ++quarterSizedCount;
            }
            else if (preference.Contains("1/2"))
            {
                ++halfSizedCount;
            }
        }
        threeQuarterSizedCount = friendCount - quarterSizedCount - halfSizedCount;

        Console.WriteLine(
            EGYPIZZA.Solve(quarterSizedCount, halfSizedCount, threeQuarterSizedCount));
    }
}
