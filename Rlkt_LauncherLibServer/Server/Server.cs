using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using NetSockets;

namespace Rlkt_LauncherLibServer.Server
{
    class Server
    {
        static int _Port = 5892;

        public static NetObjectServer server = new NetObjectServer();
        public int ConnectedClientCount = 0;

        int UserSeed = 42; //based on this seed server can handle 47 milion clients.
        int curUserId = 0;

        public Server()
        {
            tryListen();
        }

        void tryListen()
        {
            try
            {
                    if (!server.IsOnline)
                    {
                        //Set the deefault echo mode for everything that is received by the server.
                        server.EchoMode = NetEchoMode.None;

                        //Attach event handlers
                        //server.OnClientAccepted += new NetClientAcceptedEventHandler(server_OnClientAccepted);
                        server.OnClientConnected += new NetClientConnectedEventHandler(server_OnClientConnected);
                        server.OnClientDisconnected += new NetClientDisconnectedEventHandler(server_OnClientDisconnected);
                       // server.OnClientRejected += new NetClientRejectedEventHandler(server_OnClientRejected);
                        server.OnReceived += new NetClientReceivedEventHandler<NetObject>(onRecvData);
                        //server.OnStarted += new NetStartedEventHandler(server_OnStarted);
                        //server.OnStopped += new NetStoppedEventHandler(server_OnStopped);

                        //Start the server
                        server.Start(IPAddress.Any, _Port);
                        Console.WriteLine("Server Started on : " + server.Address.ToString());
                    }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    


        public int genUserId()
        {
            return ((curUserId++) + UserSeed % int.MaxValue);
        }

        private void server_OnClientDisconnected(object sender, NetClientDisconnectedEventArgs e)
        {
            Program.clients.delClientByID(e.Guid);

            new Rlkt_LauncherLibServer.PacketLogic.LogoutLogic(e.Guid);

            Console.WriteLine(String.Format("[OnClientDisconnected] Client {0}", e.Guid));

            // remove from client list
            Program.serviceWnd.RemoveClientByGUID(e.Guid);
        }

        private void server_OnClientConnected(object sender, NetClientConnectedEventArgs e)
        {
            //Anti-DOS with e.Reject!

            //Add Client to list!
            Clients.ClientInfo tempClient = new Clients.ClientInfo();
            tempClient.state = Clients.ClientState.Connected;
            tempClient.guid = e.Guid;

            Program.clients.addNewClient(tempClient);
            //
            Console.WriteLine(String.Format("[OnClientConnected] Client {0}, State: {1}", tempClient.guid, tempClient.state.ToString()));
    
            //Send GUID
            server.DispatchTo(e.Guid, new NetObject("handshake", new Packets.Login.handshake(e.Guid)));
         

            // add online clients
            Program.serviceWnd.AddClient(tempClient);
        }


        private void onRecvData(object sender, NetClientReceivedEventArgs<NetObject> e)
        {
            try
            {
                PacketHandler.RecvPacket(e.Data.Name, (Packets.packet)e.Data.Object);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



    }
}
