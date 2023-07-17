using System;

namespace H2HY.Services
{
    /// <summary>
    /// dialog navigation service.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class NavigationDialogService<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {
        private readonly IDialogService _dialog;
        private readonly Func<TViewModel> _createViewModel;

        /// <summary>
        /// Creates a navigation service which navigates to the given view model using ShowDialog.
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="createViewModel"></param>
        public NavigationDialogService(IDialogService dialog, Func<TViewModel> createViewModel)
        {
            _dialog = dialog;
            _createViewModel = createViewModel;
        }

        /// <summary>
        /// navigates
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Navigate()
        {
            _dialog.ShowDialog(_createViewModel(), (m, r) => { });
        }
    }
}