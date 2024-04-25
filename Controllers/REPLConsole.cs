using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;

namespace TransactionManager
{
    public class REPLConsole
    {
        private Dictionary<string, Action> knownCommands = new Dictionary<string, Action>();

        public REPLConsole(params IConsoleControllable[] controllers)
        {
            foreach (var c in controllers)
            {
                foreach (var kvp in c.GetConsoleCommands())
                {
                    // [TODO] No conflict resolution, out of scope for this excercise
                    knownCommands[kvp.Key] = kvp.Value;
                }
            }
        }

        public void Run()
        {
            while (true)
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
    }
}
