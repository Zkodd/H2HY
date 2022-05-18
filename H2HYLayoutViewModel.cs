namespace H2HY
{
    /// <summary>
    /// No need to inherite from here.
    /// 
    /// Create a Layout.xml and bind to: NavigationBarViewModel, ContentViewModel
    /// and use this as viewmodel : H2HYLayoutViewModel
    /// 
    /// Also u've to create a NavigationBarViewModel and register it like that:
    /// 
    ///  <![CDATA[
    ///  services.AddTransient<NavigationBarViewModel>(); 
    ///  services.AddTransient<Func<NavigationBarViewModel>>(s => () => s.GetRequiredService<NavigationBarViewModel>());
    ///  ]]>
    /// 
    /// now, create a view : NavigationBar.xml
    /// and register it to your mainwindow:
    /// <![CDATA[     
    /// <DataTemplate DataType="{x:Type h2hy:H2HYLayoutViewModel}">
    ///    <components:Layout />
    /// </DataTemplate>
    ///     ]]>
    ///</summary>
    public class H2HYLayoutViewModel : ViewModelBase
    {
        /// <summary>
        /// The H2HYLayoutViewModel is recreated on every navigation.
        /// </summary>
        /// <param name="navigationBarViewModel">navigation viewmodel</param>
        /// <param name="contentViewModel">current content viewmodel</param>
        public H2HYLayoutViewModel(ViewModelBase navigationBarViewModel, ViewModelBase contentViewModel)
        {
            NavigationBarViewModel = navigationBarViewModel;
            ContentViewModel = contentViewModel;
        }

        /// <summary>
        /// Viewmodel which is shown in the content row.
        /// </summary>
        public ViewModelBase ContentViewModel { get; }

        /// <summary>
        /// Viewmodel for the navigationbar, containing navigation commands.
        /// </summary>
        public ViewModelBase NavigationBarViewModel { get; }

        /// <summary>
        /// Dispose NavigationBarViewModel and ContentViewModel
        /// </summary>
        public override void Dispose()
        {
            NavigationBarViewModel.Dispose();
            ContentViewModel.Dispose();
        }
    }
}
