using H2HY.Stores;
using System;

namespace H2HY
{
    /// <summary>
    /// usage as MainViewModel: inherite and define a construtor like this:
    /// public MainViewModel(INavigationStore navigationStore, INavigationStoreModal navigationStoreModal) : base(navigationStore, navigationStoreModal) {}
    ///
    /// Create Bindings to:
    /// <ContentControl Content="{Binding CurrentViewModel}" />
    /// Now, Bind the viewmodelstype to the Views: add something like this to your MainView.xaml:
    /// <![CDATA[ 
    /// <Grid.Resources>
    ///    <DataTemplate DataType = "{x:Type viewmodels:HazardLogChapterEditCaptionViewModel}" >
    ///        < views:HazardLogChapterEditView />
    ///    </DataTemplate>
    /// </Grid.Resources>
    /// ]]>
    /// </summary>
    public class H2HYMainViewModel : ViewModelBase
    {
        private readonly INavigationStore _navigationStore;

        private readonly INavigationStoreModal _navigationStoreModal;

        /// <summary>
        /// Parameterless construtor is not allowed.
        /// </summary>
        public H2HYMainViewModel()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="navigationStore">Navigation store</param>
        /// <param name="modalNavigationStore">Modal navigation store</param>
        public H2HYMainViewModel
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

        public ViewModelBase CurrentModalViewModel => _navigationStoreModal.CurrentViewModel;

        /// <summary>
        /// Returns the current viewmodel. This is bind to the Mainview/Mainwindow using:
        /// <![CDATA[ <ContentControl Content="{Binding CurrentViewModel}" /> ]]>
        /// </summary>
        public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

        public bool IsModalOpen => _navigationStoreModal.IsOpen;

        protected INavigationStore NavigationStore => _navigationStore;

        protected INavigationStoreModal NavigationStoreModal => _navigationStoreModal;

        /// <summary>
        /// Also calls Dispose on the current viewmodel.
        /// </summary>
        public override void Dispose()
        {
            _navigationStoreModal.CurrentViewModelChanged -= OnCurrentModalViewModelChanged;
            _navigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;

            CurrentViewModel.Dispose();
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
