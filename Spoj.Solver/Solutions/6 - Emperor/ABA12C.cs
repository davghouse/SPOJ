using System;

// https://www.spoj.com/problems/ABA12C/ #dynamic-programming-2d #knapsack #optimization
// Finds the cheapest way to buy an exact number of apples, without buying too many packs.
public static class ABA12C
{
    public static int? Solve(int friendCount, int appleCount, int[] applePackCosts)
    {
        // This stores the minimum cost to achieve a certain number of apples a, using no
        // more than p packs--minCostForApples[p, a]. The number we're looking for is
        // minCostForApples[friendCount, appleCount]--the minimum cost for appleCount apples,
        // using no more than friendCount packs. There's a restriction that we can't buy
        // more than friendCount packs, which is what distinguishes this problem from a basic
        // unbounded knapsack. The minCostForApples[p, a] is min of {minCostForApples using
        // p-1 packs over all the viable applePackCosts}.
        int?[,] minCostForApples = new int?[friendCount + 1, appleCount + 1];

        for (int p = 1; p <= friendCount; ++p)
        {
            // Buying 0 apples costs 0, no matter how many packs we're allowed to us.
            minCostForApples[p, 0] = 0;
        }

        for (int a = 1; a <= appleCount; ++a)
        {
            // For p=1, we can only use 1 pack. So if a pack of size a is available, it gets used.
            minCostForApples[1, a] = GetPackCost(applePackCosts, packSize: a);
        }

        for (int p = 2; p <= friendCount; ++p)
        {
            for (int a = 1; a <= appleCount; ++a)
            {
                int? minCost = null;

                for (int s = 1; s <= a; ++s)
                {
                    int? packCost = GetPackCost(applePackCosts, packSize: s);
                    if (!packCost.HasValue) continue;

                    // If the array value here is null the result will be null, which is what we want.
                    // In that case, we can't use the pack as there's no way to get the rest of the apples if we do.
                    int? minCostUsingThisPackSize = minCostForApples[p - 1, a - s] + packCost;

                    if (minCostUsingThisPackSize.HasValue)
                    {
                        minCost = Math.Min(
                            minCost ?? int.MaxValue,
                            minCostUsingThisPackSize.Value);
                    }
                }

                minCostForApples[p, a] = minCost;
            }
        }

        return minCostForApples[friendCount, appleCount];
    }

    private static int? GetPackCost(int[] applePackCosts, int packSize)
    {
        int packCost = applePackCosts[packSize - 1 /* 0-indexed */];
        return packCost == -1 ? (int?)null : packCost;
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
            int friendCount = int.Parse(line[0]);
            int appleCount = int.Parse(line[1]);

            int[] applePackCosts = Array.ConvertAll(
                Console.ReadLine().Split(default(char[]), StringSplitOptions.RemoveEmptyEntries),
                int.Parse);

            Console.WriteLine(
                ABA12C.Solve(friendCount, appleCount, applePackCosts) ?? -1);
        }
    }
}
