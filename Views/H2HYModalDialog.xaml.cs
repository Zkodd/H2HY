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

        private void OkClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult= false;
            Close();
        }
    }
}
