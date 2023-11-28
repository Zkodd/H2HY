using System;
using System.Threading.Tasks;

namespace H2HY.Tools
{
    /// <summary>
    /// Extension methods for Task
    /// </summary>
    public static class TaskExtension
    {
        /// <summary>
        /// The FireAndForgetSafeAsync method  wraps tasks into a try catch block. 
        /// If an error occurs, it send the exception to the given IExceptionHandler handler
        /// </summary>
        /// <param name="task"></param>
        /// <param name="handler"></param>
        public static async void FireAndForgetSafeAsync(this Task task, IExceptionHandler handler)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.HandelException(ex);
            }
        }
    }
}
