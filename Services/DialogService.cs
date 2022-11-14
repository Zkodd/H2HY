using H2HY.Views;
using System;
using System.Collections.Generic;
using System.Windows;

namespace H2HY.Services
{
    /// <summary>
    /// Example usage:
    ///  App.cs (Composing Root) Register Dialog to ViewModel:
    /// <![CDATA[DialogService.RegistertDialog<DesiredView, DesiredViewModel>();
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
        ///  Shows given viewmodel in a modal dialog window.
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <param name="callback">callback on window close event. Can be true/false for modal - otherwise always false</param>
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
        ///  Shows given viewmodel in a modal dialog window.
        /// </summary>
        /// <param name="viewmodel"></param>
        /// <param name="callback">callback on window close event.</param>
        /// <exception cref="KeyNotFoundException"></exception>
        private void ShowModalDialog(ViewModelDialogBase viewmodel, Action<ViewModelBase, bool> callback)
        {
            Type? viewType = _mappings.GetValueOrDefault(viewmodel.GetType());
            if (viewType is null)
            {
                throw new KeyNotFoundException($"DialogService: Key {viewmodel.GetType()} not found in dictionary.");
            }

            object? view = Activator.CreateInstance(viewType);

            if (view is FrameworkElement frameworkElement)
            {
                frameworkElement.DataContext = viewmodel;
            }

            var dialog = new H2HYModalDialog();
            void closeEventHandler(object? s, EventArgs e)
            {
                try
                {
                    viewmodel.ViewClosed(dialog.H2HYDialogResult);
                    callback(viewmodel, dialog.H2HYDialogResult);
                    viewmodel.Dispose();
                }
                finally
                {
                    dialog.Closed -= closeEventHandler;
                }
            }

            dialog.Closed += closeEventHandler;
            dialog.ViewContent.Content = view;

            dialog.DataContext = viewmodel;

            dialog.SizeToContent = SizeToContent.WidthAndHeight;

            if (viewmodel.IsModal)
            {
                dialog.ShowDialog();
            }
            else
            {
                dialog.Show();
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
            Type? viewType = _mappings.GetValueOrDefault(viewmodel.GetType());
            if (viewType is null)
            {
                throw new KeyNotFoundException($"DialogService: Key {viewmodel.GetType()} not found in dictionary.");
            }

            object? view = Activator.CreateInstance(viewType);
            if (view is FrameworkElement frameworkElement)
            {
                frameworkElement.DataContext = viewmodel;
            }

            var newWindow = new Window();
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
            newWindow.Content = view;
            newWindow.SizeToContent = SizeToContent.WidthAndHeight;
            newWindow.Show();
        }
    }
}