using Rlkt_LauncherLibServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rlkt_LauncherLibServer
{ 
    static class Program
    {
        public static Clients.Clients clients;
        public static Server.Server server;
        public static CDatabase sql;

        //Games settings/data/info
        public static GameData.GamesInfo gamesInfo;

        public static ServiceWindow serviceWnd = new ServiceWindow();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // new Thread(InitializeThread).Start();
            server = new Server.Server();
            clients = new Clients.Clients();

            gamesInfo = new GameData.GamesInfo();

            sql = new CDatabase();

            // start serivce Window
            Application.EnableVisualStyles();
            Application.Run(serviceWnd);

        }


    }
}
