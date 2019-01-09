#include <iostream>
using namespace std;

// See BOMARBLE.cs for details--this solution was submitted using C++ because C# was unavailable.
class BOMARBLE
{
public:
  static int Solve(int n)
  {
    return (3 * n * n + 5 * n + 2) / 2;
  }
};

int main()
{
  int n;
  while (cin >> n && n != 0)
    cout << BOMARBLE::Solve(n) << endl;

  return 0;
}
