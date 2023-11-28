using H2HY.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H2HY.Navigation;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class NavigationDialogStore : INavigationDialogStore
{
    private readonly List<ViewModelDialogBase> _dialogs = new();
    private readonly IDialogService _dialogService;

    /// <summary>
    /// Simple dialog store. Stores all open dialogs in a list - so, can close all of them.
    /// </summary>
    /// <param name="dialogService"></param>
    public NavigationDialogStore(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IEnumerable<ViewModelDialogBase> Dialogs => _dialogs;

    /// <summary>
    /// Calls ShowDialog on set.
    /// Gets last opened view model.
    /// </summary>
    public ViewModelBase? CurrentViewModel
    {
        get
        {
            if (_dialogs.Any())
            {
                return _dialogs[0];
            }
            else
                return default;
        }

        set
        {
            if (value is not null)
            {
                ShowDialog(value);
                CurrentViewModelChanged?.Invoke();
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public event Action? CurrentViewModelChanged;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void CloseAll()
    {
        foreach (ViewModelDialogBase item in _dialogs.ToList())
        {
            if (item.Close is not null)
            {
                item.Close();
            }

            _dialogs.Remove(item);
        }
    }

    /// <summary>
    /// opens the given view model in a new dialog window.
    /// </summary>
    /// <param name="viewModel"></param>
    public void ShowDialog(ViewModelBase viewModel)
    {
        _dialogService.ShowDialog(viewModel, ViewClosedEvent);
        if (viewModel is ViewModelDialogBase v)
        {
            _dialogs.Add(v);
        }
    }

    private void ViewClosedEvent(ViewModelBase sender, bool result)
    {
        if (sender is ViewModelDialogBase viewModelDialogBase)
        {
            _dialogs.Remove(viewModelDialogBase);
        }
    }
}