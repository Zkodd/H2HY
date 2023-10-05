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
        ///  Shows given view Model in a dialog window.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="callBack">call back on window close event. Can be true/false for modal - otherwise always false</param>
        public void ShowDialog(ViewModelBase viewModel, Action<ViewModelBase, bool> callBack)
        {
            if (viewModel is ViewModelDialogBase viewModelDialogBase)
            {
                if (viewModelDialogBase.IsModal)
                {
                    ShowModalDialog(viewModelDialogBase, callBack);
                }
                else
                {
                    ShowWindowExDialog(viewModelDialogBase, callBack);
                }
            }
            else
            {
                ShowWindowDialog(viewModel, callBack);
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
        ///  Shows given view model in a non modal dialog window.
        ///  using the H2HYModalDialog.view
        /// </summary>
        /// <param name="viewModelDialogBase"></param>
        /// <param name="callBack"></param>
        private void ShowWindowExDialog(ViewModelDialogBase viewModelDialogBase, Action<ViewModelBase, bool> callBack)
        {
            H2HYModalDialog dialogWindow = new();
            dialogWindow.Closed += closeEventHandler;
            dialogWindow.DataContext = viewModelDialogBase;
            dialogWindow.Content = viewModelDialogBase;
            dialogWindow.SizeToContent = SizeToContent.WidthAndHeight;
            dialogWindow.Show();

            void closeEventHandler(object? s, EventArgs e)
            {
                try
                {
                    viewModelDialogBase.ViewClosed(dialogWindow.H2HYDialogResult);
                    callBack?.Invoke(viewModelDialogBase, dialogWindow.H2HYDialogResult);
                    viewModelDialogBase.Dispose();
                }
                finally
                {
                    dialogWindow.Closed -= closeEventHandler;
                }
            }
        }

        /// <summary>
        ///  Shows given view model in a modal dialog window.
        /// using the H2HYModalDialog.view
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="callBack">call back on window close event.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        public void ShowModalDialog(ViewModelBase viewModel, Action<ViewModelBase, bool>? callBack)
        {
            H2HYModalDialog dialogWindow = new();

            dialogWindow.Closed += closeEventHandler;
            dialogWindow.DataContext = viewModel;
            dialogWindow.Content = viewModel;
            dialogWindow.SizeToContent = SizeToContent.WidthAndHeight;
            dialogWindow.ShowDialog();

            void closeEventHandler(object? s, EventArgs e)
            {
                try
                {
                    viewModel.ViewClosed(dialogWindow.H2HYDialogResult);
                    callBack?.Invoke(viewModel, dialogWindow.H2HYDialogResult);
                    viewModel.Dispose();
                }
                finally
                {
                    dialogWindow.Closed -= closeEventHandler;
                }
            }
        }

        /// <summary>
        /// Shows given view model in a window. Dialog result will be false.
        /// using a plain window.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="callBack">call back on window close event.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        private void ShowWindowDialog(ViewModelBase viewModel, Action<ViewModelBase, bool> callBack)
        {
            var newWindow = new Window();

            newWindow.Closed += closeEventHandler;
            newWindow.DataContext = viewModel;
            newWindow.Content = viewModel;
            newWindow.SizeToContent = SizeToContent.WidthAndHeight;

            newWindow.Show();

            void closeEventHandler(object? s, EventArgs e)
            {
                try
                {
                    viewModel.ViewClosed();
                    callBack(viewModel, false);
                    viewModel.Dispose();
                }
                finally
                {
                    newWindow.Closed -= closeEventHandler;
                }
            }
        }
    }
}