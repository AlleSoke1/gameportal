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
    public partial class ServiceWindow : Form
    {

        private int connectedClient = 0;

        public ServiceWindow()
        {
            InitializeComponent();
        }

        private void serverOnBtn_Click(object sender, EventArgs e)
        {
            UpdateServerStatus(true);
        }

        private void serverOffBtn_Click(object sender, EventArgs e)
        {
            UpdateServerStatus(false);
        }

        public void UpdateConnectedClients(int number)
        {

            if (onlineCounter.InvokeRequired)
            {
                onlineCounter.Invoke((MethodInvoker)delegate
                {
                    UpdateConnectedClients(number);
                });
            }
            else
            {
                onlineCounter.Text = "Connected Clients: " + number;
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

        private void ServiceWindow_Shown(object sender, EventArgs e)
        {
           
        }

        #region  Client Data Grid Functions

            internal void RemoveClientByGUID(Guid guid)
            {
                if (clientList.InvokeRequired)
                {
                    clientList.Invoke((MethodInvoker)delegate
                    {
                        RemoveClientByGUID(guid);
                    });
                    return;
                }

                for (int v = 0; v < clientList.Rows.Count; v++)
                {
                    if (string.Equals(clientList[0, v].Value as string, guid.ToString()))
                    {
                        clientList.Rows.RemoveAt(v);
                        break;
                    }
                }

                connectedClient--;
                UpdateConnectedClients(connectedClient);
            }

            internal void AddClient(Clients.ClientInfo tempClient)
            {
                if (clientList.InvokeRequired)
                {
                    clientList.Invoke((MethodInvoker)delegate
                    {
                        AddClient(tempClient);
                    });
                    return;
                }

                clientList.Rows.Add(tempClient.guid, null, null, null, tempClient.state);


                connectedClient++;
                UpdateConnectedClients(connectedClient);
            }

            internal void UpdateClientInfo(Clients.ClientInfo client)
            {
                foreach (DataGridViewRow row in clientList.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(client.guid.ToString()))
                    {
                        row.Cells[0].Value = client.guid.ToString();
                        row.Cells[1].Value = client.userId.ToString();
                        row.Cells[2].Value = client.username.ToString();
                        row.Cells[3].Value = client.nickname.ToString();
                        row.Cells[4].Value = client.userStatus.ToString();
                        //row.Cells[5].Value = client.guid.ToString();

                        //clientList.Update();
                        //clientList.Refresh();

                        break;
                    }
                }
            }

        #endregion
        


    }
}
