using System;

namespace FibonacciCache
{
    public class FibonacciGenerator
    {
        private readonly IStorage _storage;

        public FibonacciGenerator(IStorage storage)
        {
            _storage = storage;
        }

        public int Generate(int position)
        {
            if (position < 0)
            {
                throw new ArgumentException("Nth sequence member should be positive number.");
            }

            if (position == 0 || position == 1)
            {
                return position;
            }

            var stored = _storage.GetValueOrNull(position);
            if (stored != null)
            {
                return (int)stored;
            }

            var result = Generate(position - 1) + Generate(position - 2);
            _storage.AddOrUpdate(position, result);

            return result;
        }
    }
}
