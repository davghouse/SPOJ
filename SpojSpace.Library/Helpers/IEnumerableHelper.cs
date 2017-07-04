using System.Collections.Generic;
using System.Linq;

namespace SpojSpace.Library.Helpers
{
    public static class IEnumerableHelper
    {
        public static bool SetEqual<T>(this IEnumerable<T> first, IEnumerable<T> second)
            => !first.Except(second).Any()
            && !second.Except(first).Any();
    }
}
