using H2HY.Stores;
using System;

namespace H2HY.Services
{
    public class NavigationModalService<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {
        private readonly INavigationModalStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        /// <summary>
        /// Navigates to the given viewmodel using the the given Navigationstore.
        /// </summary>
        /// <param name="navigationStoreModal"></param>
        /// <param name="createViewModel"></param>
        public NavigationModalService(INavigationModalStore navigationStoreModal, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStoreModal;
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
