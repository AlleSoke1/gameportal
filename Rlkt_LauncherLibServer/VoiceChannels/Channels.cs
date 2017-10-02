using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rlkt_LauncherLibServer.VoiceChannels
{
    public class cChannels
    {
        // channel list to save voice channels members and distribution recovered voices (Dictionary<long channelID, ChannelInfo > )
        private Dictionary<int, ChannelInfo> _ChannelList = new Dictionary<int, ChannelInfo>();

        public cChannels()
        {

        }


        

        #region Channel Actions

        public bool Create(string cName)
        {
            int cID = _ChannelList.Last().Key + 1;

            if (!_ChannelList.ContainsKey(cID))
            {
                _ChannelList.Add(cID, new ChannelInfo() { channelName = cName });
            }
            else
            {
                return this.Create(cName);
            }
            
            return true;
        }

        public bool Remove(int cID)
        {
            if (!_ChannelList.ContainsKey(cID)) return false;

            _ChannelList.Remove(cID);

            return true;
        }

        public bool AddMember(int channelID, long memberID)
        {
            if (_ChannelList[channelID].MemberIDs.Contains(memberID)) 
                return false;

           _ChannelList[channelID].MemberIDs.Add(memberID);
            return true;
        }

        public bool RemoveMember(int channelID, long memberID)
        {
            if (!_ChannelList[channelID].MemberIDs.Contains(memberID))
                return false;

            _ChannelList[channelID].MemberIDs.Remove(memberID);
            return true;
        }
        #endregion
    }


    #region chnanel enum

    [Serializable]
    public class ChannelInfo
    {
        public string channelName;
        public List<long> MemberIDs;

        public ChannelInfo()
        {
            channelName = "";

        }
    }
    #endregion
}
