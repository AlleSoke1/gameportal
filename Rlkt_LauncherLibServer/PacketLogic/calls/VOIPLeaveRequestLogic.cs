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
    class VOIPLeaveRequestLogic
    {

        public VOIPLeaveRequestLogic(Packets.packet packet)
        {
            //Set user GUID and username
            Guid guid = packet.guid;
            long userID = packet.data.userID;
            long chatID = packet.data.chatID;
            long voipChannelID = packet.data.voipChannelID;

            packet.data.userNickname = Program.clients.findClientByUserID(userID).nickname;

            int mCount = Program.VOIPManager.GetMemeberCount(voipChannelID);
            //List<long> tempMembers = Program.VOIPManager.GetMemberIDs(voipChannelID);

            foreach (long uID in Program.VOIPManager.GetMemberIDs(voipChannelID).ToList())
            {

                if (mCount <= 2)
                {
                    //Program.VOIPManager.RemoveMember(voipChannelID, uID); 
                    packet.data.endCallFlag = true;
                }

                Rlkt_LauncherLibServer.Clients.ClientInfo cInfo = Program.clients.findClientByUserID(uID);

                if (cInfo != null)
                {
                    cInfo.CallCenterBusy = false;
                    Server.Server.server.DispatchTo(cInfo.guid, new NetObject("VoiceChatLeaveNotify", (object)packet.data));
                }
            }

            if (mCount <= 2)
            {
                Program.VOIPManager.Remove(voipChannelID);
            }
            else
            {
                Program.VOIPManager.RemoveMember(voipChannelID, userID);
                Program.clients.findClientByUserID(userID).CallCenterBusy = false;
            }



        }



    }
}
