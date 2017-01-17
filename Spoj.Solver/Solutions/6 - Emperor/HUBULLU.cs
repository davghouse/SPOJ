using System;

// http://www.spoj.com/problems/HUBULLU/ #game #proof #trap
// Determines who wins in a game where a number and all its divisors are removed each turn.
public static class HUBULLU
{
    // Assume by contradication player two can win. Blah blah the players are playing
    // optimally and the problem guarantees determining a winner is possible, or whatever,
    // I don't know anything about game theory which is what made this problem tricky
    // (spent a couple hours before coming up with this solution--reminded me of a harder NGM).
    // Player one can choose number 1 as their first move, which is a divisor of everything
    // so was going to be removed by player two's move regardless. That means on the third turn
    // the game state is as if only player two had played a move, and apparently it's a losing
    // position for player one. But player one could've put player two in that same position
    // by playing his move before he did, contradicting the assumption and showing player two can't win.
    // Turns out this has a name: https://en.wikipedia.org/wiki/Strategy-stealing_argument
    public static string Solve(int numberOfHighestPiece, int firstPlayer)
        => firstPlayer == 0 ? "Airborne wins." : "Pagfloyd wins.";
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
                HUBULLU.Solve(line[0], line[1]));
        }
    }
}
