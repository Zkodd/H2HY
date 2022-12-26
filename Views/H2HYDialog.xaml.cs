using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace H2HY.Views
{
    /// <summary>
    /// Interaction logic for H2HYDialog.xaml
    /// </summary>
    public partial class H2HYDialog : Window
    {
        public H2HYDialog()
        {
            InitializeComponent();
        }

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
