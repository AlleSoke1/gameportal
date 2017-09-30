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
    class LogoutLogic
    {
        private Guid guid;
        private long userID;

        public LogoutLogic(Guid gID)
        {
            ////
            //Server.Server.server.DisconnectClient(packet.guid);
            ////

            // Send Logout Notfiy to Friends
            guid = gID;
            Rlkt_LauncherLibServer.Clients.ClientInfo cInfo = Program.clients.findClientByGuID(guid);
            userID = cInfo.userId;

            Console.WriteLine(string.Format("User [(0)] Has Logout.", cInfo.username));

            SqlCommand cmd = new SqlCommand("SPR_LogOut");

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure

            cmd.Parameters.Add(new SqlParameter("@USERID", this.userID));
            cmd.Parameters.Add("@RET", SqlDbType.TinyInt).Direction = ParameterDirection.Output;

            Program.sql.Execute(cmd, this);

        }

        public void OnSqlResult(SqlDataReader rdr, SqlCommand cmd = null)
        {
            // get online friends list ID's to send frined loing notify 

            Packets.UserInfo.CSUserStatus notifyPacket = new Packets.UserInfo.CSUserStatus()
            {
                userID = this.userID,
                userStatus = Enums.UserInfo.UserStatus.Disconnected,
                IsLogin = false
            };

            while (rdr.Read())
            {
                Rlkt_LauncherLibServer.Clients.ClientInfo cInfo = Program.clients.findClientByUserID((long)rdr["FriendID"]);

                if (cInfo != null)
                {
                    Server.Server.server.DispatchTo(cInfo.guid, new NetObject("SC_UserStatusNotify", notifyPacket));
                }
            }

            rdr.Close();




        }
    }
}
