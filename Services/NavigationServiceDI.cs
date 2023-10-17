using H2HY.Navigation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace H2HY.Services
{
    /// <summary>
    /// simple navigation service. navigates to the given view model using the given
    /// navigation store.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class NavigationServiceDI<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {
        private readonly INavigationStore _navigationStore;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Creates a navigation service which navigates to the given view model by using the given navigation store.
        /// </summary>
        /// <param name="navigationStore"></param>
        /// <param name="serviceProvider"></param>
        public NavigationServiceDI(INavigationStore navigationStore, IServiceProvider serviceProvider)
        {
            _navigationStore = navigationStore;
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Creates the given view model using the given IServiceProvider
        /// and sets it as the current view model at the navigation store.
        /// </summary>
        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _serviceProvider.GetRequiredService<TViewModel>();
        }
    }
}
