using System;
using System.Collections.Generic;

namespace TransactionManager
{
    /// <summary>
    /// Allows the implementing class to be controlled from the REPLConsole.
    /// </summary>
    public interface IConsoleControllable
    {
        /// <summary>
        /// Gets the commands exposed by implementing class. When user input matches the command,
        /// REPLConsole passes the control to the matching method.
        /// </summary>
        /// <returns>A Dictionary, where keys are lowercase commands, and values are delegates
        /// that take over when this command is input by the user.</returns>
        public Dictionary<string, Action> GetConsoleCommands();
    }
}
