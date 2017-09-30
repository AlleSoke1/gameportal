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
    class AddFriendLogic
    {
        private Guid guid;
        private long senderID;
        private string senderName;
        private string recieverName;

        public AddFriendLogic(Packets.packet packet)
        {
            //Set user GUID and username
            guid = packet.guid;
            senderID = packet.data.senderID;
            senderName = packet.data.senderName;
            recieverName = packet.data.recieverName;

            //SQL
            SqlCommand cmd = new SqlCommand("SPR_AddFriendRequest");

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@SenderUserID", senderID));
            cmd.Parameters.Add(new SqlParameter("@RecieverNickName", recieverName));
            cmd.Parameters.Add(new SqlParameter("@SenderNickName", senderName));
            cmd.Parameters.Add("@RET", SqlDbType.TinyInt).Direction = ParameterDirection.Output;

            Program.sql.Execute(cmd, this);

        }

        public void OnSqlResult(SqlDataReader rdr, SqlCommand cmd = null)
        {

            rdr.Close();

            Enums.Friend.AddFriendResult result = (Enums.Friend.AddFriendResult)Convert.ToInt32(cmd.Parameters["@RET"].Value);

            Server.Server.server.DispatchTo(guid, new NetObject("SC_AddFriendRep", (object)result));

            //If add has been success send notify to the friend if he online.
            if ((Enums.Friend.AddFriendResult)result == Enums.Friend.AddFriendResult.Successfull)
            {
                Rlkt_LauncherLibServer.Clients.ClientInfo cInfo = Program.clients.findClientByNickname(recieverName);

                if (cInfo != null)
                {
                    Packets.FriendsRequest.CSAddFriendNotify notifyPacket = new Packets.FriendsRequest.CSAddFriendNotify()
                    {
                        senderID = senderID,
                        senderName = senderName
                    };

                    Server.Server.server.DispatchTo(cInfo.guid, new NetObject("SC_AddFriendNotify", notifyPacket));
                }

            }
        }
        

    }
}
