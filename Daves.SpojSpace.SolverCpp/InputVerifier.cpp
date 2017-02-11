#define _CRT_SECURE_NO_WARNINGS
#include <iostream>
#include <stdio.h>
#include <stdexcept>
using namespace std;

// See InputVerifier.cs, this is used when C# is too slow to even verify input.
int main()
{
  int remainingTestCases;
  cin >> remainingTestCases;

  while (remainingTestCases-- > 0)
  {
    int a, b;
    cin >> a >> b;

    if (a < 0 || b < 0)
    {
      throw invalid_argument("");
    }
  }

  return 0;
}

int main1()
{
  int remainingTestCases;
  scanf("%d", &remainingTestCases);

  while (remainingTestCases-- > 0)
  {
    int a, b;
    scanf("%d %d", &a, &b);

    if (a < 0 || b < 0)
    {
      throw invalid_argument("");
    }
  }

  return 0;
}
