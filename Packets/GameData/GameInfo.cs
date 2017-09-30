using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Packets.GameData
{
    [Serializable]
    public class SC_GameInfo
    {
        public int count;
        public GameData[] gameInfo;
        public SC_GameInfo(int cnt)
        {
            count = cnt;
            gameInfo = new GameData[cnt];

            for(int i=0;i<cnt;i++)
            {
                gameInfo[i] = new GameData();
            }
        }
    }
    
    [Serializable]
    public class GameData
    {
        public int gameID;
        public string gameName;
        public string gameDisplayName;
        public string gameExeFile;
        public string gameWndColor;
        public string gameWndBgColor;
        public string gameWebsite;
        public string gameWebsiteReg;

        //Downloadable.
        public string patchUrl;
        public string patchVersion;
        public string patchServerVersion;

        public string gameDownloadUrl;
        public long gameDownloadSize;

        //Maintenance flag
        public int maintenanceFlag;


    }
}
