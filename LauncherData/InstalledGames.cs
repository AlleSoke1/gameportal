using Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherData
{

    public class InstalledGames
    {
        public GamesList gamesData;

        public Dictionary<string, GameInfo> iGameList = new Dictionary<string, GameInfo>();

        private string GameInfoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"userdata\games.xml");


        public InstalledGames(bool load)
        {
            gamesData = new GamesList();

            // check if games.xml file is exists, if not will create new one.
            fnFiles.CheckFileCreate(GameInfoPath);

            // add testing data
            //addGame("dragonnest", @"D:\DNArabic\GameClinet", "game.exe", "asdsad");
            //Save();

            // load installed games info from games.xml file
            if(load) Load();

        }


        public void Load()
        {
            gamesData = (GamesList)RlktXmlData.Xml2Object.DeSerialize(GameInfoPath, gamesData);

            foreach (GameInfo gl in gamesData.data)
            {
                GameInfo gInfo = new GameInfo();
                gInfo.gameName = gl.gameName;
                gInfo.gameFolder = gl.gameFolder;
                gInfo.startupExeFile = gl.startupExeFile;
                gInfo.startCommand = gl.startCommand;
                iGameList[gInfo.gameName] = gInfo;
            }
            
        }



        public void Save()
        {

            //Console.WriteLine("[GamesList] Saving  into " + GameInfoPath);
            string datax = RlktXmlData.Object2Xml.Serialize(gamesData);
            string userdataPath = new FileInfo(GameInfoPath).DirectoryName;
            if (!Directory.Exists(userdataPath)) Directory.CreateDirectory(userdataPath);
            File.WriteAllText(GameInfoPath, datax);
        }

        public void Reload()
        {
            Load();
        }


        public void addGame(string name, string folder, string exePath, string command)
        {
            int curCount = gamesData.count++;
            //mesData.data[curCount] = new GameInfo();
            gamesData.data[curCount].gameName = name;
            gamesData.data[curCount].gameFolder = folder;
            gamesData.data[curCount].startupExeFile = exePath;
            gamesData.data[curCount].startCommand = command;


        }

    }


    [Serializable]
    public class GamesList
    {
        public int count;
        public GameInfo[] data = new GameInfo[10];

        public GamesList()
        {
            for (int i = 0; i < data.Length; ++i)
                data[i] = new GameInfo();
        }
    }

    public class GameInfo
    {
        public string gameName;
        public string gameFolder;
        public string startupExeFile;
        public string startCommand;


        public GameInfo()
        {
            gameName = "";
            gameFolder = "";
            startupExeFile = "";
            startCommand = "";
        }
    }


}
