using System;
using System.Windows.Input;

namespace H2HY
{
    /// <summary>
    /// A standard ICommand realisation.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true on default</returns>
        public virtual bool CanExecute(object? parameter)
        {
            return true;
        }

        /// <summary>
        /// executed command.
        /// abstract - u are forced to override it.
        /// </summary>
        /// <param name="parameter"></param>
        public abstract void Execute(object? parameter);
    }
}
