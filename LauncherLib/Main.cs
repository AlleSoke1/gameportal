using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LauncherLib.Factory;
using LauncherLib.ServerConnection;

namespace LauncherLib
{
    public class Main
    {
        //Init dll
        TaskCreator tasks = new TaskCreator();

        //Init socket
        ServerConn sc = ServerConn.Instance;
        public Main()
        {
            sc.Connect();
        }
    }
}
