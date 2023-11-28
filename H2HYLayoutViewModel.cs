using System;

namespace H2HY
{
    /// <summary>
    /// No need to inherit from here. This class is used by <code>LayoutNavigationService</code>.
    /// 
    /// Create a Layout.xml and bind to: NavigationBarViewModel, ContentViewModel
    /// and use this as view model : H2HYLayoutViewModel
    /// 
    /// Also u've to create a NavigationBarViewModel and register it like that:
    /// 
    ///  <![CDATA[
    ///  services.AddTransient<NavigationBarViewModel>(); 
    ///  services.AddTransient<Func<NavigationBarViewModel>>(s => () => s.GetRequiredService<NavigationBarViewModel>());
    ///  ]]>
    /// 
    /// now, create a view : NavigationBar.xml
    /// and register it to your main window:
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
        /// <param name="navigationBarViewModel">navigation view model</param>
        /// <param name="contentViewModel">current content view model</param>
        public H2HYLayoutViewModel(H2HYNavigationBar navigationBarViewModel, ViewModelBase contentViewModel)
        {
            NavigationBarViewModel = navigationBarViewModel;
            ContentViewModel = contentViewModel;
        }

        /// <summary>
        /// Current view model which is shown in the content row.
        /// </summary>
        public ViewModelBase ContentViewModel { get; }

        /// <summary>
        /// View model for the navigation bar, containing navigation commands.
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
