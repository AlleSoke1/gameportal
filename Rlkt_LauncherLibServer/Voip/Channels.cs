using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rlkt_LauncherLibServer.Voip
{
    public class cChannels
    {
        // channel list to save voice channels members and distribution recovered voices (Dictionary<long channelID, ChannelInfo > )
        private Dictionary<long, ChannelInfo> _ChannelList = new Dictionary<long, ChannelInfo>();

        public cChannels()
        {

        }

        #region Channel Actions

        public long Create(long cID)
        {
            string cName = string.Format("Chat_{0}", cID);

            if (!_ChannelList.ContainsKey(cID))
            {
                _ChannelList.Add(cID, new ChannelInfo() { channelName = cName });
                //new Thread(() => { Program.voipwWapper.CreateChannel(cName, null); }).Start();
            }
            else
            {

                return this.Create(cID);
            }

            return cID;
        }

        public bool Remove(long cID)
        {
            if (!_ChannelList.ContainsKey(cID)) return false;

            //new Thread(() => { Program.voipwWapper.DeleteChannel(_ChannelList[cID].channelName); }).Start();
            _ChannelList.Remove(cID);


            return true;
        }

        public bool AddMember(long channelID, long memberID)
        {
            if (_ChannelList.ContainsKey(channelID))
            {
                if (_ChannelList[channelID].MemberIDs.Contains(memberID))
                    return false;

                _ChannelList[channelID].MemberIDs.Add(memberID);
                return true;
            }

            return false;
        }

        public bool RemoveMember(long channelID, long memberID)
        {
            if (!_ChannelList[channelID].MemberIDs.Contains(memberID))
                return false;

            _ChannelList[channelID].MemberIDs.Remove(memberID);
            return true;
        }
        #endregion

        public ChannelInfo GetChannel(long channelID)
        {
            if (_ChannelList.ContainsKey(channelID))
                return _ChannelList[channelID];

            return null;
        }

        public List<long> GetMemberIDs(long channelID)
        {
            if (_ChannelList.ContainsKey(channelID))
                return _ChannelList[channelID].MemberIDs;

            return null;
        }

        public int GetMemeberCount(long channelID)
        {
            if (_ChannelList.ContainsKey(channelID))
                return _ChannelList[channelID].MemberIDs.Count;

            return 0;
        }

        public void SetCallHostIp(long channelID, string ip)
        {
            if (_ChannelList.ContainsKey(channelID))
                 _ChannelList[channelID].CallHostIp = ip;
        }
    }


    #region chnanel enum

    [Serializable]
    public class ChannelInfo
    {
        public int channelID;
        public string channelName = "";
        public string CallHostIp = "";
        public List<long> MemberIDs = new List<long>();

    }
    #endregion
}
