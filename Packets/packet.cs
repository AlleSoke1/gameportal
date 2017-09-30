using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets
{
    [Serializable]
    public class packet
    {
        public Guid guid;
        public dynamic data;

        public packet(Guid id, dynamic objectData)
        {
            guid = id;
            data = objectData;
        }
    }
}
