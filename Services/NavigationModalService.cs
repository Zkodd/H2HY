using H2HY.Navigation;
using System;

namespace H2HY.Services
{
    /// <summary>
    /// Creating a view model and navigates by using the given <code>navigationStoreModal</code> 
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class NavigationModalService<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {
        private readonly INavigationModalStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;

        /// <summary>
        /// Navigates to the given view model using the the given Navigation store.
        /// </summary>
        /// <param name="navigationStoreModal"></param>
        /// <param name="createViewModel"></param>
        public NavigationModalService(INavigationModalStore navigationStoreModal, Func<TViewModel> createViewModel)
        {
            _navigationStore = navigationStoreModal;
            _createViewModel = createViewModel;
        }

        /// <summary>
        /// Creates the given view model using the given call back function
        /// and sets it as the current view model at the navigation store.
        /// </summary>
        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
