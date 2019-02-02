using System;

// https://www.spoj.com/problems/ANARC09B/ #gcd
// Finds the smallest square a tile of a certain size can make.
public static class ANARC09B
{
    // We're building a square out of tiles with dimensions w x h, all stacked
    // the same way. The square will be a certain number of tiles wide, n, and
    // a certain number of tiles tall, m, where w * n = h * m in order to be
    // square. So w/h = m/n, we want smallest m/n, so we need to write w/h in
    // lowest terms by dividing out the GCD. Example for 2x3 tiles:

    // [ | ]  [ | ]  [ | ]
    // [ | ]  [ | ]  [ | ]
    // [ | ]  [ | ]  [ | ]

    // [ | ]  [ | ]  [ | ]
    // [ | ]  [ | ]  [ | ]
    // [ | ]  [ | ]  [ | ]

    // There are 6 tiles there, each 2x3. If we stick them together, they form
    // a square of dimensions 6x6 of the base tiles we were supposed to buy.
    public static long Solve(int tileWidth, int tileHeight)
    {
        int squareWidth = tileHeight / GreatestCommonDivisor(tileWidth, tileHeight);
        int squareHeight = tileWidth / GreatestCommonDivisor(tileWidth, tileHeight);

        // The total number of tileWidth x tileHeight tiles we need to form a square:
        return squareWidth * (long)squareHeight;
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
        int[] line;
        while ((line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse))[0] != 0)
        {
            Console.WriteLine(
                ANARC09B.Solve(line[0], line[1]));
        }
    }
}
