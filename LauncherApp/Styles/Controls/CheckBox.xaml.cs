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
    /// Interaction logic for CheckBox.xaml
    /// </summary>
    
    public partial class CheckBox : UserControl
    {

        #region Text DP
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); SetActiveColor(value); }
        }

        public static readonly DependencyProperty TextProperty
            = DependencyProperty.Register(
                  "Text",
                  typeof(string),
                  typeof(CheckBox)
              );
        #endregion

        #region IsChecked DP
        public bool IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); SetCheckValue(value); }
        }


        public static readonly DependencyProperty IsCheckedProperty
            = DependencyProperty.Register(
                  "IsChecked",
                  typeof(bool),
                  typeof(CheckBox)
              );
        #endregion

        #region ActiveColor DP
        public string ActiveColor
        {
            get { return (string)GetValue(ActiveColorProperty); }
            set { SetValue(ActiveColorProperty, value);}
        }

        public static readonly DependencyProperty ActiveColorProperty
            = DependencyProperty.Register(
                  "ActiveColor",
                  typeof(string),
                  typeof(CheckBox)
              );
        #endregion

        public CheckBox()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        private void SetCheckValue(bool value)
        {
            if (value)
            {
                BoxBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString(this.ActiveColor);
                BoxIcon.Visibility = Visibility.Visible;
                return;
            }

            BoxBorder.BorderBrush = ControlElement.Foreground;
            BoxIcon.Visibility = Visibility.Hidden;
        }

        private void SetActiveColor(string value)
        {
            this.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(value);
        }


        private void ControlElement_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Background.Opacity = 1;
        }

        private void ControlElement_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Background.Opacity = 0;
        }

        private void ControlElement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.IsChecked = !IsChecked;
        }


    }

}
