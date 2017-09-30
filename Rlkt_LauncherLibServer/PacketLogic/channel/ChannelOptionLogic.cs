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
    class ChannelOptionLogic
    {
        private Guid guid;
        private long userID;
        private long chatID;

        public ChannelOptionLogic(Packets.packet packet)
        {
            //Set user GUID and username
            guid = packet.guid;
            userID = packet.data.userID;
            chatID = packet.data.chatID;

            //SQL
            SqlCommand cmd = new SqlCommand("SPR_LeaveChannel");

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@UserID", userID));
            cmd.Parameters.Add(new SqlParameter("@ChatID", chatID));
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


            Packets.Channel.ChannelOptionsRep result = new Packets.Channel.ChannelOptionsRep();

            result.status = (Enums.SqlResultReply)Convert.ToInt32(cmd.Parameters["@RET"].Value);
            result.chatID = chatID;

            Server.Server.server.DispatchTo(guid, new NetObject("ChannelOptionRep", result));


            

            //If SP has been success send notify to channel useres
            if (result.status == Enums.SqlResultReply.Success)
            {
                Packets.Channel.ChannelOptionsNotify notifyPacket = new Packets.Channel.ChannelOptionsNotify()
                {
                    chatID = this.chatID,
                    userID = this.userID,
                    status = Enums.Channel.ChannelUserNotify.Leave,
                    Nickname = Program.clients.findClientByUserID(this.userID).nickname
                };

                foreach (long channelUserID in channelUsersIDs)
                {
                    Rlkt_LauncherLibServer.Clients.ClientInfo cInfo = Program.clients.findClientByUserID(channelUserID);

                    if (cInfo != null && cInfo.userId != this.userID) // client is online and not sender ID
                    {
                        Server.Server.server.DispatchTo(cInfo.guid, new NetObject("ChannelOptionNotify", notifyPacket));
                    }
                }

            }

        }
        

    }
}
