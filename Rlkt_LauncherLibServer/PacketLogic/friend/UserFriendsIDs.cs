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
    class UserFriendsIDs
    {
        private Guid guid;
        private long userID;
        public List<long> listData = new List<long>();

        public UserFriendsIDs(long UID)
        {
            userID = UID;

            //SQL
            SqlCommand cmd = new SqlCommand("SPR_GetOnlineFriendIDs");

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@USERID", userID));
           
            Program.sql.Execute(cmd, this);

        }

        public void OnSqlResult(SqlDataReader rdr, SqlCommand cmd = null)
        {

            while (rdr.Read())
            {

                listData.Add(rdr.GetInt64(0));

            }

            rdr.Close();


        }
        

    }
}
