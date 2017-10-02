using NetSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rlkt_LauncherLibServer.Server
{
    class PacketHandler
    {
        public static void RecvPacket(string packetName, Packets.packet packet)
        {

            Console.WriteLine("[RECV PACKET] " + packetName);

            switch (packetName)
            {
                case "CS_Login": //CS: Client->Server
            
                    new PacketLogic.LoginLogic(packet);

                    break;

                //case "CS_Logout":
                //    new PacketLogic.LogoutLogic(packet);
                //    break;

                case "CS_LoginCreateNickName":

                    new PacketLogic.NicknameLogic(packet);

                    break;

                case "CS_UserStatus":
                    new PacketLogic.UserStatusLogic(packet);
                    break;


                #region Friend System Packets

                case "CS_FriendListReq":

                    new PacketLogic.FriendListLogic(packet);

                    break;

                case "CS_AddFriendReq":

                    new PacketLogic.AddFriendLogic(packet);

                    break;

                case "CS_FriendRequestOption":

                    new PacketLogic.AddFriendOptionLogic(packet);

                    break;
                case "CS_FriendOption":

                    new PacketLogic.FriendOptionLogic(packet);

                    break;

                #endregion

                #region Chat System Packets

                case "CS_GetMessageListReq":

                    new PacketLogic.ChatMessagesLogic(packet);

                    break;

                case "CS_SendMessageReq":

                    new PacketLogic.SendMessageLogic(packet);

                    break;

                #endregion

                #region Channel System Packets

                case "CreateChannelReq":

                    new PacketLogic.CreateChannelLogic(packet);

                    break;

                case "ChannelUseresListReq":

                    new PacketLogic.ChannelUseresListLogic(packet);

                    break;

                case "ChannelOptionReq":

                    new PacketLogic.ChannelOptionLogic(packet);

                    break;

                case "ChannelInviteReq":

                    new PacketLogic.ChannelInviteLogic(packet);

                    break;

                #endregion

                #region VoiceChat System Packets

                case "VoceChatReq":
                    new PacketLogic.VOIPRequestLogic(packet);
                    break;

                #endregion
                default:
                    Console.WriteLine(" -> Packet not found !! [" + packetName + "]");
                    break;
            }
        }
    }
}
