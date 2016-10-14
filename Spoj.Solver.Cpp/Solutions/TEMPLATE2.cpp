#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>

// See TEMPLATE.cs--this solution was submitted using C++ because C# I/O is too slow.
class TEMPLATE
{
public:
  TEMPLATE()
  { }

  int Solve(int a, int b)
  {
    return a * b;
  }
};

int main()
{
  TEMPLATE solver;

  int remainingTestCases;
  scanf("%d", &remainingTestCases);

  while (remainingTestCases-- > 0)
  {
    int a, b;
    scanf("%d %d", &a, &b);

    printf("%d", solver.Solve(a, b));
  }

  return 0;
}
