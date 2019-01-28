using System;

// https://www.spoj.com/problems/TEMPLATE1/ #ad-hoc #greedy #sorting
// Breaks a chocolate bar apart in the cheapest way possible.
public static class CHOCOLA
{
    // I don't know, convince yourself that greedy works. Maybe there's an inductive proof?
    // Assuming greedy works, it's important to see how to implement it. At first I went
    // down the wrong track and tried divide and conquer, where choosing an edge splits
    // the piece being considered into two. Then from those pieces, greedily choose
    // where to split them, independent of one another, and keep dividing. But it's a lot
    // easier than that, just choose the largest remaining edge and split everything by it.
    // For example, if the edge is horizontal then the number of pieces you need to split
    // to split every piece that has that edge is 1 + # of _vertical_ splits so far. We're
    // splitting on the largest edge globally, so it's definitely the largest in all those pieces.
    public static int Solve(int[] columnCosts, int[] rowCosts)
    {
        Array.Sort(columnCosts); Array.Reverse(columnCosts);
        Array.Sort(rowCosts); Array.Reverse(rowCosts);

        int columnsBroken = 0;
        int rowsBroken = 0;
        int totalCostOfBreaking = 0;

        while (columnsBroken != columnCosts.Length
            || rowsBroken != rowCosts.Length)
        {
            int nextColumnCost = columnsBroken == columnCosts.Length
                ? int.MinValue : columnCosts[columnsBroken];
            int nextRowCost = rowsBroken == rowCosts.Length
                ? int.MinValue : rowCosts[rowsBroken];
            if (nextColumnCost > nextRowCost)
            {
                totalCostOfBreaking += nextColumnCost * (1 + rowsBroken);
                ++columnsBroken;
            }
            else
            {
                totalCostOfBreaking += nextRowCost * (1 + columnsBroken);
                ++rowsBroken;
            }
        }

        return totalCostOfBreaking;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            Console.ReadLine();

            string[] line = Console.ReadLine().Split();
            int m = int.Parse(line[0]);
            int n = int.Parse(line[1]);

            int[] columnCosts = new int[m];
            for (int c = 0; c < m - 1; ++c)
            {
                columnCosts[c] = int.Parse(Console.ReadLine());
            }

            int[] rowCosts = new int[n];
            for (int r = 0; r < n - 1; ++r)
            {
                rowCosts[r] = int.Parse(Console.ReadLine());
            }

            Console.WriteLine(
                CHOCOLA.Solve(columnCosts, rowCosts));
        }
    }
}
