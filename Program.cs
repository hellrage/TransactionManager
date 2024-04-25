namespace TransactionManager
{
    class Program
    {
        static void Main(string[] args)
        {
            TransactionMemoryStorage storage = new TransactionMemoryStorage();
            ConsoleTransactionController controller = new ConsoleTransactionController(storage);
            REPLConsole terminal = new REPLConsole(controller);
            terminal.Run();
        }
    }
}
