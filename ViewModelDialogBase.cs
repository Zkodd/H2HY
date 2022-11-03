namespace H2HY
{
    public abstract class ViewModelDialogBase : ViewModelBase
    {
        private bool _isOkEnabled;
        private string _title = string.Empty;

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

    }
}
