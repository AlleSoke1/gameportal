using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets.Channel
{

    #region Actions

        [Serializable]
        public class CreateChannelReq
        {
            public long ownerID;
            public string channelName;
        
        }

        [Serializable]
        public class ChannelOptions
        {
            public long userID;
            public long chatID;
            public string channelName;
            public Enums.Channel.ChannelOptions selectedOption;

        }

        [Serializable]
        public class ChannelOptionsRep
        {
            public long chatID;
            public Enums.SqlResultReply status;
        }

        [Serializable]
        public class ChannelOptionsNotify
        {
            public long chatID;
            public long userID;
            public string Nickname;
            public string ChannelName;
            public Enums.Channel.ChannelUserNotify status;
        }

        [Serializable]
        public class ChannelInviteReq
        {
            public long senderID;
            public long chatID;
            public string inviteNickName;
            public string channelName;
        }

    #endregion

    #region Users Info
        [Serializable]
        public class ChannelUsersListReq
        {
            public long userID1;
            public long userID2;
            public long chatID;
            public int pageNum;
        }

        [Serializable]
        public class ChannelUsersListRep
        {
            public List<ChannelUserInfo> ListData = new List<ChannelUserInfo>();
            public long chatID;
        }

        [Serializable]
        public class ChannelUserInfo
        {
            public long userID;
            public string nickName;

            public ChannelUserInfo()
            {
                nickName = "";
            }
        }
    #endregion

        

}
