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

    }

    [Serializable]
    public class VoiceChatRequestResult
    {
        public long chatID;
        public Enums.Voip.CallResultReply Result;
    }



}
