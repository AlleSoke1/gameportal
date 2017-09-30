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
    class CreateChannelLogic
    {
        private Guid guid;
        private long ownerID;
        private string channelName;

        public CreateChannelLogic(Packets.packet packet)
        {
            //Set user GUID and username
            guid = packet.guid;
            ownerID = packet.data.ownerID;
            channelName = packet.data.channelName;

            //SQL
            SqlCommand cmd = new SqlCommand("SPR_CreateChannelChat");

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@UserID", ownerID));
            cmd.Parameters.Add(new SqlParameter("@ChatName", channelName));
            cmd.Parameters.Add("@RET", SqlDbType.TinyInt).Direction = ParameterDirection.Output;

            Program.sql.Execute(cmd, this);

        }

        public void OnSqlResult(SqlDataReader rdr, SqlCommand cmd = null)
        {

            rdr.Close();

            // if result = 0 so it's undentified error if more then 0 so it's chatID
            int result = Convert.ToInt32(cmd.Parameters["@RET"].Value);

            Server.Server.server.DispatchTo(guid, new NetObject("CreateChannelRep", (object)result));

        }
        

    }
}
