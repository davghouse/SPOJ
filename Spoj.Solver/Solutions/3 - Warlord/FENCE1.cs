using System;

// https://www.spoj.com/problems/FENCE1/ #intuition #math
// Calculates how much area can be fenced in using a specified length of fence connected to a big wall.
public static class FENCE1
{
    // So the answer here is to make a half circle with the fence, the wall forming the straight side.
    // Not sure how to prove it but it makes sense intuitively; convex, uses a lot of the free wall.
    // For the whole circle, C/2 = pi*r = length => r = length/pi, => A/2 = pi * r^2 / 2 = length ^ 2 / (2 * pi).
    public static double Solve(int length)
        => length * length / (2 * Math.PI);
}

public static class Program
{
    private static void Main()
    {
        int length;
        while ((length = int.Parse(Console.ReadLine())) != 0)
        {
            Console.WriteLine(
                FENCE1.Solve(length).ToString("F2"));
        }
    }
}
