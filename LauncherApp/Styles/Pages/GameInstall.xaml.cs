using Functions;
using IWshRuntimeLibrary;
using LauncherApp.Styles.Controls;
using LauncherData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LauncherApp.Styles.Pages
{
    /// <summary>
    /// Interaction logic for GameInstall.xaml
    /// </summary>
    public partial class GameInstall : UserControl
    {

        public bool InstallDoneFlag = false;
        Packets.GameData.GameData gInfo;
        private string InstallPathStr;
        private int DownloadedFileCount = 0;

        private string gameExeFile;
        private string gameStartCommand;

        private static List<string> gameFileList = new List<string>();

        FileDownload fileDownloadManager;

        public GameInstall()
        {
            InitializeComponent();

        }

        public void NewGameInstall(int gameIndex){

            showMessageWnd(true, MahApps.Metro.IconPacks.PackIconMaterialKind.VectorCircle, (string)App.Current.Resources["LoadingStr"], true);
            gInfo = LauncherFactory.getAppClass().HomePage.gamesInfo.gameInfo[gameIndex];



            fileDownloadManager = new FileDownload(
                null
                , null
                , 5120);


            Thread tFileList = new Thread(GetDownloadListFile);
            tFileList.Start();


            InstallPathStr = string.Format(Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "arcangames\\{0}"), gInfo.gameName);
            InstallPath.Content = InstallPathStr;
            checkDriverSize();

            LoadStyles();

        }
        #region UI Functions

        private void LoadStyles()
        {

            //set ui strings
            WndTitle.Content = string.Format((string)App.Current.Resources["InstallGameTitle"], gInfo.gameDisplayName.ToUpper());
            GameImage.Source = new BitmapImage(new Uri(string.Format("pack://application:,,,/LauncherResources;component/resources/images/games/background/install/{0}.png", gInfo.gameName.ToLower()))); ;

            InfoWnd.Visibility = Visibility.Visible;
            InstallWnd.Visibility = Visibility.Hidden;
            MessageIcon.Spin = true;
        }

        private void tCloseBtn_Click(object sender, RoutedEventArgs e)
        {

            LauncherFactory.ElementAnimation(this, 
                UIElement.OpacityProperty, 
                1, 
                0, 
                0.2, 
                false, 
                new Action(() => { this.Visibility = Visibility.Hidden; })
                );
        }


        private void showMessageWnd(bool sStatus, MahApps.Metro.IconPacks.PackIconMaterialKind sIcon, string sMessage, bool sSpin = false)
        {
            if (sStatus == true)
            {
                MessageWnd.Visibility = Visibility.Visible;
                MessageIcon.Kind = sIcon;
                MessageIcon.Spin = sSpin;
                MessageText.Content = sMessage;

                if (sSpin)
                {
                    MessageIcon.Visibility = Visibility.Hidden;
                    MessageLoadingIcon.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageIcon.Visibility = Visibility.Visible;
                    MessageLoadingIcon.Visibility = Visibility.Hidden;
                }
                return;
            }

            MessageWnd.Visibility = Visibility.Hidden;
        }

        private void InstallPathBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (dialog.SelectedPath.Length > 0)
                {
                    InstallPathStr = dialog.SelectedPath;
                    InstallPath.Content = InstallPathStr;
                    checkDriverSize();
                }
            }
        }

        #endregion

        #region Quick Functions

        void checkDriverSize()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            string InstalledDriver = Path.GetPathRoot(InstallPathStr);

            foreach (DriveInfo d in allDrives)
            {
                if (d.Name == InstalledDriver)
                {
                    if (d.AvailableFreeSpace > gInfo.gameDownloadSize)
                    {
                        ReqSizeIcon.Foreground = Brushes.LawnGreen;
                        ReqSizeLbil.Foreground = Brushes.LawnGreen;
                        ReqSizeErrorLbil.Foreground = Brushes.LawnGreen;
                        ReqSizeIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.Check;
                        GameInstallButton.IsEnabled = true;
                    }
                    else
                    {
                        ReqSizeIcon.Foreground = Brushes.Red;
                        ReqSizeLbil.Foreground = Brushes.Red;
                        ReqSizeErrorLbil.Foreground = Brushes.Red;
                        ReqSizeIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.Close;
                        GameInstallButton.IsEnabled = false;
                    }

                    break;
                }
            }
        }

        #endregion


        private void GameInstallButton_Click(object sender, RoutedEventArgs e)
        {


            fnFiles.CheckFolderCreate(InstallPathStr);

            tCloseBtn.IsEnabled = false;

            InstallWnd.Visibility = Visibility.Visible;
            InfoWnd.Visibility = Visibility.Hidden;

            WndInstallText.Content = string.Format((string)App.Current.Resources["InstallGameInstallText"], gInfo.gameDisplayName.ToLower());
            ProgressPath.Content = InstallPathStr;


            fileDownloadManager.StartList();
            //Thread downThread = new Thread(StartDownload);
            //downThread.IsBackground = true;
            //downThread.Start();

        }


        //private void StartDownload()
        //{
        //    int filesCount = gameFileList.Count + DownloadedFileCount;
        //    bool deletedFile = false;

        //    Dispatcher.Invoke(() =>
        //    {
        //        DownloadBar.Value = 0;
        //        DownloadStatus.Content = (DownloadedFileCount + 1) + " / " + filesCount;
        //    });

        //    DownloadManager dMan = new DownloadManager();


        //    dMan.DownloadFile(gameFileList.First(), InstallPathStr, filesCount, new Action<long, long>(delegate(long complete, long total)
        //    {
        //        Dispatcher.Invoke(() =>
        //        {
        //            DownloadBar.Maximum = total;
        //            DownloadBar.Value = complete;

        //            if (DownloadBar.Value == total && DownloadedFileCount <= filesCount)
        //            {

        //                if (deletedFile == false)
        //                {
        //                    DownloadedFileCount++;
        //                    gameFileList.RemoveAt(0);
        //                    deletedFile = true;

        //                    if (gameFileList.Count > 0)
        //                    {
        //                        Thread.Sleep(50);

        //                        Thread downThread = new Thread(StartDownload);
        //                        downThread.IsBackground = true;
        //                        downThread.Start();

        //                    }
        //                    else
        //                    {
        //                        Thread.Sleep(50);

        //                        Thread downThread = new Thread(StartInstallGame);
        //                        downThread.IsBackground = true;
        //                        downThread.Start();
        //                    }

        //                }


        //            }

        //        });

        //    }));

        //}

        private void StartInstallGame()
        {
            new GameManager().InstallGame(gInfo.gameID, gInfo.gameName, InstallPathStr, gameExeFile, gameStartCommand);

            Dispatcher.Invoke(() =>
            {
                StartBtn.IsEnabled = true;
                PauseBtn.IsEnabled = false;
                tCloseBtn.IsEnabled = true;

                if (ShortcutFlag.IsChecked == true)
                {
                    CreateAppShortCut();
                }

                showMessageWnd(true, MahApps.Metro.IconPacks.PackIconMaterialKind.Check, App.Current.Resources["InstallGameComplete"].ToString());
            });

        }

        private void CreateAppShortCut()
        {
            var startupFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var shell = new WshShell();
            var shortCutLinkFilePath = Path.Combine(startupFolderPath, string.Format("{0}.lnk", gInfo.gameDisplayName));
            var windowsApplicationShortcut = (IWshShortcut)shell.CreateShortcut(shortCutLinkFilePath);
            windowsApplicationShortcut.Description = "Arcan Games Shortcut Start " + gInfo.gameDisplayName;
            windowsApplicationShortcut.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            windowsApplicationShortcut.TargetPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Process.GetCurrentProcess().MainModule.FileName);
            windowsApplicationShortcut.Arguments = " -gameStart " + gInfo.gameID;
            windowsApplicationShortcut.Save();

        }
        private void GetDownloadListFile()
        {

            List<string> temp = new DownloadManager().getGameFileList(gInfo.gameDownloadUrl);
            string[] rInfo = temp.First().Split(':');

            List<string> gameFileList = temp; 
            gameFileList.RemoveAt(0);

            fileDownloadManager.FileDownloadList(gameFileList, this.InstallPathStr, 5120);

            Dispatcher.Invoke(() =>
            {

                ReqSizeLbil.Content = fnFiles.SizeReadAble(fileDownloadManager.TotalMultiDownloadSize);
                gameExeFile = rInfo[1];
                gameStartCommand = rInfo[2];
                DownloadBar.Maximum = fileDownloadManager.TotalMultiDownloadSize;
                showMessageWnd(false, MahApps.Metro.IconPacks.PackIconMaterialKind.None, "");

            });


            fileDownloadManager.OnFileDownloadComplete += FileDownloadComplete;
            fileDownloadManager.OnFileDownloadProgress += FileDownloadListProgress;

        }

        private void FileDownloadListProgress(object sender, FileDownloadProgressArg e)
        {
            //double completeBarLable = e.DownloadedBytes * 100.0 / e.TotalBytes;
            Dispatcher.Invoke(() =>
            {
                DownloadBar.Value = e.DownloadedBytes;
                DownloadStatus.Content = string.Format("{0}% ({1} From {2})", e.DownloadPercent, fnFiles.SizeReadAble(e.DownloadedBytes), fnFiles.SizeReadAble(e.TotalBytes));
            });
        }

        private void FileDownloadComplete(object sender, RoutedEventArgs e)
        {
            if (fileDownloadManager.Done)
            {
                Dispatcher.Invoke(() =>
                {
                    DownloadStatus.Content = "DONE!";
                    DownloadBar.Value = DownloadBar.Maximum;
                });

                Thread downThread = new Thread(StartInstallGame);
                downThread.IsBackground = true;
                downThread.Start();
            }

        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            Thread startGameThread = new Thread(() => { new GameManager().StartGame(gInfo.gameName); });
            startGameThread.Start();
        }

        private void PauseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (fileDownloadManager.GetStatus())
            {
                // file is downloading...
                fileDownloadManager.Pause();
                tCloseBtn.IsEnabled = true;
                PauseBtn.SetResourceReference(SpecialButton.TextProperty, "strContinue");
            }
            else
            {
                // file is stoped...
                fileDownloadManager.Start();
                tCloseBtn.IsEnabled = false;
                PauseBtn.SetResourceReference(SpecialButton.TextProperty, "strPause");
            }
        }


    }
}
