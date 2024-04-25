using System;
using System.Collections.Generic;

namespace TransactionManager
{
    /// <summary>
    /// Read-Eval-Print-Loop console for user interaction. Gives control over to other controllers when
    /// a matching command is entered by the user.
    /// </summary>
    public class REPLConsole
    {
        private Dictionary<string, Action> knownCommands = new Dictionary<string, Action>();

        public REPLConsole(params IConsoleControllable[] controllers)
        {
            foreach (var c in controllers)
            {
                foreach (var kvp in c.GetConsoleCommands())
                {
                    // [TODO] No conflict resolution, out of scope for this exercise
                    knownCommands[kvp.Key] = kvp.Value;
                }
            }
            knownCommands["exit"] = () => { /*Possibly resource cleanup*/ return; } ;
        }

        public void Run()
        {
            while (true)
            {
                Console.Write("Введите команду>");
                string command = Console.ReadLine().ToLower();

                Action procedure;
                if (knownCommands.TryGetValue(command, out procedure))
                {
                    procedure();
                    if (command == "exit")
                        break;
                }
                else
                {
                    Console.WriteLine($"Неизвестная команда, допустимые команды: {string.Join(", ", knownCommands.Keys)}");
                }
            }
        }
    }
}
