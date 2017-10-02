using LauncherApp.Styles.Controls;
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
using System.Windows.Shapes;

namespace LauncherApp
{
    /// <summary>
    /// Interaction logic for FriendsWindow.xaml
    /// </summary>
    public partial class FriendsWindow : Window
    {
        private bool windowMaximizeState = false;
        private Packets.Channel.ChannelOptions selectedChannelObj = new Packets.Channel.ChannelOptions();

        public FriendsWindow()
        {
            InitializeComponent();

            //RequestList.AddItem(new FriendRequestListitem() { UesrName = "Testing" });

            LoadStyles();

        }

        #region UI Functions

        private void LoadStyles()
        {
            Panel.SetZIndex(wndBorder, 1);
            ListScroll.Margin = new Thickness(0, RequestListPanel.ActualHeight, 0 ,0);
        }


        private void RequestListPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListScroll.Padding = new Thickness(0, 0, 0, RequestList.ActualHeight);
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

        private void tFullBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!windowMaximizeState)
            {
                tFullBtn.Icon = FontAwesome.WPF.FontAwesomeIcon.WindowRestore;
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                tFullBtn.Icon = FontAwesome.WPF.FontAwesomeIcon.WindowMaximize;
                this.WindowState = WindowState.Normal;
            }

            windowMaximizeState = !windowMaximizeState;

        }


        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FriendsList.FilterUseres(SearchBox.Text);
            ChannelsList.FilterChannels(SearchBox.Text);
        }


        private void newChannelBtn_Click(object sender, RoutedEventArgs e)
        {

            var nChanneldWnd = LauncherFactory.getChannelCreateClass();

            nChanneldWnd.Show();
            nChanneldWnd.Activate();
            nChanneldWnd.Topmost = true;
            nChanneldWnd.Topmost = false;
            nChanneldWnd.Focus();

            //RequestList.AddItem(new FriendRequestListitem()
            //{
            //    UserID = 100,
            //    UesrName = "Testing",
            //});


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserNickname.Content = Game_Data.Globals.nickname;
            UserID.Content = "#" + Game_Data.Globals.uid;

            //SendFriendListPacket();
        }

        public void UpdateUserStatus(Enums.UserInfo.UserStatus status)
        {
            string strStatus = "";
            SolidColorBrush colorStatus = new SolidColorBrush();


            //Change libel
            switch (status)
            {
                case Enums.UserInfo.UserStatus.Available:

                    strStatus = " " + (string)App.Current.Resources["UserStatusOnline"];
                    colorStatus = new SolidColorBrush(Color.FromRgb(22, 212, 2));

                    break;
                case Enums.UserInfo.UserStatus.Away:

                    strStatus = " " + (string)App.Current.Resources["UserStatusAway"];
                    colorStatus = new SolidColorBrush(Color.FromRgb(247, 196, 23));
                    break;
                case Enums.UserInfo.UserStatus.Busy:

                    strStatus = " " + (string)App.Current.Resources["UserStatusBusy"];
                    colorStatus = new SolidColorBrush(Color.FromRgb(216, 1, 1));

                    break;
                case Enums.UserInfo.UserStatus.Invisible:

                    break;
            }
            
            Run icon = new Run(" ");
            icon.Foreground = colorStatus;
            icon.FontFamily = this.Resources["IconsFont"] as FontFamily;
            Run text = new Run((strStatus).Replace(" ", ""));
            text.Foreground = UserID.Foreground;

            UserStatus.Text = "";
            this.UserStatus.Inlines.Add(icon);
            this.UserStatus.Inlines.Add(text);
            this.UserStatus.Foreground = colorStatus;
        }

        private void AddFriendBtn_Click(object sender, RoutedEventArgs e)
        {
            var aFriendWnd = LauncherFactory.getNewFriendClass();

            aFriendWnd.Show();
            aFriendWnd.Activate();
            aFriendWnd.Topmost = true;
            aFriendWnd.Topmost = false;
            aFriendWnd.Focus();

        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            Window SettingWnd = LauncherFactory.getSettingsClass();
            LauncherFactory.getSettingsClass().Owner = LauncherFactory.getAppClass();
            LauncherFactory.getAppClass().BlurWindow(true);
            LauncherFactory.getSettingsClass().SwitchMenuItems("settings_FriendsChat");
            LauncherFactory.getSettingsClass().Show();
            //show chat settings code
            // SettingWnd.ChatWindow.Visibility = Visibility.Visible;
        }

        private void showMessage(string text, LauncherApp.Styles.Controls.FastMessage.MessageTypes type)
        {
            alertMessage.Message = text;
            alertMessage.ShowTime = 3;
            alertMessage.Type = type;
            alertMessage.Show();
        }

        #endregion


        #region Requests Packet

        private void onRequestMenuClick(object sender, MouseButtonEventArgs e)
        {
            onRequestOptionClick(sender, new RoutedEventArgs());
        }

        private void onRequestOptionClick(object sender, RoutedEventArgs e)
        {
            int requestOwnerID;
            int[] selectedRequestObj = new int[2];

            Enums.Friend.FriendRequestOptions SelectedOption = new Enums.Friend.FriendRequestOptions();
            dynamic objSender;

            if (sender is Button)
            {
                objSender = (Button)sender;
            }
            else
            {
                objSender = (StackPanel)sender;
            }
                
           
             requestOwnerID = int.Parse(objSender.Tag.ToString());

            RequestList.IsEnabled = false;

            switch ((string)objSender.Name)
            {
                case "acceptRequest":
                    SelectedOption = Enums.Friend.FriendRequestOptions.Approve;
                    break;
                case "declineRequest":
                    SelectedOption = Enums.Friend.FriendRequestOptions.Ignore;
                    break;
                case "blockRequest":
                    SelectedOption = Enums.Friend.FriendRequestOptions.Block;
                    break;

            }

            selectedRequestObj[0] = (int)objSender.Tag;
            selectedRequestObj[1] = (int)SelectedOption;

            SendRequestOptionPacket(SelectedOption, requestOwnerID);
        }

        internal void OnRequestOptionResult(object result)
        {
            var resultObj = (Packets.FriendsRequest.SCAddFriendRep)result;
            switch (resultObj.status)
            {
                case Enums.SqlResultReply.Success:

                    Dispatcher.Invoke(() =>
                    {
                        FriendRequestListitem tempItem = RequestList.RemoveByUserID(resultObj.friendID);

                        if (resultObj.selectedOption ==  Enums.Friend.FriendRequestOptions.Approve)
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
                        showMessage("Something Wrong Happened, Please Call Support", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                    });

                    break;
            }

            Dispatcher.Invoke(() =>
            {
                RequestList.IsEnabled = true;
            });

        }

        void SendRequestOptionPacket(Enums.Friend.FriendRequestOptions selectedOption, int requestOwnerID)
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
                    RequestList.ClearItems();
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
                            RequestList.AddItem(new FriendRequestListitem()
                            {
                                Name = "Request_" + fInfo.Id,
                                UserID = fInfo.Id,
                                UesrName = fInfo.NickName,
                            });
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

                if (addToChannelsData)
                {

                    App.ChatMan._openedChannels[chatID] = new ChannelWindow()
                    {
                        channelName = channelName,
                        chatID = chatID
                    };
                }
            }

            public void UpdateUserStatus(long userID, Enums.UserInfo.UserStatus userStatus)
            {
                FriendListItem fItem = FriendsList.GetItemByID(userID);
                fItem.Status = userStatus;

            }

        #endregion


        #region Friend Options
                private void onUesrMenuClick(object sender, MouseButtonEventArgs e)
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
                                showMessage("Something Wrong Happened, Please Call Support", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                            });

                            break;
                    }

                    Dispatcher.Invoke(() =>
                    {
                        FriendsList.IsEnabled = true;
                    });
                }

                private void FriendOptionHandler(object selectedFriendObj,Enums.Friend.FriendOptions fOption, long friendID = 0)
                {

                    if (fOption == Enums.Friend.FriendOptions.Chat)
                    {

                        FriendListItem objSender = (FriendListItem)selectedFriendObj;

                        long fID = objSender.UserID;
                        string fName = objSender.FriendName;
                        long chatID = objSender.ChatID;

                        if (!App.ChatMan.OpenChatFromList(chatID))
                        {
                            ChatWindow tempChatWnd = new ChatWindow() { friendID = fID, friendName = fName, chatID = chatID };
                            tempChatWnd.SwitchUserStatus(objSender.Status);
                            App.ChatMan._openedChats.Add(chatID, tempChatWnd);
                            Thread.Sleep(50);
                            tempChatWnd.Show();
                        }

                    }
                    else
                    {
                        FriendListItem objSender = FriendsList.GetItemByID(friendID);

                        FriendsList.RemoveByUserID(objSender.UserID);
                        App.ChatMan._openedChats.Remove(objSender.ChatID);
                    }

                    FriendsList.IsEnabled = true;

                }

            #endregion

        #region Channel Options


                private void onChannelMenuClick(object sender, MouseButtonEventArgs e)
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
                                App.ChatMan._openedChannels.Remove(resultObj.chatID);
                            });

                            break;
                        case Enums.SqlResultReply.Failed:

                            Dispatcher.Invoke(() =>
                            {
                                showMessage("Something Wrong Happened, Please Call Support", LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
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
                        if (!App.ChatMan.OpenChannelFromList(cID))
                        {
                            ChannelWindow tempChatWnd = new ChannelWindow() { chatID = cID, channelName = cName };
                            App.ChatMan._openedChannels.Add(cID, tempChatWnd);
                            Thread.Sleep(50);
                            tempChatWnd.Show();
                        }

                    }
                    else
                    {
                        ChannelsList.RemoveByChannelID(cID);
                        App.ChatMan._openedChannels.Remove(cID);
                    }

                    ChannelsList.IsEnabled = true;

                }

        #endregion


    }
}
