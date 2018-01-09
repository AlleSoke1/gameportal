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
using System.Diagnostics;
using System.Threading;
using LauncherApp.Styles.Controls;

namespace LauncherApp.Styles.Pages
{
    /// <summary>
    /// Interaction logic for Social.xaml
    /// </summary>
    public partial class Social : UserControl
    {

        private Packets.Channel.ChannelOptions selectedChannelObj = new Packets.Channel.ChannelOptions();
        private bool chatControlStats = true;
        public ChatControl _chatControl = null;
        public UIElement _currectPage = null;
        
        public int friendRequestCount
        {
            get { return _friendRequestCount; }
            set { _friendRequestCount = value; UpdateRequestCounter(value); }
        }
        public int _friendRequestCount;

        public Social()
        {
            InitializeComponent();
            LoadStyles();
        }
        private void LoadStyles()
        {
            _currectPage = wndWelcomeMessage;
        }

        #region UI FUNCTIONS

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FriendsList.FilterUseres(SearchBox.Text);
            ChannelsList.FilterChannels(SearchBox.Text);
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text == "" || SearchBox.Text == null)
                SearchBoxLabel.Visibility = Visibility.Visible;

        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchBoxLabel.Visibility = Visibility.Hidden;
        }



        private void PageElement_Loaded(object sender, RoutedEventArgs e)
        {

        }


        private void UpdateRequestCounter(int value)
        {
            notifyCounter.Content = value;
        }


        public void SwitchChatWindow(string wndName){

            string tragetName = wndName;

            if (((Grid)_currectPage).Name == tragetName) return;

            var pervPage = _currectPage;
            UIElement nextPage = (UIElement)MainBody.FindName(tragetName);

            // switch pages
            Panel.SetZIndex(pervPage, 0);
            Panel.SetZIndex(nextPage, 2);
            nextPage.Opacity = 0;
            nextPage.Visibility = Visibility.Visible;

            Action tempAction = new Action(() =>
                   {
                       pervPage.Visibility = Visibility.Hidden;

                   });
            LauncherFactory.ElementAnimation(nextPage, UIElement.OpacityProperty, 0, 1, 0.2, false);
            LauncherFactory.ElementAnimation(pervPage, UIElement.OpacityProperty, 1, 0, 0.2, false, tempAction);


            _currectPage = nextPage;

            if (wndName != "wndFriendRequests") sbarNotifyBtn.isActive = false;
        }


        private void sbarNotifyBtn_Click(object sender, RoutedEventArgs e)
        {
            sbarNotifyBtn.isActive = true;
            SwitchChatWindow("wndFriendRequests");
        }

        public void SwitchChatWindow(long ChatID, string friendNickName = "none", long fID = 0, Enums.UserInfo.UserStatus fStatus = Enums.UserInfo.UserStatus.Available)
        {
            if (this._chatControl != null)
                this._chatControl.Visibility = System.Windows.Visibility.Hidden;

            if (App.ChatMan._openedChatList.ContainsKey(ChatID))
            {
                App.ChatMan._openedChatList[ChatID].Visibility = Visibility.Visible;
                this._chatControl = App.ChatMan._openedChatList[ChatID];
            }
            else
            {

                ChatControl tempChatControl = new ChatControl()
                {
                    Name = string.Format("Chat_{0}", ChatID),
                    isActive = true,
                    chatName = friendNickName,
                    friendID = fID,
                    chatID = ChatID,
                    friendStatus = fStatus,
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch,
                    VerticalAlignment = System.Windows.VerticalAlignment.Stretch,
                    Width = Double.NaN,
                    Height = Double.NaN

                };
                tempChatControl.isChannel = fID == 0 ? true : false;

                App.ChatMan._openedChatList.Add(ChatID, tempChatControl);
                wndChatControl.Children.Add(tempChatControl);
                this._chatControl = tempChatControl;

                if (tempChatControl.MessagesList.GetMessagesCount() <= 1)
                {
                    if (fID == 0)
                    {
                        tempChatControl.SendLoadUsersPacket();
                    }
                    else
                    {
                        tempChatControl.SendLoadMessagePacket();
                    }

                }

            }

            if (wndChatControl.Visibility == Visibility.Hidden) this.SwitchChatWindow("wndChatControl");



        }

        private void sbarChannelBtn_Click(object sender, RoutedEventArgs e)
        {
            CreateChannelWindow._isOpen = !CreateChannelWindow._isOpen;
            sbarChannelBtn.isActive = !sbarChannelBtn.isActive;

            if (AddFriendWindow._isOpen || sbarFriendBtn.isActive)
            {
                AddFriendWindow._isOpen = false;
                sbarFriendBtn.isActive = false;
            }

        }

        private void sbarFriendBtn_Click(object sender, RoutedEventArgs e)
        {
            AddFriendWindow._isOpen = !AddFriendWindow._isOpen;
            sbarFriendBtn.isActive = !sbarFriendBtn.isActive;

            if (CreateChannelWindow._isOpen || sbarChannelBtn.isActive)
            {
                CreateChannelWindow._isOpen = false;
                sbarChannelBtn.isActive = false;
            }
        }

        #endregion


        #region Requests Packet

        private void onRequestOptionClick(object sender, RoutedEventArgs e)
        {
            FriendRequests.IsEnabled = false;
            FriendRequestListitem senderObj = (FriendRequestListitem)sender;

            SendRequestOptionPacket(senderObj.Action, senderObj.UserID);

            this.SendRequestOptionPacket(senderObj.Action, senderObj.UserID);
        }

        internal void OnRequestOptionResult(object result)
        {
            var resultObj = (Packets.FriendsRequest.SCAddFriendRep)result;
            switch (resultObj.status)
            {
                case Enums.SqlResultReply.Success:

                    Dispatcher.Invoke(() =>
                    {
                        FriendRequestListitem tempItem = FriendRequests.RemoveByUserID(resultObj.friendID);

                        if (resultObj.selectedOption == Enums.Friend.FriendRequestOptions.Approve)
                        {
                            FriendsList.AddItem(new FriendListItem()
                            {
                                Status = Enums.UserInfo.UserStatus.Available,
                                FriendName = tempItem.UesrName,
                                UserID = tempItem.UserID,
                                ChatID = resultObj.chatID
                            });
                        }
                    });

                    break;
                case Enums.SqlResultReply.Failed:

                    Dispatcher.Invoke(() =>
                    {
                        //showMessage((string)App.Current.Resources["strCallSupport"], LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                    });

                    break;
            }

            Dispatcher.Invoke(() =>
            {
                FriendRequests.IsEnabled = true;
            });

        }

        public void SendRequestOptionPacket(Enums.Friend.FriendRequestOptions selectedOption, long requestOwnerID)
        {
            for (int i = 0; i < 1; i++)
            {
                Packets.FriendsRequest.CSFriendRequestOption packet = new Packets.FriendsRequest.CSFriendRequestOption();

                packet.recieverID = LauncherApp.Game_Data.Globals.uid;
                packet.senderID = requestOwnerID;
                packet.selectedOption = selectedOption;

                App.connection.Send(packet, "CS_FriendRequestOption");
            }
        }

        #endregion

        #region Friend List Packet

        public void SendFriendListPacket()
        {
            for (int i = 0; i < 1; i++)
            {
                App.connection.Send(LauncherApp.Game_Data.Globals.uid, "CS_FriendListReq");
            }
        }

        public void UpdateFriendList(Packets.Friends.SCFriendList fList)
        {
            Dispatcher.Invoke(() =>
            {
                //RequestList.ClearItems();
                FriendsList.ClearItems();
                ChannelsList.ClearItems();

                LauncherApp.Game_Data.Globals.OnlineFriendCount = 0;

            });

            foreach (Packets.Friends.SCFriendInfo fInfo in fList.ListData)
            {
                if (fInfo.isRequest == true)
                {
                    // add row as Reuqest
                    Dispatcher.Invoke(() =>
                    {
                        FriendRequests.AddItem(new FriendRequestListitem()
                        {
                            Name = "Request_" + fInfo.Id,
                            UserID = fInfo.Id,
                            UesrName = fInfo.NickName,
                        });
                        friendRequestCount++;
                    });

                }
                else if (fInfo.isChannel == true)
                {
                    // add row as Reuqest
                    Dispatcher.Invoke(() =>
                    {
                        AddToChannelList(fInfo.NickName, fInfo.Id);

                    });

                }
                else
                {
                    // add row as Friend
                    Dispatcher.Invoke(() =>
                    {

                        // if friend is currently online incress this value
                        if (fInfo.Status != Enums.UserInfo.UserStatus.Disconnected)
                            LauncherApp.Game_Data.Globals.OnlineFriendCount++;

                        var fItem = new FriendListItem()
                        {
                            Name = "Friend_" + fInfo.Id,
                            UserID = fInfo.Id,
                            FriendName = fInfo.NickName,
                            ChatID = fInfo.ChatId,
                            Status = fInfo.Status

                        };

                        FriendsList.AddItem(fItem);


                    });
                }

                Thread.Sleep(5);
            }


        }

        public void AddToChannelList(string channelName, long chatID, bool addToChannelsData = false)
        {
            ChannelsList.AddItem(new ChannelListItem()
            {
                ChatID = chatID,
                ChannelName = channelName
            });


        }

        public void UpdateUserStatus(long userID, Enums.UserInfo.UserStatus userStatus)
        {
            FriendListItem fItem = FriendsList.GetItemByID(userID);
            fItem.Status = userStatus;
            if (App.ChatMan._openedChatList.ContainsKey(fItem.ChatID)) App.ChatMan._openedChatList[fItem.ChatID].SwitchUserStatus(userStatus);
        }

        #endregion

        #region Friend Options
        public void onUesrMenuClick(object sender, MouseButtonEventArgs e)
        {
            FriendListItem objSender = (FriendListItem)sender;
            long friendID = objSender.UserID;
            string friendName = objSender.FriendName;
            long chatID = objSender.ChatID;

            Enums.Friend.FriendOptions SelectedOption = new Enums.Friend.FriendOptions();

            FriendsList.IsEnabled = false;

            switch (objSender.menuOrder)
            {
                case "menuChat":
                    SelectedOption = Enums.Friend.FriendOptions.Chat;
                    break;
                case "menuBlock":
                    SelectedOption = Enums.Friend.FriendOptions.Block;
                    break;
                case "menuRemove":
                    SelectedOption = Enums.Friend.FriendOptions.Remove;
                    break;

            }


            if (SelectedOption == Enums.Friend.FriendOptions.Chat)
            {
                FriendOptionHandler(sender, SelectedOption);
            }
            else
            {
                SendFriendOptionPacket(SelectedOption, friendID);
            }
        }

        void SendFriendOptionPacket(Enums.Friend.FriendOptions selectedOption, long handlerUserID)
        {
            for (int i = 0; i < 1; i++)
            {
                Packets.Friends.CSFriendOption packet = new Packets.Friends.CSFriendOption();

                packet.userID = LauncherApp.Game_Data.Globals.uid;
                packet.handlerUserID = handlerUserID;
                packet.selectedOption = selectedOption;

                App.connection.Send(packet, "CS_FriendOption");
            }
        }


        internal void OnFriendOptionResult(object data)
        {
            Packets.Friends.FriendOptionRep replyPacket = (Packets.Friends.FriendOptionRep)data;

            switch (replyPacket.sqlReply)
            {
                case Enums.SqlResultReply.Success:

                    Dispatcher.Invoke(() =>
                    {
                        FriendOptionHandler(null, Enums.Friend.FriendOptions.Remove, replyPacket.userID);
                    });

                    break;
                case Enums.SqlResultReply.Failed:

                    Dispatcher.Invoke(() =>
                    {
                        //showMessage((string)App.Current.Resources["strCallSupport"], LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                    });

                    break;
            }

            Dispatcher.Invoke(() =>
            {
                FriendsList.IsEnabled = true;
            });
        }

        private void FriendOptionHandler(object selectedFriendObj, Enums.Friend.FriendOptions fOption, long friendID = 0)
        {

            if (fOption == Enums.Friend.FriendOptions.Chat)
            {

                FriendListItem objSender = (FriendListItem)selectedFriendObj;

                this.SwitchChatWindow(objSender.ChatID, objSender.FriendName, objSender.UserID, objSender.Status);


            }
            else
            {
                FriendListItem objSender = FriendsList.GetItemByID(friendID);

                FriendsList.RemoveByUserID(objSender.UserID);
                App.ChatMan._openedChatList.Remove(objSender.ChatID);
            }

            FriendsList.IsEnabled = true;

        }

        #endregion

        #region Channel Options


        public void onChannelMenuClick(object sender, MouseButtonEventArgs e)
        {
            ChannelListItem objSender = (ChannelListItem)sender;
            long channelID = objSender.ChatID;
            string channelName = objSender.ChannelName;

            Enums.Channel.ChannelOptions SelectedOption = new Enums.Channel.ChannelOptions();

            ChannelsList.IsEnabled = false;

            switch (objSender.menuOrder)
            {
                case "menuOpen":
                    SelectedOption = Enums.Channel.ChannelOptions.Chat;
                    break;
                case "menuLeave":
                    SelectedOption = Enums.Channel.ChannelOptions.Remove;
                    break;

            }

            selectedChannelObj.chatID = channelID;
            selectedChannelObj.channelName = channelName;
            selectedChannelObj.selectedOption = SelectedOption;


            if (SelectedOption == Enums.Channel.ChannelOptions.Chat)
            {
                ChannelOptionHandler(selectedChannelObj);
            }
            else
            {
                SendChannelOptionPacket(SelectedOption, channelID, channelName);
            }
        }

        void SendChannelOptionPacket(Enums.Channel.ChannelOptions selectedOption, long channelID, string channelName)
        {
            for (int i = 0; i < 1; i++)
            {
                Packets.Channel.ChannelOptions packet = new Packets.Channel.ChannelOptions();

                packet.userID = LauncherApp.Game_Data.Globals.uid;
                packet.chatID = channelID;

                App.connection.Send(packet, "ChannelOptionReq");
            }
        }


        internal void OnChannelOptionResult(object result)
        {
            Packets.Channel.ChannelOptionsRep resultObj = (Packets.Channel.ChannelOptionsRep)result;

            switch (resultObj.status)
            {
                case Enums.SqlResultReply.Success:

                    Dispatcher.Invoke(() =>
                    {
                        ChannelsList.RemoveByChannelID(resultObj.chatID);
                        App.ChatMan._openedChatList.Remove(resultObj.chatID);
                    });

                    break;
                case Enums.SqlResultReply.Failed:

                    Dispatcher.Invoke(() =>
                    {
                        //showMessage((string)App.Current.Resources["strCallSupport"], LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                    });

                    break;
            }

            Dispatcher.Invoke(() =>
            {
                ChannelsList.IsEnabled = true;
            });
        }

        private void ChannelOptionHandler(Packets.Channel.ChannelOptions selectedCObj)
        {
            long cID = selectedCObj.chatID;
            Enums.Channel.ChannelOptions selectedOpion = selectedCObj.selectedOption;
            string cName = selectedCObj.channelName;


            if (selectedOpion == Enums.Channel.ChannelOptions.Chat)
            {

                this.SwitchChatWindow(cID, cName, 0, Enums.UserInfo.UserStatus.Available);

                //if (!App.ChatMan.OpenChannelFromList(cID))
                //{
                //    ChannelWindow tempChatWnd = new ChannelWindow() { chatID = cID, channelName = cName };
                //    App.ChatMan._openedChannels.Add(cID, tempChatWnd);
                //    Thread.Sleep(50);
                //    tempChatWnd.Show();
                //}

            }
            else
            {
                ChannelsList.RemoveByChannelID(cID);
                App.ChatMan._openedChatList.Remove(cID);
            }

            ChannelsList.IsEnabled = true;

        }

        #endregion





    }
}
