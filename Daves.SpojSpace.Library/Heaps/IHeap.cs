using System.Collections.Generic;

namespace Daves.SpojSpace.Library.Heaps
{
    public interface IHeap<TKey, TValue>
    {
        int Size { get; }
        bool IsEmpty { get; }
        KeyValuePair<TKey, TValue> Top { get; }

        void Insert(KeyValuePair<TKey, TValue> keyValuePair);
        KeyValuePair<TKey, TValue> Extract();
        KeyValuePair<TKey, TValue> Replace(KeyValuePair<TKey, TValue> keyValuePair);
    }
}
