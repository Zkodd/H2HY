using System;
using System.Collections.Generic;
using System.Windows;

namespace H2HY.Services
{
    // - unused -
    // How to use:
    //  App.cs (Composing Root) Register Dialog to ViewModel:
    //  DialogService.RegistertDialog<HazardLogChapterEditView, HazardLogChapterEditCaptionViewModel>();
    //
    // HazardLogChapterEditCaptionViewModel.cs, use Dialoagservice:
    //
    //      DialogService dialogService = new();
    //      EditCaption = new RelayCommand(i =>
    //      {
    //          dialogService.ShowDialog(, x => { new HazardLogChapterEditCaptionViewModel(modalstore, this) });
    //      });
    //
    //  Window Close is missing in wpf
    //
    public class DialogService : IDialogService
    {
        private Window _currentDialog;

        internal static Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();

        /// <summary>
        /// Register all Views and ViewModels. Best place: App.xmal.cs
        /// </summary>
        /// <typeparam name="TView"></typeparam>
        /// <typeparam name="TViewModel"></typeparam>
        public static void RegistertDialog<TView, TViewModel>()
        {
            _mappings.Add(typeof(TViewModel), typeof(TView));
        }

        public void ShowDialog(ViewModelBase viewmodel, Action<bool> callback)
        {
            if (viewmodel == null)
            {
                return;
            }

            Type viewType = _mappings[viewmodel.GetType()];
 
            _currentDialog = new Window();

            void closeEventHandler(object s, EventArgs e)
            {
                bool result = _currentDialog?.DialogResult != default ? (bool)_currentDialog.DialogResult : false;

                callback(result);
                _currentDialog.Closed -= closeEventHandler;
            }
            _currentDialog.Closed += closeEventHandler;

            object content = Activator.CreateInstance(viewType);
            (content as FrameworkElement).DataContext = viewmodel;

            _currentDialog.Content = content;
            _currentDialog.SizeToContent = SizeToContent.WidthAndHeight;
            _currentDialog.Show();
        }
    }
}
