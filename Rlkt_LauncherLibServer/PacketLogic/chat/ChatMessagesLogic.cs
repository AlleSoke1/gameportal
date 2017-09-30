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
    class ChatMessagesLogic
    {
        private Guid guid;
        private long userID1;
        private long userID2;
        private long chatID;
        private int pageNum;

        public ChatMessagesLogic(Packets.packet packet)
        {
            //Set user GUID and username
            guid = packet.guid;
            userID1 = packet.data.userID1;
            userID2 = packet.data.userID2;
            chatID = packet.data.chatID;
            pageNum = packet.data.pageNum;

            //SQL
            SqlCommand cmd = new SqlCommand("SPR_GetChatMessage");

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@UserID1", userID1));
            cmd.Parameters.Add(new SqlParameter("@UserID2", userID2));
            cmd.Parameters.Add(new SqlParameter("@ChatID", chatID));
            cmd.Parameters.Add(new SqlParameter("@PagingNumber", pageNum));
           
            Program.sql.Execute(cmd, this);

        }

        public void OnSqlResult(SqlDataReader rdr, SqlCommand cmd = null)
        {
            Packets.Chat.ChatMessagesList mList = new Packets.Chat.ChatMessagesList();
            mList.chatID = chatID;

            while (rdr.Read())
            {
                mList.ListData.Add(new Packets.Chat.ChatMessageReq() {
                    OwnerName = rdr["SenderNickName"].ToString(),
                    Message = rdr["Message"].ToString(),
                    SendDate = rdr.GetDateTime(2)
                });

            }

            rdr.Close();

            Server.Server.server.DispatchTo(guid, new NetObject("SC_GetMessageListRep", mList));

        }
        

    }
}
