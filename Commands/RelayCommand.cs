using System;
using System.Windows.Input;

namespace H2HY.Commands
{
    /// <summary>
    /// provide a base implementation of the <code>ICommand</code> interface.
    /// expose constructors taking delegates like Action, which allow 
    /// the wrapping of standard methods and lambda expressions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T?> _execute;
        private readonly Predicate<T?>? _canExecute;

        /// <summary>
        /// standard constructor for command. CanExecute returns true.
        /// </summary>
        /// <param name="execute">action, which is executed</param>
        public RelayCommand(Action<T?> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// standard constructor for command and canExecute-callback
        /// </summary>
        /// <param name="execute">action, which is executed</param>
        /// <param name="canExecute">callback, determinates if command is executable</param>
        public RelayCommand(Action<T?> execute, Predicate<T?>? canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true on default</returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute((T?)parameter);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// execute commmands. Is never call by ur code.
        /// </summary>
        /// <param name="parameter">parameter given by the WPF binding.</param>
        public void Execute(object? parameter)
        {
            _execute((T?)parameter);
        }
    }

    /// <summary>
    /// specialised Relaycommand:
    ///  ///  <![CDATA[ RelayCommand : RelayCommand<object> ]]>
    /// </summary>
    public class RelayCommand : RelayCommand<object>
    {
        /// <summary>
        /// standard constructor for command. CanExecute returns true.
        /// </summary>
        /// <param name="execute">action, which is executed</param>
        public RelayCommand(Action<object?> execute) : base(execute)
        {
        }

        /// <summary>
        /// standard constructor for command and canExecute-callback
        /// </summary>
        /// <param name="execute">action, which is executed</param>
        /// <param name="canExecute">callback, determinates if command is executable</param>
        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute) : base(execute, canExecute)
        {
        }
    }
}
