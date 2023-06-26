using H2HY.Commands;
using H2HY.Stores;
using System;
using System.Windows.Input;

namespace H2HY
{
    /// <summary>
    /// usage as MainViewModel: inherit and define a constructor like this:
    /// <![CDATA[public MyMainViewModel(INavigationStore navigationStore, INavigationStoreModal navigationStoreModal) : base(navigationStore, navigationStoreModal) {}]]>
    /// and register it to your DI service:
    /// <![CDATA[services.AddSingleton<H2HYMainViewModel>();]]>
    /// or
    /// <![CDATA[services.AddSingleton<MyMainViewModel>();]]>
    ///
    /// Create Bindings to:
    /// <![CDATA[<ContentControl Content="{Binding CurrentViewModel}" />]]>
    /// Now, Bind the view model type to the Views: add something like this to your MainView.xaml:
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
        /// Default constructor.
        /// </summary>
        /// <param name="navigationStore">Navigation store</param>
        public H2HYMainViewModel
        (
            INavigationStore navigationStore
        )
        {
            NavigationStore = navigationStore;
            NavigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            ViewPortClosing = new RelayCommand(i =>
            {
                NavigationStore.CurrentViewModel = null;
                ViewPortClosed?.Invoke();
                Dispose();
            });
        }

        /// <summary>
        /// The ViewPort is closed. The MainViewModel(=MainWindow) is closing.
        /// </summary>
        public event Action? ViewPortClosed;

        /// <summary>
        /// Returns the current view model. This is bind to the Mainview/Mainwindow by using:
        /// <![CDATA[ <ContentControl Content="{Binding CurrentViewModel}" /> ]]>
        /// </summary>
        public ViewModelBase? CurrentViewModel => NavigationStore.CurrentViewModel;

        /// <summary>
        /// Closes the current MainView(=Window) - can be bind in the view(window) like this:
        /// <![CDATA[
        /// <i:Interaction.Triggers>
        ///     <i:EventTrigger EventName = "Closing">
        ///         <i:InvokeCommandAction Command = "{Binding ViewPortClosing}" />
        ///     </i:EventTrigger>
        /// </i:Interaction.Triggers>
        /// ]]>
        /// </summary>
        public ICommand ViewPortClosing { get; private set; }

        /// <summary>
        /// Used Navigation store - used to, well, navigate.
        /// </summary>
        protected INavigationStore NavigationStore { get; private set; }

        /// <summary>
        /// Dispose.
        /// </summary>
        public override void Dispose()
        {
            NavigationStore.CurrentViewModelChanged -= OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged() => RaisePropertyChanged(nameof(CurrentViewModel));
    }
}