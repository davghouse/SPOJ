using System;

// http://www.spoj.com/problems/PARTY/ #dynamic-programming-2d #knapsack #optimization
// Chooses parties to maximize fun for the given party budget (0/1 knapsack).
public static class PARTY
{
    // If you don't know 0/1 knapsack and need a hint, knowing the DP is two-dimensional
    // on the parties being considered and the amount of budget used should be enough.
    // (If we're using i parties for some budget, the maximum fun value is the max of the value for i - 1
    // parties at that budget, and the value for i - 1 parties at [the budget minus the ith party's fee]
    // plus the fun value for the ith party.) We can't just read the lower right corner of the table
    // though, since we need the minimum budget at which that maximum fun value occurs. The budget only
    // goes to 500, so doing a binary search or something isn't necessary; just use a linear search.
    // Seems obvious that this algorithm is correct (unlike EDIST).
    public static Tuple<int, int> Solve(int partyBudget, int partyCount, int[,] partyEntranceFeesAndFunValues)
    {
        // The number of parties index is zero-based but the party budget index corresponds to the available party budget.
        int[,] partyAndBudgetTableForFunValues = new int[partyCount, partyBudget + 1];
        int nextPartyEntranceFee = partyEntranceFeesAndFunValues[0, 0];
        int nextPartyFunValue = partyEntranceFeesAndFunValues[0, 1];

        // Initialize the first row in the table; only the first party (the zeroth) is considered.
        for (int availableBudget = 0; availableBudget <= partyBudget; ++availableBudget)
        {
            partyAndBudgetTableForFunValues[0, availableBudget] = availableBudget >= nextPartyEntranceFee
                ? nextPartyFunValue
                : 0;
        }

        for (int nextParty = 1; nextParty < partyCount; ++nextParty)
        {
            nextPartyEntranceFee = partyEntranceFeesAndFunValues[nextParty, 0];
            nextPartyFunValue = partyEntranceFeesAndFunValues[nextParty, 1];

            for (int availableBudget = 0; availableBudget <= partyBudget; ++availableBudget)
            {
                partyAndBudgetTableForFunValues[nextParty, availableBudget] = availableBudget >= nextPartyEntranceFee
                    // Budget is big enough that we can try going to this party and using the remaining budget for the previous parties.
                    ? Math.Max(
                        partyAndBudgetTableForFunValues[nextParty - 1, availableBudget - nextPartyEntranceFee] + nextPartyFunValue,
                        partyAndBudgetTableForFunValues[nextParty - 1, availableBudget])
                    : partyAndBudgetTableForFunValues[nextParty - 1, availableBudget];
            }
        }

        int minimumBudgetForMaximumFunValue = partyBudget;
        int maximumFunValue = partyAndBudgetTableForFunValues[partyCount - 1, partyBudget];
        while (minimumBudgetForMaximumFunValue - 1 >= 0
            && partyAndBudgetTableForFunValues[partyCount - 1, minimumBudgetForMaximumFunValue - 1] == maximumFunValue)
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

            int[,] partyEntranceFeesAndFunValues = new int[partyCount, 2];

            for (int i = 0; i < partyCount; ++i)
            {
                line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
                partyEntranceFeesAndFunValues[i, 0] = line[0]; // entrance fee
                partyEntranceFeesAndFunValues[i, 1] = line[1]; // fun value
            }

            Tuple<int, int> budgetAndFunValueResult = PARTY.Solve(partyBudget, partyCount, partyEntranceFeesAndFunValues);
            Console.WriteLine($"{budgetAndFunValueResult.Item1} {budgetAndFunValueResult.Item2}");

            Console.ReadLine();
        }
    }
}
