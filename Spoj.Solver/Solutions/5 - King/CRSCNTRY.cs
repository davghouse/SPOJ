using System;

// https://www.spoj.com/problems/CRSCNTRY/ #dynamic-programming-2d
// Finds out how many times Tom can creep on Agnes if he chooses his race card correctly.
public sealed class CRSCNTRY
{
    private readonly int _agnesCheckpointCount;
    private readonly int[] _agnesCheckpoints;

    public CRSCNTRY(int agnesCheckpointCount, int[] agnesCheckpoints)
    {
        _agnesCheckpointCount = agnesCheckpointCount;
        _agnesCheckpoints = agnesCheckpoints;
    }

    public int MostCheckpointOverlaps { get; set; }

    // Since Tom can run as fast or slow as he needs to, this is longest common subsequence:
    // https://en.wikipedia.org/wiki/Longest_common_subsequence_problem
    public void ConsiderNewRaceCard(int raceCardCheckpointCount, int[] raceCardCheckpoints)
    {
        if (raceCardCheckpointCount <= MostCheckpointOverlaps)
            return; // Not enough checkpoints to even bother trying.

        int[,] mostCheckpointOverlaps = new int[_agnesCheckpointCount + 1, raceCardCheckpointCount + 1];

        for (int r = 1, ac = 0; r <= _agnesCheckpointCount; ++r, ++ac)
        {
            for (int c = 1, rcc = 0; c <= raceCardCheckpointCount; ++c, ++rcc)
            {
                mostCheckpointOverlaps[r, c] = _agnesCheckpoints[ac] == raceCardCheckpoints[rcc]
                    ? 1 + mostCheckpointOverlaps[r - 1, c - 1]
                    : Math.Max(mostCheckpointOverlaps[r - 1, c], mostCheckpointOverlaps[r, c - 1]);
            }
        }

        int result = mostCheckpointOverlaps[_agnesCheckpointCount, raceCardCheckpointCount];

        if (MostCheckpointOverlaps < result)
        {
            MostCheckpointOverlaps = result;
        }
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int[] agnesCheckpoints = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            var solver = new CRSCNTRY(agnesCheckpoints.Length - 1, agnesCheckpoints);

            int[] raceCardCheckpoints;
            while ((raceCardCheckpoints = Array.ConvertAll(
                Console.ReadLine().Split(), int.Parse))[0] != 0)
            {
                solver.ConsiderNewRaceCard(raceCardCheckpoints.Length - 1, raceCardCheckpoints);
            }

            Console.WriteLine(solver.MostCheckpointOverlaps);
        }
    }
}
