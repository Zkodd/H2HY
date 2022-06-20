using System;

namespace H2HY
{
    /// <summary>
    /// No need to inherite from here. This class is used by <code>LayoutNavigationService</code>.
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
        /// Parameterless constructor is now allowed.
        /// </summary>
        public H2HYLayoutViewModel()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The H2HYLayoutViewModel is recreated on every navigation.
        /// </summary>
        /// <param name="navigationBarViewModel">navigation viewmodel</param>
        /// <param name="contentViewModel">current content viewmodel</param>
        public H2HYLayoutViewModel(H2HYNavigationBar navigationBarViewModel, ViewModelBase contentViewModel)
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
        public H2HYNavigationBar NavigationBarViewModel { get; }

        /// <summary>
        /// Dispose NavigationBarViewModel and ContentViewModel
        /// </summary>
        public override void Dispose()
        {
            NavigationBarViewModel.Dispose();
            ContentViewModel.Dispose();
        }

        /// <summary>
        /// The attached view has been closed without a result. (modal or nonmodal)
        /// </summary>
        public override void ViewClosed()
        {
            base.ViewClosed();

            NavigationBarViewModel.ViewClosed();
            ContentViewModel.ViewClosed();
        }

        /// <summary>
        /// The attached modal view has been closed with the given result.
        /// </summary>
        /// <param name="dialogResult"></param>
        public override void ViewClosed(bool dialogResult)
        {
            base.ViewClosed(dialogResult);
            NavigationBarViewModel.ViewClosed(dialogResult);
            ContentViewModel.ViewClosed(dialogResult);
        }
    }
}
