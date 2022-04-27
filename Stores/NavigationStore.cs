using System;

namespace H2HY.Stores
{
    public class NavigationStore : INavigationStore, INavigationStoreModal
    {
        private ViewModelBase _currentViewModel;

        public event Action CurrentViewModelChanged;

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

        public bool IsOpen => CurrentViewModel != null;

        public void Close()
        {
            CurrentViewModel = null;
        }

        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        ~NavigationStore()
        {
            CurrentViewModel = null;
        }

    }
}
