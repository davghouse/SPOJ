using System;

// 7406 http://www.spoj.com/problems/BEENUMS/ Beehive Numbers
// Determines if the given number is a beehive number.
public static class BEENUMS
{
    // See image for details: http://i.imgur.com/ELtDEAU.jpg.
    // We try to get the beehive index of the input, then calculate the beehive
    // number from that index to see if it maps back to exactly the input.
    public static string Solve(int number)
    {
        // 4.0 to avoid integer overflow as the input number can be a billion.
        int indexOfClosestBeehiveNumber = (int)Math.Round(1 / 6.0 * (Math.Sqrt(3 * (4.0 * number - 1)) + 3));

        return number == GetBeehiveNumber(indexOfClosestBeehiveNumber) ? "Y" : "N";
    }

    private static int GetBeehiveNumber(int index)
        => 3 * (index - 1) * index + 1;

    // ...But I think we could just get the index as a double and
    // check to see if it's equal to itself casted to an integer.
    public static string SolveDifferently(int number)
    {
        double trialIndex = 1 / 6.0 * (Math.Sqrt(3 * (4.0 * number - 1)) + 3);

        return trialIndex == (int)trialIndex ? "Y" : "N";
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
                BEENUMS.Solve(number));
        }
    }
}
