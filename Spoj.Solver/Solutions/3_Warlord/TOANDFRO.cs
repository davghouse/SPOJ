using System;
using System.Text;

// To and Fro
// 400 http://www.spoj.com/problems/TOANDFRO/
// Given an encoded message and the number of columns used to encode the message,
// transform the message to the array used for the encoding, and then to the original (padded) message.
public static class TOANDFRO
{
    public static string Solve(int columnCount, string encodedMessage)
    {
        int rowCount = encodedMessage.Length / columnCount;
        char[,] messageArray = new char[rowCount, columnCount];

        // Traverse the array as it was traversed to create the encoded message,
        // but now instead of filling in the message, fill in the array.
        for (int r = 0, i = 0; r < rowCount; ++r)
        {
            if (r % 2 == 0)
            {
                // On an even row, so we're filling in from left to right.
                for (int c = 0; c < columnCount; ++c)
                {
                    messageArray[r, c] = encodedMessage[i++];
                }
            }
            else
            {
                // On an odd row, so we're filling in from right to left.
                for (int c = columnCount - 1; c >= 0; --c)
                {
                    messageArray[r, c] = encodedMessage[i++];
                }
            }
        }

        StringBuilder originalMessage = new StringBuilder();
        // Traverse the array as it was traversed to set the original message,
        // but now instead of filling in the array, fill in the original message.
        for (int c = 0; c < columnCount; ++c)
        {
            for (int r = 0; r < rowCount; ++r)
            {
                originalMessage.Append(messageArray[r, c]);
            }
        }

        return originalMessage.ToString();
    }
}

public static class Program
{
    private static void Main()
    {
        int columnCount;
        while ((columnCount = int.Parse(Console.ReadLine())) != 0)
        {
            Console.WriteLine(
                TOANDFRO.Solve(columnCount, Console.ReadLine()));
        }
    }
}
 