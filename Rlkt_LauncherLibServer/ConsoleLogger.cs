using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rlkt_LauncherLibServer
{
    class ConsoleLogger
    {
        //Singleton
        private static ConsoleLogger instance = null;
        private static readonly object padlock = new object();

        ConsoleLogger()
        {
        }

        public static ConsoleLogger Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ConsoleLogger();
                    }
                    return instance;
                }
            }
        }
        //End Singleton

        private List<String> backLog = new List<String> { };

        public List<String> GetBackLog()
        {
             return backLog;
        }

        public void ClearBackLog()
        {
            backLog = new List<String> { };
        }

        public void Log(string Text)
        {
            Console.WriteLine(Text);
            backLog.Add(Text);
        }
    }
}
