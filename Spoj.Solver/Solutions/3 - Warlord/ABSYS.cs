using System;

// http://www.spoj.com/problems/ABSYS/: ad hoc, strings
// Takes an equation with an inkblot over one number, and removes the blot.
public static class ABSYS
{
    // For whatever reason the inkblot is represented by the string "machula".
    private const string _inkblot = "machula";

    // For an equation of the form 'number + number = number', just identify
    // over what number the blot exists and solve for it using algebra.
    public static string Solve(string line)
    {
        string[] tokens = line.Split();

        string firstNumber = tokens[0];
        string secondNumber = tokens[2];
        string thirdNumber = tokens[4];

        if (firstNumber.Contains(_inkblot))
        {
            firstNumber = (int.Parse(thirdNumber) - int.Parse(secondNumber)).ToString();
        }
        else if (secondNumber.Contains(_inkblot))
        {
            secondNumber = (int.Parse(thirdNumber) - int.Parse(firstNumber)).ToString();
        }
        else
        {
            thirdNumber = (int.Parse(firstNumber) + int.Parse(secondNumber)).ToString();
        }

        return $"{firstNumber} + {secondNumber} = {thirdNumber}";
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            Console.ReadLine();
            Console.WriteLine(
                ABSYS.Solve(Console.ReadLine()));
        }
    }
}
