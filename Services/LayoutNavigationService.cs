using H2HY.Stores;
using System;

namespace H2HY.Services
{
    /// <summary>
    /// The LayoutViewModel is placed direct on top of the H2HYMainViewModel.
    /// </summary>
    /// <typeparam name="TViewModel"></typeparam>
    public class LayoutNavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
    {
        private readonly INavigationStore _navigationStore;
        private readonly Func<TViewModel> _createViewModel;
        private readonly Func<ViewModelBase> _createNavigationBarViewModel;

        public LayoutNavigationService(
            INavigationStore navigationStore,
            Func<TViewModel> createViewModel,
            Func<ViewModelBase> createNavigationBarViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
            _createNavigationBarViewModel = createNavigationBarViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = new H2HYLayoutViewModel(_createNavigationBarViewModel(), _createViewModel());
        }
    }
}
