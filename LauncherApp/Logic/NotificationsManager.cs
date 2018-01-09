using LauncherApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotifyManager
{

    public class NotifyManager
    {

        public NotifyManager()
        {
            
        }


        public void NotifyHandler(NotifyType type, object data)
        {
            string notifyMessage = "";
           
                switch (type)
                {

                    #region Update User Status

                    case NotifyType.UpdateUserStatus:

                        var statusData = (Packets.UserInfo.CSUserStatus)data;

                        App.Current.Dispatcher.Invoke(() =>
                        {
                            LauncherFactory.getAppClass().SocialPage.UpdateUserStatus(statusData.userID, statusData.userStatus);

                            if (statusData.IsLogin)
                            {
                                notifyMessage = string.Format("Your friend {0} is online now and ready to receive your messages.", statusData.Nickname);
                                LauncherFactory.getNotifyClass().AddItem(string.Format("{0} Is Online Now!", statusData.Nickname), notifyMessage, 5);
                            }
                        });

                        

                        break;

                    #endregion

                    #region  Add Friend

                    case NotifyType.AddFriend:

                        var temp1 = (Packets.FriendsRequest.CSAddFriendNotify)data;

                        notifyMessage = string.Format("You have reciver new friend request from {0}, please check your friends list.", temp1.senderName);

                        App.Current.Dispatcher.Invoke(() =>
                        {
                            LauncherFactory.getNotifyClass().AddItem("New Friend Request", notifyMessage, 5);
                            LauncherFactory.getAppClass().SocialPage.FriendRequests.AddItem(new LauncherApp.Styles.Controls.FriendRequestListitem()
                            {
                                UserID = temp1.senderID,
                                UesrName = temp1.senderName
                            });
                            LauncherFactory.getAppClass().SocialPage.friendRequestCount++;
                        });

                        break;

                    #endregion

                    #region  Remove Friend

                    case NotifyType.RemoveFriend:

                        App.Current.Dispatcher.Invoke(() =>
                        {
                            LauncherFactory.getAppClass().SocialPage.FriendsList.RemoveByUserID((int)data);
                        });

                        break;

                    #endregion

                    #region  Friend Added

                    case NotifyType.FriendAdded:

                        var temp2 = (Packets.FriendsRequest.CSFriendAddedNotify)data;

                        notifyMessage = string.Format("{0} have accept your friend request", temp2.friendName);

                        App.Current.Dispatcher.Invoke(() =>
                        {
                            LauncherFactory.getNotifyClass().AddItem("Request Accepted!", notifyMessage, 5);
                            LauncherFactory.getAppClass().SocialPage.FriendsList.AddItem(new LauncherApp.Styles.Controls.FriendListItem()
                            {
                                UserID = temp2.friendID,
                                FriendName = temp2.friendName
                            });
                        });


                        break;

                    #endregion

                    #region  New Message Added

                    case NotifyType.NewMessageAdded:

                        var msgInfo = (Packets.Chat.ChatMessageRep)data;

                        notifyMessage = string.Format("New message from {0}", msgInfo.OwnerName);

                        if (msgInfo.isChannel)
                        {
                            notifyMessage += string.Format(" in channel ", msgInfo.channelName);
                        }

                        App.Current.Dispatcher.Invoke(() =>
                        {

                            if (App.ChatMan._openedChatList.ContainsKey(msgInfo.chatID))
                            {
                                if (!App.ChatMan._openedChatList[msgInfo.chatID].isActive)
                                {
                                    LauncherFactory.getNotifyClass().AddItem("New Message!", notifyMessage, 5);
                                }

                                App.ChatMan._openedChatList[msgInfo.chatID].MessagesList.OnSendingResult(msgInfo);
                                return;
                            }

                            // if user not open chat yet will notify him.
                            LauncherFactory.getNotifyClass().AddItem("New Message!", notifyMessage, 5);
                        });


                        break;

                    #endregion

                    #region Incoming Call
                    case NotifyType.IncomingCall:

                        long chatID = ((Packets.Chat.VoiceChatRequestResult)data).chatID;

                        if (App.ChatMan._openedChatList.ContainsKey(chatID))
                        {
                            App.ChatMan._openedChatList[chatID].MessagesList.onIncomingCallRequset(data);
                        }

                        break;
                    #endregion

                    #region Incoming Call Reply
                    case NotifyType.IncomingCallReply:

                        long chatID1 = ((Packets.Chat.VoiceChatRequestResult)data).chatID;

                        if (App.ChatMan._openedChatList.ContainsKey(chatID1))
                        {
                            App.ChatMan._openedChatList[chatID1].MessagesList.onIncomingCallRequsetReply(data);
                        }

                        break;
                    #endregion

                    #region Leave Call

                    case NotifyType.LeaveCall:

                        long chatID2 = ((Packets.Chat.VoiceChatLeaveCall)data).chatID;

                        if (App.ChatMan._openedChatList.ContainsKey(chatID2))
                        {
                            App.ChatMan._openedChatList[chatID2].MessagesList.onLeaveCallNotfiy(data);
                        }

                        break;

                    #endregion


                }


        }



    }

    public enum NotifyType
    {
        AddFriend = 0,
        RemoveFriend = 1,
        FriendAdded = 2,

        UpdateUserStatus = 3,

        NewMessageAdded = 4,

        IncomingCall = 5,
        IncomingCallReply = 6,
        LeaveCall = 7,

    };

}
