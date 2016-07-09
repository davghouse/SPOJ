using System;
using System.Collections.Generic;

// Street Parade (stack-based, O(n) space)
// 95 http://www.spoj.com/problems/STPAR/
// Given the order of some love mobiles on an approach street, figures out if it's possible to
// put them in their final parade order using a side street (as a stack).
public static class STPAR
{
    // The array here might be something like 5 1 2 4 3, meaning the love mobile that's
    // 1st on the approach street needs to be 5th on the parade street, the love mobile
    // that's 2nd on the approach street needs to be 1st on the parade street, and so on.
    public static string Solve(int[] approachStreetLoveMobiles)
    {
        // Due to street widths, love mobiles can't go around each other. As each love mobile arrives on
        // the approach street, we can either push it down the side street, or move it into
        // its final parade position. Before deciding where to move it we can also pop any mobiles
        // already on the side street out of it and into their final parade position.
        Stack<int> sideStreetLoveMobiles = new Stack<int>();
        int nextLoveMobileNeededOnTheParadeStreet = 1;

        for (int i = 0; i < approachStreetLoveMobiles.Length; ++i)
        {
            while (sideStreetLoveMobiles.Count > 0
                && sideStreetLoveMobiles.Peek() == nextLoveMobileNeededOnTheParadeStreet)
            {
                sideStreetLoveMobiles.Pop();
                ++nextLoveMobileNeededOnTheParadeStreet;
            }

            int nextLoveMobileOnTheApproachStreet = approachStreetLoveMobiles[i];
            if (nextLoveMobileOnTheApproachStreet == nextLoveMobileNeededOnTheParadeStreet)
            {
                ++nextLoveMobileNeededOnTheParadeStreet;
            }
            else if (sideStreetLoveMobiles.Count == 0
                // Once a love mobile is pushed onto the side street, it'll have to come before anything already
                // on the side street in the final parade ordering, since it'll be popped out before them. So
                // its designated ordering better not conflict with what we know has to happen.
                || nextLoveMobileOnTheApproachStreet < sideStreetLoveMobiles.Peek())
            {
                sideStreetLoveMobiles.Push(nextLoveMobileOnTheApproachStreet);
            }
            // We can't move the love mobile to the parade street or to the side street!
            else return "no";
        }

        // Don't need further checks here. Nothing returned "no" up to this point. The parade street
        // only grows in the correct order (consecutively). The side street is always ordered (but not
        // necessarily consecutively). Except at the end of the loop the side street must have all the love
        // mobiles not already on the parade street, so it has a contiguous range of integers and must also
        // be consecutive, and hence pop-able to the parade street in the correct order.
        return "yes";
    }
}

public static class Program
{
    private static void Main()
    {
        int loveMobileCount;
        while ((loveMobileCount = int.Parse(Console.ReadLine())) != 0)
        {
            int[] approachStreetLoveMobiles = Array.ConvertAll(Console.ReadLine().Split(), int.Parse);

            Console.WriteLine(
                STPAR.Solve(approachStreetLoveMobiles));
        }
    }
}
