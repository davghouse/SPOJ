using System.Collections.Generic;

namespace Daves.SpojSpace.Library.Heaps
{
    public interface IHeap<TKey, TValue>
    {
        int Size { get; }
        bool IsEmpty { get; }
        KeyValuePair<TKey, TValue> Top { get; }

        void Insert(TKey key, TValue value);
        void Insert(KeyValuePair<TKey, TValue> keyValuePair);
        KeyValuePair<TKey, TValue> Extract();
        KeyValuePair<TKey, TValue> Replace(TKey key, TValue value);
        KeyValuePair<TKey, TValue> Replace(KeyValuePair<TKey, TValue> keyValuePair);
        bool Contains(TKey key);
        TValue GetValue(TKey key);
        TValue Update(TKey key, TValue value);
        TValue Update(KeyValuePair<TKey, TValue> keyValuePair);
    }
}
