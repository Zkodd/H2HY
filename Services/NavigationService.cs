using H2HY.Stores;
using System;

namespace H2HY.Services
{
    public class NavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {
        private readonly INavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        /// <summary>
        /// Navigates to the given viewmodel using the the given Navigationstore.
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="createViewModel"></param>
        public NavigationService(INavigationStore navigationStore, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        /// <summary>
        /// Creates the given viewmodel using the given callback function
        /// and sets it as the current viewmodel at the navigation store.
        /// </summary>
        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
