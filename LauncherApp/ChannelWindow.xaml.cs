using LauncherApp.Styles.Controls;
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
    public partial class ChannelWindow : Window
    {
        public string channelName;
        public long chatID;

        private int messagePageCounter = 1;

        public ChannelWindow()
        {
            InitializeComponent();

            LoadStyles();

        }

        #region UI Functions

        private void LoadStyles()
        {

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChannelNameLibl.Content = this.channelName;
            ChannelIdLibl.Content = "#" + this.chatID;
            this.Title = this.channelName;

            if (MessagesList.Children.Count <= 1)
            {
                SendLoadUsersPacket();
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void tCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void tMinieBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MicOnBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddFriendBtn_Click(object sender, RoutedEventArgs e)
        {
            LauncherFactory.getChannelInviteClass().chatID = this.chatID;
            LauncherFactory.getChannelInviteClass().channelName = this.channelName;
            LauncherFactory.getChannelInviteClass().Show();
            LauncherFactory.getChannelInviteClass().Topmost = true;
            LauncherFactory.getChannelInviteClass().Focus();
            LauncherFactory.getChannelInviteClass().Topmost = false;
        }

        private void EmojiIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image senderObj = (Image)sender;
            string emojiCode = " " + senderObj.Tag.ToString() + " ";

            for (int x = 0; x < emojiCode.Length; x++)
            {
                if (TypeBox.Text.Length < TypeBox.MaxLength)
                {
                    TypeBox.AppendText(emojiCode.ToArray()[x].ToString());
                    emojiBox.Visibility = Visibility.Hidden;
                }
            }
        }

        private void TypeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MessageLetterCounter.Content = string.Format("{0}/{1}", TypeBox.Text.Length, TypeBox.MaxLength.ToString());
        }

        private void emojiBtn_Click(object sender, RoutedEventArgs e)
        {
            if (emojiBox.Visibility == Visibility.Visible) { emojiBox.Visibility = Visibility.Hidden; return; }
            emojiBox.Visibility = Visibility.Visible;
        }


        private void EmojiImage_MouseEnter(object sender, MouseEventArgs e)
        {
            Image senderObj = (Image)sender;
            senderObj.Opacity = 1;
        }

        private void EmojiImage_MouseLeave(object sender, MouseEventArgs e)
        {
            Image senderObj = (Image)sender;
            senderObj.Opacity = 0.70;
        }


        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            Window SettingWnd = LauncherFactory.getSettingsClass();
            SettingWnd.Show();
            //show chat settings code
            // SettingWnd.ChatWindow.Visibility = Visibility.Visible;
        }

        private void showMessageWnd(bool sStatus, FontAwesome.WPF.FontAwesomeIcon sIcon, string sMessage, bool sSpin = false)
        {
            if (sStatus == true)
            {
                MessageWnd.Visibility = Visibility.Visible;
                MessageIcon.Icon = sIcon;
                MessageIcon.Spin = sSpin;
                MessageText.Content = sMessage;

                TypeBox.IsEnabled = false;
                ActionsPanel.IsEnabled = false;

                return;

            }

            MessageWnd.Visibility = Visibility.Hidden;
            TypeBox.IsEnabled = true;
            ActionsPanel.IsEnabled = true;
        }
        private void showError(string text, LauncherApp.Styles.Controls.FastMessage.MessageTypes type)
        {
            ErrorMessage.Message = text;
            ErrorMessage.ShowTime = 3;
            ErrorMessage.Type = type;
            ErrorMessage.Show();
        }

        #endregion



        #region Load Useres packet

        private void SendLoadUsersPacket()
        {
            TypeBox.IsEnabled = false;

            for (int i = 0; i < 1; i++)
            {
                App.connection.Send((object)this.chatID, "ChannelUseresListReq");
            }
        }


        internal void OnUsersListResult(object result)
        {
            Dispatcher.Invoke(() => { UserList.Children.Clear(); });

            Packets.Channel.ChannelUsersListRep data = (Packets.Channel.ChannelUsersListRep)result;

            foreach (var uInfo in data.ListData)
            {
                Dispatcher.Invoke(() =>
                {
                    UserList.Children.Add(new ChannelUserItem()
                    {
                        UserName = uInfo.nickName,
                        UserID = uInfo.userID,
                        IsOnline = true
                    });
                });
            }

            Dispatcher.Invoke(() =>
                {
                    SendLoadMessagePacket();
                });
        }


        #endregion

        #region LoadMessages packet

        private void showMoreBtn_Click(object sender, RoutedEventArgs e)
        {
            SendLoadMessagePacket();
        }

        private void SendLoadMessagePacket()
        {
            TypeBox.IsEnabled = false;

            for (int i = 0; i < 1; i++)
            {
                Packets.Chat.ChatMessageListReq packet = new Packets.Chat.ChatMessageListReq();

                packet.userID1 = LauncherApp.Game_Data.Globals.uid;
                packet.userID2 = 0;
                packet.chatID = this.chatID;
                packet.pageNum = messagePageCounter;

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
                    showMoreBtn.Height = Double.NaN;
                }
                else
                {
                    showMoreBtn.Height = 0;
                }

                foreach (Packets.Chat.ChatMessageReq msgInfo in data.ListData)
                {
                    AddMessageHandler(msgInfo);

                }

                if (messagePageCounter > 1) { SortMessagesByDate(); }

                showMessageWnd(false, FontAwesome.WPF.FontAwesomeIcon.None, "");
                messagePageCounter++;
            });


        }

        private void SortMessagesByDate()
        {
            List<ChatMessageItem> tempList = new List<ChatMessageItem>();

            foreach (var cmItem in MessagesList.Children)
            {
                if (cmItem is ChatMessageItem)
                {
                    tempList.Add((ChatMessageItem)cmItem);
                }

            }

            var sorted = tempList.OrderByDescending(x => x.SendDate);
            int xCounter = 1;
            MessagesList.Children.Clear();

            foreach (ChatMessageItem cmItem in sorted)
            {
                MessagesList.Children.Add(cmItem);
            }


        }

        private void AddMessageHandler(Packets.Chat.ChatMessageReq msgInfo = null, Packets.Chat.ChatMessageRep msgRepInfo = null)
        {
            string ownerName = msgInfo == null ? msgRepInfo.OwnerName : msgInfo.OwnerName;
            string Message = msgInfo == null ? msgRepInfo.Message : msgInfo.Message;
            string SendDate = msgInfo == null ? msgRepInfo.SendDate.ToString() : msgInfo.SendDate.ToString();

            if (MessagesList.Children.Count > 1)
            {
                var lastMessage = (ChatMessageItem)MessagesList.Children[(MessagesList.Children.Count - 1)];
                if (lastMessage.OwnerName.ToLower() == ownerName.ToLower())
                {
                    lastMessage.AddMoreMessage(Message, SendDate);
                    TypeBox.IsEnabled = true;
                    MessagesScroll.ScrollToEnd();
                    return;
                }
            }

            MessagesList.Children.Add(new ChatMessageItem()
            {
                Message = Message,
                OwnerName = ownerName,
                SendDate = SendDate
            });

            MessagesScroll.ScrollToEnd();

        }
        #endregion

        #region sendMessagePacket


        private void TypeBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter && TypeBox.Text.Length > 0)
            {
                SendChatMessagePacket();

                //MessagesList.Children.Add(new ChatMessageItem() { OwnerName = "Hossam", Message = TypeBox.Text, SendDate = DateTime.Now.ToString() });
                TypeBox.Clear();
            }
        }

        private void SendChatMessagePacket()
        {
            for (int i = 0; i < 1; i++)
            {
                Packets.Chat.ChatMessageReq packet = new Packets.Chat.ChatMessageReq();

                packet.senderUserID = LauncherApp.Game_Data.Globals.uid;
                packet.reciverUserID = 0;
                packet.chatID = this.chatID;
                packet.OwnerName = LauncherApp.Game_Data.Globals.nickname;
                packet.Message = TypeBox.Text.ToString();
                packet.SendDate = DateTime.Now;
                packet.isChannel = true;
                packet.channelName = this.channelName;

                App.connection.Send(packet, "CS_SendMessageReq");
            }

        }


        internal void OnSendingResult(object result)
        {
            Packets.Chat.ChatMessageRep resultObj = (Packets.Chat.ChatMessageRep)result;

            if (resultObj.sendingStatus == Enums.SqlResultReply.Success)
            {
                Dispatcher.Invoke(() =>
                {
                    AddMessageHandler(null, resultObj);
                });

            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    showError("Something Wrong Happend, Please Call Support.", FastMessage.MessageTypes.Warning);
                });

            }

        }
        #endregion


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
                        UserList.Children.Add(new ChannelUserItem()
                        {
                            UserID = uInfo.userID,
                            UserName = uInfo.Nickname
                        });

                        notifyMessage = string.Format("{0} has joined this channel", uInfo.Nickname);
                        AddMessageHandler(null, new Packets.Chat.ChatMessageRep() { OwnerName = "", Message = notifyMessage , SendDate = notifyTime});

                    });

                    break;

                case Enums.Channel.ChannelUserNotify.Leave:

                    Dispatcher.Invoke(() =>
                    {
                        for (int index = UserList.Children.Count - 1; index >= 0; index--)
                        {

                            if (UserList.Children[index] is ChannelUserItem)
                            {

                                if (((ChannelUserItem)UserList.Children[index]).UserID == uInfo.userID)
                                {
                                    UserList.Children.RemoveAt(index);
                                }

                            }

                        }

                        notifyMessage = string.Format("{0} has left this channel", uInfo.Nickname);
                        AddMessageHandler(null, new Packets.Chat.ChatMessageRep() { OwnerName = "", Message = notifyMessage, SendDate = notifyTime });

                    });

                    break;
            }



        }
    }
}
