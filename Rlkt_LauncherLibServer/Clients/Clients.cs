using NetSockets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rlkt_LauncherLibServer.Clients
{
    class Clients
    {
        List<ClientInfo> vecClients = new List<ClientInfo>();

        public ClientInfo findClientByGuID(Guid userId)
        {
            if(vecClients.Count <= 0) return null;

            foreach (ClientInfo client in vecClients)
            {
                if (client.guid == userId && client.isOnline()) return client;
            }

            return null;
        }

        public ClientInfo findClientByUserID(long userID)
        {
            if (vecClients.Count <= 0) return null;

            foreach (ClientInfo client in vecClients)
            {
                if (client.userId == userID && client.isOnline()) return client;
            }

            return null;
        }

        public ClientInfo findClientByUsername(string username)
        {
            if(vecClients.Count <= 0) return null;

            foreach(ClientInfo client in vecClients)
            {
                if (client.username.ToLower() == username.ToLower() && client.isOnline()) return client;
            }

            return null;
        }

        public ClientInfo findClientByNickname(string nickname)
        {
            if (vecClients.Count <= 0) return null;

            foreach (ClientInfo client in vecClients)
            {
                if (client.nickname.ToLower() == nickname.ToLower() && client.isOnline()) return client;
            }

            return null;
        }

        void BoardcastPacket(byte[] data, string packetName = "packet")
        {
            if (vecClients.Count <= 0) return;

            foreach (ClientInfo client in vecClients)
            {
                if (client.isOnline()) Server.Server.server.DispatchAll(new NetObject(packetName, data));
            }
        }

        void SendPacket(Guid userId, byte[] data, string packetName = "packet")
        {
            if (vecClients.Count <= 0) return;

            foreach (ClientInfo client in vecClients)
            {
                if (client.guid == userId && client.isOnline()) Server.Server.server.DispatchTo(userId, new NetObject(packetName, data));
            }
        }

        public bool addNewClient(ClientInfo client)
        {
            vecClients.Add(client);

            return true;
        }

        public bool delClientByID(Guid UID)
        {
            ClientInfo client = getClientByID(UID);

            if (ReferenceEquals(client, null) == false) return false;

            vecClients.Remove(client);

            return true;
        }

        public ClientInfo getClientByID(Guid UID)
        {
            var value = vecClients.First(item => item.guid == UID);
            if (value != null) return value;
           
            //foreach (ClientInfo c in vecClients)
            //{
            //    if (c.userId == UID)
            //        return c;
            //}

            return null;
        }


    }
}
