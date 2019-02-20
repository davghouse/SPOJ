using System;

// https://www.spoj.com/problems/PIGBANK/ #dynamic-programming-2d #knapsack #optimization
// Finds the minimum amount of money that could be inside a piggy bank of a certain weight.
public static class PIGBANK // v2, 2D structured similarly as 0/1 knapsack.
{
    private static int?[,] _minimumMoneyAmounts = new int?[501, 10001];

    static PIGBANK()
    {
        // No matter our available coin types, a weight of zero is attainable by (and only by)
        // leaving the piggy bank empty, with the minimum (and only) money amount being zero.
        for (int ctc = 0; ctc <= 500; ++ctc)
        {
            _minimumMoneyAmounts[ctc, 0] = 0;
        }
    }

    // 2D DP over the coins considered (rows) and weight in the piggy bank (columns).
    // The answer is in _minimumMoneyAmounts[coinTypeCount, totalCoinWeight], the lower
    // right hand corner, where all the coin types and the total coin weight are considered.
    // A null value represents the weight is unattainable using the given coin types.
    public static int? Solve(
        int totalCoinWeight, int coinTypeCount, int[] coinTypeValues, int[] coinTypeWeights)
    {
        for (int ctc = 1; ctc <= coinTypeCount; ++ctc)
        {
            int coinTypeValue = coinTypeValues[ctc - 1];
            int coinTypeWeight = coinTypeWeights[ctc - 1];

            for (int tcw = 1; tcw <= totalCoinWeight; ++tcw)
            {
                int? minimumMoneyAmountWithoutUsingThisCoinType = _minimumMoneyAmounts[ctc - 1, tcw];
                int? minimumMoneyAmountUsingThisCoinType = coinTypeWeight <= tcw
                    // Note the behavior of null addition here; null + anything is null, which we
                    // want. If that array value is still null, that weight isn't attainable up to
                    // these coin types.
                    ? _minimumMoneyAmounts[ctc, tcw - coinTypeWeight] + coinTypeValue : null;

                if (minimumMoneyAmountWithoutUsingThisCoinType.HasValue
                    && minimumMoneyAmountUsingThisCoinType.HasValue)
                {
                    _minimumMoneyAmounts[ctc, tcw] = Math.Min(
                        minimumMoneyAmountWithoutUsingThisCoinType.Value,
                        minimumMoneyAmountUsingThisCoinType.Value);
                }
                else
                {
                    // They don't both have values, so coalesce here as it'll maintain nulls but pick
                    // up any non-null.
                    _minimumMoneyAmounts[ctc, tcw] = minimumMoneyAmountWithoutUsingThisCoinType
                        ?? minimumMoneyAmountUsingThisCoinType;
                }
            }
        }

        return _minimumMoneyAmounts[coinTypeCount, totalCoinWeight];
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

            int? minimumMoneyAmount = PIGBANK.Solve(
                moniedPigWeight - emptyPigWeight, coinTypeCount, coinTypeValues, coinTypeWeights);
            Console.WriteLine(minimumMoneyAmount.HasValue
                ? $"The minimum amount of money in the piggy-bank is {minimumMoneyAmount}."
                : "This is impossible.");
        }
    }
}
