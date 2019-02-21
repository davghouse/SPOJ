using System;

// https://www.spoj.com/problems/TRGRID/ #ad-hoc #recursion
// Finds the final direction you'll be facing when spiraling in towards the center of a grid.
public static class TRGRID
{
    // Experiment a bit to observe the following properties:
    // H=1,  L=1+ => always facing right. Examples:
    //   1) [>]
    //   2) [-] ... [-][>]
    // H=2+, L=1  => always facing down. Examples:
    //  1) [|]
    //     [V]
    //  2) [|]
    //      .
    //      .
    //      .
    //     [V]
    // The above covers all grids where at least one dimension is = 1.
    // H=2,  L=2+ => always facing left. Examples:
    //  1) [-][|]
    //     [<][-]
    //  2) [-] ... [|]
    //     [<] ... [-]
    // H=3+, L=2 => always facing up. Examples:
    // 1) [-][|]
    //    [^][|]
    //    [|][-]
    // 2) [-][|]
    //    [^][|]
    //     .  .
    //     .  .
    //     .  .
    //    [|][-]
    // The above covers all grids where at least one dimension = 2.
    // If a grid isn't covered by the above base cases, then both of its dimensions are at
    // least 3. One rotation around the grid cuts off 2 rows and 2 columns, leaving an inner
    // square with dimensions h - 2 x l - 2. We always start traversing this inner square in
    // the same way as the original square, from the upper left corner. Hence, recursively
    // spiraling inward eventually gets us to an inner square covered by the base cases.
    // The number of rotations until a base case is controlled by the minimum initial dimension.
    // For example, a 7 x 11 grid needs 3 rotations to cut 6 off of 7, ending at a 1 x 5 base
    // case. An 8 x 6 grid needs 2 rotations to cut 4 off of 6, ending at 4 x 2.
    public static string Solve(int h, int l)
    {
        int minDimension = Math.Min(h, l);
        int rotationsUntilBaseCase = (minDimension - 1) / 2;
        int baseCaseH = h - 2 * rotationsUntilBaseCase;
        int baseCaseL = l - 2 * rotationsUntilBaseCase;

        return baseCaseH == 1 ? "R"
            : baseCaseL == 1 ? "D"
            : baseCaseH == 2 ? "L"
            : "U"; // baseCaseL == 2
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
            int h = int.Parse(line[0]);
            int l = int.Parse(line[1]);

            Console.WriteLine(
                TRGRID.Solve(h, l));
        }
    }
}
