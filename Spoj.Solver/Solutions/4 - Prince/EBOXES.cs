using System;

// https://www.spoj.com/problems/EBOXES/ #math
// Figures out how many boxes are given how many empty boxes there are.
public static class EBOXES
{
    // Problem statement isn't clear, but there are N initial boxes, and they aren't
    // part of the spawn. And boxes only spawn into unoccupied boxes. Let B_i and M_i be
    // the # of random big/medium boxes chosen in the ith round, into which K medium/small
    // boxes are spawned, respectively.
    // Total # of boxes = N + K(B_1 + M_1) + ... + K(B_T + M_T)
    //                  = N + K(B_1 + ... + M_T)
    // Total # of empty boxes = F = N - B_1 + KB_1 - M_1 + KM_1 - ... - B_T + KB_T - M_T + KM_T
    //                            = N + (K - 1)(B_1 + ... M_T)
    // The -1 terms are from placing the sets of K boxes into the bigger empty boxes, making
    // them no longer empty. The +K terms are from the new boxes all being empty. So the sum
    // part equals (F - N)/(K - 1), which can be substituted into the first equation to
    // calculate the total.
    public static int Solve(
        int initialBoxCount,    // N
        int boxSpawnRate,       // K
        int repetitionCount,    // T
        int totalEmptyBoxCount) // F
        => initialBoxCount
        + boxSpawnRate * (totalEmptyBoxCount - initialBoxCount) / (boxSpawnRate - 1);
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            Console.WriteLine(
                EBOXES.Solve(line[0], line[1], line[2], line[3]));
        }
    }
}
