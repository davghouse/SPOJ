using System;
using System.Text;

// 6171 http://www.spoj.com/problems/MAJOR/ Majority
// Determines if any data is properly transmitted, i.e., the majority of what's sent.
public static class MAJOR // v2, using the Boyer-Moore algorithm and two passes.
{
    // https://en.wikipedia.org/wiki/Boyer%E2%80%93Moore_majority_vote_algorithm
    public static string Solve(string[] transmissions)
    {
        // Attempt to get the majority transmission.
        string winningTransmission = null;
        for (int t = 0, counter = 0; t < transmissions.Length; ++t)
        {
            if (counter == 0)
            {
                winningTransmission = transmissions[t];
                counter = 1;
            }
            else if (winningTransmission == transmissions[t])
            {
                ++counter;
            }
            else
            {
                --counter;
            }
        }

        // Verify it's actually a majority transmission.
        int remainingCountNeededForMajority = (transmissions.Length / 2) + 1;
        for (int t = 0; t < transmissions.Length; ++t)
        {
            if (transmissions[t] == winningTransmission && --remainingCountNeededForMajority == 0)
                return $"YES {winningTransmission}";
        }

        return "NO";
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        var output = new StringBuilder();

        while (remainingTestCases-- > 0)
        {
            Console.ReadLine(); // transmissionCount
            string[] transmissions = Console.ReadLine().Split();

            output.AppendLine(
                MAJOR.Solve(transmissions));
        }

        Console.Write(output);
    }
}
