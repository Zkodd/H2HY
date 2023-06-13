using H2HY.ToolKit;
using System;
using System.Threading.Tasks;

namespace H2HY.Commands
{
    /// <summary>
    /// https://johnthiriet.com/mvvm-going-async-with-async-command/
    /// </summary>
    public class AsyncCommand<T> : CommandBase
    {
        private readonly Func<T?, bool>? _canExecute;
        private readonly IExceptionHandler _errorHandler;
        private readonly Func<T?, Task> _execute;
        private bool _isExecuting;

        /// <summary>
        /// standard constructor for command. CanExecute returns true.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="exceptionHandler"></param>
        public AsyncCommand(Func<T?, Task> execute, IExceptionHandler exceptionHandler)
        {
            _execute = execute;
            _errorHandler = exceptionHandler;
        }

        /// <summary>
        /// standard constructor for command and canExecute-call back
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        /// <param name="exceptionHandler">this should never be null!</param>
        public AsyncCommand(Func<T?, Task> execute, Func<T?, bool> canExecute, IExceptionHandler exceptionHandler)
        {
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = exceptionHandler;
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true on default</returns>
        public override bool CanExecute(object? parameter)
        {
            return CanExecute((T?)parameter);
        }

        /// <summary>
        /// executed command.
        /// abstract - u are forced to override it.
        /// </summary>
        /// <param name="parameter"></param>
        public override void Execute(object? parameter)
        {
            ExecuteAsync((T?)parameter).FireAndForgetSafeAsync(_errorHandler);
        }

        private bool CanExecute(T? parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
        }

        private async Task ExecuteAsync(T? parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    _isExecuting = true;
                    await _execute(parameter);
                }
                finally
                {
                    _isExecuting = false;
                }
            }
        }
    }

    /// <summary>
    /// specialised AsyncCommand command:
    ///  ///  <![CDATA[ AsyncCommand : AsyncCommand<object> ]]>
    /// </summary>
    public class AsyncCommand : AsyncCommand<object>
    {

        /// <summary>
        ///  standard constructor for AsyncCommand. CanExecute returns true.
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="exceptionHandler"></param>
        public AsyncCommand(Func<object?, Task> execute, IExceptionHandler exceptionHandler) : base(execute, exceptionHandler)
        {
        }

        /// <summary>
        ///standard constructor for AsyncCommand and canExecute-call back
        /// </summary>
        /// <param name="execute"></param>
        /// <param name="canExecute"></param>
        /// <param name="exceptionHandler"></param>
        public AsyncCommand(Func<object?, Task> execute, Func<object?, bool> canExecute, IExceptionHandler exceptionHandler) : base(execute, canExecute, exceptionHandler)
        {
        }
    }
}