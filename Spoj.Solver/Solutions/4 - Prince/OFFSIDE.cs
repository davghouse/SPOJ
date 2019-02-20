using System;
using System.Linq;

// https://www.spoj.com/problems/OFFSIDE/ #ad-hoc #extrema
// Determines if an attacker is offsides (using player positions only).
public static class OFFSIDE
{
    public static bool Solve(int[] attackers, int[] defenders)
    {
        // There may be attackers further than the closest attacker who are also offsides,
        // but that doesn't matter here. If anyone's offsides, the closest attacker is too.
        int closestAttacker = attackers.Min();

        // Rather than finding the closest and second closest defenders, note that the
        // attacker is offsides if he is cleanly before all the defenders except at
        // most one (the goalie).
        int defendersFurtherThanClosestAttacker = defenders.Count(d => d > closestAttacker);

        return defendersFurtherThanClosestAttacker >= defenders.Length - 1;
    }
}

public static class Program
{
    private static void Main()
    {
        int[] line;
        while ((line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse))[0] != 0)
        {
            int[] attackers = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int[] defenders = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            Console.WriteLine(
                OFFSIDE.Solve(attackers, defenders) ? "Y" : "N");
        }
    }
}
