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
                       
                        LauncherFactory.getAppClass().onGameListRecv(info);
                    }
                    break;

                #region Friend Client Packets

                case "SC_FrinedListRep":

                    Packets.Friends.SCFriendList fList = (Packets.Friends.SCFriendList)data.Object;

                    // update friends list
                    LauncherFactory.getFriendsClass().UpdateFriendList(fList);

                    break;
                case "SC_AddFriendRep":

                    LauncherFactory.getNewFriendClass().OnLoginResult(data.Object);
                    break;

                case "SC_FriendRequestOption":

                    LauncherFactory.getFriendsClass().OnRequestOptionResult(data.Object);
                    break;
                case "SC_FriendOption":

                    LauncherFactory.getFriendsClass().OnFriendOptionResult(data.Object);
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

                    if (App.ChatMan._openedChats.ContainsKey(handlerChatID))
                    {
                        App.ChatMan._openedChats[handlerChatID].OnMessageListResult(data.Object); 
                    }

                    if (App.ChatMan._openedChannels.ContainsKey(handlerChatID))
                    {
                        App.ChatMan._openedChannels[handlerChatID].OnMessageListResult(data.Object); 
                    }

                    break;

                case "SC_SendMessageRep":

                    handlerChatID = ((Packets.Chat.ChatMessageRep)data.Object).chatID;

                    if (App.ChatMan._openedChats.ContainsKey(handlerChatID))
                    {
                        App.ChatMan._openedChats[handlerChatID].OnSendingResult(data.Object);
                    }

                    if (App.ChatMan._openedChannels.ContainsKey(handlerChatID))
                    {
                        App.ChatMan._openedChannels[handlerChatID].OnSendingResult(data.Object);
                    }

                    break;
                case "SC_SendMessageNotify":

                    App.NotifyMan.NotifyHandler(NotifyManager.NotifyType.NewMessageAdded, data.Object);

                    break;
                #endregion


                #region Channel Client Packets

                case "CreateChannelRep":

                    LauncherFactory.getChannelCreateClass().OnLoginResult(data.Object);

                    break;

                case "ChannelUseresListRep":

                    App.ChatMan._openedChannels[((Packets.Channel.ChannelUsersListRep)data.Object).chatID].OnUsersListResult(data.Object);

                    break;
                case "ChannelOptionRep":

                    LauncherFactory.getFriendsClass().OnChannelOptionResult(data.Object);

                    break;
                case "ChannelInviteRep":

                    LauncherFactory.getChannelInviteClass().OnLoginResult(data.Object);

                    break;

                case "ChannelOptionNotify":

                    Packets.Channel.ChannelOptionsNotify notifyData = (Packets.Channel.ChannelOptionsNotify)data.Object;

                    if (notifyData.userID == LauncherApp.Game_Data.Globals.uid)
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            LauncherFactory.getFriendsClass().AddToChannelList(notifyData.ChannelName, notifyData.chatID, true);
                        });
                    }

                    App.ChatMan._openedChannels[notifyData.chatID].OnUserNotifyResult(data.Object);

                    break;
                #endregion


                default:
                    Console.WriteLine("Packet does not exists!! " + data.Name);
                    break;
            }
        }
    }
}
