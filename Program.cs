namespace TransactionManager
{
    class Program
    {
        static void Main(string[] args)
        {
            MemoryStorage<Transaction> transactionStorage = new MemoryStorage<Transaction>();
            ConsoleTransactionController controller = new ConsoleTransactionController(transactionStorage);
            REPLConsole terminal = new REPLConsole(controller);
            terminal.Run();
        }
    }
}
