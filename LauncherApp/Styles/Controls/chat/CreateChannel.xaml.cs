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
    /// Interaction logic for CreateChannel.xaml
    /// </summary>
    public partial class CreateChannel : UserControl
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
                  typeof(CreateChannel)
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
                  typeof(CreateChannel)
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
                  typeof(CreateChannel)
              );

        #endregion



        //invite settings
        private int inviteLimit = 8;

        public CreateChannel()
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
            string channelName = SearchBox.Text;
            SendButton.IsEnabled = false;
            SearchBox.IsEnabled = false;
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
                SendButton.IsEnabled = true;
                SearchBox.IsEnabled = true;
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
        internal void OnReciveResult(object result)
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
                    channelName = SearchBox.Text;

                    LauncherFactory.getAppClass().SocialPage.AddToChannelList(channelName, chatID);
                    LauncherFactory.getAppClass().SocialPage.SwitchChatWindow(chatID, channelName, 0, Enums.UserInfo.UserStatus.Available);

                    this._isOpen = false;
                    LauncherFactory.getAppClass().SocialPage.sbarChannelBtn.isActive = false;

                });

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
