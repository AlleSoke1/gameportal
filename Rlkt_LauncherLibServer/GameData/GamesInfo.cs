using NetSockets;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rlkt_LauncherLibServer.GameData
{
    [Serializable]
    public class Games
    {
          public int count;
          public GamesData[] data = new GamesData[10];

          public Games()
          {
              for (int i = 0; i < data.Length; ++i)
                  data[i] = new GamesData();
          }
    } 

    public class GamesData
    {
        public int gameID;
        public string gameName;
        public string gameDisplayName;
        public string gameExeFile;
        public string gameBtnIcon;

        public string gameWndColor;
        public string gameWndBgColor;
        public string gameWebsite;
        public string gameWebsiteReg;

        public int maintenance;
        public string downloadFileUrl;

        public GamesData()
        {
            gameID = new int();
            gameName = "";
            gameExeFile = "";
            maintenance = 0;
            downloadFileUrl = "";
        }
    }

    public class GamesInfo
    {
        Games gameData;
        private string gamesDataFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"DATA\GamesInfo.xml");

        
        public GamesInfo()
        {
            gameData = new Games();
            //addGame("DragonNest", "A free to play action MMORPG based on cartoon characters", "pack://application:,,,/LauncherApp;component/resources/images/games/logo/logo_dn.png", 0,
            //  "pack://application:,,,/LauncherApp;component/resources/images/games/logo/dngamelogo.png", "0xFFFFFFF", "Play this shit" ,  "http://gayshit.net", "http://gayshit.net/register" ,
            //  "pack://application:,,,/LauncherApp;component/resources/images/games/background/dn_bg1.png");
            //addGame("GayShit", "A free to play action MMORPG based on cartoon characters", "pack://application:,,,/LauncherApp;component/resources/images/games/logo/logo_dn.png", 0,
            //  "pack://application:,,,/LauncherApp;component/resources/images/games/logo/wowgamelogo.png", "0xFFFFFFF", "Another shit", "http://gayshit.net", "http://gayshit.net/register",
            //  "pack://application:,,,/LauncherApp;component/resources/images/games/background/wow_bg1.png");
            //addGame("Uso", "A free to play action MMORPG based on cartoon characters", "pack://application:,,,/LauncherApp;component/resources/images/games/logo/logo_wow.png", 0,
            //  "pack://application:,,,/LauncherApp;component/resources/images/games/logo/wowgamelogo.png", "0xFFFFFFF", "This is 20% luck 80% skill", "http://gayshit.net", "http://gayshit.net/register",
            //  "pack://application:,,,/LauncherApp;component/resources/images/games/background/wow_bg1.png");
            
            //Save();
           Load();

           Console.WriteLine("games Into Path: " + gamesDataFile);
        }
    
        void Load()
        {
            Console.WriteLine("[GameData] Loading from " + gamesDataFile);
            gameData = (Games)RlktXmlData.Xml2Object.DeSerialize(gamesDataFile, gameData);
        }

        void Save()
        {
            Console.WriteLine("[GameData] Saving  into " + gamesDataFile);
            string datax = RlktXmlData.Object2Xml.Serialize(gameData);
            string userdataPath = new FileInfo(gamesDataFile).DirectoryName;
            if (!Directory.Exists(userdataPath)) Directory.CreateDirectory(userdataPath);
            File.WriteAllText(gamesDataFile, datax);
        }

        void Reload()
        {
            Load();
        }

        void addGame(int id, string name, string DisplayName, string gameExeFile, int maintenace, string color, string message, string web, string webReg, string downloadFile)
        {

            int curCount = gameData.count++;
            gameData.data[curCount].gameID = id;
            gameData.data[curCount].gameName = name;
            gameData.data[curCount].gameDisplayName = DisplayName;
            gameData.data[curCount].gameExeFile = gameExeFile;
            gameData.data[curCount].maintenance = maintenace;
            gameData.data[curCount].gameWndColor  = color;
            gameData.data[curCount].gameWebsite = web;
            gameData.data[curCount].gameWebsiteReg = webReg;
            gameData.data[curCount].downloadFileUrl = downloadFile;

        }


        public Games getGameInfoCls() { return gameData; }

        
        public void sendGameListPacket(Guid user)
        {

            if (gameData.count <= 0) return;

            Packets.GameData.SC_GameInfo pkt = new Packets.GameData.SC_GameInfo(gameData.count);

            for(int i=0;i<pkt.count;i++)
            {
                pkt.gameInfo[i].gameID = gameData.data[i].gameID;
                pkt.gameInfo[i].gameName = gameData.data[i].gameName;
                pkt.gameInfo[i].gameDisplayName = gameData.data[i].gameDisplayName;
                pkt.gameInfo[i].gameExeFile = gameData.data[i].gameExeFile;
                pkt.gameInfo[i].maintenanceFlag = gameData.data[i].maintenance;
                pkt.gameInfo[i].gameWndColor = gameData.data[i].gameWndColor;
                pkt.gameInfo[i].gameWebsite = gameData.data[i].gameWebsite;
                pkt.gameInfo[i].gameWebsiteReg = gameData.data[i].gameWebsiteReg;
                pkt.gameInfo[i].gameWndBgColor = gameData.data[i].gameWndBgColor;
                pkt.gameInfo[i].gameDownloadUrl = gameData.data[i].downloadFileUrl;
            }

            Server.Server.server.DispatchTo(user, new NetObject("SC_GameInfo", pkt));
        }
    }
}
