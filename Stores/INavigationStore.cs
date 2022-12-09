using System;

namespace H2HY.Stores
{
    public interface INavigationStore
    {
        event Action CurrentViewModelChanged;

        /// <summary>
        /// Sets current viewmodel which will be shown in the corresponding store-target.
        /// </summary>
        ViewModelBase? CurrentViewModel { get; set; }

        bool IsOpen { get; }

        void Close();
    }
}
