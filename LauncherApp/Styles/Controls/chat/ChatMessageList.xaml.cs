using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for FriendList.xaml
    /// </summary>
    public partial class ChatMessageList : UserControl
    {

        public int messagePageCounter = 1;

        public long chatID;
       
        public ChatMessageList()
        {
            InitializeComponent();

        }


        internal int GetMessagesCount()
        {
            return MessagesList.Children.Count;
        }

        internal bool UpdateCallUserStatus(string uName, FontAwesome.WPF.FontAwesomeIcon icon)
        {
            foreach (CallUserIcon uIcon in this.CallUserList.Children)
            {
                if (uIcon.MemeberName.ToLower() == uName.ToLower())
                {
                    uIcon.Icon = icon;
                    return true;
                }
            }

            return false; 
        }


        internal void ShowMoreButton(bool flag)
        {
            if (flag)
            {
                showMoreBtn.Height = Double.NaN;
                return;
            }

            showMoreBtn.Height = 0;
        }

        public void AddMessageHandler(Packets.Chat.ChatMessageReq msgInfo = null, Packets.Chat.ChatMessageRep msgRepInfo = null)
        {
            string ownerName = msgInfo == null ? msgRepInfo.OwnerName : msgInfo.OwnerName;
            string Message = msgInfo == null ? msgRepInfo.Message : msgInfo.Message;
            DateTime SendDate = msgInfo == null ? msgRepInfo.SendDate : msgInfo.SendDate;
            bool Channelflag = msgInfo == null ? msgRepInfo.isChannel : msgInfo.isChannel;

            ChatMessageItem tempMessageItem = new ChatMessageItem()
            {
                Message = Message,
                OwnerName = ownerName,
                SendDate = SendDate
            };
            MessagesList.Children.Add(tempMessageItem);
            
            MessagesScroll.ScrollToEnd();


        }


        public void SortMessagesByDate()
        {
            List<dynamic> tempList = new List<dynamic>();

            foreach (var cmItem in MessagesList.Children)
            {
                if (cmItem is ChatMessageItem)
                {
                    tempList.Add((dynamic)cmItem);
                }

            }

            var sorted = tempList.ToList().OrderBy(x => x.SendDate);
            MessagesList.Children.Clear();

            foreach (object cmItem in sorted)
            {
                MessagesList.Children.Add((ChatMessageItem)cmItem);
            }


        }

        private void showMoreBtn_Click(object sender, RoutedEventArgs e)
        {
            LauncherFactory.getAppClass().SocialPage._chatControl.SendLoadMessagePacket();
        }

        internal void OnSendingResult(object result)
        {
            Packets.Chat.ChatMessageRep resultObj = (Packets.Chat.ChatMessageRep)result;

            if (resultObj.sendingStatus == Enums.SqlResultReply.Success)
            {
                Dispatcher.Invoke(() =>
                {
                    AddMessageHandler(null, resultObj);

                    if (App.ChatMan._openedChatList[this.chatID].isActive)
                    {
                        if (resultObj.OwnerName.ToLower() != LauncherApp.Game_Data.Globals.nickname.ToLower())
                        {
                            App.SoundMan.PlaySound(LauncherApp.Logic.SoundsType.ChatMessage);
                        }
                    }
                });

            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    LauncherFactory.getAppClass().SocialPage._chatControl.showError((string)App.Current.Resources["strCallSupport"], FastMessage.MessageTypes.Warning);
                });

            }

        }

        internal void ClearMessages()
        {
            MessagesList.Children.Clear();
        }

        public void showCallWindow(bool flag)
        {
            if (flag)
            {
                if (vCallWindow.ActualHeight == 0)
                {
                    LauncherFactory.ElementAnimation(MessagesScroll, ScrollViewer.MarginProperty, new Thickness(0, 0, 0, 0), new Thickness(0, 150, 0, 0), 0.1, true);
                    LauncherFactory.ElementAnimation(vCallWindow, UserControl.HeightProperty, 0, 150, 0.1, false);
                    App.ChatMan._openedChatList[this.chatID].VoipCallButton.Visibility = System.Windows.Visibility.Hidden;
                }
                
            }

            else
            {
                if (vCallWindow.ActualHeight != 0)
                {
                    LauncherFactory.ElementAnimation(MessagesScroll, ScrollViewer.MarginProperty, new Thickness(0, 150, 0, 0), new Thickness(0, 0, 0, 0), 0.1, true);
                    LauncherFactory.ElementAnimation(vCallWindow, UserControl.HeightProperty, 150, 0, 0.1, false);
                    App.ChatMan._openedChatList[this.chatID].VoipCallButton.Visibility = System.Windows.Visibility.Visible;
                }
                
            }

            //Console.WriteLine(string.Format("{0}  /n", MessagesScroll.Margin.ToString()));

                
        }

        internal void onCallRequsetResult(Packets.Chat.VoiceChatRequestResult resultPacket)
        {
            if (resultPacket.Result == Enums.Voip.CallResultReply.Success)
            {
               

                return;
            }

            if (!LauncherApp.Game_Data.Globals.CallCenterIsBusy)
            {
                string tempMSG = "";

                switch (resultPacket.Result)
                {

                    case Enums.Voip.CallResultReply.Failed:
                        tempMSG = (string)App.Current.Resources["CallErrorFail"];
                        break;

                    case Enums.Voip.CallResultReply.FriendOffline:
                        tempMSG = (string)App.Current.Resources["CallErrorOffline"];
                        break;

                    case Enums.Voip.CallResultReply.FriendOnCall:
                        tempMSG = (string)App.Current.Resources["CallErrorUserBusy"];
                        break;
                }


                // for force app to make one call.
                LauncherApp.Game_Data.Globals.CallCenterIsBusy = false;
                // 

                Dispatcher.Invoke(() =>
                {
                    LauncherFactory.getAppClass().SocialPage._chatControl.showError(tempMSG, FastMessage.MessageTypes.Warning);
                    LauncherFactory.getAppClass().SocialPage._chatControl.VoipCallButton.Visibility = Visibility.Visible;
                    showCallWindow(false);
                });


            }
        }

        internal void onIncomingCallRequset(object packetData)
        {
            // for force app to make one call.
            LauncherApp.Game_Data.Globals.CallCenterIsBusy = true;
            LauncherApp.Game_Data.Globals.CallCenterHostIp = ((Packets.Chat.VoiceChatRequestResult)packetData).CallHostIp;
            // 

            Dispatcher.Invoke(() =>
            {
                string tempFrindName = App.ChatMan._openedChatList[this.chatID].chatName;

                this.CallUserList.Children.Clear();
                this.CallUserList.Children.Add(new CallUserIcon() { Name = string.Format("User_{0}", tempFrindName), MemeberName = tempFrindName, Icon = FontAwesome.WPF.FontAwesomeIcon.Microphone });
                this.CallUserList.Children.Add(new CallUserIcon() { Name = string.Format("User_{0}", Game_Data.Globals.nickname), MemeberName = LauncherApp.Game_Data.Globals.nickname, Icon = FontAwesome.WPF.FontAwesomeIcon.Phone });

                vCallActions.Visibility = Visibility.Hidden;
                IncomingCallActions.Visibility = Visibility.Visible;
                App.ChatMan._openedChatList[this.chatID].VoipCallButton.Visibility = Visibility.Hidden;
                showCallWindow(true);
            });
        }

        internal void onIncomingCallRequsetReply(object packetData)
        {
            Packets.Chat.VoiceChatRequestResult pData = (Packets.Chat.VoiceChatRequestResult)packetData;

            if (pData.Result == Enums.Voip.CallResultReply.Success)
            {
                // for force app to make one call.
                LauncherApp.Game_Data.Globals.CallCenterIsBusy = true;
                LauncherApp.Game_Data.Globals.CallCenterChannelID = pData.voipCID;
                //


                Dispatcher.Invoke(() =>
                {
                    showCallWindow(true);

                    vCallActions.Visibility = Visibility.Visible;
                    IncomingCallActions.Visibility = Visibility.Hidden;

                    UpdateCallUserStatus(pData.replyUserName, FontAwesome.WPF.FontAwesomeIcon.Microphone);

                });



            }
            else
            {
                // for force app to make one call.
                LauncherApp.Game_Data.Globals.CallCenterIsBusy = false;
                LauncherApp.Game_Data.Globals.CallCenterChannelID = 0;
                //

                Dispatcher.Invoke(() =>
                {
                    showCallWindow(false);
                    App.ChatMan._openedChatList[this.chatID].VoipCallButton.Visibility = Visibility.Visible;
                });

            }

        }

        private void ActionCallBtn_Click(object sender, RoutedEventArgs e)
        {
            LauncherFactory.getAppClass().SocialPage._chatControl.SendCallReplyPacket(sender);

            switch (((Button)sender).Name)
            {
                case "acceptCallBtn":


                    IncomingCallActions.Visibility = Visibility.Hidden;
                    vCallActions.Visibility = Visibility.Visible;
                    UpdateCallUserStatus(LauncherApp.Game_Data.Globals.nickname, FontAwesome.WPF.FontAwesomeIcon.Microphone);


                    break;

                case "declineCallBtn":
                    showCallWindow(false);
                    break;
                    
            }

        }

        private void LeaveCallBtn_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 1; i++)
            {
                Packets.Chat.VoiceChatLeaveCall packet = new Packets.Chat.VoiceChatLeaveCall();

                packet.userID = Game_Data.Globals.uid;
                packet.chatID = this.chatID;
                packet.voipChannelID = Game_Data.Globals.CallCenterChannelID;

                App.connection.Send(packet, "VoiceChatLeaveCallReq");
            }
        }


        internal void onLeaveCallNotfiy(object data)
        {
            Packets.Chat.VoiceChatLeaveCall dataObj = (Packets.Chat.VoiceChatLeaveCall)data;
            string notifyMessage = "";


            notifyMessage = dataObj.userID != Game_Data.Globals.uid ? string.Format((string)App.Current.Resources["ChatMessgeCallLeft"], dataObj.userNickname) : (string)App.Current.Resources["ChatMessgeCalEnd"];

            if (dataObj.endCallFlag)
            {

                // for force app to make one call.
                LauncherApp.Game_Data.Globals.CallCenterIsBusy = false;
                LauncherApp.Game_Data.Globals.CallCenterChannelID = 0;
                //


                Dispatcher.Invoke(() =>
                {
                    showCallWindow(false);
                    App.ChatMan._openedChatList[this.chatID].VoipCallButton.Visibility = Visibility.Visible;

                    this.AddMessageHandler(null, new Packets.Chat.ChatMessageRep() { OwnerName = "", Message = notifyMessage, SendDate = DateTime.Now, isChannel = false });
                });


                return;
            }

            if (dataObj.userID == Game_Data.Globals.uid)
            {

                // for force app to make one call.
                LauncherApp.Game_Data.Globals.CallCenterIsBusy = false;
                LauncherApp.Game_Data.Globals.CallCenterChannelID = 0;
                //


                Dispatcher.Invoke(() =>
                {
                    showCallWindow(false);
                    App.ChatMan._openedChatList[this.chatID].VoipCallButton.Visibility = Visibility.Visible;

                });
            }

            Dispatcher.Invoke(() =>
            {
                this.AddMessageHandler(null, new Packets.Chat.ChatMessageRep() { OwnerName = "", Message = notifyMessage, SendDate = DateTime.Now, isChannel = false });
            });

        }

    }
}
