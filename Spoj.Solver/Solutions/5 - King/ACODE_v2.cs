using System;

// https://www.spoj.com/problems/ACODE/ #dynamic-programming-1d
// Returns the number of ways to decode a number (string of digits) representing characters in alphabetical position.
public static class ACODE
{
    // Array that's built dynamically, containing at index i the decode count for the first i digits
    // of the given number. The number is guaranteed to have no more than 5000 digits.
    private static long[] _decodeCounts = new long[5001];

    public static long Solve(string number)
    {
        // [0] is 1 as a base case for no digits, [1] is 1 as the first digit always decodes in a single way.
        _decodeCounts[0] = _decodeCounts[1] = 1;

        // _decodeCounts index is one-based, number is zero-based, kind of annoying but this looks the cleanest.
        for (int n = 1, d = 2; n < number.Length; ++n, ++d)
        {
            // If the digit is 0, it has to be used with the previous digit; 0 can only end a coded letter.
            // Hence, the previous digit plus it create one letter that gets bolted on to however many valid
            // decodings are formed by the first d - 2 digits.
            if (number[n] == '0')
            {
                _decodeCounts[d] = _decodeCounts[d - 2];
            }
            // The digit isn't zero, so it can be translated to a number by itself. Hence, it can be bolted
            // on to however many valid decodings are formed by the first d - 1 digits...
            else
            {
                _decodeCounts[d] = _decodeCounts[d - 1];

                // ...But it might also combine with the previous digit to form a decoded letter, in which case those two
                // together could additionally get bolted on to however many valid decodings there are for the first d - 2 digits.
                if (number[n - 1] == '1' // 1[1-9]
                    || number[n - 1] == '2' && number[n] <= '6') // 2[1-6]
                {
                    _decodeCounts[d] += _decodeCounts[d - 2];
                }
            }
        }

        return _decodeCounts[number.Length];
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
