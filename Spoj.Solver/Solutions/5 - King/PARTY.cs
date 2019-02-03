using System;

// https://www.spoj.com/problems/PARTY/ #dynamic-programming-2d #knapsack #optimization
// Chooses parties to maximize fun for the given party budget (0/1 knapsack).
public static class PARTY
{
    // If you don't know 0/1 knapsack and need a hint, knowing the DP is two-dimensional
    // on the parties being considered and the amount of budget used might be enough.
    // (If we're using i parties for some budget, the maximum fun value is the max of the
    // value for i - 1 parties at that budget, and the value for i - 1 parties at [the
    // budget minus the ith party's fee] plus the fun value for the ith party.) We can't
    // just read the lower right corner of the table though, since we need the minimum
    // budget at which that maximum fun value occurs. The budget only goes to 500, so
    // doing a binary search or something isn't necessary; just use a linear search. Seems
    // obvious that this algorithm is correct (unlike EDIST).
    public static Tuple<int, int> Solve(int partyBudget, int partyCount, int[,] parties)
    {
        int[,] funValues = new int[partyCount, partyBudget + 1];
        int partyEntranceFee = parties[0, 0];
        int partyFunValue = parties[0, 1];

        // Initialize the first row in the table; only the first party is considered.
        for (int budget = 0; budget <= partyBudget; ++budget)
        {
            funValues[0, budget] = budget >= partyEntranceFee ? partyFunValue : 0;
        }

        for (int party = 1; party < partyCount; ++party)
        {
            partyEntranceFee = parties[party, 0];
            partyFunValue = parties[party, 1];

            for (int budget = 0; budget <= partyBudget; ++budget)
            {
                funValues[party, budget] = budget >= partyEntranceFee
                    // Budget is big enough that we can try going to this party and
                    // using the remaining budget for the previous parties.
                    ? Math.Max(
                        funValues[party - 1, budget - partyEntranceFee] + partyFunValue,
                        funValues[party - 1, budget])
                    : funValues[party - 1, budget];
            }
        }

        int minimumBudgetForMaximumFunValue = partyBudget;
        int maximumFunValue = funValues[partyCount - 1, partyBudget];
        while (minimumBudgetForMaximumFunValue - 1 >= 0
            && funValues[partyCount - 1, minimumBudgetForMaximumFunValue - 1] == maximumFunValue)
        {
            --minimumBudgetForMaximumFunValue;
        }

        return Tuple.Create(minimumBudgetForMaximumFunValue, maximumFunValue);
    }
}

public static class Program
{
    private static void Main()
    {
        while (true)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int partyBudget = line[0];
            int partyCount = line[1];
            if (partyBudget == 0 && partyCount == 0) return;

            int[,] parties = new int[partyCount, 2];
            for (int i = 0; i < partyCount; ++i)
            {
                line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                parties[i, 0] = line[0]; // entrance fee
                parties[i, 1] = line[1]; // fun value
            }

            var result = PARTY.Solve(partyBudget, partyCount, parties);
            Console.WriteLine($"{result.Item1} {result.Item2}");

            Console.ReadLine();
        }
    }
}
