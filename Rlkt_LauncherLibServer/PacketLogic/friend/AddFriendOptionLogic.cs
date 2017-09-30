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
    class AddFriendOptionLogic
    {
        private Guid guid;
        private long recieverID;
        private long senderID;
        private Enums.Friend.FriendRequestOptions selectedOption;

        public AddFriendOptionLogic(Packets.packet packet)
        {
            //Set user GUID and username
            guid = packet.guid;
            recieverID = packet.data.recieverID;
            senderID = packet.data.senderID;
            selectedOption = packet.data.selectedOption;

            //SQL
            SqlCommand cmd = new SqlCommand("SPR_ModFriendRequest");

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@RecieverUserID", recieverID));
            cmd.Parameters.Add(new SqlParameter("@SenderUserID", senderID));
            cmd.Parameters.Add(new SqlParameter("@Status", (int)selectedOption));
            cmd.Parameters.Add("@RET", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@ChatID", SqlDbType.TinyInt).Direction = ParameterDirection.Output;

            Program.sql.Execute(cmd, this);

        }

        public void OnSqlResult(SqlDataReader rdr, SqlCommand cmd = null)
        {

            rdr.Close();

            Packets.FriendsRequest.SCAddFriendRep result = new Packets.FriendsRequest.SCAddFriendRep()
            {
                status = (Enums.SqlResultReply)Convert.ToInt32(cmd.Parameters["@RET"].Value),
                friendID = senderID,
                selectedOption = selectedOption
            };
            if (selectedOption == Enums.Friend.FriendRequestOptions.Approve) result.chatID = Convert.ToInt64(cmd.Parameters["@ChatID"].Value);

            Server.Server.server.DispatchTo(guid, new NetObject("SC_FriendRequestOption", result));

            //If add has been success send notify to the friend if he online.
            if (result.status == Enums.SqlResultReply.Success && selectedOption == Enums.Friend.FriendRequestOptions.Approve)
            {
                Rlkt_LauncherLibServer.Clients.ClientInfo cInfo = Program.clients.findClientByUserID(senderID);

                if (cInfo != null)
                {
                    Packets.FriendsRequest.CSFriendAddedNotify notifyPacket = new Packets.FriendsRequest.CSFriendAddedNotify()
                    {
                        friendID = recieverID,
                        friendName = Program.clients.findClientByUserID(recieverID).nickname
                    };

                    Server.Server.server.DispatchTo(cInfo.guid, new NetObject("SC_FriendAddedNotify", notifyPacket));
                }
            }
        }
        

    }
}
