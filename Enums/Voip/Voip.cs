using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums.Voip

{

    public enum CallResultReply : int
    {
        Success = 0,
        Failed = 1,
        FriendOffline = 2,
        FriendOnCall = 3

    }

}
