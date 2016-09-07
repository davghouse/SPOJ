using System;
using System.Collections.Generic;
using System.Linq;

// 24 http://www.spoj.com/problems/FCTRL2/ Small factorials
// Returns n! for 1 <= n <= 100.
public static class FCTRL2
{
    private const int _limit = 100;
    private static readonly IReadOnlyList<BigInteger> _factorials;

    static FCTRL2()
    {
        var factorials = new BigInteger[_limit + 1];
        factorials[0] = BigInteger.One;

        for (int i = 1; i <= _limit; ++i)
        {
            factorials[i] = factorials[i - 1] * (new BigInteger(i));
        }

        _factorials = factorials;
    }

    public static BigInteger Solve(int n)
        => _factorials[n];
}

public struct BigInteger : IEquatable<BigInteger>
{
    private static readonly BigInteger _zero = new BigInteger(0);
    private static readonly BigInteger _one = new BigInteger(1);
    public static BigInteger Zero => _zero;
    public static BigInteger One => _one;

    private readonly IReadOnlyList<byte> _digits;

    private BigInteger(IReadOnlyList<byte> digits)
    {
        _digits = digits;
    }

    public BigInteger(int n)
        : this(n.ToString())
    { }

    public BigInteger(string digits)
    {
        var digitsArray = new byte[digits.Length];

        for (int i = 0; i < digits.Length; ++i)
        {
            digitsArray[i] = byte.Parse(digits[digits.Length - i - 1].ToString());
        }

        _digits = Array.AsReadOnly(digitsArray);
    }

    public bool IsZero => this == Zero;
    public bool IsOne => this == One;

    public static BigInteger operator +(BigInteger a, BigInteger b)
    {
        // No more than multiplying the larger by 2, so can't require more than one extra digit.
        int maxDigitCount = Math.Max(a._digits.Count, b._digits.Count);
        var result = new List<byte>(maxDigitCount + 1);

        byte carry = 0;
        for (int i = 0; i < maxDigitCount; ++i)
        {
            byte value = carry;
            if (i < a._digits.Count)
            {
                value += a._digits[i];
            }
            if (i < b._digits.Count)
            {
                value += b._digits[i];
            }
            result.Add((byte)(value % 10));
            carry = (byte)(value / 10);
        }
        if (carry != 0)
        {
            result.Add(carry);
        }

        return new BigInteger(result.AsReadOnly());
    }

    public static BigInteger operator *(BigInteger a, BigInteger b)
    {
        var result = BigInteger.Zero;

        for (int i = 0; i < b._digits.Count; ++i)
        {
            result += a.MultiplyByDigit(b._digits[i]).MultiplyByPowerOfTen(i);
        }

        return result;
    }

    private BigInteger MultiplyByDigit(byte digit)
    {
        if (IsZero || digit == 1) return this;
        if (digit == 0) return BigInteger.Zero;

        // Digit is less than 10 so the result can't require more than one extra digit.
        var result = new List<byte>(_digits.Count + 1);

        byte carry = 0;
        for (int i = 0; i < _digits.Count; i++)
        {
            byte value = (byte)(_digits[i] * digit + carry);
            result.Add((byte)(value % 10));
            carry = (byte)(value / 10);
        }
        if (carry != 0)
        {
            result.Add(carry);
        }

        return new BigInteger(result.AsReadOnly());
    }

    private BigInteger MultiplyByPowerOfTen(int power)
    {
        if (IsZero || power == 0) return this;

        var result = new byte[_digits.Count + power];

        for (int i = 0; i < power; ++i)
        {
            result[i] = 0;
        }
        for (int i = 0; i < _digits.Count; ++i)
        {
            result[power + i] = _digits[i];
        }

        return new BigInteger(Array.AsReadOnly(result));
    }

    public override string ToString()
        => string.Concat(_digits.Reverse());

    #region IEquatable<BigInteger>

    public bool Equals(BigInteger other)
        => _digits.SequenceEqual(other._digits);

    public override bool Equals(object obj)
        => obj is BigInteger ? Equals((BigInteger)obj) : false;

    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }

    public static bool operator ==(BigInteger a, BigInteger b)
        => a.Equals(b);

    public static bool operator !=(BigInteger a, BigInteger b)
        => !(a == b);

    #endregion IEquatable<BigInteger>
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());

        while (remainingTestCases-- > 0)
        {
            Console.WriteLine(
                FCTRL2.Solve(int.Parse(Console.ReadLine())));
        }
    }
}
