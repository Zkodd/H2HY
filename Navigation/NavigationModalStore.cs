using H2HY.Services;
using System;

namespace H2HY.Navigation
{
    /// <summary>
    /// using the given dialog service to open an MODAL dialog.
    /// </summary>
    public class NavigationModalStore : INavigationStore
    {
        private readonly IDialogService _dialogService;
        private ViewModelBase? _currentViewModel;

        /// <summary>
        /// Dialog store vor multiple dialogs.
        /// </summary>
        /// <param name="dialogService"></param>
        public NavigationModalStore(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        /// <summary>
        /// Dispose. No need to call.
        /// </summary>
        ~NavigationModalStore()
        {
            CurrentViewModel = null;
        }

        /// <summary>
        /// opens the set view model as dialog
        /// returns the last set view model.
        /// </summary>
        public ViewModelBase? CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;

                if (value is not null)
                {
                    _dialogService.ShowModalDialog(value, ViewClosedEvent);
                }

                CurrentViewModelChanged?.Invoke();
            }
        }


        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public event Action? CurrentViewModelChanged;

        private void ViewClosedEvent(ViewModelBase sender, bool result)
        {
            //unused
        }
    }
}