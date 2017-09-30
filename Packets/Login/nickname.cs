using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets.Login
{


    [Serializable]
    public class CScreateNickName
    {
       public string nickname;
       public string username;
    }

    [Serializable]
    public class CSchangeNickName
    {
       public string username;
       public string nickname;
       public string oldnickname;
    }

    [Serializable]
    public  class SCcreateNickName
    {
        public Enums.Login.NicknameResult result;
        public string nickname;
    }

    [Serializable]
    public class SCchangeNickName
    {
       public Enums.Login.NicknameResult result;
    }
}
