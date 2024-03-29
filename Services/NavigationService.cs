﻿using H2HY.Navigation;
using System;

namespace H2HY.Services;


/// <summary>
/// simple navigation service. navigates to the given view model using the given
/// navigation store.
/// </summary>
/// <typeparam name="TViewModel"></typeparam>
public class NavigationService<TViewModel> : INavigationService where TViewModel : ViewModelBase
{
    private readonly INavigationStore _navigationStore;
    private readonly Func<TViewModel> _createViewModel;

    /// <summary>
    /// Creates a navigation service which navigates to the given view model by using the given navigation store.
    /// </summary>
    /// <param name="navigationStore"></param>
    /// <param name="createViewModel"></param>
    public NavigationService(INavigationStore navigationStore, Func<TViewModel> createViewModel)
    {
        _navigationStore = navigationStore;
        _createViewModel = createViewModel;
    }

    /// <summary>
    /// Creates the given view model using the given call back function
    /// and sets it as the current view model at the navigation store.
    /// </summary>
    public void Navigate()
    {
        _navigationStore.CurrentViewModel = _createViewModel();
    }
}