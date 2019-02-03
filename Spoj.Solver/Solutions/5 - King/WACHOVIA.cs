using System;
using System.IO;
using System.Text;

// https://www.spoj.com/problems/WACHOVIA/ #dynamic-programming-2d #knapsack #optimization
// Chooses the bags a robber should take (0/1 knapsack).
public static class WACHOVIA
{
    // If you don't know 0/1 knapsack and need a hint, knowing the DP is two-dimensional
    // on the bags being considered and the total weight used might be enough. (If we're
    // using i bags for some weight, the maximum value is the max of the value for i - 1
    // bags at that weight, and the value for i - 1 bags at [the weight minus the ith
    // bag's weight] plus the value for the ith bag.)
    public static int Solve(int maxWeight, int bagCount, int[,] bags)
    {
        int[,] heistValues = new int[bagCount, maxWeight + 1];
        int bagWeight = bags[0, 0];
        int bagValue = bags[0, 1];

        // Initialize the first row in the table; only the first bag is considered.
        for (int weight = 0; weight <= maxWeight; ++weight)
        {
            heistValues[0, weight] = weight >= bagWeight ? bagValue : 0;
        }

        for (int bag = 1; bag < bagCount; ++bag)
        {
            bagWeight = bags[bag, 0];
            bagValue = bags[bag, 1];

            for (int weight = 0; weight <= maxWeight; ++weight)
            {
                heistValues[bag, weight] = weight >= bagWeight
                    // Budget is big enough that we can try grabbing this bag and
                    // using the remaining weight for the previous bags.
                    ? Math.Max(
                        heistValues[bag - 1, weight - bagWeight] + bagValue,
                        heistValues[bag - 1, weight])
                    : heistValues[bag - 1, weight];
            }
        }

        return heistValues[bagCount - 1, maxWeight];
    }
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int remainingTestCases = FastIO.ReadNonNegativeInt();
        while (remainingTestCases-- > 0)
        {
            int maxWeight = FastIO.ReadNonNegativeInt();
            int bagCount = FastIO.ReadNonNegativeInt();

            int[,] bags = new int[bagCount, 2];
            for (int b = 0; b < bagCount; ++b)
            {
                bags[b, 0] = FastIO.ReadNonNegativeInt(); // bag weight
                bags[b, 1] = FastIO.ReadNonNegativeInt(); // bag value
            }

            int result = WACHOVIA.Solve(maxWeight, bagCount, bags);
            output.AppendLine($"Hey stupid robber, you can get {result}.");
        }

        Console.Write(output);
    }
}

// This is based in part on submissions from https://www.codechef.com/status/INTEST.
// It's assumed the input is well-formed, so if you try to read an integer when no
// integers remain in the input, there's undefined behavior (infinite loop).
// NOTE: just working around improperly formatted input--probably don't need for speed.
public static class FastIO
{
    private const byte _null = (byte)'\0';
    private const byte _newLine = (byte)'\n';
    private const byte _minusSign = (byte)'-';
    private const byte _zero = (byte)'0';
    private const int _inputBufferLimit = 8192;

    private static readonly Stream _inputStream = Console.OpenStandardInput();
    private static readonly byte[] _inputBuffer = new byte[_inputBufferLimit];
    private static int _inputBufferSize = 0;
    private static int _inputBufferIndex = 0;

    private static byte ReadByte()
    {
        if (_inputBufferIndex == _inputBufferSize)
        {
            _inputBufferIndex = 0;
            _inputBufferSize = _inputStream.Read(_inputBuffer, 0, _inputBufferLimit);
            if (_inputBufferSize == 0)
                return _null; // All input has been read.
        }

        return _inputBuffer[_inputBufferIndex++];
    }

    public static int ReadNonNegativeInt()
    {
        byte digit;

        // Consume and discard whitespace characters (their ASCII codes are all < _minusSign).
        do
        {
            digit = ReadByte();
        }
        while (digit < _minusSign);

        // Build up the integer from its digits, until we run into whitespace or the null byte.
        int result = digit - _zero;
        while (true)
        {
            digit = ReadByte();
            if (digit < _zero) break;
            result = result * 10 + (digit - _zero);
        }

        return result;
    }
}
