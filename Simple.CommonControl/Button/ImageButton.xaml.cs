using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// Interaction logic for ImageButton.xaml
    /// </summary>
    public partial class ImageButton : Button
    {
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register("Source", typeof(ImageSource), typeof(ImageButton));
        public ImageSource Source
        {
            get {  return (ImageSource)GetValue(SourceProperty);}
            set{SetValue(SourceProperty, value);}
        }

        PressEffect m_pressEffect;

        public ImageButton()
        {
            InitializeComponent();
            m_pressEffect = new PressEffect();
            this.Effect = m_pressEffect;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            IsClickDown(true);
        }

        protected override void OnTouchDown(TouchEventArgs e)
        {
            base.OnTouchDown(e);
            IsClickDown(true);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            IsClickDown(false);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            base.OnMouseLeave(e);
            IsClickDown(false);
        }

        protected override void OnTouchUp(TouchEventArgs e)
        {
            base.OnTouchUp(e);
            IsClickDown(false);
        }

        protected override void OnTouchLeave(TouchEventArgs e)
        {
            base.OnTouchLeave(e);
            IsClickDown(false);
        }

        private void IsClickDown(bool isPressed)
        {
            if (isPressed)
                m_pressEffect.IsPressed = 1;
            else
                m_pressEffect.IsPressed = 0;
        }
    }
}
