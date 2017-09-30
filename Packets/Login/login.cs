using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets.Login
{
 

    [Serializable]
    public class CSLoginPacket
    {
        public string username;
        public string password;
    }

    [Serializable]
    public class SCLoginPacket
    {
        public Enums.Login.LoginResult status;
    }

    [Serializable]
    public class LogoutNotfiyPacket
    {
        public long userID;
    }
}
