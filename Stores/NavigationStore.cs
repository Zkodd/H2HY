using System;

namespace H2HY.Stores
{
    /// <summary>
    /// A Navigation store for the mainview model.
    /// </summary>
    public class NavigationStore : INavigationStore, INavigationStoreModal
    {
        private ViewModelBase _currentViewModel;

        /// <summary>
        /// Called after the current view model has changed. So, the mainview model can 
        /// call property changed.
        /// </summary>
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
