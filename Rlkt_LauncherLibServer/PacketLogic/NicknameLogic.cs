using NetSockets;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Rlkt_LauncherLibServer.PacketLogic
{
    class NicknameLogic
    {
        private Guid guid;
        private string tempNickname;

        public NicknameLogic(Packets.packet packet)
        {
            //Set user GUID
            guid = packet.guid;

            //SQL
            SqlCommand cmd = new SqlCommand("SPR_SetUserNickName");

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@USERID", Program.clients.getClientByID(guid).userId));

            cmd.Parameters.Add(new SqlParameter("@NICKNAME", packet.data.nickname));

            cmd.Parameters.Add("@RET", SqlDbType.TinyInt).Direction = ParameterDirection.Output;

            tempNickname = packet.data.nickname;

            Program.sql.Execute(cmd, this);
        }

        public void OnSqlResult(SqlDataReader rdr, SqlCommand cmd = null)
        {
            rdr.Close();

            int result =  Convert.ToInt32(cmd.Parameters["@RET"].Value);

            //Create new packet.
            Packets.Login.SCcreateNickName packetReply = new Packets.Login.SCcreateNickName();
            //
            packetReply.result = (Enums.Login.NicknameResult)result;

            if(packetReply.result == Enums.Login.NicknameResult.Successfull)
            {
                packetReply.nickname = tempNickname;
            }

            //
            Server.Server.server.DispatchTo(guid, new NetObject("SC_LoginCreateNickName", packetReply));

            //Set nickname in server.
            if (Enums.Login.NicknameResult.Successfull == (Enums.Login.NicknameResult)result)
            {
                Console.WriteLine("User {1} has set his nickname to {0}", tempNickname, Program.clients.getClientByID(guid).username);
                Program.clients.getClientByID(guid).nickname = tempNickname;
            }
        }
        
    }
}
