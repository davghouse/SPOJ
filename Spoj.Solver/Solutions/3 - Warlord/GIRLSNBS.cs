using System;

// https://www.spoj.com/problems/GIRLSNBS/ #division #experiment
// Minimizes the maximum consecutive genders for some girls and boys sitting in a row.
public static class GIRLSNBS
{
    public static int Solve(int girlCount, int boyCount)
    {
        int maxCount = Math.Max(girlCount, boyCount);
        int minCount = Math.Min(girlCount, boyCount);

        // This happens to work for the case where both are equal (including both zero).
        // For normal cases where maxCount != minCount, note that minCount creates minCount + 1
        // slots to distribute maxCount kids (minCount - 1 interior, 2 exterior). Then round up.
        return (int)Math.Ceiling((double)maxCount / (minCount + 1));
    }
}

public static class Program
{
    private static void Main()
    {
        int[] line;
        while ((line = Array.ConvertAll(Console.ReadLine().Split(), int.Parse))[0] != -1)
        {
            Console.WriteLine(
                GIRLSNBS.Solve(line[0], line[1]));
        }
    }
}
