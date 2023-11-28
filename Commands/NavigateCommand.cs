using H2HY.Services;

namespace H2HY.Commands
{
    /// <summary>
    /// Converts a INavigationService to an ICommand.
    /// </summary>
    public class NavigateCommand : CommandBase
    {
        private readonly INavigationService _navigationService;

        /// <summary>
        ///
        /// </summary>
        /// <param name="navigationService"> Used navigation service.</param>
        public NavigateCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        /// <summary>
        /// calls Navigate() on the given Service.
        /// </summary>
        /// <param name="parameter">unused.</param>
        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}
