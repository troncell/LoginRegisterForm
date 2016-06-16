using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Simple.CommonControl
{
    /// <summary>
    /// Interaction logic for InfoPasswordBox.xaml
    /// </summary>
    public partial class InfoPasswordBox : UserControl
    {
        public InfoPasswordBox()
        {
            InitializeComponent();
            password.PasswordChanged += Password_PasswordChanged;
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            SetValue(TextProperty, password.Password);
            TextBoxHelper.SetHasText(password, !string.IsNullOrEmpty(password.Password));
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                                                               "Text",
                                                               typeof(string),
                                                               typeof(InfoPasswordBox),
                                                               new FrameworkPropertyMetadata { BindsTwoWayByDefault=true });

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextBoxInfoProperty = DependencyProperty.Register(
      "TextBoxInfo",
      typeof(string),
      typeof(InfoPasswordBox),
      new PropertyMetadata(string.Empty));

        public string TextBoxInfo
        {
            get { return (string)GetValue(TextBoxInfoProperty); }
            set { SetValue(TextBoxInfoProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(ImageSource), typeof(InfoPasswordBox));

        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
    }

    public class TextBoxHelper : DependencyObject
    {
        public static readonly DependencyProperty HasTextProperty =
    DependencyProperty.RegisterAttached("HasText",
    typeof(bool), typeof(TextBoxHelper),
    new FrameworkPropertyMetadata(false,
        FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// Sets the orientation.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value.</param>
        public static void SetHasText(UIElement element, bool value)
        {
            element.SetValue(HasTextProperty, value);
        }

        /// <summary>
        /// Gets the orientation.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        public static bool GetHasText(UIElement element)
        {
            return (bool)element.GetValue(HasTextProperty);
        }


    }

}
