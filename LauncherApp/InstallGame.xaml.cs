using Functions;
using IWshRuntimeLibrary;
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


namespace LauncherApp
{
    /// <summary>
    /// Interaction logic for InstallGame.xaml
    /// </summary>
    public partial class InstallGame : Window
    {
        public bool InstallDoneFlag = false;
        Packets.GameData.GameData gInfo;
        private string InstallPathStr;
        private int DownloadedFileCount = 0;

        private string gameExeFile;
        private string gameStartCommand;

        private static List<string> gameFileList = new List<string>();

        public InstallGame(int gameIndex)
        {
            InitializeComponent();

            gInfo = LauncherFactory.getAppClass().gamesInfo.gameInfo[gameIndex];

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
            Panel.SetZIndex(WndBorder, 0);

            //set ui strings
            WndTitle.Content = string.Format((string)App.Current.Resources["InstallGameTitle"], gInfo.gameDisplayName.ToUpper());
            GameImage.Source = new BitmapImage(new Uri(string.Format("resources/images/games/background/install/{0}.png", gInfo.gameName.ToLower()), UriKind.Relative)); ;


        }


        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void tCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            LauncherFactory.getAppClass().BlurWindow(false);
            this.Hide();
        }


        private void showMessageWnd(bool sStatus, FontAwesome.WPF.FontAwesomeIcon sIcon, string sMessage, bool sSpin = false)
        {
            if (sStatus == true)
            {
                MessageWnd.Visibility = Visibility.Visible;
                MessageIcon.Icon = sIcon;
                MessageIcon.Spin = sSpin;
                MessageText.Content = sMessage;
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
                        ReqSizeIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.Check;
                        InstallBtn.IsEnabled = true;
                        ReqSizeErrorLbil.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        ReqSizeIcon.Foreground = Brushes.Red;
                        ReqSizeLbil.Foreground = Brushes.Red;
                        ReqSizeIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.Close;
                        InstallBtn.IsEnabled = false;
                        ReqSizeErrorLbil.Visibility = Visibility.Visible;
                    }

                    break;
                }
            }
        }

        #endregion

        private void InstallBtn_Click(object sender, RoutedEventArgs e)
        {


            fnFiles.CheckFolderCreate(InstallPathStr);

            tCloseBtn.IsEnabled = false;

            InstallWnd.Visibility = Visibility.Visible;
            InfoWnd.Visibility = Visibility.Hidden;

            WndInstallText.Content = string.Format((string)App.Current.Resources["InstallGameInstallText"], gInfo.gameDisplayName.ToLower());
            ProgressPath.Content = InstallPathStr;



            Thread downThread = new Thread(StartDownload);
            downThread.IsBackground = true;
            downThread.Start();

        }


        private void StartDownload()
        {
            int filesCount = gameFileList.Count + DownloadedFileCount;
            bool deletedFile = false;

            Dispatcher.Invoke(() =>
            {
                    DownloadBar.Value = 0;
                    DownloadStatus.Content = (DownloadedFileCount + 1) + " / " + filesCount ;
                });

            DownloadManager dMan = new DownloadManager();


            dMan.DownloadFile(gameFileList.First(), InstallPathStr, filesCount, new Action<long, long>(delegate(long complete, long total)
            {
                Dispatcher.Invoke(() =>
                {
                    DownloadBar.Maximum = total;
                    DownloadBar.Value = complete;

                    if (DownloadBar.Value == total && DownloadedFileCount <= filesCount)
                    {
                        
                        if (deletedFile == false)
                        {
                            DownloadedFileCount++;
                            gameFileList.RemoveAt(0);
                            deletedFile = true;

                            if (gameFileList.Count > 0)
                            {
                                Thread.Sleep(50);

                                Thread downThread = new Thread(StartDownload);
                                downThread.IsBackground = true;
                                downThread.Start();

                            }
                            else
                            {
                                Thread.Sleep(50);

                                Thread downThread = new Thread(StartInstallGame);
                                downThread.IsBackground = true;
                                downThread.Start();
                            }
                            
                        }
                            
                        
                    } 

                });

            }));

        }

        private void StartInstallGame()
        {
            new GameManager().InstallGame(gInfo.gameID, gInfo.gameName, InstallPathStr, gameExeFile, gameStartCommand);

            Dispatcher.Invoke(() =>
            {
                InstallBar.Value = 100;
                StartBtn.IsEnabled = true;
                tCloseBtn.IsEnabled = true;

                if (ShortcutFlag.IsChecked == true)
                {
                    CreateAppShortCut();
                }

                showMessageWnd(true, FontAwesome.WPF.FontAwesomeIcon.Check, App.Current.Resources["InstallGameComplete"].ToString());
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

            Dispatcher.Invoke(() =>
            {

                string[] rInfo = temp.First().Split(',');
                ReqSizeLbil.Content = fnFiles.SizeReadAble(long.Parse(rInfo[0]));
                gameExeFile = rInfo[1];
                gameStartCommand = rInfo[2];
                temp.RemoveAt(0);
                gameFileList = temp;
                showMessageWnd(false, FontAwesome.WPF.FontAwesomeIcon.None, "");

            });
           
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            Thread startGameThread = new Thread(() => { new GameManager().StartGame(gInfo.gameName); });
            startGameThread.Start();
        }




    }
}
