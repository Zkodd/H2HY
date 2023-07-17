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
        /// <param name="viewModel"></param>
        /// <param name="callBack">call back on window close event. Can be true/false for modal - otherwise always false</param>
        public void ShowDialog(ViewModelBase viewModel, Action<ViewModelBase, bool> callBack)
        {
            if (viewModel is ViewModelDialogBase viewModelDialogBase)
            {
                ShowModalDialog(viewModelDialogBase, callBack);
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
        ///  Shows given view model in a modal dialog window.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="callBack">call back on window close event.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        public void ShowModalDialog(ViewModelBase viewModel, Action<ViewModelBase, bool>? callBack)
        {
            H2HYDialog dialogWindow = new();

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
            dialogWindow.Closed += closeEventHandler;

            dialogWindow.DataContext = viewModel;
            dialogWindow.SizeToContent = SizeToContent.WidthAndHeight;

            dialogWindow.ShowDialog();
        }

        /// <summary>
        /// Shows given view model in a window. Dialog result will be false.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="callBack">call back on window close event.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        private void ShowWindowDialog(ViewModelBase viewModel, Action<ViewModelBase, bool> callBack)
        {
            var newWindow = new Window();

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
            newWindow.Closed += closeEventHandler;

            newWindow.DataContext = viewModel;
            newWindow.Content = viewModel;

            newWindow.SizeToContent = SizeToContent.WidthAndHeight;

            newWindow.Show();
        }
    }
}