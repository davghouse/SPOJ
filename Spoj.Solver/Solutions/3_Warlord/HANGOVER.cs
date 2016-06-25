using System;
using System.Collections.Generic;

// HANGOVER
// 902 http://www.spoj.com/problems/HANGOVER/
// Finds the number of cards necessary to achieve a given overhang value (less than 5.2).
public static class HANGOVER
{
    private const float _overhangLimit = 5.2f;
    // Took a look at the data and it seems like float's precision won't be a problem here (at least for the first 277 terms).
    private static readonly List<float> _runningOverhangTotals;

    static HANGOVER()
    {
        _runningOverhangTotals = new List<float>();
        _runningOverhangTotals.Add(0); // Zero cards has zero overhang.

        int cardCounter = 0;
        float overhangTotal = 0f;
        while (overhangTotal < _overhangLimit)
        {
            ++cardCounter;
            // The nth card adds 1 / (n + 1) to the overhang total.
            overhangTotal += 1f / (cardCounter + 1);
            _runningOverhangTotals.Add(overhangTotal);
        }
    }

    public static string Solve(float overhang)
    {
        int cardCount = _runningOverhangTotals.BinarySearch(overhang);
        cardCount = cardCount < 0 ? ~cardCount : cardCount;

        return $"{cardCount} card(s)";
    }
}

public static class Program
{
    private static void Main()
    {
        float overhang;
        while ((overhang = float.Parse(Console.ReadLine())) != 0f)
        {
            Console.WriteLine(
                HANGOVER.Solve(overhang));
        }
    }
}
