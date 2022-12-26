using System.Windows;

namespace H2HY.Views
{
    /// <summary>
    /// Interaction logic for H2HYDialog.xaml
    /// </summary>
    public partial class H2HYDialog : Window
    {
        /// <summary>
        /// internal use for dialogServiceWPF
        /// </summary>
        public H2HYDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// retruns modal dialog result
        /// </summary>
        public bool H2HYDialogResult { get; private set; } = false;

        private void OkClick(object sender, RoutedEventArgs e)
        {
            H2HYDialogResult = true;
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            H2HYDialogResult = false;
            Close();
        }
    }
}
