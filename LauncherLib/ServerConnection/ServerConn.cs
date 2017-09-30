using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LauncherLib.ServerConnection
{
    class ServerConn
    {
        protected string _ServerIP;
        protected string _ServerDNS;
        protected int _ServerPort;

        //Singleton
        private static ServerConn instance = null;
        private static readonly object padlock = new object();

  

        public static ServerConn Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ServerConn();
                    }
                    return instance;
                }
            }
        }
        //End Singleton

        ServerConn()
        {
            _ServerIP  = "127.0.0.1";
            _ServerDNS = "localhost";
            _ServerPort = 5892;
        }

        private RlktTcpClient client = null;

        void StartServer()
        {
            client = new RlktTcpClient();
            client.SetParameters(_ServerDNS, _ServerPort);
            client.OnDataReceived += new ServerHandlePacketData(RecvPacket);
            client.OnConnect += new _OnConnect(OnConnect);
            client.OnDisconnect += new _OnDisconnect(OnDisconnect);
            client.Start();
        }

        public void Connect()
        {
            if(!ReferenceEquals(client,null))
            {
                client.Start();
            }
            else
            {
                StartServer();
            }
        }

        private void OnConnect()
        {
            Console.WriteLine(this.ToString() + " OnConnect");
            //if can't connect try connect again
        }

        private void OnDisconnect()
        {
            Console.WriteLine(this.ToString() + " OnDisconnect");
            //try to reconnect
        }

        private void RecvPacket(byte[] data, int bytesRead, Socket client)
        {

            int PacketSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(data, 0));
            if (PacketSize > bytesRead) return;
            //There is only one packet
            if (PacketSize == bytesRead)
            {
                Packet packet = new Packet(new ArraySegment<byte>(data));

                new PacketClass().ProcessPacket(packet.PacketType, packet);
                Console.WriteLine(packet.PacketType);
            }
            else //There are more than 1 packets?
            {
                Console.WriteLine(this.ToString() + "  NETWORK PROTOCOL ??");
            }
         
        }

        public void Send(byte[] data)
        {
            if (!ReferenceEquals(client, null))
            {
                client.Send(data);
            }
        }
    }
}
