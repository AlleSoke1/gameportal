using FontAwesome.WPF;
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

namespace LauncherApp.Styles.Controls
{
    /// <summary>
    /// Interaction logic for ChatMemeberListItem.xaml
    /// </summary>
    public partial class ChatMemeberListItem : UserControl
    {


        #region UserID DP
        public long UserID
        {
            get { return (long)GetValue(UserIDProperty); }
            set { SetValue(UserIDProperty, value); }
        }

        public static readonly DependencyProperty UserIDProperty
            = DependencyProperty.Register(
                  "UserID",
                  typeof(long),
                  typeof(ChatMemeberListItem)
              );
        #endregion

        #region UserName DP
        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); SetUserName(value); }
        }


        public static readonly DependencyProperty UserNameProperty
            = DependencyProperty.Register(
                  "UserName",
                  typeof(string),
                  typeof(ChatMemeberListItem)
              );
        #endregion

        #region UserStatus DP
        public Enums.UserInfo.UserStatus UserStatus
        {
            get { return (Enums.UserInfo.UserStatus)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); SwithUserStatus(value); }
        }


        public static readonly DependencyProperty StatusProperty
            = DependencyProperty.Register(
                  "UserStatus",
                  typeof(Enums.UserInfo.UserStatus),
                  typeof(ChatMemeberListItem)
              );
        #endregion

        public ChatMemeberListItem()
        {
            InitializeComponent();

            this.DataContext = this;
            
        }

        #region UI Functions

        private void SetUserName(string value)
        {
            NameLabel.Content = value;
            NameLetterLabel.Content = value[0];
        }


        private void ItemElement_MouseEnter(object sender, MouseEventArgs e)
        {
            ItemElement.Background.Opacity = 0.10;
        }

        private void ItemElement_MouseLeave(object sender, MouseEventArgs e)
        {
            ItemElement.Background.Opacity = 0;
        }

        private void SwithUserStatus(Enums.UserInfo.UserStatus userStatus)
        {


            switch (userStatus)
            {
                case Enums.UserInfo.UserStatus.Disconnected:

                    UserStatusIcon.Background = Brushes.Silver;

                    break;

                case Enums.UserInfo.UserStatus.Available:

                    UserStatusIcon.Background = Brushes.Lime;

                    break;
                case Enums.UserInfo.UserStatus.Away:

                    UserStatusIcon.Background = Brushes.Orange;

                    break;
                case Enums.UserInfo.UserStatus.Busy:

                    UserStatusIcon.Background = Brushes.Red;

                    break;
            }

        }

        #endregion










    }
}
