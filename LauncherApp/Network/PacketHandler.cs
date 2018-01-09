using NetSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace LauncherApp.Network
{
    class PacketHandler
    {
        public static void RecvPacket(NetObject data)
        {
            Console.WriteLine("[RECV PACKET] "+data.Name);
            long handlerChatID;


            switch(data.Name)
            {
                case "handshake":
                    {
                        Packets.Login.handshake handshake =  (Packets.Login.handshake)data.Object;
                        App.connection.setGuid(handshake.guid);
                    }
                    break;

                #region User Packets

                    case "SC_Login":
                        {
                            Packets.Login.SCLoginPacket packet = (Packets.Login.SCLoginPacket)data.Object;
                            LauncherFactory.getLoginClass().OnLoginResult(packet.status);
                        }
                        break;

                    case "SC_LoginCreateNickName":
                        {
                            Packets.Login.SCcreateNickName createNickPacket = (Packets.Login.SCcreateNickName)data.Object;
                            LauncherFactory.getNicknameClass().OnNicknameResult((int)createNickPacket.result);

                            if (createNickPacket.result == Enums.Login.NicknameResult.Successfull)
                                Game_Data.Globals.nickname = createNickPacket.nickname;
                        }
                        break;


                    case "SC_WndCreateNickname":
                        {
                            LauncherFactory.getNicknameClass().ShowNicknameWindow();
                        }
                        break;

                    case "SC_UserInfo":
                        Packets.Login.SCUserInfo userInfo = (Packets.Login.SCUserInfo)data.Object;
                        Game_Data.Globals.username = userInfo.username;
                        Game_Data.Globals.uid = userInfo.userId;
                        Game_Data.Globals.nickname = userInfo.nickname;
                        Game_Data.Globals.email = userInfo.email;

                        LauncherFactory.getAppClass().UpdateInfo();

                        //set global userid,username,nickname
                        break;

                    case "SC_UserStatusNotify":

                        App.NotifyMan.NotifyHandler(NotifyManager.NotifyType.UpdateUserStatus, data.Object);

                        break;

                #endregion

               

                case "SC_GameInfo":
                    {
                        Packets.GameData.SC_GameInfo info = (Packets.GameData.SC_GameInfo)data.Object;
                       
                        LauncherFactory.getAppClass().HomePage.onGameListRecv(info);
                    }
                    break;

                #region Friend Client Packets

                case "SC_FrinedListRep":

                    Packets.Friends.SCFriendList fList = (Packets.Friends.SCFriendList)data.Object;

                    // update friends list
                    LauncherFactory.getAppClass().SocialPage.UpdateFriendList(fList);

                    break;
                case "SC_AddFriendRep":

                    LauncherFactory.getAppClass().SocialPage.AddFriendWindow.OnReciveResult(data.Object);
                    break;

                case "SC_FriendRequestOption":

                    LauncherFactory.getAppClass().SocialPage.OnRequestOptionResult(data.Object);
                    break;
                case "SC_FriendOption":

                    LauncherFactory.getAppClass().SocialPage.OnFriendOptionResult(data.Object);
                    break;
                case "SC_AddFriendNotify":

                    App.NotifyMan.NotifyHandler(NotifyManager.NotifyType.AddFriend, data.Object);
                    break;
                case "SC_RemoveFriendNotify":

                    App.NotifyMan.NotifyHandler(NotifyManager.NotifyType.RemoveFriend, data.Object);
                    break;
                case "SC_FriendAddedNotify":

                    App.NotifyMan.NotifyHandler(NotifyManager.NotifyType.FriendAdded, data.Object);
                    break;
                #endregion


                #region Chat Client Packets

                case "SC_GetMessageListRep":

                    handlerChatID = ((Packets.Chat.ChatMessagesList)data.Object).chatID;

                    LauncherFactory.getAppClass().SocialPage._chatControl.OnMessageListResult(data.Object);

                    //if (App.ChatMan._openedChats.ContainsKey(handlerChatID))
                    //{
                    //    App.ChatMan._openedChats[handlerChatID].OnMessageListResult(data.Object); 
                    //}

                    //if (App.ChatMan._openedChannels.ContainsKey(handlerChatID))
                    //{
                    //    App.ChatMan._openedChannels[handlerChatID].OnMessageListResult(data.Object); 
                    //}

                    break;

                case "SC_SendMessageRep":

                    handlerChatID = ((Packets.Chat.ChatMessageRep)data.Object).chatID;

                    if (App.ChatMan._openedChatList.ContainsKey(handlerChatID))
                    {
                        App.ChatMan._openedChatList[handlerChatID].MessagesList.OnSendingResult(data.Object);
                    }


                    break;
                case "SC_SendMessageNotify":

                    App.NotifyMan.NotifyHandler(NotifyManager.NotifyType.NewMessageAdded, data.Object);

                    break;
                #endregion


                #region Channel Client Packets

                case "CreateChannelRep":

                    LauncherFactory.getAppClass().SocialPage.CreateChannelWindow.OnReciveResult(data.Object);

                    break;

                case "ChannelUseresListRep":

                    App.ChatMan._openedChatList[((Packets.Channel.ChannelUsersListRep)data.Object).chatID].OnUsersListResult(data.Object);

                    break;
                case "ChannelOptionRep":

                    LauncherFactory.getAppClass().SocialPage.OnChannelOptionResult(data.Object);

                    break;
                case "ChannelInviteRep":

                    long[] tempPacket = (long[])data.Object;
                    App.ChatMan._openedChatList[tempPacket[1]].inviteWinfow.OnReciveResult((int)tempPacket[0]);

                    break;

                case "ChannelOptionNotify":

                    Packets.Channel.ChannelOptionsNotify notifyData = (Packets.Channel.ChannelOptionsNotify)data.Object;

                    if (notifyData.userID == LauncherApp.Game_Data.Globals.uid)
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            LauncherFactory.getAppClass().SocialPage.AddToChannelList(notifyData.ChannelName, notifyData.chatID, true);
                        });
                    }

                    if (App.ChatMan._openedChatList.ContainsKey(notifyData.chatID))
                        App.ChatMan._openedChatList[notifyData.chatID].OnUserNotifyResult(data.Object);

                    break;
                #endregion

                #region VoiceChat Packets

                case "VoceChatReqResult":
                    {
                        Packets.Chat.VoiceChatRequestResult resultPacket = (Packets.Chat.VoiceChatRequestResult)data.Object;

                        if (App.ChatMan._openedChatList.ContainsKey(resultPacket.chatID))
                        {
                            App.ChatMan._openedChatList[resultPacket.chatID].MessagesList.onCallRequsetResult(resultPacket);
                        }

                    }
                    break;

                case "VoceChatReqNotify":

                    App.NotifyMan.NotifyHandler(NotifyManager.NotifyType.IncomingCall, data.Object);

                    break;

                case "VoceChatReqRepNotify":

                    App.NotifyMan.NotifyHandler(NotifyManager.NotifyType.IncomingCallReply, data.Object);

                    break;

                case "VoiceChatLeaveNotify":

                    App.NotifyMan.NotifyHandler(NotifyManager.NotifyType.LeaveCall, data.Object);

                    break;

                    
                #endregion

                default:
                    Console.WriteLine("Packet does not exists!! " + data.Name);
                    break;
            }
        }
    }
}
