﻿using H2HY.Services;

namespace H2HY.Stores
{
    /// <summary>
    /// using the given Dialogservice to open an MODAL dialog.
    /// </summary>
    public class NavigationModalStore : INavigationModalStore
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
            }
        }

        private void ViewClosedEvent(ViewModelBase sender, bool result)
        {
           //unused
        }
    }
}