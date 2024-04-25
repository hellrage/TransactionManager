namespace TransactionManager
{
    class Program
    {
        static void Main(string[] args)
        {
            TransactionMemoryStorage storage = new TransactionMemoryStorage();
            TransactionController controller = new TransactionController(storage);
            REPLConsole terminal = new REPLConsole(controller);
            terminal.Run();
        }
    }
}
