using System;

namespace H2HY.Navigation
{
    /// <summary>
    /// basic navigation store interface.
    /// </summary>
    public interface INavigationStore
    {
        /// <summary>
        /// Called when current view model has changed.
        /// </summary>
        event Action CurrentViewModelChanged;

        /// <summary>
        /// Sets current view model which will be shown in the corresponding store-target.
        /// </summary>
        ViewModelBase? CurrentViewModel { get; set; }
    }
}
