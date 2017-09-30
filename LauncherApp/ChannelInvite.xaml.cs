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
using System.Windows.Shapes;

namespace LauncherApp
{
    /// <summary>
    /// Interaction logic for ChannelInvite.xaml
    /// </summary>
    public partial class ChannelInvite : Window
    {
        public long chatID;
        public string channelName;

        public ChannelInvite()
        {
            InitializeComponent();

            LoadStyles();


        }

        #region UI Functions

        private void LoadStyles()
        {

        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void NameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (NameBox.Text != "" && !NameBox.Text.Contains("Type Your Channel Name"))
                {
                    SendButton.IsEnabled = true;
                }
                else
                {
                    SendButton.IsEnabled = false;
                }
            }
            catch { }
            
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

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string frinedName = NameBox.Text;
            SendButton.IsEnabled = false;
            NameBox.IsEnabled = false;
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
                NameBox.IsEnabled = true;
                LoadingStatus(false);
            }

        }

        void SendInviteFriendPacket(string frinedName)
        {
            for (int i = 0; i < 1; i++)
            {
                Packets.Channel.ChannelInviteReq packet = new Packets.Channel.ChannelInviteReq();

                packet.senderID = LauncherApp.Game_Data.Globals.uid;
                packet.chatID = this.chatID;
                packet.inviteNickName = frinedName;
                packet.channelName = this.channelName;
                
                App.connection.Send(packet, "ChannelInviteReq");
            }
        }


        internal void OnLoginResult(object result)
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
                NameBox.IsEnabled = true;
                LoadingStatus(false);
            });

        }

        private void tCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void tMinieBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

    }
}
