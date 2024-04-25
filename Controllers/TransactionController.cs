namespace TransactionManager
{
    
    public class TransactionController
    {
        private readonly IRepository<Transaction> _storage;

        public TransactionController(IRepository<Transaction> storage)
        {
            _storage = storage;
        }

        public Transaction GetTransaction(int id)
        {
            return _storage.Get(id);
        }

        public void AddTransaction(Transaction transaction)
        {
            if (transaction == null) return;

            _storage.Add(transaction);
        }

        public bool TransactionExists(int id)
        {
            return _storage.IdExists(id);
        }
    }
}
