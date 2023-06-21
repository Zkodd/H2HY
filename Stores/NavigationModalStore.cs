using H2HY.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace H2HY.Stores
{
    /// <summary>
    /// using the given Dialogservice to open an MODAL dialog.
    /// </summary>
    public class NavigationModalStore : INavigationModalStore
    {
        private readonly IDialogService _dialogService;
        private ViewModelBase? _lastViewModel;
        private readonly HashSet<ViewModelBase> _openViews = new();

        /// <summary>
        /// Dialog store vor multiple dialogs.
        /// </summary>
        /// <param name="dialogService"></param>
        public NavigationModalStore(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        /// <summary>
        /// Dispose. No need to call.
        /// </summary>
        ~NavigationModalStore()
        {
            CurrentViewModel = null;
        }

        /// <summary>
        /// opens the set view model as dialog
        /// returns the last set view model.
        /// </summary>
        public ViewModelBase? CurrentViewModel
        {
            get => _lastViewModel;
            set
            {
                _lastViewModel = value;

                if (value is not null)
                {
                    _openViews.Add(value);
                    _dialogService.ShowDialog(value, ViewClosedEvent);
                }
            }
        }

        private void ViewClosedEvent(ViewModelBase sender, bool result)
        {
            _openViews.Remove(sender);
        }

        /// <summary>
        /// closes all opens ViewModelDialogBase viewmodels.
        /// </summary>
        public void Close()
        {
            Collection<ViewModelBase> openViews = new(_openViews.ToList());
            foreach (var item in openViews)
            {
                if (item is ViewModelDialogBase vm)
                {
                    vm.CloseDialog();
                }
            }
        }
    }
}