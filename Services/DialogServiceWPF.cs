using H2HY.Views;
using System;
using System.Collections.Generic;
using System.Windows;

namespace H2HY.Services
{
    public class DialogServiceWPF : IDialogService
    {

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

        public MessageBoxResult ShowMessageBox(string message, string caption, MessageBoxButton buttons, MessageBoxIcon icon)
        {
            return (MessageBoxResult)MessageBox.Show(message,
                                               caption,
                                               (System.Windows.MessageBoxButton)buttons,
                                               (MessageBoxImage)icon);
        }


        private static void SetupWindow(Window dialogWindow, FrameworkElement view)
        {
            dialogWindow.MaxHeight = view.MaxHeight + dialogWindow.BorderThickness.Top + dialogWindow.BorderThickness.Bottom;
            dialogWindow.MaxWidth = view.MaxWidth + dialogWindow.BorderThickness.Left + dialogWindow.BorderThickness.Right;

            dialogWindow.MinHeight = view.MinHeight + dialogWindow.BorderThickness.Top + dialogWindow.BorderThickness.Bottom;
            dialogWindow.MinWidth = view.MinWidth + dialogWindow.BorderThickness.Left + dialogWindow.BorderThickness.Right;
        }

        /// <summary>
        ///  Shows given viewmodel in a modal dialog window.
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <param name="callback">callback on window close event.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        private void ShowModalDialog(ViewModelDialogBase viewmodel, Action<ViewModelBase, bool>? callback)
        {
            H2HYDialog dialogWindow = new()
            {
                DataContext = viewmodel
            };

            void closeEventHandler(object? s, EventArgs e)
            {
                try
                {
                    viewmodel.ViewClosed(dialogWindow.H2HYDialogResult);
                    callback?.Invoke(viewmodel, dialogWindow.H2HYDialogResult);
                    viewmodel.Dispose();
                }
                finally
                {
                    dialogWindow.Closed -= closeEventHandler;
                }
            }
            dialogWindow.Closed += closeEventHandler;

            //violating mvvm prinicples here! We know the view. 
            //better create nested viewmodel and use databinding.

            dialogWindow.DataContext = viewmodel;
            dialogWindow.SizeToContent = SizeToContent.WidthAndHeight;

            if (viewmodel.IsModal)
            {
                dialogWindow.ShowDialog();
            }
            else
            {
                dialogWindow.Show();
            }
        }
        /// <summary>
        /// Shows given viewmodel in a window. Dialoagresult will be false.
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <param name="callback">callback on window close event.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        private void ShowWindowDialog(ViewModelBase viewmodel, Action<ViewModelBase, bool> callback)
        {
            var newWindow = new System.Windows.Window()
            {
                DataContext = viewmodel
            };

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
            newWindow.SizeToContent = SizeToContent.WidthAndHeight;
            newWindow.Show();
        }
    }
}
