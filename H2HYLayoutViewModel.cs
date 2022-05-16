namespace H2HY
{
    /// <summary>
    /// No need to inherite from here.
    /// 
    /// Create a Layout.xml and bind to: NavigationBarViewModel, ContentViewModel
    /// and use this as viewmodel : H2HYLayoutViewModel
    /// 
    /// Also u've to create a NavigationBarViewModel and register it like that:
    ///     services.AddTransient<NavigationBarViewModel>();
    ///     services.AddTransient<Func<NavigationBarViewModel>>(s => () => s.GetRequiredService<NavigationBarViewModel>());
    /// 
    /// now, create a view : NavigationBar.xml
    /// and register it to your mainwindow:
    /// 
    /// <DataTemplate DataType="{x:Type h2hy:H2HYLayoutViewModel}">
    ///   <components:Layout />
    /// </DataTemplate>
    /// 
    ///</summary>
    public class H2HYLayoutViewModel : ViewModelBase
    {
        public ViewModelBase NavigationBarViewModel { get; }
        public ViewModelBase ContentViewModel { get; }

        public H2HYLayoutViewModel(ViewModelBase navigationBarViewModel, ViewModelBase contentViewModel)
        {
            NavigationBarViewModel = navigationBarViewModel;
            ContentViewModel = contentViewModel;
        }

        public override void Dispose()
        {
            NavigationBarViewModel.Dispose();
            ContentViewModel.Dispose();
        }
    }
}
