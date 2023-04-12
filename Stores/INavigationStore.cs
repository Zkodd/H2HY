using System;

namespace H2HY.Stores
{
    /// <summary>
    /// basic navigation store interface.
    /// </summary>
    public interface INavigationStore
    {
        /// <summary>
        /// Is callen current viewmodel has changed.
        /// </summary>
        event Action CurrentViewModelChanged;

        /// <summary>
        /// Sets current viewmodel which will be shown in the corresponding store-target.
        /// </summary>
        ViewModelBase? CurrentViewModel { get; set; }

        /// <summary>
        ///  true if an current viewmodel is set.
        /// </summary>
        bool IsOpen { get; }

        /// <summary>
        /// closes the current viewm(model)
        /// </summary>
        void Close();
    }
}
