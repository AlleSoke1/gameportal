using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets.Chat
{

    [Serializable]
    public class ChatMessageReq
    {
        public long senderUserID;
        public long reciverUserID;
        public long chatID;
        public string OwnerName;
        public string Message;
        public DateTime SendDate;
        public bool isChannel;
        public string channelName;

        public ChatMessageReq()
        {
            OwnerName = "";
            Message = "";
            channelName = "";
        }
    }

    [Serializable]
    public class ChatMessageRep
    {
        public long reciverUserID;
        public Enums.SqlResultReply sendingStatus;
        public string OwnerName;
        public string Message;
        public DateTime SendDate;
        public long chatID;
        public bool isChannel;
        public string channelName;

        public ChatMessageRep()
        {
            OwnerName = "";
            Message = "";
            channelName = "";
        }

    }



    [Serializable]
    public class ChatMessagesList
    {
        public List<ChatMessageReq> ListData = new List<ChatMessageReq>();
        public long chatID;

        public ChatMessagesList()
        {
            
        }

    }

    [Serializable]
    public class ChatMessageListReq
    {
        public long userID1;
        public long userID2;
        public long chatID;
        public int pageNum;
    }

}
