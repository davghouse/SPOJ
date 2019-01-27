using System;
using System.IO;

// https://www.spoj.com/problems/ASSIGN/ #bitmask #dynamic-programming
// Finds the number of ways to assign topics such that all students are happy.
public static class ASSIGN
{
    // The number of students (== number of topics) only goes up to 20. There are 2^20 different
    // sets of students. These sets can be represented using the bits of an integer; 00...0 for
    // the empty set (integer value of 0), to 11...1 for the set of all students (integer value
    // 2^20 - 1 = 1048575).Tthe ith bit being 0 or 1 represents the ith student being in the
    // set or not. Imagine a 2D array where one dimension has the sets of students (0 to 2^20 - 1),
    // and the other has the number of topics being considered. We want to know how many ways
    // there are to satisfy a set s's students when the first t topics are being considered.
    // Call this n(s, t). The final number we want is n(s=11...1, t = studentCount)--the number
    // of ways to satisfy the set of all students using all studentCount topics.

    // Well, n(s, t) includes everything from n(s, t - 1), since we still have access to all
    // those t - 1 topics. Anything extra that n(s, t - 1) doesn't already include must make
    // use of the t'th topic. Say a student e in s is okay doing the t'th topic. Then we add on
    // n(s - e, t - 1), the number of ways to satisfy the students other than e (he's satisfied now),
    // using all the remaining topics (everything up through t - 1). We add this on for each
    // student e that is okay with doing the t'th topic. We don't double count anything because
    // the student getting assigned to t is different for each of these. It's important to
    // understand what s - e means. The set s has an integer, and s - e is gotten by turning
    // off the bit in that integer's binary representation corresponding to student e.
    // If we start from s = 0 and run the DP to s = 1048575, we'll always have the s - e row
    // filled in when we need it, since s - e is less than s.

    // If you do an example on paper of the above, it becomes clear we don't need the 2nd
    // dimension for t. Consider some set s. It has a number of students in it, say c,
    // equal to however many bits are turned on. We can't satisfy c people unless we use at
    // least c topics. So n(s, t) = 0 for all t < c. And then to figure out n(s, c), we only use
    // data from n(s - e, c - 1)--again, where t (now c - 1) equals the size of the set (now s - e).
    // Storing n(s, t) for t < c is pointless, they're all 0, and storing n(s, t) for t > c is
    // useless, because the DP only ever uses n(s, c), where c equals the size of the set s.
    public static long Solve(int studentCount, bool[,] studentPreferences)
    {
        int numberOfStudentSets = 1 << studentCount;

        // The number of ways to satisfy a set of students, by assigning them only the first t
        // topics, where t equals the size of the set. The index into this array corresponds
        // to the set of students being considered, via the index's binary representation.
        long[] numberOfWaysToSatisfyAStudentSet = new long[numberOfStudentSets];
        numberOfWaysToSatisfyAStudentSet[0] = 1; // No students, no topics, only 1 way.

        for (int studentSet = 1; studentSet < numberOfStudentSets; ++studentSet)
        {
            int numberOfStudentsInSet = 0;
            for (int student = 0; student < studentCount; ++student)
            {
                if (IsStudentInSet(studentSet, student))
                {
                    ++numberOfStudentsInSet;
                }
            }

            for (int student = 0; student < studentCount; ++student)
            {
                // We are considering the first t topics, where t = numberOfStudentsInSet. If the
                // student is in the set and he's willing to take topic t, we add in how many
                // students from the set minus the chosen student are willing to take the first
                // t - 1 topics, which is a value we've already calculated by this point.
                if (IsStudentInSet(studentSet, student)
                    && studentPreferences[student, numberOfStudentsInSet - 1 /* 0-indexed */])
                {
                    numberOfWaysToSatisfyAStudentSet[studentSet]
                        += numberOfWaysToSatisfyAStudentSet[studentSet ^ (1 << student)];
                }
            }
        }

        return numberOfWaysToSatisfyAStudentSet[numberOfStudentSets - 1];
    }

    // If the studentSet has the student's bit turned on, the student is in the set.
    private static bool IsStudentInSet(int studentSet, int student)
        => (studentSet & (1 << student)) != 0;
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = FastIO.ReadNonNegativeInt();
        while (remainingTestCases-- > 0)
        {
            int studentCount = FastIO.ReadNonNegativeInt();
            bool[,] studentPreferences = new bool[studentCount, studentCount];

            for (int s = 0; s < studentCount; ++s)
            {
                for (int t = 0; t < studentCount; ++t)
                {
                    studentPreferences[s, t] = FastIO.ReadNonNegativeInt() == 1;
                }
            }

            FastIO.WriteNonNegativeLong(
                ASSIGN.Solve(studentCount, studentPreferences));
            FastIO.WriteLine();
        }

        FastIO.Flush();
    }
}

// This is based in part on submissions from https://www.codechef.com/status/INTEST.
// It's assumed the input is well-formed, so if you try to read an integer when no
// integers remain in the input, there's undefined behavior (infinite loop).
// NOTE: Not sure if FastIO is necessary, but TLE could be an issue.
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
    private static readonly byte[] _digitsBuffer = new byte[20];
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

    public static void WriteNonNegativeLong(long value)
    {
        int digitCount = 0;
        do
        {
            int digit = (int)(value % 10);
            _digitsBuffer[digitCount++] = (byte)(digit + _zero);
            value /= 10;
        } while (value > 0);

        if (_outputBufferSize + digitCount > _outputBufferLimit)
        {
            _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
            _outputBufferSize = 0;
        }

        while (digitCount > 0)
        {
            _outputBuffer[_outputBufferSize++] = _digitsBuffer[--digitCount];
        }
    }

    public static void WriteLine()
    {
        if (_outputBufferSize == _outputBufferLimit) // else _outputBufferSize < _outputBufferLimit.
        {
            _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
            _outputBufferSize = 0;
        }

        _outputBuffer[_outputBufferSize++] = _newLine;
    }

    public static void Flush()
    {
        _outputStream.Write(_outputBuffer, 0, _outputBufferSize);
        _outputBufferSize = 0;
        _outputStream.Flush();
    }
}
