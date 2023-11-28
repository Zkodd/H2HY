using System.Windows;

namespace H2HY.Properties
{
    /// <summary>
    /// Usage: attach to view window like this:
    /// <code>h2hyProp:WindowCloser.EnableWindowClosing="True"</code>
    /// Add interface <code>ICloseWindow</code> to the used view model.
    /// </summary>
    public class WindowCloser
    {
        /// <summary>
        /// Getter for EnableWindowClosing
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool GetEnableWindowClosing(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableWindowClosingProperty);
        }

        /// <summary>
        /// Setter for EnableWindowClosing
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetEnableWindowClosing(DependencyObject obj, bool value)
        {
            obj.SetValue(EnableWindowClosingProperty, value);
        }

        /// <summary>
        /// enables or disables the windows close property
        /// </summary>
        public static readonly DependencyProperty EnableWindowClosingProperty =
            DependencyProperty.RegisterAttached("EnableWindowClosing", typeof(bool), typeof(WindowCloser), new PropertyMetadata(false, OnEnableWindowClosingChanged));

        private static void OnEnableWindowClosingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Window window)
            {
                window.Loaded += (s, e) =>
                {
                    if (window.DataContext is ICloseWindow vm)
                    {
                        //add some events to the given view model(data context):
                        vm.Close += () =>
                        {//i will execute window.close() if view model.Close is invoked.
                            window.Close();
                        };

                        window.Closing += (s, e) =>
                        {
                            e.Cancel = !vm.CanCloseWindow;
                        };
                    }
                };
            }
        }
    }
}