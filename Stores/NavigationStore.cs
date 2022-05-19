using System;

namespace H2HY.Stores
{
    public class NavigationStore : INavigationStore, INavigationStoreModal
    {
        private ViewModelBase _currentViewModel;

        public event Action CurrentViewModelChanged;
        /// <summary>
        /// Set the current viewmodel.
        /// Calls view closed and dispose in case of an replacing an already assigned viewmodel.
        /// </summary>
        public ViewModelBase CurrentViewModel
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
        /// true if an current viewmodel is set.
        /// </summary>
        public bool IsOpen => CurrentViewModel != null;

        /// <summary>
        /// closes the curent content view.
        /// </summary>
        public void Close()
        {
            CurrentViewModel = null;
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        /// <summary>
        /// Dispose. No need to call.
        /// </summary>
        ~NavigationStore()
        {
            CurrentViewModel = null;
        }

    }
}
