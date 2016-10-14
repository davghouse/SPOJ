using System;

// 39 http://www.spoj.com/problems/PIGBANK/ Piggy-Bank
// Finds the minimum  amount of money that could be inside a piggy bank of a certain weight.
public static class PIGBANK // v1, 1D as an unbounded knapsack problem.
{
    private static int?[] _minimumMoneyAmounts = new int?[10001];

    static PIGBANK()
    {
        // No matter our available coin types, a weight of zero is attainable by (and only by)
        // leaving the piggy bank empty, with the minimum (and only) money amount being zero.
        _minimumMoneyAmounts[0] = 0;
    }

    // 1D DP over the weight in the piggy bank, the answer in _minimumMoneyAmounts[totalCoinWeight].
    // A null value represents the weight is unattainable.
    public static int? Solve(int totalCoinWeight, int coinTypeCount, int[] coinTypeValues, int[] coinTypeWeights)
    {
        Array.Sort(coinTypeWeights, coinTypeValues);

        for (int tcw = 1; tcw <= totalCoinWeight; ++tcw)
        {
            int? minimumMoneyAmountForThisTotalWeight = null;

            // Try using a coin from each coin type that fits within this total coin weight. the coinTypeWeights
            // are sorted above so that we can break at the first too-heavy coin.
            for (int t = 0; t < coinTypeCount; ++t)
            {
                int coinTypeValue = coinTypeValues[t];
                int coinTypeWeight = coinTypeWeights[t];
                if (coinTypeWeight > tcw) break;

                // If the array value here is null the result will be null, which is what we want.
                // In that case, we can't use the coin type as there's no way to get the rest of the weight if we do.
                int? minimumMoneyAmountUsingThisCoinType = _minimumMoneyAmounts[tcw - coinTypeWeight] + coinTypeValue;

                if (minimumMoneyAmountUsingThisCoinType.HasValue)
                {
                    if (minimumMoneyAmountForThisTotalWeight.HasValue)
                    {
                        minimumMoneyAmountForThisTotalWeight = Math.Min(
                            minimumMoneyAmountForThisTotalWeight.Value,
                            minimumMoneyAmountUsingThisCoinType.Value);
                    }
                    else
                    {
                        minimumMoneyAmountForThisTotalWeight = minimumMoneyAmountUsingThisCoinType;
                    }
                }
            }

            _minimumMoneyAmounts[tcw] = minimumMoneyAmountForThisTotalWeight;
        }

        return _minimumMoneyAmounts[totalCoinWeight];
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        while (remainingTestCases-- > 0)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int emptyPigWeight = line[0];
            int moniedPigWeight = line[1];

            int coinTypeCount = int.Parse(Console.ReadLine());
            int[] coinTypeValues = new int[coinTypeCount];
            int[] coinTypeWeights = new int[coinTypeCount];

            for (int t = 0; t < coinTypeCount; ++t)
            {
                line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                coinTypeValues[t] = line[0];
                coinTypeWeights[t] = line[1];
            }

            int? minimumMoneyAmount = PIGBANK.Solve(moniedPigWeight - emptyPigWeight, coinTypeCount, coinTypeValues, coinTypeWeights);
            Console.WriteLine(minimumMoneyAmount.HasValue
                ? $"The minimum amount of money in the piggy-bank is {minimumMoneyAmount}."
                : "This is impossible.");
        }
    }
}
