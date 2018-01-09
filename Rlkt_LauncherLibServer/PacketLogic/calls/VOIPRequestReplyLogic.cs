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
using System.Threading;

namespace Rlkt_LauncherLibServer.PacketLogic
{
    class VOIPRequestReplyLogic
    {

        public VOIPRequestReplyLogic(Packets.packet packet)
        {
            //Set user GUID and username
            Guid guid = packet.guid;
            long senderUserID = packet.data.senderUserID;
            long reciverUserID = packet.data.reciverUserID;
            long chatID = packet.data.chatID;
            Enums.Voip.CallResultReply requestReply = packet.data.requestReply;

            Rlkt_LauncherLibServer.Clients.ClientInfo friendInfo = Program.clients.findClientByUserID(reciverUserID);


            Packets.Chat.VoiceChatRequestResult resultPacket = new Packets.Chat.VoiceChatRequestResult()
            {
                chatID = chatID,
                Result = Enums.Voip.CallResultReply.Success
            };

            if (requestReply == Enums.Voip.CallResultReply.Failed)
            {
                // reply call result
                if (friendInfo == null)
                {
                    resultPacket.Result = Enums.Voip.CallResultReply.Failed;
                    Server.Server.server.DispatchTo(friendInfo.guid, new NetObject("VoceChatReqRepNotify", resultPacket));
                }

                return;
            }


            resultPacket.voipCID = chatID;
            resultPacket.replyUserName = Program.clients.findClientByUserID(senderUserID).nickname;

            bool addMembersError = false;
            // add members to voip channel
            
            if (!Program.VOIPManager.AddMember(chatID, reciverUserID))
            {
                addMembersError = true;
            }

            if (addMembersError)
            {
                // here destroy the channel and send faild packet to users
            }
            else
            {
                // send to reciverUserID call notfiy
                Server.Server.server.DispatchTo(friendInfo.guid, new NetObject("VoceChatReqRepNotify", resultPacket));

                // send to senderUserID call notfiy
                Server.Server.server.DispatchTo(guid, new NetObject("VoceChatReqRepNotify", resultPacket));

                new Thread(() => { Program._voiceServer.AddChannel(Program.VOIPManager.GetChannel(chatID)); }).Start();
            }

        }



    }
}
