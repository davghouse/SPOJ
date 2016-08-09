#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>

// See OLOLO.cs--this solution was submitted using C++ because C# I/O is too slow.
int main()
{
  long remainingPyanis, pyaniNumber, result = 0;
  scanf("%ld", &remainingPyanis);

  while (remainingPyanis-- > 0)
  {
    scanf("%ld", &pyaniNumber);
    result ^= pyaniNumber;
  }

  printf("%ld", result);

  return 0;
}
