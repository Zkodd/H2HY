using System;

namespace H2HY.Stores
{
    /// <summary>
    /// A Navigation store for the main view model.
    /// </summary>
    public class NavigationStore : INavigationStore
    {
        private ViewModelBase? _currentViewModel;

        /// <summary>
        /// Dispose. No need to call.
        /// </summary>
        ~NavigationStore()
        {
            CurrentViewModel = null;
        }

        /// <summary>
        /// Called after the current view model has changed.
        /// </summary>
        public event Action? CurrentViewModelChanged;

        /// <summary>
        /// Set the current view model.
        /// Calls view closed and dispose in case of an replacing an already assigned view model.
        /// </summary>
        public ViewModelBase? CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel?.ViewClosed();
                _currentViewModel?.DisposeAll();
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        /// <summary>
        /// true if an current view model is set.
        /// </summary>
        public bool IsOpen => CurrentViewModel is not null;

        /// <summary>
        /// closes the current content view.
        /// </summary>
        public void Close()
        {
            CurrentViewModel = null;
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
    }
}