using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets.Friends
{

    [Serializable]
    public class SCFriendList
    {
        public List<SCFriendInfo> ListData = new List<SCFriendInfo>();

    }
    [Serializable]
    public class SCFriendInfo
    {
        public long Id;
        public string NickName;
        public DateTime Date;
        public bool isRequest;
        public Enums.UserInfo.UserStatus Status;
        public long ChatId;
        public bool isChannel;

        public SCFriendInfo()
        {
            NickName = "";
        }
    }


    [Serializable]
    public class CSFriendOption
    {
        public long userID;
        public long handlerUserID;
        public Enums.Friend.FriendOptions selectedOption;
        public string friendName;
        public long chatID;
    }

    [Serializable]
    public class FriendOptionRep
    {
        public long userID;
        public Enums.SqlResultReply sqlReply;
    }

}

namespace Packets.FriendsRequest
{

    [Serializable]
    public class CSAddFriendReq
    {
        public long senderID;
        public string senderName;
        public string recieverName;

        public CSAddFriendReq()
        {
            senderName = "";
            recieverName = "";
        }
    }

    [Serializable]
    public class SCAddFriendRep
    {
        public long friendID;
        public Enums.SqlResultReply status;
        public long chatID;
        public Enums.Friend.FriendRequestOptions selectedOption;

    }

    [Serializable]
    public class CSAddFriendNotify
    {
        public long senderID;
        public string senderName;

        public CSAddFriendNotify()
        {
            senderName = "";
        }
    }

    [Serializable]
    public class CSFriendAddedNotify
    {
        public long friendID;
        public string friendName;

        public CSFriendAddedNotify()
        {
            friendName = "";
        }
    }


    [Serializable]
    public class CSFriendRequestOption
    {
        public long recieverID;
        public long senderID;
        public Enums.Friend.FriendRequestOptions selectedOption;
    }

}