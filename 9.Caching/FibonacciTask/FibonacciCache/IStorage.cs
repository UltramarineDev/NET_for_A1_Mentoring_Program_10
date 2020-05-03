namespace FibonacciCache
{
    public interface IStorage
    {
        int? GetValueOrNull(int key);
        void AddOrUpdate(int key, int value);
    }
}
