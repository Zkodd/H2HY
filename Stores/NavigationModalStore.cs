using H2HY.Services;

namespace H2HY.Stores
{
    /// <summary>
    /// using the given Dialogservice to open an MODAL dialoag.
    /// </summary>
    public class NavigationModalStore : INavigationModalStore
    {
        private readonly IDialogService _dialogService;
        private ViewModelBase? _lastViewModel;
        public NavigationModalStore(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        /// <summary>
        /// Dispose. No need to call.
        /// </summary>
        ~NavigationModalStore()
        {
            CurrentViewModel = null;
        }

        public ViewModelBase? CurrentViewModel
        {
            get => _lastViewModel;
            set
            {
                _lastViewModel = value;

                if (value is not null)
                {
                    _dialogService.ShowDialog(value, OnCurrentViewClosed);
                }
            }
        }

        private void OnCurrentViewClosed(bool result)
        {
            //NOP
        }
    }
}