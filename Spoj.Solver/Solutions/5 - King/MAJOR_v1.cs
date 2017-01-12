using System;
using System.Collections.Generic;
using System.Text;

// http://www.spoj.com/problems/MAJOR/ #ad-hoc #extrema
// Determines if any number makes up the majority of all the numbers sent.
public static class MAJOR // v1, using a dictionary and no more than one full pass.
{
    // TLE is strict here, don't even have time to parse the transmissions into ints.
    // Don't have time to sort and traverse the array either, but dictionary of counts
    // might work. And we can optimize that a bit, keeping track of the most counted
    // so far, the count remaining, and short-circuiting if even if all of the remaining
    // transmissions go to the most so far, it still wouldn't become a majority. And
    // keeping track of the most counted as we go means we don't have to do an O(n) lookup
    // once we're done, and we can short-circuit again as soon as it becomes a majority.
    // I get the worst time (AC/TLE mixed) using this so there's probably a better way (see v2).
    public static string Solve(string[] transmissions)
    {
        // For example, 8 / 2 + 1 = 5, 7 / 2 + 1 = 4.
        int majorityCount = (transmissions.Length / 2) + 1;

        // If there were majorityCount unique transmissions, there'd be no way for a
        // a single transmission to have majorityCount occurrences (since then the total
        // transmission count would exceed how many transmissions there actually are).
        // So, we'll short-circuit before we can ever get more than majorityCount
        // keys in this dictionary, so we only need to reserve a capacity of majorityCount.
        // Actually that's only true for an even total count, so should add 1, but add 2 for safety.
        var transmissionCounts = new Dictionary<string, int>(majorityCount + 2) { { transmissions[0], 1 } };
        string mostCountedTransmission = transmissions[0];
        int mostCountedTransmissionCount = 1;

        for (int t = 1; t < transmissions.Length; ++t)
        {
            // If the most counted so far plus all the remaining doesn't get a majority, we're done.
            if (mostCountedTransmissionCount + (transmissions.Length - t) < majorityCount)
                break;

            string transmission = transmissions[t];
            int transmissionCount;

            if (transmissionCounts.TryGetValue(transmission, out transmissionCount))
            {
                transmissionCounts[transmission] = ++transmissionCount;

                if (transmissionCount > mostCountedTransmissionCount)
                {
                    mostCountedTransmission = transmission;
                    mostCountedTransmissionCount = transmissionCount;

                    // If the most counted just became the majority, we're done.
                    if (mostCountedTransmissionCount >= majorityCount)
                        break;
                }
            }
            else
            {
                transmissionCounts[transmission] = 1;
            }
        }

        return mostCountedTransmissionCount < majorityCount
            ? "NO" : $"YES {mostCountedTransmission}";
    }
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            Console.ReadLine();
            string[] transmissions = Console.ReadLine().Split();

            output.AppendLine(
                MAJOR.Solve(transmissions));
        }

        Console.Write(output);
    }
}
