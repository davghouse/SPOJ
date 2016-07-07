using System;
using System.Linq;

// Army Strength
// 2727 http://www.spoj.com/problems/ARMY/
// Figures out if the army of Godzilla or the army of MechaGodzilla wins the war.
public static class ARMY
{
    // Each battle a random weakest unit dies, cross-army ties lost by MechaGodzilla's army.
    // The battles continue until one army is depleted. In effect, the victor is determined
    // by the strongest unit in each army, a tie going Godzilla's way.
    public static string Solve(int[] strengthsOfGodzillasArmy, int[] strengthsOfMechaGodzillasArmy)
        => strengthsOfGodzillasArmy.Max() >= strengthsOfMechaGodzillasArmy.Max() ? "Godzilla" : "MechaGodzilla";
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        while (remainingTestCases-- > 0)
        {
            Console.ReadLine();
            Console.ReadLine();
            int[] strengthsOfGodzillasArmy = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);
            int[] strengthsOfMechaGodzillasArmy = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            Console.WriteLine(
                ARMY.Solve(strengthsOfGodzillasArmy, strengthsOfMechaGodzillasArmy));
        }
    }
}
