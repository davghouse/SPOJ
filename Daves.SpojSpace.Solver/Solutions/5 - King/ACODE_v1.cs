using System;
using System.Collections.Generic;

// http://www.spoj.com/problems/ACODE/ #dynamic-programming #memoization #recursion
// Returns the number of ways to decode a number (string of digits) representing characters in alphabetical position.
public static class ACODE
{
    // For example, 1 decodes into A, 10 decodes into J, and 15 decodes into AE and O.
    private static readonly IReadOnlyDictionary<string, long> _codedLetterDecodeCounts = new Dictionary<string, long>
    {
        { "1", 1 }, { "2", 1 }, { "3", 1 }, { "4", 1 }, { "5", 1 }, { "6", 1 }, { "7", 1 },
        { "8", 1 }, { "9", 1 }, { "10", 1 }, { "11", 2 }, { "12", 2 }, { "13", 2 }, { "14", 2},
        { "15", 2 }, { "16", 2 }, { "17", 2 }, { "18", 2 }, { "19", 2 }, { "20", 1 }, { "21", 2 },
        { "22", 2 }, { "23", 2 }, { "24", 2 }, { "25", 2 }, { "26", 2 }
    };

    // The two-way branching of the recursion causes us to call Solve on the same number many times; use this
    // dictionary to memoize. Depth of recursion/size of the dictionary only grows to about the length of the
    // input number, which is limited to 5000.
    private static readonly Dictionary<string, long> _numberDecodeCounts = new Dictionary<string, long>();

    public static long Solve(string number)
    {
        _numberDecodeCounts.Clear();

        return SolveRecursively(number);
    }

    private static long SolveRecursively(string number)
    {
        // Base case could be number.Length == 0 return 1, but this was convenient.
        if (IsNumberACodedLetter(number))
            return CodedLetterDecodeCount(number);

        long decodeCount;
        if (_numberDecodeCounts.TryGetValue(number, out decodeCount))
            return decodeCount;

        bool firstDigitCanBeLetter = IsNumberACodedLetter(number.Substring(0, 1));
        bool firstTwoDigitsCanBeLetter = number.Length >= 2 && IsNumberACodedLetter(number.Substring(0, 2));

        if (firstDigitCanBeLetter)
        {
            string numberAfterFirstDigit = number.Substring(1);
            decodeCount += Solve(numberAfterFirstDigit);
        }

        if (firstTwoDigitsCanBeLetter)
        {
            string numberAfterFirstTwoDigits = number.Substring(2);
            decodeCount += Solve(numberAfterFirstTwoDigits);
        }

        _numberDecodeCounts.Add(number, decodeCount);

        return decodeCount;
    }

    private static bool IsNumberACodedLetter(string number)
        => _codedLetterDecodeCounts.ContainsKey(number);

    private static long CodedLetterDecodeCount(string codedLetter)
        => _codedLetterDecodeCounts[codedLetter];
}

public static class Program
{
    private static void Main()
    {
        string number;
        while ((number = Console.ReadLine()) != "0")
        {
            Console.WriteLine(
                ACODE.Solve(number));
        }
    }
}
