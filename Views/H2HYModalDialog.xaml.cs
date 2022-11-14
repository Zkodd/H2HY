using System.Windows;

namespace H2HY.Views
{
    /// <summary>
    /// Interaction logic for H2HYDialog.xaml
    /// </summary>
    public partial class H2HYModalDialog : Window
    {
        public H2HYModalDialog()
        {
            InitializeComponent();
        }

        public bool H2HYDialogResult { get; private set; } = false;

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            H2HYDialogResult = false;
            Close();
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            H2HYDialogResult = true;
            Close();
        }
    }
}