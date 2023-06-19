using System;

namespace H2HY.Stores
{
    /// <summary>
    /// basic navigation store interface.
    /// </summary>
    public interface INavigationStore
    {
        /// <summary>
        /// Is called when current view model has changed.
        /// </summary>
        event Action CurrentViewModelChanged;

        /// <summary>
        /// Sets current view model which will be shown in the corresponding store-target.
        /// </summary>
        ViewModelBase? CurrentViewModel { get; set; }

        /// <summary>
        ///  true if an current view model is set.
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        /// closes the current view (model)
        /// </summary>
        void Close();
    }
}
