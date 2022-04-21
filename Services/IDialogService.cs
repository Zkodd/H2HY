using System;

namespace H2HY.Services
{
    public interface IDialogService
    {
        void ShowDialog(ViewModelBase viewmodel, Action<bool> callback);
    }
}
