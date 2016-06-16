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
    public partial class InfoPasswordBox : TextBox
    {
        static InfoPasswordBox()
        {
            TextProperty.OverrideMetadata(typeof(InfoPasswordBox), new FrameworkPropertyMetadata(new PropertyChangedCallback(TextPropertyChanged)));
        }

        public InfoPasswordBox()
        {
            InitializeComponent();
            this.Style = FindResource("textStyle") as Style;
            
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

        private static readonly DependencyPropertyKey HasTextPropertyKey = DependencyProperty.RegisterReadOnly(
            "HasText",
            typeof(bool),
            typeof(InfoPasswordBox),
            new FrameworkPropertyMetadata(false));

        public static readonly DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;

        public bool HasText
        {
            get
            {
                return (bool)GetValue(HasTextProperty);
            }
        }

        public static readonly DependencyProperty PasswordProperty = DependencyProperty.Register("Password", typeof(string), typeof(InfoPasswordBox));
        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        static void TextPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            InfoPasswordBox itb = (InfoPasswordBox)sender;

            bool actuallyHasText = itb.Text.Length > 0;
            itb.SetValue(PasswordProperty, itb.Text);
            if (actuallyHasText != itb.HasText)
            {
                itb.SetValue(HasTextPropertyKey, actuallyHasText);
            }
        }
    }

    public class TextToStar : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string password = value as string;
            if (string.IsNullOrEmpty(password))
                return "";
            string stars = "";
            for (int i = 0; i < password.Length; i++)
            {
                stars += "*";
            }
            return stars;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
