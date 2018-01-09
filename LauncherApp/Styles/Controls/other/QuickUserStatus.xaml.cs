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
    
    public partial class QuickUserStatus : UserControl
    {

        #region Status DP
        public Enums.UserInfo.UserStatus Status
        {
            get { return (Enums.UserInfo.UserStatus)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); SetStatus(value); }
        }


        public static readonly DependencyProperty StatusProperty
            = DependencyProperty.Register(
                  "Status",
                  typeof(Enums.UserInfo.UserStatus),
                  typeof(QuickUserStatus)
              );
        #endregion

        #region Nickname DP
        public string Nickname
        {
            get { return (string)GetValue(NicknameProperty); }
            set { SetValue(NicknameProperty, value); }
        }

        public static readonly DependencyProperty NicknameProperty
            = DependencyProperty.Register(
                  "Nickname",
                  typeof(string),
                  typeof(QuickUserStatus)
              );
        #endregion

        #region MenuFlag DP
        public bool MenuFlag
        {
            get { return (bool)GetValue(MenuFlagProperty); }
            set { SetValue(MenuFlagProperty, value); SetMenuFlag(value); }
        }


        public static readonly DependencyProperty MenuFlagProperty
            = DependencyProperty.Register(
                  "MenuFlag",
                  typeof(bool),
                  typeof(QuickUserStatus)
              );
        #endregion

        public event RoutedEventHandler Click;

        public QuickUserStatus()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        private void SetStatus(Enums.UserInfo.UserStatus value)
        {
            switch (value)
            {
                case Enums.UserInfo.UserStatus.Available:
                    StatusIcon.Foreground = Brushes.Lime;
                    break;
                case Enums.UserInfo.UserStatus.Busy:
                    StatusIcon.Foreground = Brushes.Red;
                    break;
                case Enums.UserInfo.UserStatus.Away:
                    StatusIcon.Foreground = Brushes.Orange;
                    break;
                case Enums.UserInfo.UserStatus.Invisible:
                    StatusIcon.Foreground = Brushes.Gray;
                    break;
            }
        }


        private void SetMenuFlag(bool value)
        {
            if (MenuFlag)
            {
                MenuIcon.Visibility = Visibility.Visible;
                return;
            }

            MenuIcon.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Click != null)
            {
                this.Click(this, e);
            }
        }

    }

}
