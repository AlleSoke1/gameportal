using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums.Friend
{
    public enum AddFriendResult : int
    {
        Successfull = 0,
        UserBlocked = 1,
        AlreadyFriend = 2,
        Undentified = 3,
        UserNotExists = 4,
        AlreadyRequested = 5,
    }

    public enum FriendRequestOptions : int
    {
        Approve = 0,
        Block = 1,
        Ignore = 2
    }

    public enum FriendOptions : int
    {
        Chat = 0,
        Block = 1,
        Remove = 2
    }


}
