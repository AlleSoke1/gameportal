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


namespace LauncherApp
{
    /// <summary>
    /// Interaction logic for ScanGames.xaml
    /// </summary>
    public partial class ScanGames : Window
    {

        private string GamesDefaultPath = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "arcangames\\");
        private List<string> ScanedList = new List<string>();


        public ScanGames()
        {
            InitializeComponent();



            LoadStyles();

        }

        private void SettingsWnd_Loaded(object sender, RoutedEventArgs e)
        {

            new Thread(() => { StartGameScan(GamesDefaultPath); }).Start();
        }

        #region UI Functions

        private void LoadStyles()
        {
            Panel.SetZIndex(WndBorder, 0);

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

        private void LocalScan_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                if (dialog.SelectedPath.Length > 0)
                {
                    string nPath = dialog.SelectedPath.ToString();
                    new Thread(() => { StartGameScan(nPath); }).Start();
                }
            }
        }

        public void ShowMessageWnd(bool sStatus, bool closeAble, string sMessage, FontAwesome.WPF.FontAwesomeIcon sIcon = FontAwesome.WPF.FontAwesomeIcon.None, bool sSpin = false)
        {
            if (SearchGameList.Children.Count > 0) {
                startInstallBtn.IsEnabled = true;
            }
            else
            {
                startInstallBtn.IsEnabled = false;
            }

            if (sStatus == true)
            {
                MessageWnd.Visibility = Visibility.Visible;
                MessageIcon.Icon = sIcon;
                MessageIcon.Spin = sSpin;
                MessageText.Content = sMessage;

                if (closeAble) MessageCloseBtn.Visibility = Visibility.Visible; else MessageCloseBtn.Visibility = Visibility.Hidden;
                return;
            }

            MessageWnd.Visibility = Visibility.Hidden;
        }


        private void MessageCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.ShowMessageWnd(false, false, "");
        }

        #endregion

        #region Quick Functions


        #endregion

        public void AddGame(bool selected, string name, string path, string id,int index, string exeFile)
        {

            if (ScanedList.Contains(id))
            {
                foreach (ScanListItem sli in SearchGameList.Children)
                {
                    if (sli.GameNameID.ToLower() == id.ToLower())
                    {
                        sli.GamePath = path;
                        SearchGameList.UpdateLayout();
                        return;
                    }
                }
                
            }

            ScanListItem slItem = new ScanListItem() {
                Name = "scanned_" + id,
                IsSelected = selected,
                GameName = name,
                GamePath = path,
                GameExeFile = exeFile,
                GameNameID = id,
                GameIndex = index,
            };

            SearchGameList.Children.Add(slItem);
            ScanedList.Add(id);
        }

        private void StartGameScan(string scanedPath)
        {
            Dispatcher.Invoke(() => {
                this.ShowMessageWnd(true, false, App.Current.Resources["LoadingStr"].ToString(), FontAwesome.WPF.FontAwesomeIcon.CircleOutlineNotch, true); 
            });

            if (GamesDefaultPath == scanedPath)
            {
                new GameManager().ScanGamesPath(scanedPath, true);
                return;
            }

            new GameManager().ScanGamesPath(scanedPath, false);

        }


        private void startInstallBtn_Click(object sender, RoutedEventArgs e)
        {
            if (SearchGameList.Children.Count > 0) {

                startInstallBtn.IsEnabled = false;

                ShowMessageWnd(true, false, App.Current.Resources["LoadingStr"].ToString(), FontAwesome.WPF.FontAwesomeIcon.CircleOutlineNotch, true);

                List<string[]> tempList = new List<string[]>();

                foreach (ScanListItem sli in SearchGameList.Children)
                {
                    if (sli.IsSelected)
                    {
                        string[] tempstring = new string[4];
                        tempstring[0] = sli.GameIndex.ToString();
                        tempstring[1] = sli.GameNameID.ToString();
                        tempstring[2] = sli.GamePath.ToString();
                        tempstring[3] = sli.GameExeFile.ToString();

                        tempList.Add(tempstring);
                    }
                    else
                    {
                        SearchGameList.Children.Remove(sli);
                    }

                }

                new Thread(() => { StartInstallGames(tempList); }).Start();

            }
        }


        private void StartInstallGames(List<string[]> itemsList)
        {
            GameManager cGameMan = new GameManager();
            int xCounter = 0;

            foreach (string[] item in itemsList)
            {
                var gInfo = LauncherFactory.getAppClass().gamesInfo.gameInfo[int.Parse(item[0])];
                string[] rInfo = new DownloadManager().DownloadFirstString(gInfo.gameDownloadUrl).Split(',');

                cGameMan.InstallGame(int.Parse(item[0]), item[1], item[2], item[3], rInfo[2]);
                
                Dispatcher.Invoke(() =>
                {
                    SearchGameList.Children.RemoveAt(xCounter);
                });

                xCounter++;
            }

            Dispatcher.Invoke(() =>
            {
                LauncherFactory.getScanGamesClass().ShowMessageWnd(true, true, App.Current.Resources["InstallGameComplete"].ToString(), FontAwesome.WPF.FontAwesomeIcon.Check);
                
            });

        }





    }
}
