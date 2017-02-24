namespace Daves.SpojSpace.Library.Heaps
{
    public interface ISimpleHeap<T>
    {
        int Size { get; }
        bool IsEmpty { get; }
        T Top { get; }

        void Insert(T value);
        T Extract();
        T Replace(T value);
    }
}
