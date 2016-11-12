using System;
using System.Collections.Generic;

// 12471 http://www.spoj.com/problems/DIEHARD/ DIE HARD
// Finds how long we can survive while moving between fire, water, and air.
public static class DIEHARD
{
    private const int _airHealthDelta = 3;
    private const int _airArmorDelta = 2;
    private const int _waterHealthDelta = -5;
    private const int _waterArmorDelta = -10;
    private const int _fireHealthDelta = -20;
    private const int _fireArmorDelta = 5;

    private static Dictionary<Tuple<int, int>, int> _timesUntilDeath = new Dictionary<Tuple<int, int>, int>();

    // Air is good since it increases both health and armor, so we'll always move into
    // it when available. That means the first move will be into air, and we'll live there
    // for a unit of time. Then it's either move to fire and back to air, or move to water
    // and back to air. So effectively, the game starts after 1 unit where we have (health + 3)
    // and (armor + 2) initial values. And there are two moves: air->fire->air, two units
    // of time for a health delta of -17, armor delta of 7, and air->water->air, two units
    // of time for a health delta of -2, armor delta of -8. (Might die in the middle though...)
    public static int Solve(int health, int armor)
        => 1 + SolveWithMemoization(health + _airHealthDelta, armor + _airArmorDelta);

    // Could do it with tabulation but would have to figure out the upper bound for armor.
    // A cell's answer would depend on everything above it (less health, less or more armor).
    private static int SolveWithMemoization(int health, int armor)
    {
        Tuple<int, int> healthAndArmor = Tuple.Create(health, armor);
        int timeUntilDeath = 0;
        if (_timesUntilDeath.TryGetValue(healthAndArmor, out timeUntilDeath))
            return timeUntilDeath;

        bool canSurviveMovingIntoWater = health + _waterHealthDelta > 0 && armor + _waterArmorDelta > 0;
        bool canSurviveMovingIntoFire = health + _fireHealthDelta > 0 && armor + _fireArmorDelta > 0;

        if (canSurviveMovingIntoWater && canSurviveMovingIntoFire)
            timeUntilDeath = 2 + Math.Max(
                SolveWithMemoization(health + _waterHealthDelta + _airHealthDelta, armor + _waterArmorDelta + _airArmorDelta),
                SolveWithMemoization(health + _fireHealthDelta + _airHealthDelta, armor + _fireArmorDelta + _airArmorDelta));
        else if (canSurviveMovingIntoWater)
            timeUntilDeath = 2 + SolveWithMemoization(health + _waterHealthDelta + _airHealthDelta, armor + _waterArmorDelta + _airArmorDelta);
        else if (canSurviveMovingIntoFire)
            timeUntilDeath = 2 + SolveWithMemoization(health + _fireHealthDelta + _airHealthDelta, armor + _fireArmorDelta + _airArmorDelta);

        _timesUntilDeath[healthAndArmor] = timeUntilDeath;
        return timeUntilDeath;
    }
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
                DIEHARD.Solve(line[0], line[1]));
        }
    }
}
