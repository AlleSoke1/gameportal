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

    public partial class SpecialButton : UserControl
    {

        

        #region Text DP
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty TextProperty
            = DependencyProperty.Register(
                  "Text",
                  typeof(string),
                  typeof(SpecialButton)
              );
        #endregion

        #region ExtraIcon DP
        public MahApps.Metro.IconPacks.PackIconMaterialKind ExtraIcon
        {
            get { return (MahApps.Metro.IconPacks.PackIconMaterialKind)GetValue(ExtraIconProperty); }
            set { SetValue(ExtraIconProperty, value); }
        }

        public static readonly DependencyProperty ExtraIconProperty
            = DependencyProperty.Register(
                  "ExtraIcon",
                  typeof(MahApps.Metro.IconPacks.PackIconMaterialKind),
                  typeof(SpecialButton)
              );
        #endregion


        #region BtnBackground DP
        public Brush BtnBackground
        {
            get { return (Brush)GetValue(BtnBackgroundProperty); }
            set { SetValue(BtnBackgroundProperty, value); }
        }

        public static readonly DependencyProperty BtnBackgroundProperty
            = DependencyProperty.Register(
                  "BtnBackground",
                  typeof(Brush),
                  typeof(SpecialButton)
              );
        #endregion

        #region BtnBackgroundHover DP
        public Brush BtnBackgroundHover
        {
            get { return (Brush)GetValue(BtnBackgroundHoverProperty); }
            set { SetValue(BtnBackgroundHoverProperty, value); }
        }

        public static readonly DependencyProperty BtnBackgroundHoverProperty
            = DependencyProperty.Register(
                  "BtnBackgroundHover",
                  typeof(Brush),
                  typeof(SpecialButton)
              );
        #endregion

        #region BtnBorder DP
        public Brush BtnBorder
        {
            get { return (Brush)GetValue(BtnBorderProperty); }
            set { SetValue(BtnBorderProperty, value); }
        }

        public static readonly DependencyProperty BtnBorderProperty
            = DependencyProperty.Register(
                  "BtnBorder",
                  typeof(Brush),
                  typeof(SpecialButton)
              );
        #endregion

        #region BtnRadius DP
        public CornerRadius BtnRadius
        {
            get { return (CornerRadius)GetValue(BtnRadiusProperty); }
            set { SetValue(BtnRadiusProperty, value); }
        }

        public static readonly DependencyProperty BtnRadiusProperty
            = DependencyProperty.Register(
                  "BtnRadius",
                  typeof(CornerRadius),
                  typeof(SpecialButton)
              );
        #endregion

        #region BtnBorderThickness DP
        public Thickness BtnBorderThickness
        {
            get { return (Thickness)GetValue(BtnBorderThicknessProperty); }
            set { SetValue(BtnBorderThicknessProperty, value); }
        }

        public static readonly DependencyProperty BtnBorderThicknessProperty
            = DependencyProperty.Register(
                  "BtnBorderThickness",
                  typeof(Thickness),
                  typeof(SpecialButton)
              );
        #endregion

        #region BtnStyle DP
        public SpecialStyle BtnStyle
        {
            get { return (SpecialStyle)GetValue(BtnStyleProperty); }
            set { SetValue(BtnStyleProperty, value); loadButtonStyle(value); }
        }

        public static readonly DependencyProperty BtnStyleProperty
            = DependencyProperty.Register(
                  "BtnStyle",
                  typeof(SpecialStyle),
                  typeof(SpecialButton)
              );
        #endregion

        public event RoutedEventHandler Click;

        public SpecialButton()
        {
            InitializeComponent();
            this.DataContext = this;

        }

        private void loadButtonStyle(SpecialStyle _style)
        {
            if (_style == SpecialStyle.Flat)
            {
                ButtonBorder.Background.Opacity = 1;
            }
            else if (_style == SpecialStyle.FlatOutLine)
            {
                if (ButtonBorder.Background != null)
                    ButtonBorder.Background.Opacity = 0;
            }
            else if (_style == SpecialStyle.TransBordered)
            {
                ButtonBorder.Background.Opacity = 0.3;
                this.ContentFiled.Opacity = 0.8;
            }

            else if (_style == SpecialStyle.NoBackground)
            {
                this.ContentFiled.Opacity = 0.8;
            }
        }

        public enum SpecialStyle : int
        {
            Flat,
            FlatOutLine,
            TransBordered,
            NoBackground,
        }

        public void DoClick()
        {
            if (this.Click != null)
            {
                this.Click(this, null);
            }
        }
        private void ControlElement_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.BtnStyle == SpecialStyle.Flat)
            {
                ButtonBorder.Background = this.BtnBackgroundHover;
            }
            else if (this.BtnStyle == SpecialStyle.FlatOutLine)
            {
                ButtonBorder.BorderBrush = this.BtnBackgroundHover;
                ButtonBorder.Background = this.BtnBackgroundHover;
                ButtonBorder.Background.Opacity = 1;
            }
            else if (this.BtnStyle == SpecialStyle.TransBordered)
            {
                this.ContentFiled.Opacity = 1;
            }

            else if (this.BtnStyle == SpecialStyle.NoBackground)
            {
                this.ContentFiled.Opacity = 1;
            }
        }

        private void ControlElement_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.BtnStyle == SpecialStyle.Flat)
            {
                ButtonBorder.Background = this.BtnBackground;
            }
            else if (this.BtnStyle == SpecialStyle.FlatOutLine)
            {
                ButtonBorder.BorderBrush = this.BtnBorder;
                ButtonBorder.Background.Opacity = 0;
            }
            else if (this.BtnStyle == SpecialStyle.TransBordered)
            {
                this.ContentFiled.Opacity = 0.8;
            }
            else if (this.BtnStyle == SpecialStyle.NoBackground)
            {
                this.ContentFiled.Opacity = 0.8;
            }
        }

        private void ControlElement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }

        private void ControlElement_Loaded(object sender, RoutedEventArgs e)
        {
            loadButtonStyle(this.BtnStyle);
            if (ExtraIcon == null || ExtraIcon == MahApps.Metro.IconPacks.PackIconMaterialKind.None)
            {
                iconFiled.Width = 0;
                iconFiled.Margin =  new Thickness(0);
            }

            if (Text == null || Text == "")
            {
                iconFiled.Margin = new Thickness(0);
            }
        }

        private void ControlElement_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == false)
            {
                this.Opacity = 0.5;
                this.Cursor = Cursors.No;
            }else{
                this.Opacity = 1;
                this.Cursor = Cursors.Hand;
            }
        }

    }

}
