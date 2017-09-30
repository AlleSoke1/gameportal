using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LauncherLib.ServerConnection
{
    public delegate void ServerHandlePacketData(byte[] data, int bytesRead, Socket client);
    public delegate void _OnDisconnect();
    public delegate void _OnConnect();

    class RlktTcpClient
    {
        public event ServerHandlePacketData OnDataReceived;

        public event _OnDisconnect OnDisconnect;
        public event _OnConnect    OnConnect;


        private string _IP;
        private int _Port;
        private byte[] buffer = new byte[4096];
        // Create a TCP/IP socket.  
        Socket client;

        Thread serverThread;
        public RlktTcpClient()
        {
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void SetParameters(string ip, int port)
        {
            _IP = ip;
            _Port = port;
        }
  
        public void Start()
        {
            serverThread = new Thread(ServerThread);
            serverThread.Start();
        }

        public void Send(byte[] data)
        {
            try {
                client.Send(data);
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void ServerThread()
        {
            IPHostEntry ipHostInfo = Dns.Resolve(_IP);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, _Port);
            try
            {
                client.Connect(remoteEP);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                OnConnect();
                while(true)
                {
                    int bytesRead;
                    while ((bytesRead = client.Receive(buffer)) > 0)
                    {
                        //
                            OnDataReceived(buffer, bytesRead, client);
                        //
                    }
                    Thread.Sleep(1);
                }

                OnDisconnect();

            }
            catch (Exception e)
            {
                OnDisconnect();
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }  
        }

    }
}
