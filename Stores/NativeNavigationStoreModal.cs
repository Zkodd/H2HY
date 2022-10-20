using H2HY.Services;
using System;

namespace H2HY.Stores
{
    public class NativeNavigationStoreModal : INavigationStoreModal
    {
        private ViewModelBase? _currentViewModel;
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Called after the current view model has changed. So, the mainview model can 
        /// call property changed.
        /// </summary>
        public event Action? CurrentViewModelChanged;

        public NativeNavigationStoreModal(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public ViewModelBase? CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.DisposeAll();
                _currentViewModel = value;

                if (value is not null)
                {
                    _dialogService.ShowDialog(value, OnCurrentViewClosed);
                }

                OnCurrentViewModelChanged();
            }
        }

        public bool IsOpen => CurrentViewModel != null;

        public void Close()
        {
            CurrentViewModel?.ViewClosed();
            CurrentViewModel?.ViewClosed(false);
            CurrentViewModel = null;
        }

        private void OnCurrentViewClosed(bool result)
        {
            CurrentViewModel?.ViewClosed();
            CurrentViewModel?.ViewClosed(result);
            CurrentViewModel = null;
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        /// <summary>
        /// Dispose. No need to call.
        /// </summary>
        ~NativeNavigationStoreModal()
        {
            CurrentViewModel = null;
        }

    }
}
