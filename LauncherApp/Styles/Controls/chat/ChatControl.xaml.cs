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
using System.Net;
using System.Net.Sockets;

namespace LauncherApp.Styles.Controls
{
    /// <summary>
    /// Interaction logic for ChatControl.xaml
    /// </summary>
    public partial class ChatControl : UserControl
    {

        public string chatName;
        public long friendID;
        public long chatID;
        public Enums.UserInfo.UserStatus friendStatus;
        public bool isActive;
        private bool isOnCall;

        #region isChannel DP
        public bool isChannel
        {
            get { return (bool)GetValue(isChannelProperty); }
            set { SetValue(isChannelProperty, value); ActiveChannelMode(value); }
        }


        public static readonly DependencyProperty isChannelProperty
            = DependencyProperty.Register(
                  "isChannel",
                  typeof(bool),
                  typeof(ChatControl)
              );
        #endregion

        string CallIPHost = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].MapToIPv4().ToString();

        public ChatControl()
        {
            InitializeComponent();
        }

        internal void SwitchUserStatus(Enums.UserInfo.UserStatus userStatus)
        {
            friendStatus = userStatus;

            switch (userStatus)
            {
                case Enums.UserInfo.UserStatus.Disconnected:

                    UserStatusIcon.Foreground = Brushes.Silver;
                    VoipCallButton.IsEnabled = false;

                    break;

                case Enums.UserInfo.UserStatus.Available:

                    UserStatusIcon.Foreground = Brushes.Lime;
                    VoipCallButton.IsEnabled = true;

                    break;
                case Enums.UserInfo.UserStatus.Away:

                    UserStatusIcon.Foreground = Brushes.Orange;
                    VoipCallButton.IsEnabled = true;

                    break;
                case Enums.UserInfo.UserStatus.Busy:

                    UserStatusIcon.Foreground = Brushes.Red;
                    VoipCallButton.IsEnabled = true;

                    break;
            }
        }

        #region UI Functions

        private void ListElement_Loaded(object sender, RoutedEventArgs e)
        {
            //SwitchChatWindow("Rafeal", 2, 7, Enums.UserInfo.UserStatus.Available);

            this.inviteWinfow.Visibility = Visibility.Hidden;
            this.MessagesList.chatID = this.chatID;

            UsernameLabel.Content = this.chatName;
            SwitchUserStatus(this.friendStatus);

            if (!isChannel)
            {
                TopGrid.Children.Remove(inviteWinfow);
            }

        }

        public void showError(string text, LauncherApp.Styles.Controls.FastMessage.MessageTypes type)
        {
            ErrorMessage.Message = text;
            ErrorMessage.ShowTime = 3;
            ErrorMessage.Type = type;
            ErrorMessage.Show();
        }

        private void TypeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MessageLetterCounter.Content = string.Format("{0}/{1}", TypeBox.Text.Length, TypeBox.MaxLength.ToString());
        }

        private void TypeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && TypeBox.Text.Length > 0)
            {
                SendChatMessagePacket();
                //MessagesList.Children.Add(new ChatMessageItem() { OwnerName = "Hossam", Message = TypeBox.Text, SendDate = DateTime.Now.ToString() });
                TypeBox.Clear();
            }

            if (TypeBox.Text.Length == 0)
            {
                TypeBoxLabel.Visibility = Visibility.Visible;
            }
            else
            {
                TypeBoxLabel.Visibility = Visibility.Hidden;
            }
        }

        private void showMessageWnd(bool sStatus, MahApps.Metro.IconPacks.PackIconMaterialKind sIcon, string sMessage, bool sSpin = false)
        {
            if (sStatus == true)
            {
                MessageWnd.Visibility = Visibility.Visible;
                MessageIcon.Kind = sIcon;
                MessageIcon.Spin = sSpin;
                MessageText.Content = sMessage;

                TypeBox.IsEnabled = false;
                return;
            }

            MessageWnd.Visibility = Visibility.Hidden;
            TypeBox.IsEnabled = true;
        }

        

        private void EmojiButton_Click(object sender, RoutedEventArgs e)
        {
            if(chatEmojiBox.Visibility == System.Windows.Visibility.Hidden){
                chatEmojiBox.Visibility = System.Windows.Visibility.Visible;
                return;
            }
            chatEmojiBox.Visibility = System.Windows.Visibility.Hidden;
        }


        private void ActiveChannelMode(bool flag)
        {
            if (flag)
            {
                UserStatusIcon.Width = 0;
                MemeberListBtn.Visibility = Visibility.Visible;
                AddFriendBtn.Visibility = Visibility.Visible;
                MemebersList.Visibility = Visibility.Visible;
            }
        }


        #region Channel UI Functions

            private void AddFriendBtn_Click(object sender, RoutedEventArgs e)
            {
                this.inviteWinfow._isOpen = !this.inviteWinfow._isOpen;
            }


            private void SettingsBtn_Click(object sender, RoutedEventArgs e)
            {
                LauncherFactory.getAppClass().SettingsPage.SwitchMenuItems("settings_FriendsChat");
                LauncherFactory.getAppClass().SwitchToPage("Settings");
            }


            private void MemeberListBtn_MouseDown(object sender, MouseButtonEventArgs e)
            {
                MemebersList.isOpen = !MemebersList.isOpen;
                
                if(MemebersList.isOpen){
                    LauncherFactory.ElementAnimation(MessagesList, ChatMessageList.MarginProperty, new Thickness(0, 0, 0, 0), new Thickness(0, 0, 240, 0), 0.1, true);
                }else{
                    LauncherFactory.ElementAnimation(MessagesList, ChatMessageList.MarginProperty, new Thickness(0, 0, 240, 0), new Thickness(0, 0, 0, 0), 0.1, true);
                }


                    
            }

            public void UpdateMemebersCount()
            {
                MemeberListBtn.Content = string.Format("{0} " + (string)App.Current.Resources["ChatParticipantsLabel"], MemebersList.GetCount());
            }
        #endregion

        #endregion

        #region Load Useres packet

            public void SendLoadUsersPacket()
            {
                TypeBox.IsEnabled = false;

                for (int i = 0; i < 1; i++)
                {
                    App.connection.Send((object)this.chatID, "ChannelUseresListReq");
                }
            }


            internal void OnUsersListResult(object result)
            {
                Dispatcher.Invoke(() => { MemebersList.Clear(); });

                Packets.Channel.ChannelUsersListRep data = (Packets.Channel.ChannelUsersListRep)result;

                foreach (var uInfo in data.ListData)
                {
                    Dispatcher.Invoke(() =>
                    {
                        MemebersList.Add(uInfo.nickName , uInfo.userID, this);
                    });
                }

                Dispatcher.Invoke(() =>
                {
                    inviteWinfow._chatID = this.chatID;
                    inviteWinfow.channelName = this.chatName;
                    inviteWinfow._memeberCount = data.ListData.Count;

                    SendLoadMessagePacket();
                });
            }


            internal void OnUserNotifyResult(object data)
            {
                Packets.Channel.ChannelOptionsNotify uInfo = (Packets.Channel.ChannelOptionsNotify)data;
                string notifyMessage = "";
                DateTime notifyTime = DateTime.Now;

                switch (uInfo.status)
                {
                    case Enums.Channel.ChannelUserNotify.Join:

                        Dispatcher.Invoke(() =>
                        {
                            MemebersList.Add(uInfo.Nickname,uInfo.userID, this);

                            notifyMessage = string.Format((string)App.Current.Resources["ChatJoinNotify"], uInfo.Nickname);
                            this.MessagesList.AddMessageHandler(null, new Packets.Chat.ChatMessageRep() { OwnerName = "", Message = notifyMessage, SendDate = notifyTime, isChannel = this.isChannel });

                        });

                        break;

                    case Enums.Channel.ChannelUserNotify.Leave:

                        Dispatcher.Invoke(() =>
                        {
                            MemebersList.Remove(uInfo.userID, this);

                            notifyMessage = string.Format((string)App.Current.Resources["ChatLeaveNotify"], uInfo.Nickname);
                            this.MessagesList.AddMessageHandler(null, new Packets.Chat.ChatMessageRep() { OwnerName = "", Message = notifyMessage, SendDate = notifyTime, isChannel = this.isChannel });

                        });

                        break;
                }

                Dispatcher.Invoke(() =>
                        {
                            inviteWinfow._memeberCount = MemebersList.GetCount();
                        });
            }

            #endregion

        #region Load Message Functions

        public void SendLoadMessagePacket()
        {
            TypeBox.IsEnabled = false;

            showMessageWnd(true, MahApps.Metro.IconPacks.PackIconMaterialKind.VectorCircle, (string)App.Current.Resources["LoadingStr"], true);

            for (int i = 0; i < 1; i++)
            {
                Packets.Chat.ChatMessageListReq packet = new Packets.Chat.ChatMessageListReq();

                packet.userID1 = LauncherApp.Game_Data.Globals.uid;
                packet.userID2 = friendID;
                packet.chatID = chatID;
                packet.pageNum = this.MessagesList.messagePageCounter;

                App.connection.Send(packet, "CS_GetMessageListReq");
            }
        }

        internal void OnMessageListResult(object result)
        {
            Packets.Chat.ChatMessagesList data = (Packets.Chat.ChatMessagesList)result;

            Dispatcher.Invoke(() =>
            {
                if (data.ListData.Count == 10)
                {
                    this.MessagesList.ShowMoreButton(true);
                }
                else
                {
                    this.MessagesList.ShowMoreButton(false);
                }

                foreach (Packets.Chat.ChatMessageReq msgInfo in data.ListData.ToList().OrderBy(x => x.SendDate))
                {
                    msgInfo.isChannel = this.isChannel;
                    this.MessagesList.AddMessageHandler(msgInfo);
                }

                if (this.MessagesList.messagePageCounter > 1) { this.MessagesList.SortMessagesByDate(); }

                showMessageWnd(false, MahApps.Metro.IconPacks.PackIconMaterialKind.None, "");
                this.MessagesList.messagePageCounter++;

                

            });

        }

        #endregion

        #region Send Message Functiosn

        private void SendChatMessagePacket()
        {
            for (int i = 0; i < 1; i++)
            {

                Packets.Chat.ChatMessageReq packet = new Packets.Chat.ChatMessageReq();

                packet.senderUserID = LauncherApp.Game_Data.Globals.uid;
                packet.reciverUserID = this.isChannel == true ? 0 : this.friendID;
                packet.chatID = this.chatID;
                packet.OwnerName = LauncherApp.Game_Data.Globals.nickname;
                packet.Message = TypeBox.Text.ToString();
                packet.SendDate = DateTime.Now;
                packet.isChannel = this.isChannel;
                packet.channelName = this.chatName;


                App.connection.Send(packet, "CS_SendMessageReq");

            }

        }



        #endregion

        #region CallSystem

        private void VoipCallButton_Click(object sender, RoutedEventArgs e)
        {
            if (!LauncherApp.Game_Data.Globals.CallCenterIsBusy)
            {
                this.MessagesList.CallUserList.Children.Clear();

                this.MessagesList.CallUserList.Children.Add(new CallUserIcon() { MemeberName = LauncherApp.Game_Data.Globals.nickname, Icon = FontAwesome.WPF.FontAwesomeIcon.Microphone });
                this.MessagesList.CallUserList.Children.Add(new CallUserIcon() { MemeberName = this.chatName, Icon = FontAwesome.WPF.FontAwesomeIcon.Phone });

                // for force app to make one call.
                LauncherApp.Game_Data.Globals.CallCenterIsBusy = true;
                // 

                //VoipCallButton.Visibility = Visibility.Hidden;
                this.MessagesList.showCallWindow(true);

                this.MessagesList.IncomingCallActions.Visibility = Visibility.Hidden;
                this.MessagesList.vCallActions.Visibility = Visibility.Visible;
                    

                isOnCall = true;

                SendCallRequsetPacket();

            }
            else
            {
                showError((string)App.Current.Resources["ChatErrorCallBusy"], FastMessage.MessageTypes.Warning);
            }

        }


        private void SendCallRequsetPacket()
        {
            for (int i = 0; i < 1; i++)
            {
                Packets.Chat.VoiceChatRequest packet = new Packets.Chat.VoiceChatRequest();

                packet.senderUserID = LauncherApp.Game_Data.Globals.uid;
                packet.reciverUserID = this.friendID;
                packet.chatID = this.chatID;
                packet.CallIpHost = this.CallIPHost;

                App.connection.Send(packet, "VoceChatReq");
            }
        }


        public void SendCallReplyPacket(object sender)
        {
            Button senderObj = (Button)sender;

            for (int i = 0; i < 1; i++)
            {
                Packets.Chat.VoiceChatRequestReply packet = new Packets.Chat.VoiceChatRequestReply();

                packet.senderUserID = LauncherApp.Game_Data.Globals.uid;
                packet.reciverUserID = this.friendID;
                packet.chatID = this.chatID;
                packet.requestReply = senderObj.Name == "acceptCallBtn" ? Enums.Voip.CallResultReply.Success : Enums.Voip.CallResultReply.Failed;

                App.connection.Send(packet, "VoceChatReqReply");
            }


        }
        



        #endregion


    }
}
