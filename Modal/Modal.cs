using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace H2HY.Modal
{
    /// <summary>
    /// Not done now. Only for .net >= 5
    /// <![CDATA[ 
    /// https://www.youtube.com/watch?v=M8BAIq0yoy8&list=PLA8ZIAm2I03ggP55JbLOrXl6puKw4rEb2&index=7
    /// ]]>
    /// </summary>
    public class Modal : ContentControl
    {
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(Modal), 
                new PropertyMetadata(false));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        static Modal()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Modal), new FrameworkPropertyMetadata(typeof(Modal)));
            BackgroundProperty.OverrideMetadata(typeof(Modal), new FrameworkPropertyMetadata(CreateDefaultBackground()));
        }

        private static object CreateDefaultBackground()
        {
            return new SolidColorBrush(Colors.Black)
            {
                Opacity = 0.3
            };
        }
    }
}
