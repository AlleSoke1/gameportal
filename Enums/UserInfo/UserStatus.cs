using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enums.UserInfo
{
    public enum UserStatus : int
    {
        Disconnected = 0,
        Available = 1,
        Away = 2,
        Busy = 3,
        Invisible = 4,
    }
}
