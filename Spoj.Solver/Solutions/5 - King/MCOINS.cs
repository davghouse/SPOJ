using System;
using System.Linq;
using System.Text;

// 3885 http://www.spoj.com/problems/MCOINS/ Coins Game
// Plays a finite, perfect information 2-player game where coins are removed from a stack.
public static class MCOINS
{
    // This ties back to HUBULLU and NGM and the idea of a determinate game, for background see:
    // https://en.wikipedia.org/wiki/Zermelo%27s_theorem_(game_theory)
    // http://www.math.harvard.edu/~elkies/FS23j.03/zermelo.pdf
    // I think about it like, if the first player doesn't have any move to play that'll allow them
    // to win no matter what the second player plays, then the second player must have some move
    // to play no matter what the first player plays, so given the start state, we know either first or
    // second wins. It's less weird than HUBULLU/NGM, there are only 3 moves that can be made by either
    // player in a turn. So the recursive solution is obvious, the start player can win for a stack size
    // of n if the start player **can't** win for a stack size of n - 1, n - k, or n - l. That is, the
    // first player can win if for some move the second player can't win.
    public static string Solve(int k, int l, int[] coinCounts)
    {
        int maxCoinCount = coinCounts.Max();

        bool[] firstPlayerWins = new bool[maxCoinCount + 1];
        firstPlayerWins[0] = false; // False needed for the base case.
        firstPlayerWins[1] = true;  // By playing 1.

        for (int coinCount = 2; coinCount <= maxCoinCount; ++coinCount)
        {
            bool existsAStartMoveThatForcesSecondPlayerToLose = 
                !firstPlayerWins[coinCount - 1]
                || coinCount - k >= 0 && !firstPlayerWins[coinCount - k]
                || coinCount - l >= 0 && !firstPlayerWins[coinCount - l];

            firstPlayerWins[coinCount] = existsAStartMoveThatForcesSecondPlayerToLose;
        }

        var winners = new StringBuilder();
        foreach (int coinCount in coinCounts)
        {
            winners.Append(firstPlayerWins[coinCount] ? 'A' : 'B');
        }

        return winners.ToString();
    }
}

public static class Program
{
    private static void Main()
    {
        int[] line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
        int[] coinCounts = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

        Console.WriteLine(
            MCOINS.Solve(line[0], line[1], coinCounts));
    }
}
