using H2HY.Views;
using System;
using System.Collections.Generic;
using System.Windows;

namespace H2HY.Services
{
    /// <summary>
    /// Example usage:
    ///  App.cs (Composing Root) Register Dialog to ViewModel:
    /// <![CDATA[DialogService.RegisterDialog<DesiredView, DesiredViewModel>();
    ///
    ///      DialogService dialogService = new();
    ///      EditCaption = new RelayCommand(i =>
    ///      {
    ///          dialogService.ShowWindow(desiredViewModel, () => { DoSomething(); });
    ///      });
    ///  ]]>
    /// </summary>
    public class DialogService : IDialogService
    {
        internal static Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();

        /// <summary>
        /// Register all Views and ViewModels. Best place: App.xmal.cs
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <typeparam name="TViewModel"></typeparam>
        public static void RegistertDialog<TView, TViewModel>() where TViewModel : ViewModelBase
        {
            _mappings.Add(typeof(TViewModel), typeof(TView));
        }

        /// <summary>
        ///  Shows given view model in a modal dialog window.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="callback">call back on window close event. Can be true/false for modal - otherwise always false</param>
        public void ShowDialog(ViewModelBase viewModel, Action<ViewModelBase, bool> callback)
        {
            if (viewModel is ViewModelDialogBase viewModelDialogBase)
            {
                ShowModalDialog(viewModelDialogBase, callback);
            }
            else
            {
                ShowWindowDialog(viewModel, callback);
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

        private static void SetupWindow(Window dialogWindow, FrameworkElement view)
        {
            dialogWindow.MaxHeight = view.MaxHeight + dialogWindow.BorderThickness.Top + dialogWindow.BorderThickness.Bottom;
            dialogWindow.MaxWidth = view.MaxWidth + dialogWindow.BorderThickness.Left + dialogWindow.BorderThickness.Right;

            dialogWindow.MinHeight = view.MinHeight + dialogWindow.BorderThickness.Top + dialogWindow.BorderThickness.Bottom;
            dialogWindow.MinWidth = view.MinWidth + dialogWindow.BorderThickness.Left + dialogWindow.BorderThickness.Right;
        }

        /// <summary>
        ///  Shows given view model in a modal dialog window.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="callback">call back on window close event.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        public void ShowModalDialog(ViewModelBase viewModel, Action<ViewModelBase, bool>? callback)
        {
            Type? viewType = _mappings.GetValueOrDefault(viewModel.GetType()) ?? throw new KeyNotFoundException($"DialogService: Key {viewModel.GetType()} not found in dictionary.");
            if (Activator.CreateInstance(viewType) is not FrameworkElement view)
            {
                throw new Exception($"Resolved View for {viewModel.GetType()} is not a framework element.");
            }

            view.DataContext = viewModel;

            H2HYModalDialog dialogWindow = new();
            SetupWindow(dialogWindow, view);
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
            dialogWindow.ViewContent.Content = view;

            //violating mvvm principles here! We know the view.
            //better create nested view model and use databinding.

            dialogWindow.DataContext = viewModel;
            dialogWindow.SizeToContent = SizeToContent.WidthAndHeight;

            dialogWindow.ShowDialog();
        }

        /// <summary>
        /// Shows given view model in a window. Dialog result will be false.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="callback">call back on window close event.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        private void ShowWindowDialog(ViewModelBase viewModel, Action<ViewModelBase, bool> callback)
        {
            Type? viewType = _mappings.GetValueOrDefault(viewModel.GetType());
            if (viewType is null)
            {
                throw new KeyNotFoundException($"DialogService: Key {viewModel.GetType()} not found in dictionary.");
            }

            if (Activator.CreateInstance(viewType) is not FrameworkElement view)
            {
                throw new Exception($"Resolved View for {viewModel.GetType()} is not a frameworkelement.");
            }

            view.DataContext = viewModel;

            var newWindow = new System.Windows.Window();
            SetupWindow(newWindow, view);
            void closeEventHandler(object? s, EventArgs e)
            {
                try
                {
                    viewModel.ViewClosed();
                    callback(viewModel, false);
                    viewModel.Dispose();
                }
                finally
                {
                    newWindow.Closed -= closeEventHandler;
                }
            }

            newWindow.Closed += closeEventHandler;
            newWindow.Content = view;
            newWindow.SizeToContent = SizeToContent.WidthAndHeight;
            newWindow.Show();
        }
    }
}