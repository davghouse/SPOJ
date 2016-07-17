using System;

// 1419 http://www.spoj.com/problems/NGM/ A Game with Numbers
// For a game between two players where digits are subtracted from a number
// in turn, finds who wins the game and what player one's first move should be if he can win.
public static class NGM
{
    // If the number doesn't end in zero then the first player guarantees
    // victory by subtracting the final digit to make a number that does end in zero.
    // Then the second player has to subtract less than 10 from that number, so the
    // number after his turn definitely doesn't end in zero and the cycle repeats.
    // The reverse happens if the number starts out ending in zero.
    // For example, here are the moves for number = 1234567891:
    // =P1=> 1234567890
    // =P2=> 123456788x
    // =P1=> 1234576880
    // ...
    // =P2=> 27
    // =P1=> 20
    // =P2=> 18
    // =P1=> 10
    // =P2=> 9
    // =P1=> 0 (P1 wins)
    public static Tuple<int, int?> Solve(int number)
    {
        bool playerOneWins = number % 10 != 0;
        int winner = playerOneWins ? 1 : 2;
        int? winningFirstMoveForPlayerOne = playerOneWins ? number % 10 : (int?)null;

        return Tuple.Create(winner, winningFirstMoveForPlayerOne);
    }
}


public static class Program
{
    private static void Main()
    {
        var gameResults = NGM.Solve(int.Parse(Console.ReadLine()));

        Console.WriteLine(gameResults.Item1);
        if (gameResults.Item2.HasValue)
        {
            Console.WriteLine(gameResults.Item2);
        }
    }
}
