using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

// https://www.spoj.com/problems/RENT/ #binary-search #dynamic-programming-1d #sorting
// Finds which flight orders to accept, given their duration and profit.
public static class RENT
{
    public static int Solve(int orderCount, FlightOrder[] orders)
    {
        // Sort by ascending end time.
        Array.Sort(orders, (f1, f2) => f1.EndTime.CompareTo(f2.EndTime));

        // For the DP, store the best profit we can get through the end times we've seen so far.
        var bestEndTimeProfits = new List<EndTimeProfit>(capacity: orders.Length)
        {
            new EndTimeProfit(endTime: 0, profit: 0)
        };

        int bestProfitSoFar = 0;

        for (int o = 0; o < orderCount; o++)
        {
            var order = orders[o];
            int bestProfitUsingOrder = order.Profit
                + bestEndTimeProfits[BinarySearch.Search(
                    start: 0,
                    end: bestEndTimeProfits.Count - 1,
                    // If we use this order, it blocks everything from its start time to its
                    // end time. So find the best profit using flights ending before its start time.
                    verifier: i => bestEndTimeProfits[i].EndTime <= order.StartTime,
                    mode: BinarySearch.Mode.TrueToFalse).Value].Profit;

            bestProfitSoFar = Math.Max(bestProfitSoFar, bestProfitUsingOrder);

            bestEndTimeProfits.Add(new EndTimeProfit(
                endTime: order.EndTime,
                profit: bestProfitSoFar));
        }

        return bestProfitSoFar;
    }
}

public struct FlightOrder
{
    public FlightOrder(int startTime, int duration, int profit)
    {
        StartTime = startTime;
        EndTime = startTime + duration;
        Profit = profit;
    }

    public int StartTime { get; }
    public int EndTime { get; }
    public int Profit { get; }
}

public struct EndTimeProfit
{
    public EndTimeProfit(int endTime, int profit)
    {
        EndTime = endTime;
        Profit = profit;
    }

    public int EndTime { get; }
    public int Profit { get; }
}

// This facilitates predicate-based binary searching, where the values being searched on
// satisfy the predicate in an ordered manner, in one of two ways:
// [false false false ... false true ... true true true] (true => anything larger is true)
// [true true true ... true false ... false false false] (true => anything smaller is true)
// In the first, the goal of the search is to locate the smallest value satisfying the predicate.
// In the second, the goal of the search is to locate the largest value satisfying the predicate.
// For more info, see: https://www.topcoder.com/community/data-science/data-science-tutorials/binary-search/.
public static class BinarySearch
{
    public enum Mode
    {
        FalseToTrue,
        TrueToFalse
    };

    public static int? Search(int start, int end, Predicate<int> verifier, Mode mode)
        => mode == Mode.FalseToTrue
        ? SearchFalseToTrue(start, end, verifier)
        : SearchTrueToFalse(start, end, verifier);

    private static int? SearchFalseToTrue(int start, int end, Predicate<int> verifier)
    {
        if (start > end) return null;

        int mid;

        while (start != end)
        {
            mid = start + (end - start) / 2;

            if (verifier(mid))
            {
                end = mid;
            }
            else
            {
                start = mid + 1;
            }
        }

        return verifier(start) ? start : (int?)null;
    }

    private static int? SearchTrueToFalse(int start, int end, Predicate<int> verifier)
    {
        if (start > end) return null;

        int mid;

        while (start != end)
        {
            mid = start + (end - start + 1) / 2;

            if (verifier(mid))
            {
                start = mid;
            }
            else
            {
                end = mid - 1;
            }
        }

        return verifier(start) ? start : (int?)null;
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
            int orderCount = FastIO.ReadNonNegativeInt();
            var orders = new FlightOrder[orderCount];
            for (int o = 0; o < orderCount; ++o)
            {
                orders[o] = new FlightOrder(
                    startTime: FastIO.ReadNonNegativeInt(),
                    duration: FastIO.ReadNonNegativeInt(),
                    profit: FastIO.ReadNonNegativeInt());
            }

            output.Append(
                RENT.Solve(orderCount, orders));
            output.AppendLine();
        }

        Console.Write(output);
    }
}

// This is based in part on submissions from https://www.codechef.com/status/INTEST.
// It's assumed the input is well-formed, so if you try to read an integer when no
// integers remain in the input, there's undefined behavior (infinite loop).
// NOTE: FastIO might not be necessary, but the problem came with an IO warning.
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
