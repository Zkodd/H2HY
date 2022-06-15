using H2HY.Stores;
using System;
using System.Windows.Input;

namespace H2HY
{
    /// <summary>
    /// usage as MainViewModel: inherite and define a construtor like this:
    /// <![CDATA[public MyMainViewModel(INavigationStore navigationStore, INavigationStoreModal navigationStoreModal) : base(navigationStore, navigationStoreModal) {}]]>
    /// and register it to your DI service:
    /// <![CDATA[services.AddSingleton<H2HYMainViewModel>();]]>
    /// or
    /// <![CDATA[services.AddSingleton<MyMainViewModel>();]]>
    /// 
    /// Create Bindings to:
    /// <![CDATA[<ContentControl Content="{Binding CurrentViewModel}" />]]>
    /// Now, Bind the viewmodelstype to the Views: add something like this to your MainView.xaml:
    /// <![CDATA[ 
    /// <Grid.Resources>
    ///    <DataTemplate DataType = "{x:Type viewmodels:HazardLogChapterEditCaptionViewModel}" >
    ///        <views:HazardLogChapterEditView />
    ///    </DataTemplate>
    /// </Grid.Resources>
    /// ]]>
    /// </summary>
    public class H2HYMainViewModel : ViewModelBase
    {
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
            NavigationStoreModal = modalNavigationStore;
            NavigationStoreModal.CurrentViewModelChanged += OnCurrentModalViewModelChanged;

            NavigationStore = navigationStore;
            NavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            ViewPortClosing = new RelayCommand(i => NavigationStore.CurrentViewModel = null);
        }

        public ViewModelBase CurrentModalViewModel => NavigationStoreModal.CurrentViewModel;

        /// <summary>
        /// Returns the current viewmodel. This is bind to the Mainview/Mainwindow using:
        /// <![CDATA[ <ContentControl Content="{Binding CurrentViewModel}" /> ]]>
        /// </summary>
        public ViewModelBase CurrentViewModel => NavigationStore.CurrentViewModel;

        public bool IsModalOpen => NavigationStoreModal.IsOpen;

        /// <summary>
        /// Bind to view/window/port closing. Will call Dispose().
        // using something like this:
        /// <![CDATA[
        /// <i:Interaction.Triggers>
        ///   <i:EventTrigger EventName = "Closing" >
        ///        <i:InvokeCommandAction Command = "{Binding ViewPortClosing}" />
        ///   </i:EventTrigger>
        /// </i:Interaction.Triggers>
        ///]]>
        /// </summary>
        public ICommand ViewPortClosing { get; }

        protected INavigationStore NavigationStore { get; }

        protected INavigationStoreModal NavigationStoreModal { get; }

        /// <summary>
        /// Also calls Dispose on the current viewmodel.
        /// </summary>
        public override void Dispose()
        {
            NavigationStoreModal.CurrentViewModelChanged -= OnCurrentModalViewModelChanged;
            NavigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;

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
