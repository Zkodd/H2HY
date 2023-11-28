using System.Collections.Generic;

namespace H2HY.Navigation;

/// <summary>
/// navigation service which collects opened dialogs.
/// Does not handle modal windows.
/// </summary>
public interface INavigationDialogStore : INavigationStore
{
    /// <summary>
    /// opens a dialog
    /// has to be a ViewModelDialogBase 
    /// </summary>
    /// <param name="viewModel"></param>
    void ShowDialog(ViewModelBase viewModel);

    /// <summary>
    /// open dialogs
    /// </summary>
    public IEnumerable<ViewModelDialogBase> Dialogs { get; }

    /// <summary>
    /// close all open dialogs
    /// </summary>
    public void CloseAll();
}