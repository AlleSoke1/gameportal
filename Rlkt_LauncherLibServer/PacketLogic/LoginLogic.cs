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
    class LoginLogic
    {
        private Guid guid;
        private string username = "";

        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public LoginLogic(Packets.packet packet)
        {
            //Set user GUID and username
            guid = packet.guid;
            username = packet.data.username;

            Console.WriteLine(this.ToString() + " user:" + packet.data.username + " pass:" + packet.data.password);


            //SQL
            SqlCommand cmd = new SqlCommand("SPR_Login");

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            if(!IsValidEmail(packet.data.username)) //Is Email 
            {
                cmd.Parameters.Add(new SqlParameter("@AccountName", packet.data.username));
            }
            else {
                cmd.Parameters.Add(new SqlParameter("@Email", packet.data.username));
            }
           
            cmd.Parameters.Add(new SqlParameter("@Password", packet.data.password));

            cmd.Parameters.Add("@USERID", SqlDbType.BigInt).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@NICKNAME", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("@RET", SqlDbType.TinyInt).Direction = ParameterDirection.Output;

            Program.sql.Execute(cmd, this);

        }

        public void OnSqlResult(SqlDataReader rdr, SqlCommand cmd = null)
        {
            // get online friends list ID's to send frined loing notify 
            List<long> onFriendList = new List<long>();
            while (rdr.Read())
            {
                onFriendList.Add((long)rdr["FriendID"]);
            }

            rdr.Close();

            int result = Convert.ToInt32(cmd.Parameters["@RET"].Value);

 
            //Create new packet.
            Packets.Login.SCLoginPacket packetReply = new Packets.Login.SCLoginPacket();

            packetReply.status = (Enums.Login.LoginResult)result;
          
            Server.Server.server.DispatchTo(guid, new NetObject("SC_Login", packetReply));


            //If user is successfully authed.
            if ((Enums.Login.LoginResult)result == Enums.Login.LoginResult.Successfull)
            {
                Int64 userId = Convert.ToInt64(cmd.Parameters["@USERID"].Value);
                string uNickname = cmd.Parameters["@NICKNAME"].Value.ToString();

                //Set client username and status to connected!
                Clients.ClientInfo client = Program.clients.getClientByID(guid);
                client.username = username;
                client.state    = Clients.ClientState.Authed;
                client.userId   = userId;
                client.userStatus = Enums.UserInfo.UserStatus.Available;


                //Send UserInfo 
                new UserInfoLogic(guid);

                //Send Game List
                Program.gamesInfo.sendGameListPacket(guid);


                Packets.UserInfo.CSUserStatus notifyPacket = new Packets.UserInfo.CSUserStatus()
                {
                    userID = userId,
                    userStatus = Enums.UserInfo.UserStatus.Available,
                    IsLogin = true,
                    Nickname = uNickname
                };

                foreach (long friendID in onFriendList)
                {
                    Rlkt_LauncherLibServer.Clients.ClientInfo cInfo = Program.clients.findClientByUserID(friendID);

                    if (cInfo != null)
                    {
                        Server.Server.server.DispatchTo(cInfo.guid, new NetObject("SC_UserStatusNotify", notifyPacket));
                    }
                }
            }

        }
        

    }
}
