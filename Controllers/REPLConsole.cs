using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;

namespace TransactionManager
{
    public class REPLConsole
    {
        private TransactionController _controller;
        private Dictionary<string, Action> knownCommands = new Dictionary<string, Action>();
        private bool abort = false;

        public REPLConsole(TransactionController controller)
        {
            _controller = controller;
            knownCommands.Add("exit", () => this.abort = true);
            knownCommands.Add("get", () => this.GetTransaction());
            knownCommands.Add("add", () => this.AddTransaction());
        }

        public void Run()
        {
            while (!abort)
            {
                Console.Write("Введите команду>");
                string command = Console.ReadLine().ToLower();
                if (command == "exit") break;

                Action procedure;
                if (knownCommands.TryGetValue(command, out procedure))
                    procedure();
                else
                {
                    Console.WriteLine($"Неизвестная команда, допустимые команды: {string.Join(", ", knownCommands.Keys)}");
                }
            }
        }

        public void GetTransaction()
        {
            while(true)
            {
                Console.Write("Введите Id: ");

                var input = Console.ReadLine();
                if (input == "exit")
                {
                    abort = true;
                    return;
                }

                if (!int.TryParse(input, out int result) || result < 0)
                {
                    Console.WriteLine($"Id должен быть положительным целым числом до {int.MaxValue}");
                    continue;
                }

                Transaction transaction = _controller.GetTransaction(result);
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

        public void AddTransaction()
        {
            Transaction candidate = new Transaction();

            while (true)
            {
                Console.Write("Введите Id: ");
                var input = Console.ReadLine();

                if (input == "exit")
                {
                    abort = true;
                    return;
                }

                if (!int.TryParse(input, out int result) || result < 0)
                {
                    Console.WriteLine($"Id должен быть положительным целым числом до {int.MaxValue}");
                    continue;
                }

                if(_controller.TransactionExists(result))
                {
                    Console.Write($"Id {result} уже занят. Продолжить и перезаписать транзакцию с этим Id? y/n: ");
                    if (Console.ReadLine().ToLower() == "n")
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
                {
                    abort = true;
                    return;
                }

                CultureInfo ruRu = new CultureInfo("ru-RU");
                if (DateTime.TryParseExact(input, "dd.MM.yyyy", ruRu, DateTimeStyles.None, out DateTime result))
                {
                    candidate.TransactionDate = result;
                    break;
                }
                else
                    Console.WriteLine($"Неправильный формат даты, ожидается: dd.MM.yyyy");
            }

            while (true)
            {
                Console.Write("Введите сумму: ");

                var input = Console.ReadLine();
                if (input == "exit")
                {
                    abort = true;
                    return;
                }

                if (decimal.TryParse(input, out decimal result))
                {
                    candidate.Amount = result;
                    break;
                }
                else
                    Console.WriteLine($"Неправильный формат суммы, ожидается: [-]x[.x]");
            }

            _controller.AddTransaction(candidate);
            Console.WriteLine("[OK]");
        }
    }
}
