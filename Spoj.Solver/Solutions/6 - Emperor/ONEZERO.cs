using System;
using System.Collections.Generic;
using System.Text;

// https://www.spoj.com/problems/ONEZERO/ #ad-hoc #bfs #bitmask #mod-math
// Finds the smallest multiple of a number that's composed of all 1s and 0s.
public static class ONEZERO
{
    public static string Solve(int n)
    {
        if (n == 1)
            return "1";

        bool[] discoveredNumbersModN = new bool[n];
        var numbersToVisit = new Queue<NumberHaving1sAnd0s>();
        discoveredNumbersModN[1] = true;
        numbersToVisit.Enqueue(new NumberHaving1sAnd0s(
            numberModN: 1 % n,
            binaryEqualingNumber: 1));

        // The BFS traverses numbers composed of all 1s and 0s in the following order:
        // 1,
        // 10,       11,
        // 100, 101, 110, 111..
        // From smallest to largest, by multiplying by 10, and multiplying by 10 and adding
        // 1 to produce two new numbers from the number currently being considered. These will
        // get really huge so we work with the numbers % n. (number % n * 10) % n = (number * 10) % n
        // and similar when addition is involved, so this is fine. The big optimization is not
        // enqueuing a number into the BFS if we've already enqueued an earlier number equal to it % n.
        // The operations % n would all be the same, but the earlier number would find a smaller multiple.
        // The actual numbers are too big to store as numbers, so store the number whose binary
        // representation (parsed as a number) equals the number. Example:
        // Say n = 17. 101 % 17 = 16. We store the 16, and we store 101 by storing 5, because 5's
        // binary representation is 101. To search from 101 we do 101 * 10 % 17 = 16 * 10 % 17 = 7,
        // we store the 7, and we store 1010 by storing 5 << 1 = 10, because 10's binary representation
        // is 1010. We also do (101 * 10 + 1) % 17 = (16 * 10 + 1) % 17 = 8, we store the 8, and we store
        // 1011 by storing (5 << 1) + 1 = 11, because 11's binary representation is 1011.
        while (true)
        {
            int waveSize = numbersToVisit.Count;
            for (int i = 0; i < waveSize; ++i)
            {
                var number = numbersToVisit.Dequeue();

                var numberTimes10 = number.GetNumberTimes10(n);
                if (numberTimes10.NumberModN == 0)
                    return numberTimes10.Number;
                else if (!discoveredNumbersModN[numberTimes10.NumberModN])
                {
                    discoveredNumbersModN[numberTimes10.NumberModN] = true;
                    numbersToVisit.Enqueue(numberTimes10);
                }

                var numberTimes10Plus1 = number.GetNumberTimes10Plus1(n);
                if (numberTimes10Plus1.NumberModN == 0)
                    return numberTimes10Plus1.Number;
                else if (!discoveredNumbersModN[numberTimes10Plus1.NumberModN])
                {
                    discoveredNumbersModN[numberTimes10Plus1.NumberModN] = true;
                    numbersToVisit.Enqueue(numberTimes10Plus1);
                }
            }
        }
    }

    private struct NumberHaving1sAnd0s
    {
        public NumberHaving1sAnd0s(int numberModN, long binaryEqualingNumber)
        {
            NumberModN = numberModN;
            BinaryEqualingNumber = binaryEqualingNumber;
        }

        public int NumberModN { get; set; }
        public long BinaryEqualingNumber { get; set; }
        public string Number => Convert.ToString(BinaryEqualingNumber, toBase: 2);

        public NumberHaving1sAnd0s GetNumberTimes10(int n)
            => new NumberHaving1sAnd0s(
                numberModN: NumberModN * 10 % n,
                binaryEqualingNumber: BinaryEqualingNumber << 1);

        public NumberHaving1sAnd0s GetNumberTimes10Plus1(int n)
            => new NumberHaving1sAnd0s(
                numberModN: (NumberModN * 10 + 1) % n,
                binaryEqualingNumber: (BinaryEqualingNumber << 1) + 1);
    }
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            int n = int.Parse(Console.ReadLine());

            output.Append(
                ONEZERO.Solve(n));
            output.AppendLine();
        }

        Console.Write(output);
    }
}
