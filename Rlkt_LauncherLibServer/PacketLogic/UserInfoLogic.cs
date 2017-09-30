using NetSockets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rlkt_LauncherLibServer.PacketLogic
{
    class UserInfoLogic
    {
        private Guid guid;

        //When user requests it
        public UserInfoLogic(Packets.packet packet)
        {
            this.guid = packet.guid;
            SendUserInfo();
        }

        //When is requested by other classes and they have GUID
        public UserInfoLogic(Guid guid)
        {
            this.guid = guid;
            SendUserInfo();
        }

        public void SendUserInfo()
        {

            Int64 userId = Program.clients.getClientByID(guid).userId;

            //SQL
            SqlCommand cmd = new SqlCommand("SPR_GetUserInfo");

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@USERID", userId));

            Program.sql.Execute(cmd, this);
        }

        public void OnSqlResult(SqlDataReader rdr, SqlCommand cmd = null)
        {
            string accountname = "";
            string nickname = "";
            string email = "";
            long userID = new long();

            while(rdr.Read())
            {
                 accountname = rdr["AccountName"].ToString();
                 nickname    = rdr["NickName"].ToString();
                 email       = rdr["Email"].ToString();
                 userID = int.Parse(rdr["UserID"].ToString());
            }

            rdr.Close();
            
            Clients.ClientInfo client = Program.clients.getClientByID(guid);


            client.username = accountname;
            client.nickname = nickname;
            client.email = email;
            client.userId = userID;
            

            // update client info in service window
            Program.serviceWnd.UpdateClientInfo(client);

            if(nickname == "" || nickname == null)
            {       
                //check if nickname is set ,if its not set send for client nickname window to open:
                Console.WriteLine("User {0} must set nickname!!!", accountname);
                Server.Server.server.DispatchTo(guid, new NetObject("SC_WndCreateNickname", new Packets.Windows.SC_OpenNicknameWindow())); //data is empty cuz no needed.
            }


            Packets.Login.SCUserInfo packet = new Packets.Login.SCUserInfo();
            packet.userId = client.userId;
            packet.username = accountname;
            packet.nickname = nickname; //nickname will be empty if user did not choose any! 
            packet.email = email;
            Server.Server.server.DispatchTo(guid, new NetObject("SC_UserInfo", packet));


        }



    }
}
