using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using NetSockets;
using System.Net;
using System.Windows.Threading;

namespace LauncherApp.Network
{
    public class ServerConnection
    {
        static NetObjectClient client = new NetObjectClient();
        public Guid guid; //network user id

        public ServerConnection()
        {
            Thread connect = new Thread(tryConnect);
            connect.IsBackground = true;
            connect.Start();
        }

        void tryConnect()
        {
            try {
                //Attach event handlers
                client.OnDisconnected += new NetDisconnectedEventHandler(client_OnDisconnected);
                client.OnReceived += new NetReceivedEventHandler<NetObject>(client_OnReceived);
                client.OnConnected += new NetConnectedEventHandler(client_OnConnected);

                //Connect to the server
                client.TryConnect(App._conServerIp, 5892);
              //  client.TryConnect("89.39.13.247", 5892);

                //client.OnReceived += client_OnReceived;
                //client.OnDisconnected += client_OnDisconnected;
                //client.OnConnected += client_OnConnected;
       

                Console.WriteLine("connected??!");
            }catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void client_OnConnected(object sender, NetConnectedEventArgs e)
        {
            //Here set status to connect!
        }

        private void client_OnDisconnected(object sender, NetDisconnectedEventArgs e)
        {
             //close all windows and show only login window!
             App.Current.Dispatcher.Invoke(() =>{
                        LauncherFactory.HideAllWindows();
                        LauncherFactory.getLoginClass().ShowLogout();
                    });
        }

        private void client_OnReceived(object sender, NetReceivedEventArgs<NetObject> e)
        {
            PacketHandler.RecvPacket(e.Data);
            Console.WriteLine("RECV PACKET "+ e.Data.Name);
        }

        public void Send(object data, string packetName = "packet")
        {
            if (!client.IsConnected)
            {
                retryConnect(); 
                return; 
            }

            client.Send(new NetObject(packetName, new Packets.packet(this.guid, data)));
        }

        public void Disconnect()
        {
            if(client != null)
            {
                client.Disconnect();
            }
        }

        public void Reconnect()
        {
            if (!client.IsConnected)
                tryConnect();
        }

        public void retryConnect()
        {
            for (int i = 0; i < 5; i++)
            {
                if (!client.IsConnected)
                {
                    tryConnect();
                    Thread.Sleep(2000);
                }
            }
        }

        public bool IsConnected() { return client.IsConnected; }

        internal void setGuid(Guid guid)
        {
            this.guid = guid;
        }
    }
}
