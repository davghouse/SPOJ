// Actual submission, 233 bytes:
// #include<iostream>
// #include<algorithm>
// using namespace std;int main(){int s,r,i,j,x,v[100][100]={0};cin>>s;while(cin>>s){r=0;for(i=1;i<=s;++i)for(j=1,x;j<=i;++j){cin>>x;r=max(r,v[i][j]=x+max(v[i-1][j],v[i-1][j-1]));}cout<<r<<endl;}}

#include <iostream>
#include <algorithm>
using namespace std;

// See SUMITR.cs for details--this solution was submitted using C++ because I couldn't get C# under 256 bytes.
int main()
{
  int size;
  int values[100][100] = {0};
  cin >> size;
  while (cin >> size)
  {
    int result = 0;
    for (int i = 1; i <= size; ++i)
    {
      for (int j = 1, x; j <= i; ++j)
      {
        cin >> x;
        result = max(result, values[i][j] = x + max(values[i - 1][j], values[i - 1][j - 1]));
      }
    }
    cout << result << endl;
  }
}
