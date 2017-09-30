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
    class FriendListLogic
    {
        private Guid guid;
        private long userID;

        public FriendListLogic(Packets.packet packet)
        {
            //Set user GUID and username
            guid = packet.guid;
            userID = packet.data;

            //SQL
            SqlCommand cmd = new SqlCommand("SPR_GetFriendList");

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@USERID", userID));
           
            Program.sql.Execute(cmd, this);

        }

        public void OnSqlResult(SqlDataReader rdr, SqlCommand cmd = null)
        {
            Packets.Friends.SCFriendList fList = new Packets.Friends.SCFriendList();

            while (rdr.Read())
            {

                Packets.Friends.SCFriendInfo uInfo = new Packets.Friends.SCFriendInfo();
                uInfo.Id = rdr.GetInt64(0);
                uInfo.NickName = rdr.GetString(1);
                uInfo.Date = rdr.IsDBNull(2) ? DateTime.Now : rdr.GetDateTime(2);
                uInfo.Status = Program.clients.findClientByUserID(uInfo.Id) == null ? Enums.UserInfo.UserStatus.Disconnected : Program.clients.findClientByUserID(uInfo.Id).userStatus;
                uInfo.isRequest = (int)rdr["IsRequest"] == 0 ? false: true;
                uInfo.ChatId = (int)rdr["ChatID"];
                uInfo.isChannel = (int)rdr["IsChannel"] == 0 ? false : true;
                fList.ListData.Add(uInfo);

            }

            rdr.Close();

            Server.Server.server.DispatchTo(guid, new NetObject("SC_FrinedListRep", fList));

        }
        

    }
}
