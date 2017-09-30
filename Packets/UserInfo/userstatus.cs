using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets.UserInfo
{
    [Serializable]
    public class CSUserStatus
    {
        public long userID;
        public Enums.UserInfo.UserStatus userStatus;
        public bool IsLogin;
        public string Nickname;

    }
}
