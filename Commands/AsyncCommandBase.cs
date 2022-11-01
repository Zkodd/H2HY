using System;
using System.Threading.Tasks;

namespace H2HY.Commands
{
    /// <summary>
    /// Asnyc ICommand realisation.
    /// </summary>
    public abstract class AsyncCommandBase : CommandBase
    {
        private bool _isExecuting;
        
        /// <summary>
        /// The current command is executing atm.
        /// </summary>
        public bool IsExecuting
        {
            get
            {
                return _isExecuting;
            }
            set
            {
                _isExecuting = value;
                //call can execute change
                //CanExecuteChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns>true on default</returns>
        public override bool CanExecute(object? parameter)
        {
            return !IsExecuting && base.CanExecute(parameter);
        }

        /// <summary>
        /// public interface called by the gui-thread. Do not touch.
        /// </summary>
        /// <param name="parameter"></param>
        public override async void Execute(object? parameter)
        {
            IsExecuting = true;

            try
            {
                await ExecuteAsync(parameter);
            }
            catch (Exception) { }
            finally
            {
                IsExecuting = false;
            }
        }

        /// <summary>
        /// Asnyc Task to execute. Override as needed.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public abstract Task ExecuteAsync(object? parameter);
    }
}
