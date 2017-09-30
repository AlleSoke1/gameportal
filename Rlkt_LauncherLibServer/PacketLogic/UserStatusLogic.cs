using NetSockets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Rlkt_LauncherLibServer.PacketLogic
{
    class UserStatusLogic
    {
        private Guid guid;
        private long userID;
        private Enums.UserInfo.UserStatus status;

        public UserStatusLogic(Packets.packet packet)
        {
            guid = packet.guid;
            userID = (long)packet.data.userID;
            status = packet.data.userStatus;


            //SQL
            SqlCommand cmd = new SqlCommand("SPR_ModUserStatus");

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@UserID", userID));

            cmd.Parameters.Add(new SqlParameter("@Status", (int)status));

            cmd.Parameters.Add("@RET", SqlDbType.TinyInt).Direction = ParameterDirection.Output;

            Program.sql.Execute(cmd, this);


            
        }


        public void OnSqlResult(SqlDataReader rdr, SqlCommand cmd = null)
        {
            Console.WriteLine("User {0} changed status to {1}!", Program.clients.getClientByID(guid).nickname, this.status);


            var clientInfo = Program.clients.getClientByID(this.guid);
            clientInfo.userStatus = this.status;

            // update client info in service window
            Program.serviceWnd.UpdateClientInfo(clientInfo);



            Packets.UserInfo.CSUserStatus notifyPacket = new Packets.UserInfo.CSUserStatus() { 
                userID = this.userID,
                userStatus = this.status,
                IsLogin = false
            };

            //If SP has been success send notify to friends if they online.
            if ((Enums.SqlResultReply)Convert.ToInt32(cmd.Parameters["@RET"].Value) == Enums.SqlResultReply.Success)
            {
                while (rdr.Read())
                {
                    Rlkt_LauncherLibServer.Clients.ClientInfo cInfo = Program.clients.findClientByUserID((long)rdr["FriendID"]);

                    if (cInfo != null)
                    {
                        Server.Server.server.DispatchTo(cInfo.guid, new NetObject("SC_UserStatusNotify", notifyPacket));
                    }
                }

            }



            rdr.Close();
        }
    }
}
