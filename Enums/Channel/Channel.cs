using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums.Channel
{

    public enum ChannelOptions : int
    {
        Chat = 0,
        Remove = 2
    }

    public enum InviteChannelReply : int
    {
        Successfull = 0,
        Undentified = 1,
        UserInChannel = 2,
        EmptyName = 3,
        UserNotExists = 4,
        UserNotFriend = 5,
    }

    public enum ChannelUserNotify : int
    {
        Join = 0,
        Leave = 1
    }
}
