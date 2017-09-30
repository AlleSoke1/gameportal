using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rlkt_LauncherLibServer.Clients
{
    public  enum ClientState
    {
        Disconnected,
        Connected,      // Newly connected, not auth yet!
        Authed,         // Client is successfully authentificated in his account and APPWINDOW is showing!
        PlayingGame,    // Client is playing game!
    }
    class ClientInfo
    {
        //User Info
        public Int64 userId { get; set; }      //account id
        public string username { get; set; }   //account name
        public string email { get; set; }      //email address
        public string nickname { get; set; }   //nickname

        public Enums.UserInfo.UserStatus userStatus { get; set; } //User available/idle/busy


        //Network
        public Guid guid { get; set; }         //session id
        public ClientState state { get; set; } //session state

        internal bool isOnline()
        {
            return (state >= ClientState.Authed);
        }
    }
}
