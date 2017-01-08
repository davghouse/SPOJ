using System;

// http://www.spoj.com/problems/BEENUMS/ #formula #math #proof
// Determines if the given number is a beehive number.
public static class BEENUMS
{
    // See image for details: http://i.imgur.com/ELtDEAU.jpg.
    // That shows that for "number" to be a beehive number, it must equal 3(n - 1)n + 1 for some integer n,
    // called the index. The quadratic equation lets us solve for the positive n index given "number", and then
    // we just need to verify it's actually an integer. That's done in a roundabout way by rounding it to an
    // integer, computing the beehive number from that index, and checking to see if it's equal to "number"...
    public static bool Solve(int number)
    {
        // 4.0 to avoid integer overflow as the input number can be a billion.
        int indexOfClosestBeehiveNumber = (int)Math.Round(1 / 6.0 * (Math.Sqrt(3 * (4.0 * number - 1)) + 3));

        return number == GetBeehiveNumber(indexOfClosestBeehiveNumber);
    }

    private static int GetBeehiveNumber(int index)
        => 3 * (index - 1) * index + 1;

    // ...Or more directly by just checking to see if it's equal to its int cast, which I think also works.
    public static bool SolveDifferently(int number)
    {
        double trialIndex = 1 / 6.0 * (Math.Sqrt(3 * (4.0 * number - 1)) + 3);

        return trialIndex == (int)trialIndex;
    }
}

public static class Program
{
    private static void Main()
    {
        int number;
        while ((number = int.Parse(Console.ReadLine())) != -1)
        {
            Console.WriteLine(
                BEENUMS.Solve(number) ? "Y" : "N");
        }
    }
}
