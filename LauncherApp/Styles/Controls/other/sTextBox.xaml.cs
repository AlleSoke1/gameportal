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
    /// Interaction logic for sTextBox.xaml
    /// </summary>

    public partial class sTextBox : UserControl
    {

        

        #region Text DP
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); OnTextChanged(value); }
        }

        public static readonly DependencyProperty TextProperty
            = DependencyProperty.Register(
                  "Text",
                  typeof(string),
                  typeof(sTextBox)
              );
        #endregion

        #region SecText DP
        public string SecText
        {
            get { return (string)GetValue(SecTextProperty); }
            set { SetValue(SecTextProperty, value); }
        }

        public static readonly DependencyProperty SecTextProperty
            = DependencyProperty.Register(
                  "SecText",
                  typeof(string),
                  typeof(sTextBox)
              );
        #endregion

        #region IsReadOnly DP
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); ChangeReadMode(value); }
        }



        public static readonly DependencyProperty IsReadOnlyProperty
            = DependencyProperty.Register(
                  "IsReadOnly",
                  typeof(bool),
                  typeof(sTextBox)
              );
        #endregion

        #region IsPassword DP
        public bool IsPassword
        {
            get { return (bool)GetValue(IsPasswordProperty); }
            set { SetValue(IsPasswordProperty, value); SwitchPasswordMode(value); }
        }

        public static readonly DependencyProperty IsPasswordProperty
            = DependencyProperty.Register(
                  "IsPassword",
                  typeof(bool),
                  typeof(sTextBox)
              );
        #endregion

        #region ShowPasswordAble DP
        public bool ShowPasswordAble
        {
            get { return (bool)GetValue(ShowPasswordAbleProperty); }
            set { SetValue(ShowPasswordAbleProperty, value); ShowPasswordAbleButton(value); }
        }

        public static readonly DependencyProperty ShowPasswordAbleProperty
            = DependencyProperty.Register(
                  "ShowPasswordAble",
                  typeof(bool),
                  typeof(sTextBox)
              );
        #endregion

        #region boxBackground DP
        public Brush boxBackground
        {
            get { return (Brush)GetValue(boxBackgroundProperty); }
            set { SetValue(boxBackgroundProperty, value); }
        }

        public static readonly DependencyProperty boxBackgroundProperty
            = DependencyProperty.Register(
                  "boxBackground",
                  typeof(Brush),
                  typeof(sTextBox)
              );
        #endregion

        #region boxBackgroundHover DP
        public Brush boxBackgroundHover
        {
            get { return (Brush)GetValue(boxBackgroundHoverProperty); }
            set { SetValue(boxBackgroundHoverProperty, value); }
        }

        public static readonly DependencyProperty boxBackgroundHoverProperty
            = DependencyProperty.Register(
                  "boxBackgroundHover",
                  typeof(Brush),
                  typeof(sTextBox)
              );
        #endregion

        #region boxBorder DP
        public Brush boxBorder
        {
            get { return (Brush)GetValue(boxBorderProperty); }
            set { SetValue(boxBorderProperty, value); }
        }

        public static readonly DependencyProperty boxBorderProperty
            = DependencyProperty.Register(
                  "boxBorder",
                  typeof(Brush),
                  typeof(sTextBox)
              );
        #endregion

        #region boxBorderHover DP
        public Brush boxBorderHover
        {
            get { return (Brush)GetValue(boxBorderHoverProperty); }
            set { SetValue(boxBorderHoverProperty, value); }
        }

        public static readonly DependencyProperty boxBorderHoverProperty
            = DependencyProperty.Register(
                  "boxBorderHover",
                  typeof(Brush),
                  typeof(sTextBox)
              );
        #endregion

        #region boxCaretBrush DP
        public Brush boxCaretBrush
        {
            get { return (Brush)GetValue(boxCaretBrushProperty); }
            set { SetValue(boxCaretBrushProperty, value); }
        }

        public static readonly DependencyProperty boxCaretBrushProperty
            = DependencyProperty.Register(
                  "boxCaretBrush",
                  typeof(Brush),
                  typeof(sTextBox)
              );
        #endregion

        #region boxRadius DP
        public CornerRadius boxRadius
        {
            get { return (CornerRadius)GetValue(boxRadiusProperty); }
            set { SetValue(boxRadiusProperty, value); }
        }

        public static readonly DependencyProperty boxRadiusProperty
            = DependencyProperty.Register(
                  "boxRadius",
                  typeof(CornerRadius),
                  typeof(sTextBox)
              );
        #endregion

        #region boxBorderThickness DP
        public Thickness boxBorderThickness
        {
            get { return (Thickness)GetValue(boxBorderThicknessProperty); }
            set { SetValue(boxBorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty boxBorderThicknessProperty
            = DependencyProperty.Register(
                  "boxBorderThickness",
                  typeof(Thickness),
                  typeof(SpecialButton)
              );
        #endregion

        #region boxPadding DP
        public Thickness boxPadding
        {
            get { return (Thickness)GetValue(boxPaddingProperty); }
            set { SetValue(boxPaddingProperty, value); }
        }

        public static readonly DependencyProperty boxPaddingProperty
            = DependencyProperty.Register(
                  "boxPadding",
                  typeof(Thickness),
                  typeof(SpecialButton)
              );
        #endregion

        public event TextChangedEventHandler TextChangedEvent;
        public event KeyEventHandler KeyDownEvent;

        public sTextBox()
        {
            InitializeComponent();
            this.DataContext = this;

        }


        private void ChangeReadMode(bool value)
        {
            boxInputPassword.IsEnabled = !value;
            boxInput.IsReadOnly = value;
        }

        public void SetInputText(string newText)
        {
            boxInputPassword.Password = newText;
            boxInput.Text = newText;
        }


        private void boxInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if (boxBackgroundHover != null)
            {
                Boxorder.Background = this.boxBackgroundHover;
            }

            if (boxBorderHover != null)
            {
                Boxorder.BorderBrush = this.boxBorderHover;
            }
        }

        private void boxInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (boxBackgroundHover != null)
            {
                Boxorder.Background = this.boxBackground;
            }

            if (boxBorderHover != null)
            {
                Boxorder.BorderBrush = this.boxBorder;
            }
        }


        private void OnTextChanged(string value)
        {
            if (value.Length == 0)
            {
                backLabel.Visibility = Visibility.Visible;
            }
            else
            {
                backLabel.Visibility = Visibility.Hidden;
            }
        }


        private void boxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextChangedEvent != null)
                TextChangedEvent(sender, e);

            this.Text = boxInput.Text;

           
        }


        private void boxInputPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {

            if (TextChangedEvent != null)
                TextChangedEvent(sender, null);

            this.Text = boxInputPassword.Password.ToString(); ;
        }

        private void boxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (KeyDownEvent != null)
                KeyDownEvent(sender, e);
        }


        private void ShowPasswordAbleButton(bool value)
        {
            if (value)
            {
                PasswordIcon.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordIcon.Visibility = Visibility.Hidden;
            }
        }


        private void SwitchPasswordMode(bool value, bool isButton = false)
        {
            if (value)
            {
                if (boxInput.IsFocused)
                {
                    boxInputPassword.Focus();
                }
                boxInput.Visibility = Visibility.Hidden;
                boxInputPassword.Visibility = Visibility.Visible;
                if (isButton) boxInputPassword.Password = this.Text;
                return;
            }

            if (boxInputPassword.IsFocused)
            {
                boxInput.Focus();
            }
            boxInput.Visibility = Visibility.Visible;
            boxInputPassword.Visibility = Visibility.Hidden;
            if (isButton) boxInput.Text = this.Text;
        }

        private void PackIconMaterial_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool flag = boxInput.Visibility == Visibility.Visible ? true: false;
            SwitchPasswordMode(flag, true);
        }

        private void ControlElement_Loaded(object sender, RoutedEventArgs e)
        {
            ShowPasswordAbleButton(this.ShowPasswordAble);
            SwitchPasswordMode(this.IsPassword);
            ChangeReadMode(this.IsReadOnly);
        }

    }

}
