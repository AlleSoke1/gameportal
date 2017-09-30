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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace LauncherApp.Styles.Controls
{
    /// <summary>
    /// Interaction logic for FastMessage.xaml
    /// </summary>

    public partial class HeaderButton : UserControl
    {

        #region Icon DP
        public FontAwesome.WPF.FontAwesomeIcon Icon
        {
            get { return (FontAwesome.WPF.FontAwesomeIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty
            = DependencyProperty.Register(
                  "Icon",
                  typeof(FontAwesome.WPF.FontAwesomeIcon),
                  typeof(HeaderButton)
              );
        #endregion

        #region BackgroundColor DP
        public string BackgroundColor
        {
            get { return (string)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); SetBackgroundColor(value); }
        }

        public static readonly DependencyProperty BackgroundColorProperty
            = DependencyProperty.Register(
                  "BackgroundColor",
                  typeof(string),
                  typeof(HeaderButton)
              );
        #endregion

        public event RoutedEventHandler Click;

        public HeaderButton()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        private void SetBackgroundColor(string value)
        {
            if (value == "null")
            {

                return;
            }

            this.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(value);
        }


        private void ControlElement_MouseEnter(object sender, MouseEventArgs e)
        {
            if (BackgroundColor == "null")
            {
                ButtonIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFDA0029");
                return;
            }

            this.Background.Opacity = 1;
        }

        private void ControlElement_MouseLeave(object sender, MouseEventArgs e)
        {
            if (BackgroundColor == "null")
            {
                ButtonIcon.Foreground = Brushes.White;
                return;
            }

            this.Background.Opacity = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(sender, e);
            }
        }

    }

}
