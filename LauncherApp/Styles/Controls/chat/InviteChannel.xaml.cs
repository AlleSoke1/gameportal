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
    /// Interaction logic for InviteChannel.xaml
    /// </summary>
    public partial class InviteChannel : UserControl
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
                  typeof(InviteChannel)
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
                  typeof(InviteChannel)
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
                  typeof(InviteChannel)
              );

        #endregion

        public string channelName;


        //invite settings
        private int inviteLimit = 8;

        public InviteChannel()
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
            string frinedName = SearchBox.Text;
            SendButton.IsEnabled = false;
            SearchBox.IsEnabled = false;
            LoadingStatus(true);
            bool ErrorStatus = false;

            if (frinedName.ToLower() == LauncherApp.Game_Data.Globals.nickname.ToLower())
            {
                // user trying to invite him self
                showMessage("You cannot invite your self.", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                ErrorStatus = true;

            }


            if (ErrorStatus == false)
            {
                SendInviteFriendPacket(frinedName);
                return;
            }
            else
            {
                SendButton.IsEnabled = true;
                SearchBox.IsEnabled = true;
                LoadingStatus(false);
            }
        }

        void SendInviteFriendPacket(string frinedName)
        {
            for (int i = 0; i < 1; i++)
            {
                Packets.Channel.ChannelInviteReq packet = new Packets.Channel.ChannelInviteReq();

                packet.senderID = LauncherApp.Game_Data.Globals.uid;
                packet.chatID = this._chatID;
                packet.inviteNickName = frinedName;
                packet.channelName = this.channelName;

                App.connection.Send(packet, "ChannelInviteReq");
            }
        }

        internal void OnReciveResult(int result)
        {
            Enums.Channel.InviteChannelReply repStatus = (Enums.Channel.InviteChannelReply)result;

            switch (repStatus)
            {
                case Enums.Channel.InviteChannelReply.Successfull:

                    Dispatcher.Invoke(() =>
                    {
                        showMessage("Invite has been success.", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Success);
                    });

                    break;

                case Enums.Channel.InviteChannelReply.EmptyName:

                    Dispatcher.Invoke(() =>
                    {
                        showMessage("You can't send invite to empty name.", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                    });

                    break;

                case Enums.Channel.InviteChannelReply.Undentified:

                    Dispatcher.Invoke(() =>
                    {
                        showMessage("Something Wrong Happend, Call Support Team.", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                    });

                    break;

                case Enums.Channel.InviteChannelReply.UserInChannel:

                    Dispatcher.Invoke(() =>
                    {
                        showMessage("Sorry you can't invite user already in the channel", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                    });

                    break;
                case Enums.Channel.InviteChannelReply.UserNotExists:

                    Dispatcher.Invoke(() =>
                    {
                        showMessage("Sorry we can't find this user.", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                    });

                    break;
                case Enums.Channel.InviteChannelReply.UserNotFriend:

                    Dispatcher.Invoke(() =>
                    {
                        showMessage("Invited user must be in your friend list.", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
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
