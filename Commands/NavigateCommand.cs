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
        /// Used Navigationservice.
        /// </summary>
        /// <param name="navigationService"></param>
        public NavigateCommand(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        /// <summary>
        /// calls Navigate() on the given Service.
        /// </summary>
        /// <param name="parameter">unsued.</param>
        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}
