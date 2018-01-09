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
        public MahApps.Metro.IconPacks.PackIconMaterialKind Icon
        {
            get { return (MahApps.Metro.IconPacks.PackIconMaterialKind)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty
            = DependencyProperty.Register(
                  "Icon",
                  typeof(MahApps.Metro.IconPacks.PackIconMaterialKind),
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

        #region isActive DP
        public bool isActive
        {
            get { return (bool)GetValue(isActiveProperty); }
            set { SetValue(isActiveProperty, value); ActiveButton(value); }
        }

        public static readonly DependencyProperty isActiveProperty
            = DependencyProperty.Register(
                  "isActive",
                  typeof(bool),
                  typeof(HeaderButton)
              );
        #endregion

        public event RoutedEventHandler Click;

        public HeaderButton()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        private void ActiveButton(bool value)
        {
            if (value)
            {
                this.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFDA0029");
                this.Background.Opacity = 1;
                return;
            }

            this.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(this.BackgroundColor);
            this.Background.Opacity = 0;

        }


        private void SetBackgroundColor(string value)
        {
            if (!isActive)
            {
                if (value == "null")
                {

                    return;
                }

                this.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(value);
            }
           
        }


        private void ControlElement_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!isActive)
            {
                if (BackgroundColor == "null")
                {
                    ButtonIcon.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFDA0029");
                    return;
                }

                this.Background.Opacity = 1;
            }
        }

        private void ControlElement_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!isActive)
            {
                if (BackgroundColor == "null")
                {
                    ButtonIcon.Foreground = Brushes.White;
                    return;
                }

                this.Background.Opacity = 0;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }

        private void ControlElement_Loaded(object sender, RoutedEventArgs e)
        {
            if (isActive)
            {
                this.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFDA0029");
                this.Background.Opacity = 1;
                return;
            }
        }

    }

}
