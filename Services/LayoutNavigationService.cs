using H2HY.Navigation;
using System;

namespace H2HY.Services;

/// <summary>
/// The LayoutViewModel is placed direct on top of the H2HYMainViewModel.
/// Its using the H2HYLayoutViewModel.
/// Example implementation:
/// <code><![CDATA[
/// private INavigationService CreateFaultListNavigation(IServiceProvider serviceProvider)
///    {
///        return new LayoutNavigationService<FaultListViewModel>(//Content view model
///           serviceProvider.GetRequiredService<INavigationStore>(),//INavigationStore or INavigationStoreModal
///           () => serviceProvider.GetRequiredService<FaultListViewModel>(),//Content view model
///           () => serviceProvider.GetRequiredService<NavigationBarViewModel>()//Navigation bar view model
///           );
///    }
/// ]]></code>
/// </summary>
/// <typeparam name="TViewModel">Content view model</typeparam>
public class LayoutNavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
{
    private readonly Func<TViewModel> _createContentViewModel;

    private readonly Func<H2HYNavigationBar> _createNavigationBarViewModel;

    private readonly INavigationStore _navigationStore;

    /// <summary>
    ///
    /// </summary>
    /// <param name="navigationStore">Navigation Store (modal or main)</param>
    /// <param name="createContentViewModel">Function to create the content view model</param>
    /// <param name="createNavigationBarViewModel">Function to create the navigation bar view model</param>
    public LayoutNavigationService(
        INavigationStore navigationStore,
        Func<TViewModel> createContentViewModel,
        Func<H2HYNavigationBar> createNavigationBarViewModel)
    {
        _navigationStore = navigationStore;
        _createContentViewModel = createContentViewModel;
        _createNavigationBarViewModel = createNavigationBarViewModel;
    }

    /// <summary>
    /// Navigates to the given view model. The Current view model is set to an H2HYLayoutViewModel.
    /// which is created by the navigation bar view model and the content view model.
    /// </summary>
    public void Navigate()
    {
        _navigationStore.CurrentViewModel = new H2HYLayoutViewModel(_createNavigationBarViewModel(), _createContentViewModel());
    }
}