using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets.Login
{
    [Serializable]
    public class handshake
    {
        public Guid guid;
        public handshake(Guid id)
        {
            guid = id;
        }
    }
}
