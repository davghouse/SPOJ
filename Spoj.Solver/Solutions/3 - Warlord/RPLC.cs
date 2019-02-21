using System;

// https://www.spoj.com/problems/RPLC/ #ad-hoc
// Finds the lowest possible starting energy level when drinking ordered +- cokes.
public static class RPLC
{
    public static long Solve(int[] cokes)
    {
        // Assuming we start with zero energy, how low would we go?
        long currentEnergyLevel = 0;
        long lowestEnergyLevel = 0;
        for (int i = 0; i < cokes.Length; ++i)
        {
            currentEnergyLevel += cokes[i];
            if (currentEnergyLevel < lowestEnergyLevel)
            {
                lowestEnergyLevel = currentEnergyLevel;
            }
        }

        return lowestEnergyLevel < 0
            // E.g., if we got to -3, we would've dipped down to the min of 1 if we started with 4.
            ? -1 * lowestEnergyLevel + 1
            // If we never got to a negative energy level, we still need 1 to begin the journey.
            : 1;
    }
}

public static class Program
{
    private static void Main()
    {
        int testCount = int.Parse(Console.ReadLine());
        for (int t = 1; t <= testCount; ++t)
        {
            int n = int.Parse(Console.ReadLine());
            int[] cokes = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            Console.WriteLine(
                $"Scenario #{t}: {RPLC.Solve(cokes)}");
        }
    }
}
