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
        private Guid guid;
        private long senderUserID;
        private long reciverUserID;
        private long chatID;

        public VOIPRequestLogic(Packets.packet packet)
        {
            //Set user GUID and username
            guid = packet.guid;
            senderUserID = packet.data.senderUserID;
            reciverUserID = packet.data.reciverUserID;
            chatID = packet.data.chatID;

            Rlkt_LauncherLibServer.Clients.ClientInfo cInfo = Program.clients.findClientByUserID(reciverUserID);

            Packets.Chat.VoiceChatRequestResult resultPacket = new Packets.Chat.VoiceChatRequestResult() { 
                chatID = this.chatID , 
                Result = Enums.Voip.CallResultReply.Success
            };

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
            Server.Server.server.DispatchTo(cInfo.guid, new NetObject("VoceChatReqNotify", chatID));

            // reply call result
            Server.Server.server.DispatchTo(guid, new NetObject("VoceChatReqResult", resultPacket));

        }

        

    }
}
