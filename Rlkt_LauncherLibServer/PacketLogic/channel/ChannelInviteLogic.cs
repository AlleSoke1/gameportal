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
    class ChannelInviteLogic
    {
        private Guid guid;
        private long senderID;
        private long chatID;
        private string inviteNickName;
        private string channelName;

        public ChannelInviteLogic(Packets.packet packet)
        {
            //Set user GUID and packet info
            guid = packet.guid;
            senderID = packet.data.senderID;
            chatID = packet.data.chatID;
            inviteNickName = packet.data.inviteNickName;
            channelName = packet.data.channelName;

            //SQL
            SqlCommand cmd = new SqlCommand("SPR_AddChannelChatMember");

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@ChatID", chatID));
            cmd.Parameters.Add(new SqlParameter("@SenderUserID", senderID));
            cmd.Parameters.Add(new SqlParameter("@InviteNickname", inviteNickName));
            cmd.Parameters.Add("@RET", SqlDbType.TinyInt).Direction = ParameterDirection.Output;

            Program.sql.Execute(cmd, this);

        }

        public void OnSqlResult(SqlDataReader rdr, SqlCommand cmd = null)
        {
            List<long> channelUsersIDs = new List<long>();

            while (rdr.Read())
            {
                channelUsersIDs.Add((long)rdr["UserID"]);
            }

            rdr.Close();

            long[] returnPacket = new long[2];
            Enums.Channel.InviteChannelReply result = (Enums.Channel.InviteChannelReply)Convert.ToInt32(cmd.Parameters["@RET"].Value);

            returnPacket[0] = (int)result;
            returnPacket[1] = this.chatID;

            Server.Server.server.DispatchTo(guid, new NetObject("ChannelInviteRep", (object)returnPacket));

            //If SP has been success send notify to channel useres
            if (result == Enums.Channel.InviteChannelReply.Successfull)
            {

                Packets.Channel.ChannelOptionsNotify notifyPacket = new Packets.Channel.ChannelOptionsNotify()
                {   
                    chatID = this.chatID,
                    userID = Program.clients.findClientByNickname(inviteNickName).userId,
                    status = Enums.Channel.ChannelUserNotify.Join,
                    Nickname = inviteNickName,
                    ChannelName = this.channelName
                };

                foreach (long channelUserID in channelUsersIDs)
                {
                    Rlkt_LauncherLibServer.Clients.ClientInfo cInfo = Program.clients.findClientByUserID(channelUserID);

                    if (cInfo != null) // client is online
                    {
                        Server.Server.server.DispatchTo(cInfo.guid, new NetObject("ChannelOptionNotify", notifyPacket));
                    }
                }

            }

        }
        

    }
}
