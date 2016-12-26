using System;
using System.Collections;

// http://www.spoj.com/problems/HPYNOS/ #digits #simulation
// Determines if repeatedly adding the squared digits of a number eventually gets to 1.
public static class HPYNOS
{
    // n is limited to 2,147,483,647, so by inspection 1,999,999,999 will give us the
    // limit of the numbers we can get after at least one breaking. That is:
    // 1^2 + 9 * 9^2 = 730. So we'll have a size 731 bool array to keep track of what's already seen.
    public static int Solve(int n)
    {
        var numbersAlreadySeen = new bool[731];
        int breakCount = 0;

        while (n != 1)
        {
            n = Break(n);
            ++breakCount;

            if (numbersAlreadySeen[n])
            {
                return -1; // Caught in a cycle that will never terminate.
            }
            else
            {
                numbersAlreadySeen[n] = true;
            }
        }

        return breakCount;
    }

    private static int Break(int n)
    {
        int result = 0;

        while (n != 0)
        {
            int lastDigit = n % 10;
            result += lastDigit * lastDigit;
            n /= 10;
        }

        return result;
    }
}

public static class Program
{
    private static void Main()
    {
        Console.WriteLine(
            HPYNOS.Solve(int.Parse(Console.ReadLine())));
    }
}
