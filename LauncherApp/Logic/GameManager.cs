using LauncherApp.Game_Data;
using LauncherData;
using Packets.GameData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LauncherApp
{
    public class GameManager
    {
        public bool ActiveTimer = false;

        public void StartGame(string gameName)
        {
          
            ActiveTimer = true;
            Thread timerThread = new Thread(() => { StartGameTimer(); });
            var gParams = App.installedGamesList[gameName];

            App.Current.Dispatcher.Invoke(() =>
            {
                LauncherFactory.getAppClass().HomePage.RunGameStatus(true);
                LauncherFactory.getAppClass().MyGamesPage.RunGameStatus(true);
            });

            ProcessStartInfo procStartInfo = new ProcessStartInfo();
            Process procExecuting = new Process();

            var _with1 = procStartInfo;
            _with1.UseShellExecute = true;
            _with1.FileName = Path.Combine(gParams.gameFolder, gParams.startupExeFile);
            _with1.WorkingDirectory = gParams.gameFolder;
            _with1.Verb = "runas";
            _with1.Arguments = string.Format(gParams.startCommand);

            if (File.Exists(_with1.FileName))
            {
                procExecuting = Process.Start(procStartInfo);
                timerThread.Start();

                // wait while game is running
                procExecuting.WaitForExit();

            }

            // after game close
            ActiveTimer = false;
            Thread.Sleep(200);

            App.Current.Dispatcher.Invoke(() =>
            {
                LauncherFactory.getAppClass().HomePage.RunGameStatus(false);
                LauncherFactory.getAppClass().MyGamesPage.RunGameStatus(false);
            });

        }

        public void StartGameTimer()
        {
            long secs = 0;
            while (ActiveTimer)
            {
                secs++;
                App.Current.Dispatcher.Invoke(() =>
                {
                    LauncherFactory.getAppClass().HomePage.UpdatePlayTime(secs);
                });
                Thread.Sleep(1000);
            }

        }

        public void ScanGamesPath(string path, bool isDefaultPath)
        {

            GameData[] varGamesInfo = LauncherFactory.getAppClass().HomePage.gamesInfo.gameInfo;

            var installedGames = App.installedGamesList;

            foreach (GameData gData in varGamesInfo)
            {
                string correctPath = path;
                if (isDefaultPath) correctPath = Path.Combine(path, gData.gameName);

                string verifyPath = Path.Combine(correctPath, gData.gameExeFile);

                bool existsFalg = Functions.fnFiles.CheckFileExists(verifyPath);

                if (existsFalg)
                {
                    if (!installedGames.ContainsKey(gData.gameName) || correctPath.ToLower() != installedGames[gData.gameName].gameFolder.ToLower())
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            LauncherFactory.getAppClass().HomePage.GameScanPage.AddGame(true, gData.gameDisplayName, correctPath, gData.gameName, gData.gameID, gData.gameExeFile);
                        });

                    }


                }
            }

            Thread.Sleep(1000);

            App.Current.Dispatcher.Invoke(() =>
            {
                LauncherFactory.getAppClass().HomePage.GameScanPage.ShowMessageWnd(false, false, "");
            });

        }

        public void InstallGame(int gameIndex, string gameNameID, string installPath, string exeFile, string startCommand)
        {
            InstalledGames inGames = new InstalledGames(false);

            int insCount = App.installedGamesList.Count;

            for (int i = 0; insCount > i; i++)
            {
                var el = App.installedGamesList.ElementAt(i).Value;
                inGames.addGame(el.gameName, el.gameFolder, el.startupExeFile, el.startCommand);
            }

            inGames.addGame(gameNameID, installPath, exeFile, startCommand);

            inGames.Save();

            GameInfo igInfo = new GameInfo();
            igInfo.gameName = gameNameID;
            igInfo.gameFolder = installPath;
            igInfo.startupExeFile = exeFile;
            igInfo.startCommand = startCommand;

            if (!App.installedGamesList.ContainsKey(gameNameID)) App.installedGamesList[gameNameID] = igInfo;

            App.Current.Dispatcher.Invoke(() =>
            {
                LauncherFactory.getAppClass().HomePage.UpdateInstalledGame(gameIndex);
                LauncherFactory.getAppClass().MyGamesPage.AddGameToList(igInfo.gameName, LauncherFactory.getAppClass().HomePage.GetGameListIDByName(igInfo.gameName));
            });

        }


    }
}
