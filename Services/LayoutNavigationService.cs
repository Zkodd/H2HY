using H2HY.Stores;
using System;

namespace H2HY.Services
{
    /// <summary>
    /// The LayoutViewModel is placed direct on top of the H2HYMainViewModel.
    /// Example implementation:
    /// <![CDATA[
    /// private INavigationService CreateFaultListNavigation(IServiceProvider serviceProvider)
    ///    {
    ///        return new LayoutNavigationService<FaultListViewModel>(//Content viewmodel
    ///           serviceProvider.GetRequiredService<INavigationStore>(),//INavigationStore or INavigationStoreModal
    ///           () => serviceProvider.GetRequiredService<FaultListViewModel>(),//Content viewmodel
    ///           () => serviceProvider.GetRequiredService<NavigationBarViewModel>()//Navigation bar viewmodel
    ///           );
    ///    }
    /// ]]>
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class LayoutNavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {
        private readonly Func<TViewModel> _createContentViewModel;

        private readonly Func<H2HYNavigationBar> _createNavigationBarViewModel;

        private readonly INavigationStore _navigationStore;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="navigationStore">Navigation Store (modal or main)</param>
        /// <param name="createViewModel">Function to create the content viewmodel</param>
        /// <param name="createNavigationBarViewModel">Function to create the navigation bar viewmodel</param>
        public LayoutNavigationService(
            INavigationStore navigationStore,
            Func<TViewModel> createViewModel,
            Func<H2HYNavigationBar> createNavigationBarViewModel)
        {
            _navigationStore = navigationStore;
            _createContentViewModel = createViewModel;
            _createNavigationBarViewModel = createNavigationBarViewModel;
        }

        /// <summary>
        /// Navigates to the given viewmodel. The Current viewmodel is set to an H2HYLayoutViewModel.
        /// which is created by the navigationbar viewmodel and the content viewmodel.
        /// </summary>
        public void Navigate()
        {
            _navigationStore.CurrentViewModel = new H2HYLayoutViewModel(_createNavigationBarViewModel(), _createContentViewModel());
        }
    }
}
