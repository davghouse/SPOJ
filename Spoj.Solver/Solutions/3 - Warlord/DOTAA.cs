using System;

// https://www.spoj.com/problems/DOTAA/ #ad-hoc #division #game #io
// Figures out if the heroes can make it past the towers without anyone dying.
public static class DOTAA
{
    // All towers do the same damage. Each hero can tank a certain amount of
    // tower hits before dying. We can calculate how many survivable hits for
    // each individual hero as they are read, then compare that total to the tower
    // count to see if it's possible for all heroes to survive.
    public static bool Solve(int heroCount, int towerCount, int towerDamage)
    {
        int totalSurvivableHits = 0;
        for (int h = 0; h < heroCount; ++h)
        {
            int heroHealth = int.Parse(Console.ReadLine());

            // Divide the expendable health by the tower damage (and round down, implicitly).
            totalSurvivableHits += (heroHealth - 1) / towerDamage;
        }

        return totalSurvivableHits >= towerCount;
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

            bool canHeroesSurvive = DOTAA.Solve(
                heroCount: int.Parse(line[0]),
                towerCount: int.Parse(line[1]),
                towerDamage: int.Parse(line[2]));

            Console.WriteLine(canHeroesSurvive ? "YES" : "NO");
        }
    }
}
