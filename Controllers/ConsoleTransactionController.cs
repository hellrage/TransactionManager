using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;

namespace TransactionManager
{
    public class ConsoleTransactionController : TransactionController, IConsoleControllable
    {
        protected Dictionary<string, Action> Commands;

        public ConsoleTransactionController(IRepository<Transaction> storage) : base(storage)
        {
            Commands = new Dictionary<string, Action>();
            Commands.Add("get", () => this.ConsoleGetTransaction());
            Commands.Add("add", () => this.ConsoleAddTransaction());
        }

        public Dictionary<string, Action> GetConsoleCommands()
        {
            return Commands;
        }

        private void ConsoleGetTransaction()
        {
            while (true)
            {
                Console.Write("Введите Id: ");

                var input = Console.ReadLine();
                if (input == "exit")
                    return;

                if (!int.TryParse(input, out int result) || result < 0)
                {
                    Console.WriteLine($"Id должен быть положительным целым числом до {int.MaxValue}");
                    continue;
                }

                Transaction transaction = this.GetTransaction(result);
                if (transaction == null)
                {
                    Console.WriteLine($"Транзакции с Id {result} не найдено.");
                    return;
                }
                else
                {
                    Console.WriteLine(JsonSerializer.Serialize(transaction));
                    Console.WriteLine("[OK]");
                    return;
                }
            }
        }

        private void ConsoleAddTransaction()
        {
            Transaction candidate = new Transaction();

            while (true)
            {
                Console.Write("Введите Id: ");
                var input = Console.ReadLine();

                if (input == "exit")
                    return;

                if (!int.TryParse(input, out int result) || result < 0)
                {
                    Console.WriteLine($"Id должен быть положительным целым числом до {int.MaxValue}");
                    continue;
                }

                if (this.TransactionExists(result))
                {
                    Console.Write($"Id {result} уже занят. Продолжить и перезаписать транзакцию с этим Id? y/n: ");
                    if (Console.ReadLine().ToLower() != "y")
                        continue;
                }

                candidate.Id = result;
                break;
            }

            while (true)
            {
                Console.Write("Введите дату: ");

                var input = Console.ReadLine();
                if (input == "exit")
                    return;

                CultureInfo ruRu = new CultureInfo("ru-RU");
                if (DateTime.TryParse(input, ruRu, DateTimeStyles.None, out DateTime result))
                {
                    candidate.TransactionDate = result;
                    break;
                }
                else
                    Console.WriteLine($"Неправильный формат даты, ожидается: dd.MM.yyyy[ hh:mm:ss]");
            }

            while (true)
            {
                Console.Write("Введите сумму: ");

                var input = Console.ReadLine();
                if (input == "exit")
                    return;

                if (decimal.TryParse(input, out decimal result))
                {
                    candidate.Amount = result;
                    break;
                }
                else
                    Console.WriteLine($"Неправильный формат суммы, ожидается: [-]x[.x]");
            }

            // [TODO] Additional validation for actual values (min-max amount, date, etc)?
            this.AddTransaction(candidate);
            Console.WriteLine("[OK]");
        }
    }
}
