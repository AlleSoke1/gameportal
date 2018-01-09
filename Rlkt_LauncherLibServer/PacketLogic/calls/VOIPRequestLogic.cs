using NetSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rlkt_LauncherLibServer.Server;
using System.Net.Mail;
using System.Data.SqlClient;
using System.Data;

namespace Rlkt_LauncherLibServer.PacketLogic
{
    class VOIPRequestLogic
    {
        public VOIPRequestLogic(Packets.packet packet)
        {
            //Set user GUID and username
            Guid guid = packet.guid;
            long senderUserID = packet.data.senderUserID;
            long reciverUserID = packet.data.reciverUserID;
            long chatID = packet.data.chatID;
            string CallIpHost = packet.data.CallIpHost;

            Rlkt_LauncherLibServer.Clients.ClientInfo cInfo = Program.clients.findClientByUserID(reciverUserID);

            Packets.Chat.VoiceChatRequestResult resultPacket = new Packets.Chat.VoiceChatRequestResult()
            {
                chatID = chatID,
                Result = Enums.Voip.CallResultReply.Success,
                CallHostIp = CallIpHost
            };


            long createdVoipChannelID = Program.VOIPManager.Create(chatID);
            bool addMembersError = false;
            if (!Program.VOIPManager.AddMember(chatID, senderUserID))
            {
                addMembersError = true;
            }

            if (addMembersError)
            {
                // here destroy the channel and send faild packet to users
            }else{

                Program.VOIPManager.SetCallHostIp(chatID, CallIpHost);

                // reply call result
                if (cInfo == null)
                {
                    resultPacket.Result = Enums.Voip.CallResultReply.FriendOffline;
                    Server.Server.server.DispatchTo(guid, new NetObject("VoceChatReqResult", resultPacket));
                    return;
                }

                if (cInfo.CallCenterBusy)
                {
                    resultPacket.Result = Enums.Voip.CallResultReply.FriendOnCall;
                    Server.Server.server.DispatchTo(guid, new NetObject("VoceChatReqResult", resultPacket));
                    return;
                }

                // send to reciverUserID the call request
                Server.Server.server.DispatchTo(cInfo.guid, new NetObject("VoceChatReqNotify", resultPacket));

                // reply call result
                Server.Server.server.DispatchTo(guid, new NetObject("VoceChatReqResult", resultPacket));
            }
           

        }



    }
}
