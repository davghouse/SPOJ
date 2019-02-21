using System;

// https://www.spoj.com/problems/POUR1/ #ad-hoc #mod-math #simulation
// Given two jugs, figures out how to fill them to reach a certain water level.
public static class POUR1
{
    // Okay so, I just experimented with pen & paper for an hour and convinced myself the following
    // was worth trying. It seems there are two options. You can start by using the small jug to
    // fill the large jug, or by using the large jug to fill the small jug. Once you start either
    // of these processes, it becomes clear there's always only one productive next move. For example,
    // take jugs w/ limits 5 & 16. Starting with small jug, you fill small jug, dump into large jug.
    // It wouldn't make sense to fill large jug at this point, you could've just done that in the first
    // place. So only viable move is to fill small jug and dump it into the large jug again, and so on,
    // until the large jug fills up and you're left with some remainder in the small jug. At that point
    // it wouldn't make sense to fill small jug, as we've already done that. Only sensible move is to
    // empty large jug and pour small jug into it, and then repeat the process. Similar stuff when
    // starting with the large jug. I tried a few and it seems like all multiples of the two limits' GCD
    // are generated (and nothing else), but haven't proven that. If that's true it's not really a fact
    // we need to use, as we could just recognize when the simulation starts to repeat (assuming that
    // that wouldn't take too long) and stop it then.
    public static int Solve(int firstJugLimit, int secondJugLimit, int goal)
    {
        int smallJugLimit = Math.Min(firstJugLimit, secondJugLimit);
        int largeJugLimit = Math.Max(firstJugLimit, secondJugLimit);
        int limitsGCD = GreatestCommonDivisor(smallJugLimit, largeJugLimit);

        if (goal > largeJugLimit) return -1;
        if (goal == smallJugLimit || goal == largeJugLimit) return 1;
        if (goal % limitsGCD != 0) return -1;

        return Math.Min(
            StartFromSmallJug(smallJugLimit, largeJugLimit, goal),
            StartFromLargeJug(smallJugLimit, largeJugLimit, goal));
    }

    private static int StartFromSmallJug(int smallJugLimit, int largeJugLimit, int goal)
    {
        int smallJugLevel = smallJugLimit;
        int largeJugLevel = 0;
        int pourCount = 1;

        // Loop constraint: small jug isn't empty, large jug is empty.
        while (true)
        {
            // Fill large jug using small jug until the next small jug would overflow it.
            while (largeJugLevel + smallJugLevel <= largeJugLimit)
            {
                // Pour small jug into large jug.
                largeJugLevel += smallJugLevel;
                smallJugLevel = 0;
                ++pourCount;
                if (largeJugLevel == goal)
                    return pourCount;

                // Refill small jug.
                smallJugLevel = smallJugLimit;
                ++pourCount;
            }

            // Pour as much of the small jug into the large jug as possible.
            int unfilledPortionOfLargeJug = largeJugLimit - largeJugLevel;
            smallJugLevel -= unfilledPortionOfLargeJug;
            largeJugLevel = largeJugLimit;
            ++pourCount;
            if (smallJugLevel == goal)
                return pourCount;

            // Empty the large jug.
            largeJugLevel = 0;
            ++pourCount;
        }
    }

    private static int StartFromLargeJug(int smallJugLimit, int largeJugLimit, int goal)
    {
        int smallJugLevel = 0;
        int largeJugLevel = largeJugLimit;
        int pourCount = 1;

        // Loop constraint: large jug is full, small jug has leftovers from last time.
        while (true)
        {
            // Pour as much of the large jug into the small jug as possible.
            int unfilledPortionOfSmallJug = smallJugLimit - smallJugLevel;
            largeJugLevel -= unfilledPortionOfSmallJug;
            smallJugLevel = smallJugLimit;
            ++pourCount;
            if (largeJugLevel == goal)
                return pourCount;

            // Empty the small jug.
            smallJugLevel = 0;
            ++pourCount;

            while (largeJugLevel - smallJugLimit >= 0)
            {
                // Empty large jug into small jug.
                largeJugLevel -= smallJugLimit;
                smallJugLevel = smallJugLimit;
                ++pourCount;
                if (largeJugLevel == goal)
                    return pourCount;

                // Empty the small jug.
                smallJugLevel = 0;
                ++pourCount;
            }

            // Empty large jug into small jug.
            smallJugLevel = largeJugLevel;
            largeJugLevel = 0;
            ++pourCount;

            // Fill large jug.
            largeJugLevel = largeJugLimit;
            ++pourCount;
        }
    }

    // This is a good article (first section): http://www.cut-the-knot.org/blue/Euclid.shtml.
    // One point to note, for a = bt + r, the gcd(a, b) divides a so it divides bt + r.
    // And it divides b, so it divides bt, which means for bt + r to be divisible by it,
    // r also needs to be divisible by it. So it divides both b and r. And the article
    // notes the importance of showing not only does it divide b and r, it's also their gcd.
    private static int GreatestCommonDivisor(int a, int b)
    {
        int temp;
        while (b != 0)
        {
            temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int a = int.Parse(Console.ReadLine());
            int b = int.Parse(Console.ReadLine());
            int c = int.Parse(Console.ReadLine());

            Console.WriteLine(
                POUR1.Solve(a, b, c));
        }
    }
}
