using System;

namespace H2HY
{
    public abstract class ViewModelDialogBase : ViewModelBase, ICloseWindow
    {
        private bool _isOkEnabled;
        private string _title = string.Empty;

        /// <summary>
        /// Dialog window close methode is attached here. No need to touch.
        /// </summary>
        public Action? Close { get; set; }

        /// <summary>
        /// Assigend after the window is closed and holds the dialogresult. Default is false.
        /// </summary>
        public bool DialogResult { get; set; }

        /// <summary>
        /// Enables Ok button on form. Default is false.
        /// </summary>
        public bool IsOkEnabled
        {
            get => _isOkEnabled;
            set
            {
                SetProperty(ref _isOkEnabled, value);
            }
        }

        /// <summary>
        /// Window title. Default is empty.
        /// </summary>
        public string Title
        {
            get => _title;
            set
            {
                SetProperty(ref _title, value);
            }
        }

        /// <summary>
        /// Handles the Can-close behaivor. Default is true.
        /// </summary>
        public bool CanCloseWindow { get; set; } = true;

        /// <summary>
        /// closes the current dialog window.
        /// </summary>
        public void CloseDialog()
        {
            Close?.Invoke();
        }
    }
}