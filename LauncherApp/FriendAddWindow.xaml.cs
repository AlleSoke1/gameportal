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
    /// Interaction logic for FriendsWindow.xaml
    /// </summary>
    public partial class FriendAddWindow : Window
    {
        public FriendAddWindow()
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

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (SearchBox.Text != "" && !SearchBox.Text.Contains(" "))
                {
                    SendInvButton.IsEnabled = true;
                }
                else
                {
                    SendInvButton.IsEnabled = false;
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

        private void SendInvButton_Click(object sender, RoutedEventArgs e)
        {
            string invtedName = SearchBox.Text;
            SendInvButton.IsEnabled = false;
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
                SendInvButton.IsEnabled = true;
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


        internal void OnLoginResult(object result)
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
                SendInvButton.IsEnabled = true;
                SearchBox.IsEnabled = true;
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
