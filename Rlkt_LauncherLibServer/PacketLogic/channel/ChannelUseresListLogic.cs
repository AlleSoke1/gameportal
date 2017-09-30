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
    class ChannelUseresListLogic
    {
        private Guid guid;
        private long chatID;

        public ChannelUseresListLogic(Packets.packet packet)
        {
            //Set user GUID and username
            guid = packet.guid;
            chatID = packet.data;

            //SQL
            SqlCommand cmd = new SqlCommand("SPR_GetChannelMembers");

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@ChatID", chatID));
           
            Program.sql.Execute(cmd, this);

        }

        public void OnSqlResult(SqlDataReader rdr, SqlCommand cmd = null)
        {
            Packets.Channel.ChannelUsersListRep cList = new Packets.Channel.ChannelUsersListRep();
            cList.chatID = chatID;

            while (rdr.Read())
            {
                Packets.Channel.ChannelUserInfo uInfo = new Packets.Channel.ChannelUserInfo();
                uInfo.userID = rdr.GetInt64(0);
                uInfo.nickName = rdr.GetString(1);
                cList.ListData.Add(uInfo);

            }

            rdr.Close();

            Server.Server.server.DispatchTo(guid, new NetObject("ChannelUseresListRep", cList));

        }
        

    }
}
