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
    /// Interaction logic for ChannelCreate.xaml
    /// </summary>
    public partial class ChannelCreate : Window
    {
        public ChannelCreate()
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

        private void ChannelNamBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (ChannelNamBox.Text != "" && !ChannelNamBox.Text.Contains("Type Your Channel Name"))
                {
                    CreateChannelButton.IsEnabled = true;
                }
                else
                {
                    CreateChannelButton.IsEnabled = false;
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

        private void CreateChannelButton_Click(object sender, RoutedEventArgs e)
        {
            string channelName = ChannelNamBox.Text;
            CreateChannelButton.IsEnabled = false;
            ChannelNamBox.IsEnabled = false;
            LoadingStatus(true);
            bool ErrorStatus = false;

            if (channelName == "" || channelName == " ")
            {
                // user trying to invite him self
                showMessage("You Can't Leave Channel Name Input Empty.", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Info);
                ErrorStatus = true;

            }


            if (ErrorStatus == false)
            {
                SendCreateChannelPacket(channelName);
                return;
            }
            else
            {
                CreateChannelButton.IsEnabled = true;
                ChannelNamBox.IsEnabled = true;
                LoadingStatus(false);
            }

        }

        void SendCreateChannelPacket(string channelName)
        {
            for (int i = 0; i < 1; i++)
            {
                Packets.Channel.CreateChannelReq packet = new Packets.Channel.CreateChannelReq();

                packet.ownerID = LauncherApp.Game_Data.Globals.uid;
                packet.channelName = channelName;
                
                App.connection.Send(packet, "CreateChannelReq");
            }
        }


        internal void OnLoginResult(object result)
        {
            int chatID = (int)result;
            string channelName = "";

            if (chatID == 0)
            {
                Dispatcher.Invoke(() =>
                {
                    showMessage("Something Wrong Happened! For More Info Call Support.", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                });
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    channelName = ChannelNamBox.Text;

                    LauncherFactory.getFriendsClass().AddToChannelList(channelName, chatID);
                    App.ChatMan._openedChannels[chatID] = new ChannelWindow() {
                        channelName = channelName,
                        chatID = chatID
                    };
                    App.ChatMan.OpenChannelFromList(chatID);

                    this.Hide();

                });

            }

            

            Dispatcher.Invoke(() =>
            {
                CreateChannelButton.IsEnabled = true;
                ChannelNamBox.IsEnabled = true;
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
