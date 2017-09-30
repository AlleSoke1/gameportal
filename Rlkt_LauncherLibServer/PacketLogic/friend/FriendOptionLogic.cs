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
    class FriendOptionLogic
    {
        private Guid guid;
        private long userID;
        private long handlerUserID;
        private Enums.Friend.FriendOptions selectedOption;

        public FriendOptionLogic(Packets.packet packet)
        {
            //Set user GUID and username
            guid = packet.guid;
            userID = packet.data.userID;
            handlerUserID = packet.data.handlerUserID;
            selectedOption = packet.data.selectedOption;

            SqlCommand cmd = new SqlCommand();
            string sqlVar2 = "";

            //SQL
            switch (selectedOption)
            {
                case Enums.Friend.FriendOptions.Block:
                    cmd.CommandText = "SPR_BlockUser";
                    sqlVar2 = "@BlockedUserID";
                    break;
                case Enums.Friend.FriendOptions.Remove:
                    cmd.CommandText = "SPR_RemoveFriend";
                    sqlVar2 = "@FriendUserID";
                    break;
            }

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@UserID", userID));
            cmd.Parameters.Add(new SqlParameter(sqlVar2, handlerUserID));
            cmd.Parameters.Add("@RET", SqlDbType.TinyInt).Direction = ParameterDirection.Output;

            Program.sql.Execute(cmd, this);

        }

        public void OnSqlResult(SqlDataReader rdr, SqlCommand cmd = null)
        {

            rdr.Close();

            Enums.SqlResultReply result = (Enums.SqlResultReply)Convert.ToInt32(cmd.Parameters["@RET"].Value);


            Packets.Friends.FriendOptionRep replyPacket = new Packets.Friends.FriendOptionRep() {
                userID = handlerUserID,
                sqlReply = result
            };

            Server.Server.server.DispatchTo(guid, new NetObject("SC_FriendOption", replyPacket));

            //If success send notify to the friend if he online to delete user from his list
            if ((Enums.SqlResultReply)result == Enums.SqlResultReply.Success)
            {
                Rlkt_LauncherLibServer.Clients.ClientInfo cInfo = Program.clients.findClientByUserID(handlerUserID);

                if (cInfo != null)
                {
                    Server.Server.server.DispatchTo(cInfo.guid, new NetObject("SC_RemoveFriendNotify", (object)userID));
                }
            }
        }
        

    }
}
