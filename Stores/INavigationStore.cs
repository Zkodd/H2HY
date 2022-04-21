using System;

namespace H2HY.Stores
{
    public interface INavigationStore
    {
        event Action CurrentViewModelChanged;

        ViewModelBase CurrentViewModel { get; set; }

        bool IsOpen { get; }

        void Close();
    }
}
