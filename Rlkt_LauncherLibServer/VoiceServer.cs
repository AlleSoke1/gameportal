using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rlkt_LauncherLibServer
{
    public partial class VoiceServer : Form
    {

        private int voipChannels = 0;

        public VoiceServer()
        {
            InitializeComponent();
        }


        #region UI Funtions
        private void serverOnBtn_Click(object sender, EventArgs e)
        {
            UpdateServerStatus(true);
        }

        private void serverOffBtn_Click(object sender, EventArgs e)
        {
            UpdateServerStatus(false);
        }


        public void UpdateVoipChannelsCount(int number)
        {

            if (voipChannelsCounter.InvokeRequired)
            {
                voipChannelsCounter.Invoke((MethodInvoker)delegate
                {
                    UpdateVoipChannelsCount(number);
                });
            }
            else
            {
                voipChannelsCounter.Text = "Voip Channels:  " + number;
            }

        }

        public void UpdateServerStatus(bool isOnline)
        {

            if (isOnline)
            {
                serverStatus.Text = "Online";
                serverStatus.ForeColor = Color.LimeGreen;

                return;
            }

            serverStatus.Text = "Offline";
            serverStatus.ForeColor = Color.Red;

        }


        #region  Voip Data Grid Functions

        internal void RemoveChannelByID(int guid)
        {
            if (voipChannelList.InvokeRequired)
            {
                voipChannelList.Invoke((MethodInvoker)delegate
                {
                    RemoveChannelByID(guid);
                });
                return;
            }

            for (int v = 0; v < voipChannelList.Rows.Count; v++)
            {
                if (string.Equals(voipChannelList[0, v].Value as string, guid.ToString()))
                {
                    voipChannelList.Rows.RemoveAt(v);
                    break;
                }
            }

            voipChannels--;
            UpdateVoipChannelsCount(voipChannels);
        }

        internal void AddChannel(Rlkt_LauncherLibServer.Voip.ChannelInfo tempChannel)
        {
            if (tempChannel == null) return;

            if (voipChannelList.InvokeRequired)
            {
                voipChannelList.Invoke((MethodInvoker)delegate
                {
                    AddChannel(tempChannel);
                });
                return;
            }

            string usersListStr = "";

            foreach (long uID in tempChannel.MemberIDs)
            {
                usersListStr += uID.ToString() + ", ";
            }

            voipChannelList.Rows.Add(tempChannel.channelID, tempChannel.channelName, usersListStr);


            voipChannels++;
            UpdateVoipChannelsCount(voipChannels);
        }

        internal void UpdateChannelInfo(Rlkt_LauncherLibServer.Voip.ChannelInfo tempChannel)
        {
            foreach (DataGridViewRow row in voipChannelList.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(tempChannel.channelID.ToString()))
                {
                    string usersListStr = "";

                    foreach (long uID in tempChannel.MemberIDs)
                    {
                        usersListStr += uID.ToString() + ", ";
                    }

                    row.Cells[0].Value = tempChannel.channelID.ToString();
                    row.Cells[1].Value = tempChannel.channelName.ToString();
                    row.Cells[2].Value = usersListStr;


                    break;
                }
            }
        }

        #endregion

        #endregion

     

        private void ServiceWindow_Shown(object sender, EventArgs e)
        {
           
        }





    }
}
