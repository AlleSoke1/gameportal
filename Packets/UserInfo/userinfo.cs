using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets.Login
{
    [Serializable]
    public class SCUserInfo
    {
        public Int64 userId;
        public string username;
        public string email;
        public string nickname;
    }
}
