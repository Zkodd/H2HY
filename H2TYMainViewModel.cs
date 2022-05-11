using H2HY.Stores;

namespace H2HY
{
    /// <summary>
    /// usage as MainViewModel: inherite and define a construtor like this:
    /// public MainViewModel(INavigationStore navigationStore, INavigationStoreModal navigationStoreModal) : base(navigationStore, navigationStoreModal) {}
    ///
    /// Create Bindings to:
    /// <ContentControl Content="{Binding CurrentViewModel}" />
    /// Now, Bind the viewmodelstype to the Views: add something like this to your MainView.xaml:
    /// <Grid.Resources>
    ///    <DataTemplate DataType = "{x:Type viewmodels:HazardLogChapterEditCaptionViewModel}" >
    ///        < views:HazardLogChapterEditView />
    ///    </DataTemplate>
    /// </Grid.Resources>
    /// </summary>
    public class H2TYMainViewModel : ViewModelBase
    {
        private readonly INavigationStore _navigationStore;

        private readonly INavigationStoreModal _navigationStoreModal;

        public H2TYMainViewModel
        (
            INavigationStore navigationStore,
            INavigationStoreModal modalNavigationStore
        )
        {
            _navigationStoreModal = modalNavigationStore;
            _navigationStoreModal.CurrentViewModelChanged += OnCurrentModalViewModelChanged;

            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        protected INavigationStore NavigationStore => _navigationStore;

        protected INavigationStoreModal NavigationStoreModal => _navigationStoreModal;

        public ViewModelBase CurrentModalViewModel => _navigationStoreModal.CurrentViewModel;

        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public bool IsModalOpen => _navigationStoreModal.IsOpen;

        public override void Dispose()
        {
            _navigationStoreModal.CurrentViewModelChanged -= OnCurrentModalViewModelChanged;
            _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;
        }

        private void OnCurrentModalViewModelChanged()
        {
            RaisePropertyChanged(nameof(CurrentModalViewModel));
            RaisePropertyChanged(nameof(IsModalOpen));
        }

        private void OnCurrentViewModelChanged()
        {
            RaisePropertyChanged(nameof(CurrentViewModel));
        }
    }
}
