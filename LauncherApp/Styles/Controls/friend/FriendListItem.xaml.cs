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
    /// Interaction logic for FriendListItem.xaml
    /// </summary>
    public partial class FriendListItem : UserControl
    {


        public event MouseButtonEventHandler MenuClick;

        #region Status DP
        public Enums.UserInfo.UserStatus Status
        {
            get { return (Enums.UserInfo.UserStatus)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); SwithUserStatus(value); }
        }


        public static readonly DependencyProperty StatusProperty
            = DependencyProperty.Register(
                  "Status",
                  typeof(Enums.UserInfo.UserStatus),
                  typeof(FriendListItem)
              );
        #endregion

        #region IsSelected DP
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); SwithSelectedStatus(value); }
        }

        public static readonly DependencyProperty IsSelectedProperty
            = DependencyProperty.Register(
                  "IsSelected",
                  typeof(bool),
                  typeof(FriendListItem)
              );
        #endregion

        #region FriendName DP
        public string FriendName
        {
            get { return (string)GetValue(FriendNameProperty); }
            set { SetValue(FriendNameProperty, value); }
        }

        public static readonly DependencyProperty FriendNameProperty
            = DependencyProperty.Register(
                  "FriendName",
                  typeof(string),
                  typeof(FriendListItem)
              );
        #endregion

        #region UserID DP
        public long UserID
        {
            get { return (long)GetValue(UserIDProperty); }
            set { SetValue(UserIDProperty, value); UpdateUserID(value); }
        }

        public static readonly DependencyProperty UserIDProperty
            = DependencyProperty.Register(
                  "UserID",
                  typeof(long),
                  typeof(FriendListItem)
              );

        #endregion

        #region ChatID DP
        public long ChatID
        {
            get { return (long)GetValue(ChatIDProperty); }
            set { SetValue(ChatIDProperty, value); }
        }

        public static readonly DependencyProperty ChatIDProperty
            = DependencyProperty.Register(
                  "ChatID",
                  typeof(long),
                  typeof(FriendListItem)
              );

        #endregion


        public string menuOrder;

        public FriendListItem()
        {
            InitializeComponent();

            this.DataContext = this;
            
        }

        #region UI Functions

        private void TittlePanel_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsSelected)
            {
                TittlePanel.Background.Opacity = 0.10;
                PanelBorder.BorderBrush.Opacity = 1;
            }
            
        }


        private void TittlePanel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsSelected)
            {
                TittlePanel.Background.Opacity = 0;
                PanelBorder.BorderBrush.Opacity = 0;
            }
            
        }

        private void UpdateUserID(long value)
        {
            UserIdLabel.Content = "#" + value.ToString();
        }

        private void SwithSelectedStatus(bool value)
        {
            if (value)
            {
                PanelBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFDA0029");
                TittlePanel.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFDA0029");
                TittlePanel.Background.Opacity = 0.10;
                PanelBorder.BorderBrush.Opacity = 1;
                return;
            }

            PanelBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF68394B");
            TittlePanel.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFFFFFF");
            TittlePanel.Background.Opacity = 0;
            PanelBorder.BorderBrush.Opacity = 0;

        }
        
        private void TittlePanel_MouseDown(object sender, MouseButtonEventArgs e)
        {

            
        }
        #endregion


        private void onMenuClick(object sender, MouseButtonEventArgs e)
        {
            if (this.MenuClick != null)
            {
                menuOrder = ((StackPanel)sender).Name.ToString();
                this.MenuClick(this, e);
                
            }
        }


        private void SwithUserStatus(Enums.UserInfo.UserStatus userStatus)
        {

            UserImage.Source = new BitmapImage(new Uri("pack://application:,,,/resources/images/avatar.png"));

            switch (userStatus)
            {
                case Enums.UserInfo.UserStatus.Disconnected: 

                    UserName.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF700015");
                    StatusText.Content = "Offline";
                    StatusIcon.Foreground = Brushes.Silver;
                    UserImage.Source = new BitmapImage(new Uri("pack://application:,,,/resources/images/avatar-off.png"));

                    break ;

                case Enums.UserInfo.UserStatus.Available:
                    UserName.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFDA0029");
                    StatusText.Content = "Online";
                    StatusIcon.Foreground = Brushes.Lime;
                    break;
                case Enums.UserInfo.UserStatus.Away:
                    UserName.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFDA0029");
                    StatusText.Content = "Away";
                    StatusIcon.Foreground = Brushes.Orange;
                    break;
                case Enums.UserInfo.UserStatus.Busy:
                    UserName.Foreground = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFDA0029");
                    StatusText.Content = "Busy";
                    StatusIcon.Foreground = Brushes.Red;
                    break;
            }


            if (App.ChatMan._openedChats.ContainsKey(this.ChatID)) App.ChatMan._openedChats[this.ChatID].SwitchUserStatus(userStatus);

        }

        private void UserMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu temp = (ContextMenu)this.Resources["UserMenu"];
            temp.PlacementTarget = (Button)sender;
            temp.IsOpen = true;
        }



    }
}
