using H2HY.Views;
using System;
using System.Collections.Generic;
using System.Windows;

namespace H2HY.Services
{
    /// <summary>
    /// Example usage:
    ///  Add to App.xml or another dictionary:
    /// <![CDATA[
    ///     <DataTemplate DataType="{x:Type viewmodels:RenameViewModel}">
    ///         <views:RenameView />
    ///     </DataTemplate>
    ///  ]]>
    /// </summary>
    public class DialogServiceWPF : IDialogService
    {
        /// <summary>
        ///  Shows given view Model in a modal dialog window.
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <param name="callback">call back on window close event. Can be true/false for modal - otherwise always false</param>
        public void ShowDialog(ViewModelBase viewmodel, Action<ViewModelBase, bool> callback)
        {
            if (viewmodel is ViewModelDialogBase viewModelDialogBase)
            {
                ShowModalDialog(viewModelDialogBase, callback);
            }
            else
            {
                ShowWindowDialog(viewmodel, callback);
            }
        }

        /// <summary>
        /// Opens a modal message box.
        /// </summary>
        /// <param name="message">A string that specifies the text to display.</param>
        /// <param name="caption">A string that specifies caption to display.</param>
        /// <param name="buttons"></param>
        /// <param name="icon"></param>
        /// <returns></returns>
        public MessageBoxResult ShowMessageBox(string message, string caption, MessageBoxButton buttons, MessageBoxIcon icon)
        {
            return (MessageBoxResult)MessageBox.Show(message,
                                               caption,
                                               (System.Windows.MessageBoxButton)buttons,
                                               (MessageBoxImage)icon);
        }

        /// <summary>
        ///  Shows given view model in a modal dialog window.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="callback">call back on window close event.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        private void ShowModalDialog(ViewModelDialogBase viewModel, Action<ViewModelBase, bool>? callback)
        {
            H2HYDialog dialogWindow = new();

            void closeEventHandler(object? s, EventArgs e)
            {
                try
                {
                    viewModel.ViewClosed(dialogWindow.H2HYDialogResult);
                    callback?.Invoke(viewModel, dialogWindow.H2HYDialogResult);
                    viewModel.Dispose();
                }
                finally
                {
                    dialogWindow.Closed -= closeEventHandler;
                }
            }
            dialogWindow.Closed += closeEventHandler;

            dialogWindow.DataContext = viewModel;
            dialogWindow.SizeToContent = SizeToContent.WidthAndHeight;

            if (viewModel.IsModal)
            {
                dialogWindow.ShowDialog();
            }
            else
            {
                dialogWindow.Show();
            }
        }

        /// <summary>
        /// Shows given view model in a window. Dialog result will be false.
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <param name="callback">call back on window close event.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        private void ShowWindowDialog(ViewModelBase viewmodel, Action<ViewModelBase, bool> callback)
        {
            var newWindow = new System.Windows.Window();

            void closeEventHandler(object? s, EventArgs e)
            {
                try
                {
                    viewmodel.ViewClosed();
                    callback(viewmodel, false);
                    viewmodel.Dispose();
                }
                finally
                {
                    newWindow.Closed -= closeEventHandler;
                }
            }
            newWindow.Closed += closeEventHandler;

            newWindow.DataContext = viewmodel;
            newWindow.Content = viewmodel;

            newWindow.SizeToContent = SizeToContent.WidthAndHeight;

            newWindow.Show();
        }
    }
}