using System;
using System.Collections.Generic;

// 394 http://www.spoj.com/problems/ACODE/ Alphacode
// Returns the number of ways to decode a number (string of digits) representing characters in alphabetical position.
public static class ACODE // v1, top-down, recursion with memoization
{
    private static IReadOnlyDictionary<string, long> _codedLetterDecodeCounts = new Dictionary<string, long>
    {
        { "1", 1 }, { "2", 1 }, { "3", 1 }, { "4", 1 }, { "5", 1 }, { "6", 1 }, { "7", 1 },
        { "8", 1 }, { "9", 1 }, { "10", 1 }, { "11", 2 }, { "12", 2 }, { "13", 2 }, { "14", 2},
        { "15", 2 }, { "16", 2 }, { "17", 2 }, { "18", 2 }, { "19", 2 }, { "20", 1 }, { "21", 2 },
        { "22", 2 }, { "23", 2 }, { "24", 2 }, { "25", 2 }, { "26", 2 }
    };

    private static bool IsNumberACodedLetter(string number)
        => _codedLetterDecodeCounts.ContainsKey(number);

    private static long CodedLetterDecodeCount(string codedLetter)
        => _codedLetterDecodeCounts[codedLetter];

    public static long Solve(string number)
    {
        Solver s = new Solver(number);
        return s.Solve();
    }

    private class Solver
    {
        private string _number;
        // The two way branching of the recursion causes us to call Solve on the same number many times; use this dictionary to memoize.
        // Depth of recursion/size of the dictionary only grows to about the length of the input number, which is limited to 5000.
        private Dictionary<string, long> _decodeCounts = new Dictionary<string, long>();

        public Solver(string numbers)
        {
            _number = numbers;
        }

        public long Solve()
            => Solve(_number);

        private long Solve(string number)
        {
            // Base case could be number.Length == 0 return 1, but this was convenient.
            if (IsNumberACodedLetter(number))
                return CodedLetterDecodeCount(number);

            long decodeCount;
            if (_decodeCounts.TryGetValue(number, out decodeCount))
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

            _decodeCounts.Add(number, decodeCount);
            return decodeCount;
        }

        // Cuts the number of calls to Solve by a lot but I guess it's less natural/doesn't read as well/has code duplication.
        //private long Solve(string number)
        //{
        //    if (IsNumberACodedLetter(number))
        //        return CodedLetterDecodeCount(number);

        //    bool firstDigitCanBeLetter = IsNumberACodedLetter(number.Substring(0, 1));
        //    bool firstTwoDigitsCanBeLetter = number.Length >= 2 && IsNumberACodedLetter(number.Substring(0, 2));

        //    long decodeCount = 0;
        //    if (firstDigitCanBeLetter)
        //    {
        //        string numberAfterFirstDigit = number.Substring(1);
        //        long decodeCountOfNumberAfterFirstDigit;
        //        if (!_decodeCounts.TryGetValue(numberAfterFirstDigit, out decodeCountOfNumberAfterFirstDigit))
        //        {
        //            decodeCountOfNumberAfterFirstDigit = Solve(numberAfterFirstDigit);
        //            _decodeCounts.Add(numberAfterFirstDigit, decodeCountOfNumberAfterFirstDigit);
        //        }

        //        decodeCount += decodeCountOfNumberAfterFirstDigit;
        //    }
        //    if (firstTwoDigitsCanBeLetter)
        //    {
        //        string numberAfterFirstTwoDigits = number.Substring(2);
        //        long decodeCountOfNumberAfterFirstTwoDigits;
        //        if (!_decodeCounts.TryGetValue(numberAfterFirstTwoDigits, out decodeCountOfNumberAfterFirstTwoDigits))
        //        {
        //            decodeCountOfNumberAfterFirstTwoDigits = Solve(numberAfterFirstTwoDigits);
        //            _decodeCounts.Add(numberAfterFirstTwoDigits, decodeCountOfNumberAfterFirstTwoDigits);
        //        }

        //        decodeCount += decodeCountOfNumberAfterFirstTwoDigits;
        //    }

        //    return decodeCount;
        //}
    }
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
