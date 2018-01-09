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
using System.ComponentModel;

namespace LauncherApp.Styles.Controls
{
    /// <summary>
    /// Interaction logic for AddFriend.xaml
    /// </summary>
    public partial class AddFriend : UserControl
    {

        #region _isOpen DP

        public bool _isOpen
        {
            get { return (bool)GetValue(_isOpenProperty); }
            set { SetValue(_isOpenProperty, value); OpenControl(value); }
        }



        public static readonly DependencyProperty _isOpenProperty = DependencyProperty.Register(
                  "_isOpen",
                  typeof(bool),
                  typeof(AddFriend)
              );

        #endregion

        #region _chatID DP

        public long _chatID
        {
            get { return (long)GetValue(_chatIDProperty); }
            set { SetValue(_chatIDProperty, value); }
        }


        public static readonly DependencyProperty _chatIDProperty = DependencyProperty.Register(
                  "_chatID",
                  typeof(long),
                  typeof(AddFriend)
              );

        #endregion

        #region _memeberCount DP

        public int _memeberCount
        {
            get { return (int)GetValue(_memeberCountProperty); }
            set { SetValue(_memeberCountProperty, value); UpdateCountLimit(value); }
        }


        public static readonly DependencyProperty _memeberCountProperty = DependencyProperty.Register(
                  "_memeberCount",
                  typeof(int),
                  typeof(AddFriend)
              );

        #endregion


        //invite settings
        private int inviteLimit = 8;

        public AddFriend()
        {
            InitializeComponent();
        }


        #region UI Functions


        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text.Length > 0)
            {
                
                TypeBoxLabel.Visibility = Visibility.Hidden;

                if (!SearchBox.Text.Contains(" ")) SendButton.IsEnabled = true;
            }
            else
            {
                TypeBoxLabel.Visibility = Visibility.Visible;
                SendButton.IsEnabled = false;
            }
        }


        private void OpenControl(bool value)
        {
            if (value)
            {
                this.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                this.Visibility = System.Windows.Visibility.Hidden;
            }
        }


        private void UpdateCountLimit(int value)
        {
            LimitLabel.Content = string.Format("You can more {0} friends to this channel.", (inviteLimit - value));
        }


        private void showMessage(string text, LauncherApp.Styles.Controls.FastMessage.MessageTypes type)
        {
            alertMessage.Message = text;
            alertMessage.ShowTime = 3;
            alertMessage.Type = type;
            alertMessage.Show();
        }

        private void LoadingStatus(bool p)
        {
            if (p)
            {
                LoginLoading.Visibility = Visibility.Visible;
            }
            else
            {
                LoginLoading.Visibility = Visibility.Hidden;
            }

        }

        #endregion

        private void hideBtn_Click(object sender, RoutedEventArgs e)
        {
            this._isOpen = false;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string invtedName = SearchBox.Text;
            SendButton.IsEnabled = false;
            SearchBox.IsEnabled = false;
            LoadingStatus(true);
            bool ErrorStatus = false;

            if (invtedName.ToLower() == LauncherApp.Game_Data.Globals.nickname.ToLower())
            {
                // user trying to invite him self
                showMessage("You cannot invite your self.", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                ErrorStatus = true;

            }


            if (ErrorStatus == false)
            {
                SendInviteFriendPacket(invtedName);
                return;
            }
            else
            {
                SendButton.IsEnabled = true;
                SearchBox.IsEnabled = true;
                LoadingStatus(false);
            }
        }


        void SendInviteFriendPacket(string invtedName)
        {
            for (int i = 0; i < 1; i++)
            {
                Packets.FriendsRequest.CSAddFriendReq packet = new Packets.FriendsRequest.CSAddFriendReq();

                packet.recieverName = invtedName;
                packet.senderID = LauncherApp.Game_Data.Globals.uid;
                packet.senderName = LauncherApp.Game_Data.Globals.nickname;

                App.connection.Send(packet, "CS_AddFriendReq");
            }
        }

        internal void OnReciveResult(object result)
        {

            switch ((Enums.Friend.AddFriendResult)result)
            {
                case Enums.Friend.AddFriendResult.Successfull:

                    Dispatcher.Invoke(() =>
                    {
                        showMessage("Request has been send to " + SearchBox.Text.ToString(), LauncherApp.Styles.Controls.FastMessage.MessageTypes.Success);
                    });

                    break;
                case Enums.Friend.AddFriendResult.UserBlocked:

                    Dispatcher.Invoke(() =>
                    {
                        showMessage("This user have block you.", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                    });

                    break;
                case Enums.Friend.AddFriendResult.AlreadyFriend:

                    Dispatcher.Invoke(() =>
                    {
                        showMessage("This user are already in your friend list.", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Info);
                    });

                    break;
                case Enums.Friend.AddFriendResult.AlreadyRequested:

                    Dispatcher.Invoke(() =>
                    {
                        showMessage("You have already send request for this user.", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Info);
                    });

                    break;
                case Enums.Friend.AddFriendResult.UserNotExists:

                    Dispatcher.Invoke(() =>
                    {
                        showMessage("Sorry we can't find this user, Please check name again.", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                    });

                    break;
                case Enums.Friend.AddFriendResult.Undentified:

                    Dispatcher.Invoke(() =>
                    {
                        showMessage("Something Wrong Happened! For More Info Call Support.", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Info);
                    });

                    break;
            }

            Dispatcher.Invoke(() =>
            {
                SendButton.IsEnabled = true;
                SearchBox.IsEnabled = true;
                LoadingStatus(false);
            });

        }

        


    }
}
