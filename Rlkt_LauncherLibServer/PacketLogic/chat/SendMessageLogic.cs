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
    class SendMessageLogic
    {
        private Guid guid;
        private long senderUserID;
        private long reciverUserID;
        private long chatID;
        private string OwnerName;
        private string Message;
        private DateTime SendDate;
        private bool isChannel;
        private string channelName;

        public SendMessageLogic(Packets.packet packet)
        {
            //Set user GUID and username
            guid = packet.guid;
            senderUserID = packet.data.senderUserID;
            reciverUserID = packet.data.reciverUserID;
            chatID = packet.data.chatID;
            OwnerName = packet.data.OwnerName;
            Message = packet.data.Message;
            SendDate = DateTime.Now;
            isChannel = packet.data.isChannel;
            channelName = packet.data.channelName;

            //SQL
            SqlCommand cmd = new SqlCommand("SPR_AddChatMessage");

            // 2. set the command object so it knows to execute a stored procedure
            cmd.CommandType = CommandType.StoredProcedure;

            // 3. add parameter to command, which will be passed to the stored procedure
            cmd.Parameters.Add(new SqlParameter("@FromID", senderUserID));
            cmd.Parameters.Add(new SqlParameter("@ToID", reciverUserID));
            cmd.Parameters.Add(new SqlParameter("@ChatID", chatID));
            cmd.Parameters.Add(new SqlParameter("@MessageText", Message));
            cmd.Parameters.Add(new SqlParameter("@SendDate", SendDate));
            cmd.Parameters.Add(new SqlParameter("@IsChannel", isChannel));
            cmd.Parameters.Add("@RET", SqlDbType.TinyInt).Direction = ParameterDirection.Output;

            Program.sql.Execute(cmd, this);

        }

        public void OnSqlResult(SqlDataReader rdr, SqlCommand cmd = null)
        {

           

            Packets.Chat.ChatMessageRep result = new Packets.Chat.ChatMessageRep();

            result.reciverUserID = reciverUserID;
            result.sendingStatus = (Enums.SqlResultReply)Convert.ToInt32(cmd.Parameters["@RET"].Value);
            result.OwnerName = OwnerName;
            result.Message = Message;
            result.SendDate = SendDate;
            result.chatID = chatID;

            Server.Server.server.DispatchTo(guid, new NetObject("SC_SendMessageRep", result));


            //If add has been success send notify to the friend if he online.
            if (result.sendingStatus == Enums.SqlResultReply.Success)
            {
                if (isChannel)
                {
                    while (rdr.Read())
                    {
                        Rlkt_LauncherLibServer.Clients.ClientInfo cInfo = Program.clients.findClientByUserID((long)rdr["UserID"]);

                        if (cInfo != null && cInfo.userId != senderUserID) // client is online and not sender ID
                        {
                            Packets.Chat.ChatMessageRep notifyPacket = new Packets.Chat.ChatMessageRep()
                            {
                                reciverUserID = this.reciverUserID,
                                sendingStatus = Enums.SqlResultReply.Success,
                                OwnerName = this.OwnerName,
                                Message = this.Message,
                                SendDate = this.SendDate,
                                chatID = this.chatID,
                                isChannel = this.isChannel,
                                channelName = this.channelName
                            };

                            Server.Server.server.DispatchTo(cInfo.guid, new NetObject("SC_SendMessageNotify", notifyPacket));
                        }
                    }
                }
                else
                {
                    Rlkt_LauncherLibServer.Clients.ClientInfo cInfo = Program.clients.findClientByUserID(reciverUserID);

                    if (cInfo != null) // client is online
                    {
                        Packets.Chat.ChatMessageRep notifyPacket = new Packets.Chat.ChatMessageRep()
                        {
                            reciverUserID = this.reciverUserID,
                            sendingStatus = Enums.SqlResultReply.Success,
                            OwnerName = this.OwnerName,
                            Message = this.Message,
                            SendDate = this.SendDate,
                            chatID = this.chatID
                        };

                        Server.Server.server.DispatchTo(cInfo.guid, new NetObject("SC_SendMessageNotify", notifyPacket));
                    }
                }
                

            }



            rdr.Close();
        }
        

    }
}
