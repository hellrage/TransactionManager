using System.Collections.Generic;

namespace TransactionManager
{
    /// <summary>
    /// Volatile storage for keeping entities in-memory.
    /// </summary>
    public class MemoryStorage<T> : IRepository<T>
    {
        private Dictionary<int, T> storage;

        public MemoryStorage()
        {
            storage = new Dictionary<int, T>();
        }

        public void Add(T entity, int id)
        {
            if (entity == null) return;

            storage[id] = entity;
        }

        public T Get(int id)
        {
            T value;
            storage.TryGetValue(id, out value);

            return value;
        }

        public bool IdExists(int id)
        {
            return storage.ContainsKey(id);
        }
    }
}
