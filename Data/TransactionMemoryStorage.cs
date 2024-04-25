using System.Collections.Generic;

namespace TransactionManager
{
    /// <summary>
    /// Volatile storage for keeping entities in-memory.
    /// </summary>
    public class TransactionMemoryStorage : IRepository<Transaction>
    {
        private Dictionary<int, Transaction> storage;

        public TransactionMemoryStorage()
        {
            storage = new Dictionary<int, Transaction>();
        }

        public void Add(Transaction entity)
        {
            if (entity == null) return;

            storage[entity.Id] = entity;
        }

        public Transaction Get(int id)
        {
            Transaction value;
            storage.TryGetValue(id, out value);

            return value;
        }

        public bool IdExists(int id)
        {
            return storage.ContainsKey(id);
        }
    }
}
