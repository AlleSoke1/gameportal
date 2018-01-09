using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets.Chat
{

    [Serializable]
    public class VoiceChatRequest
    {
        public long senderUserID;
        public long reciverUserID;
        public long chatID;
        public string CallIpHost;

    }

    [Serializable]
    public class VoiceChatRequestResult
    {
        public long chatID;
        public string replyUserName;
        public long voipCID;
        public Enums.Voip.CallResultReply Result;
        public string CallHostIp = "";
    }

    [Serializable]
    public class VoiceChatRequestReply
    {
        public long senderUserID;
        public long reciverUserID;
        public long chatID;
        public Enums.Voip.CallResultReply requestReply;

    }

    [Serializable]
    public class VoiceChatLeaveCall
    {
        public long userID;
        public string userNickname;
        public long chatID;
        public long voipChannelID;
        public bool endCallFlag;

    }



}
