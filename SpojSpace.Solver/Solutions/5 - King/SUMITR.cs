// Actual submission, 312 bytes:
// using System;class P{static int I()=>int.Parse(Console.ReadLine());static void Main(){int t=I(),s,m,i,j;while(t-->0){s=I()+1;var v=new int[s,s];m=0;for(i=1;i<s;++i){var l=Console.ReadLine().Split();for(j=1;j<=i;++j)m=Math.Max(m,v[i,j]=int.Parse(l[j-1])+Math.Max(v[i-1,j],v[i-1,j-1]));}Console.WriteLine(m);}}}

using System;

// http://www.spoj.com/problems/SUMITR/ #dynamic-programming-2d #golf #path-optimization
// Finds greatest path sum in a triangular grid, while minimizing bytes of code.
// See SUMITR.cpp--this solution was submitted using C++ because I couldn't get C# under 256 bytes.
public static class SUMITR
{
    // Standard DP like BYTESM2, just with two viable moves instead of three. Simplify
    // by storing the triangle within a rectangular grid. Add an extra row and
    // column (of zeros) on the top and the left, to handle movement edge cases implicitly.
    // Carry the max along, and both parse the input and do the DP at the same time.
    public static int Solve(int size)
    {
        int max = 0;
        int[,] values = new int[size + 1, size + 1];

        for (int i = 1; i <= size; ++i)
        {
            string[] line = Console.ReadLine().Split();
            for (int j = 1; j <= i; ++j)
            {
                max = Math.Max(max,
                    values[i, j] = int.Parse(line[j - 1]) + Math.Max(values[i - 1, j], values[i - 1, j - 1]));
            }
        }

        return max;
    }
}

public static class Program
{
    private static void Main()
    {
        int remainingTestCases = int.Parse(Console.ReadLine());
        while (remainingTestCases-- > 0)
        {
            Console.WriteLine(
                SUMITR.Solve(int.Parse(Console.ReadLine())));
        }
    }
}
