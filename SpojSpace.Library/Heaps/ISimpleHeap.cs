namespace SpojSpace.Library.Heaps
{
    public interface ISimpleHeap<T>
    {
        int Size { get; }
        bool IsEmpty { get; }
        T Top { get; }

        void Add(T value);
        T Extract();
        T Replace(T value);
    }
}
