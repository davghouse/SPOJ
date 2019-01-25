using System;
using System.IO;
using System.Text;

// https://www.spoj.com/problems/TWENDS/ #dynamic-programming-2d #game #optimization
// Finds how bad a greedy strategy is when players take turns choosing end cards.
public static class TWENDS
{
    // Note the recursive solution: first player can choose either left or right
    // end. Once they do, the greedy player chooses the biggest of the ends. Take
    // the max of (those two options + the solution for their remaining cards). Can
    // set the dynamic programming use a 2d array, where the row is the start index
    // of the card range we're considering, and the column is the end index. We
    // need the fill in the array in order to get bestFirstPlayerScore[0, cardCount - 1],
    // the best the first player can do when playing on the full range of cards.
    //  | 0 | 1 | 2 | 3 | 4 | 5 |
    // -|------------------------
    // 0|   | ? |   | ? |   | A |
    // -|------------------------
    // 1|   |   | ? |   | ? |   |
    // -|------------------------
    // 2|   |   |   | ? |   | ? |
    // -|------------------------
    // 3|   |   |   |   | ? |   |
    // -|------------------------
    // 4|   |   |   |   |   | ? |
    // -|------------------------
    // 5|   |   |   |   |   |   |
    // -|------------------------
    // Consider the case where there are 6 cards. We want [0, 5]. In order to get [0, 5],
    // we can choose 0 for the first player and 1 or 5 for the second, or we can choose 5
    // for the first players and 0 or 4 for the second. Thus, we might need to know the
    // answer to [2, 5], [1, 4], or [0, 3]--all the cells 2 away from the final answer.
    // So we build up these diagonals, starting from the center and workings towards A.
    public static int Solve(int cardCount, int[] cards)
    {
        int[,] bestFirstPlayerScore = new int[cardCount, cardCount];

        // Initialize along the first diagonal--these ranges includes just two cards.
        for (int r = 0; r < cardCount - 1; ++r)
        {
            bestFirstPlayerScore[r, r + 1] = Math.Abs(cards[r] - cards[r + 1]);
        }

        int startColumn = 3;
        while (startColumn < cardCount)
        {
            for (int r = 0, c = r + startColumn; c < cardCount; ++r, ++c)
            {
                /* We're determining [r, c], the best score the first player can get when
                   playing the cards from r to c. The first player can choose left or right. */

                // Try choosing the left card...
                int bestfirstPlayerScoreChoosingLeftCard = cards[r];
                // The second player must choose the largest end card from the range [r + 1, c]...
                if (cards[r + 1] >= cards[c])
                {
                    // r + 1 was greedily chosen, leaving the range [r + 2, c] still to be played.
                    bestfirstPlayerScoreChoosingLeftCard += -cards[r + 1]
                        + bestFirstPlayerScore[r + 2, c];
                }
                else
                {
                    // c was greedily chosen, leaving the range [r + 1, c - 1] still to be played.
                    bestfirstPlayerScoreChoosingLeftCard += -cards[c]
                        + bestFirstPlayerScore[r + 1, c - 1];
                }

                // Try choosing the right card...
                int bestFirstPlayerScoreChoosingRightCard = cards[c];
                // The second player must choose the largest end card from the range [r, c - 1]...
                if (cards[r] >= cards[c - 1])
                {
                    // r was greedily chosen, leaving the range [r + 1, c - 1] still to be played.
                    bestFirstPlayerScoreChoosingRightCard += -cards[r]
                        + bestFirstPlayerScore[r + 1, c - 1];
                }
                else
                {
                    // c - 1 was greedily chosen, leaving the range [r, c - 2] still to be played.
                    bestFirstPlayerScoreChoosingRightCard += -cards[c - 1]
                        + bestFirstPlayerScore[r, c - 2];
                }

                bestFirstPlayerScore[r, c] = Math.Max(
                    bestfirstPlayerScoreChoosingLeftCard,
                    bestFirstPlayerScoreChoosingRightCard);
            }

            startColumn += 2;
        }

        return bestFirstPlayerScore[0, cardCount - 1];
    }
}

public static class Program
{
    private static void Main()
    {
        var output = new StringBuilder();
        int gameCounter = 0;
        int cardCount;
        while ((cardCount = FastIO.ReadNonNegativeInt()) != 0)
        {
            int[] cards = new int[cardCount];
            for (int c = 0; c < cardCount; ++c)
            {
                cards[c] = FastIO.ReadNonNegativeInt();
            }

            output.Append(
                $"In game {++gameCounter}, the greedy strategy might lose by as" +
                $" many as {TWENDS.Solve(cardCount, cards)} points.");
            output.AppendLine();
        }

        Console.Write(output);
    }
}

// This is based in part on submissions from https://www.codechef.com/status/INTEST.
// It's assumed the input is well-formed, so if you try to read an integer when no
// integers remain in the input, there's undefined behavior (infinite loop).
// NOTE: FastIO might not be necessary, but the problem seems to have a strict time limit.
public static class FastIO
{
    private const byte _null = (byte)'\0';
    private const byte _newLine = (byte)'\n';
    private const byte _minusSign = (byte)'-';
    private const byte _zero = (byte)'0';
    private const int _inputBufferLimit = 8192;
    private const int _outputBufferLimit = 8192;

    private static readonly Stream _inputStream = Console.OpenStandardInput();
    private static readonly byte[] _inputBuffer = new byte[_inputBufferLimit];
    private static int _inputBufferSize = 0;
    private static int _inputBufferIndex = 0;

    private static readonly Stream _outputStream = Console.OpenStandardOutput();
    private static readonly byte[] _outputBuffer = new byte[_outputBufferLimit];
    private static readonly byte[] _digitsBuffer = new byte[11];
    private static int _outputBufferSize = 0;

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
