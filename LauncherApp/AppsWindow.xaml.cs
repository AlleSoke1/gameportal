using LauncherApp.Network;
using LauncherApp.Styles.Controls;
using LauncherData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace LauncherApp
{
    /// <summary>
    /// Interaction logic for AppWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {

        //globals
        public string CashSopWebUrl = "https://accounts.arcangames.com/remote/shop";
        public Packets.GameData.SC_GameInfo gamesInfo;

        private int SelectedGameId;

        public bool windowMaximizeState = false; //true = maximize , false = minimize



        public AppWindow()
        {
            
            InitializeComponent();

            LoadStyles();

            LoadDropMenus();

        }

        #region UI Functions

        private void LoadStyles()
        {
            CopyrightLibl.Content = (string)App.Current.Resources["AppCopyrights"] + DateTime.Now.Year.ToString();
            UserDropMenu.Visibility = Visibility.Hidden;
            LogoDropMenu.Visibility = Visibility.Hidden;

            Panel.SetZIndex(wndBorder, 1);
        }



        private void GameWebsiteBtn_Click(object sender, RoutedEventArgs e)
        {
            Button sBtn = (Button)sender;
            Process.Start(sBtn.Tag.ToString());
        }


        private void LoadingStatus(bool p, string color = "White")
        {
            if (p)
            {
                LoadingIcon.Visibility = Visibility.Visible;
            }
            else
            {
                LoadingIcon.Visibility = Visibility.Hidden;
            }

            LoadingIconFa.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(color));

        }

        private void AppPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && sender is Grid)
                this.DragMove();
        }

        private void GameVideoBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadingStatus(true);
            GameVideoBtn.Visibility = Visibility.Hidden;
            GameVideoUrl.Visibility = Visibility.Visible;
            GameVideoUrl.Source = new Uri("https://www.youtube.com/embed/Oy4N2LeE2TE?autoplay=1");
        }

        private void GameVideoUrl_LoadingFrameComplete(object sender, Awesomium.Core.FrameEventArgs e)
        {
            LoadingStatus(false);
        }


        public void BlurWindow(bool status)
        {
            if (status == true)
            {
                this.IsEnabled = false;
                BlurBitmapEffect myBlurEffect = new BlurBitmapEffect();
                myBlurEffect.Radius = 2;
                myBlurEffect.KernelType = KernelType.Box;
                AppsWindow.BitmapEffect = myBlurEffect;
                isWindowBlur = true;
            }
            else
            {
                this.IsEnabled = true;
                AppsWindow.BitmapEffect = null;
                isWindowBlur = false;
            }
        }

        private void FriendsWindowBtn_Click(object sender, RoutedEventArgs e)
        {

            LauncherFactory.getFriendsClass().Show();
            LauncherFactory.getFriendsClass().Topmost = true;
            LauncherFactory.getFriendsClass().Topmost = false;
            LauncherFactory.getFriendsClass().Focus();

        }


        private void tCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void tFullBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!windowMaximizeState)
            {
                tFullBtn.Icon = FontAwesome.WPF.FontAwesomeIcon.WindowRestore;
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                tFullBtn.Icon = FontAwesome.WPF.FontAwesomeIcon.WindowMaximize;
                this.WindowState = WindowState.Normal;
            }

            windowMaximizeState = !windowMaximizeState;
        }

        private void tMinieBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void OfflineInstall_Click(object sender, RoutedEventArgs e)
        {
            LauncherFactory.getScanGamesClass().Owner = this;
            BlurWindow(true);
            LauncherFactory.getScanGamesClass().ShowDialog();
        }

        private void AppSettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            LauncherFactory.getSettingsClass().Owner = this;
            LauncherFactory.getSettingsClass().LoadSettings();
            BlurWindow(true);
            LauncherFactory.getSettingsClass().Show();
        }


        private void menuShop_Click(object sender, RoutedEventArgs e)
        {
            //ShopPanel.Visibility = Visibility.Visible;
            menuGames.IsDefault = false;
            menuShop.IsDefault = true;


            ShopWnd.Visibility = Visibility.Visible;
            GameInfo.Visibility = Visibility.Hidden;

            //Console.WriteLine("shop url:" + ShopWebUrl.Source.ToString());

            if (ShopWebUrl.Source.ToString() == "about:blank" || ShopWebUrl.Source.ToString() != CashSopWebUrl)
            {
                LoadingStatus(true, "Black");
                ShopWebUrl.Source = new Uri(CashSopWebUrl);
                ShopWebUrl.Loaded += ShopWebUrlComplete;
            }
        }

        private void ShopWebUrlComplete(object sender, RoutedEventArgs e)
        {
            LoadingStatus(false);
        }


        private void menuGames_Click(object sender, RoutedEventArgs e)
        {
            //ShopPanel.Visibility = Visibility.Hidden;
            menuGames.IsDefault = true;
            menuShop.IsDefault = false;

            ShopWnd.Visibility = Visibility.Hidden;
            GameInfo.Visibility = Visibility.Visible;
        }



        private void LoadDropMenus()
        {
            //  logo menu
            LogoDropMenu.addItem((string)App.Current.Resources["LogoMenuAccounts"], FontAwesome.WPF.FontAwesomeIcon.UserCircle, true, new Action(delegate() { menuAccounts_MouseDown(); }));
            LogoDropMenu.addItem((string)App.Current.Resources["LogoMenuSupport"], FontAwesome.WPF.FontAwesomeIcon.QuestionCircleOutline, true, new Action(delegate() { menuSupport_MouseDown(); }));
            LogoDropMenu.addItem((string)App.Current.Resources["LogoMenuSettings"], FontAwesome.WPF.FontAwesomeIcon.Cog, false, new Action(delegate() { menuSettings_MouseDown(); }));
            LogoDropMenu.addSpactor();
            LogoDropMenu.addItem((string)App.Current.Resources["LogoMenuForums"], FontAwesome.WPF.FontAwesomeIcon.CommentsOutline, true, new Action(delegate() { menuForums_MouseDown(); }));
            LogoDropMenu.addSpactor();
            LogoDropMenu.addItem((string)App.Current.Resources["LogoMenuLogout"], FontAwesome.WPF.FontAwesomeIcon.PowerOff, false, new Action(delegate() { MenwuLogout_MouseDown(); }));
            LogoDropMenu.addItem((string)App.Current.Resources["LogoMenuExit"], FontAwesome.WPF.FontAwesomeIcon.Lock, false, new Action(delegate() { menuExit_MouseDown(); }));

            //  user menu
            UserDropMenu.addItem((string)App.Current.Resources["UserStatusOnline"], FontAwesome.WPF.FontAwesomeIcon.Circle, false, new Action(delegate() { menuStatusOnline_MouseDown(); }), Brushes.Lime);
            UserDropMenu.addItem((string)App.Current.Resources["UserStatusAway"], FontAwesome.WPF.FontAwesomeIcon.Circle, false, new Action(delegate() { menuStatusAway_MouseDown(); }), Brushes.Orange);
            UserDropMenu.addItem((string)App.Current.Resources["UserStatusBusy"], FontAwesome.WPF.FontAwesomeIcon.Circle, false, new Action(delegate() { menuStatusBusy_MouseDown(); }), Brushes.Red);
            UserDropMenu.addSpactor();
            UserDropMenu.addItem((string)App.Current.Resources["LogoMenuLogout"], FontAwesome.WPF.FontAwesomeIcon.Lock, false, new Action(delegate() { MenwuLogout_MouseDown(); }));

        }

        private void menuAccounts_MouseDown()
        {
            Process.Start("https://accounts.arcangames.com/my/settings");
            LogoDropMenu.Visibility = Visibility.Hidden;
        }
        private void menuSupport_MouseDown()
        {
            Process.Start("https://support.arcangames.com/");
            LogoDropMenu.Visibility = Visibility.Hidden;
        }
        private void menuSettings_MouseDown()
        {
            LauncherFactory.getSettingsClass().Owner = this;
            LauncherFactory.getSettingsClass().LoadSettings();
            BlurWindow(true);
            LauncherFactory.getSettingsClass().Show();
            LogoDropMenu.Visibility = Visibility.Hidden;
        }
        private void menuForums_MouseDown()
        {
            Process.Start("https://forums.arcangames.com/");
            LogoDropMenu.Visibility = Visibility.Hidden;
        }
        private void menuExit_MouseDown()
        {
            App.Current.Shutdown();
        }
        private void MenwuLogout_MouseDown()
        {
            SendLogoutPacket();
        }

        private void menuStatusOnline_MouseDown()
        {
            SendStatusChange(Enums.UserInfo.UserStatus.Available);
            UserDropMenu.Visibility = Visibility.Hidden;
        }
        private void menuStatusBusy_MouseDown()
        {
            SendStatusChange(Enums.UserInfo.UserStatus.Busy);

            this.UserDropMenu.Visibility = Visibility.Hidden;
        }
        private void menuStatusAway_MouseDown()
        {
            SendStatusChange(Enums.UserInfo.UserStatus.Away);

            this.UserDropMenu.Visibility = Visibility.Hidden;
        }


        private void MainLogo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (LogoDropMenu.Visibility == Visibility.Visible)
                LogoDropMenu.Visibility = Visibility.Hidden;
            else
                LogoDropMenu.Visibility = Visibility.Visible;
        }

        public bool isWindowBlur { get; set; }

        private void MainUserNickname_Click(object sender, RoutedEventArgs e)
        {
            if (UserDropMenu.Visibility == Visibility.Hidden)
            {
                UserDropMenu.Visibility = Visibility.Visible;
            }
            else
            {
                UserDropMenu.Visibility = Visibility.Hidden;
            }
        }



        #endregion

       
        public void UpdateInfo()
        {
            Dispatcher.Invoke(() => {
                MainUserNickname.Nickname = Game_Data.Globals.nickname;
            });
            
        }



        private void SendLogoutPacket()
        {
            //
            App.connection.Disconnect(); // Disconnect();
            //
            //Dispatcher.Invoke(() =>
            //{
            //    ServerConnection.Send("", "CS_Logout");
            
            //});
        }




        private void SendStatusChange(Enums.UserInfo.UserStatus status)
        {

            MainUserNickname.Status = status;

            LauncherFactory.getFriendsClass().UpdateUserStatus(status);
            
            //Update settings , set default status!
            App.launcherSettings.data.UserStatus = status;
            LauncherApp.Game_Data.Globals.status = status;

            //Send update to server
            Packets.UserInfo.CSUserStatus packet = new Packets.UserInfo.CSUserStatus() {
                userID = LauncherApp.Game_Data.Globals.uid,
                userStatus = status
            };

            App.connection.Send(packet, "CS_UserStatus");
        }

        //Game List!!
        async Task Delay()
        {
            await Task.Delay(500);
        }

        public void onGameListRecv(Packets.GameData.SC_GameInfo packet)
        {
            Dispatcher.Invoke(() => {
                GameIconList.Children.Clear();
            });

            gamesInfo = packet;
            for(int i=0;i<packet.count;i++)
            {
                AddGameToList(i);
            }

            Thread.Sleep(100);
            LauncherFactory.getFriendsClass().SendFriendListPacket();

            Dispatcher.Invoke(() =>
            {
                LauncherFactory.getLoginClass().Hide();
                LauncherFactory.getAppClass().Show();
            });

            
        }

        public void UpdateInstalledGame(int gameID)
        {
            if (SelectedGameId != gameID) return;

            GameStartUpBtn.Tag = "start";
            GameStartUpBtn.Content = (string)App.Current.Resources["GameStartBtn"];
            OfflineInstall.Visibility = Visibility.Hidden;
        }

        private void AddGameToList(int gIndex)
        {

            Dispatcher.Invoke(() => {

                Packets.GameData.GameData gInfo = gamesInfo.gameInfo[gIndex];

                GameListItem glItem = new GameListItem();
                glItem.GameNameID = gInfo.gameName;
                glItem.GameName = gInfo.gameDisplayName;
                glItem.MouseDown += GameSelected;
                glItem.Tag = gInfo.gameID;
                glItem.IsSelected = false;

                if (gIndex == 0)
                {
                    glItem.Margin = new Thickness(0, 20, 0, 0);
                    glItem.RaiseEvent(new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left)
                    {
                        RoutedEvent = Mouse.MouseDownEvent,
                        Source = this,
                    });
                }

                GameIconList.Children.Add(glItem);
                GameIconList.UpdateLayout();

         });
        
        }

        private async void GameSelected(object sender, RoutedEventArgs e)
        {
            GameIconList.IsEnabled = false;


            this.LoadingStatus(true);

            await Delay();

            Dispatcher.Invoke(() =>
            {
                this.LoadingStatus(false);
            });


            GameListItem itemBtn = (GameListItem)sender;

            foreach (GameListItem gli in GameIconList.Children) gli.IsSelected = false;
            
            itemBtn.IsSelected = true;

            Packets.GameData.GameData gInfo = gamesInfo.gameInfo[(int)itemBtn.Tag];

            bool installedFlag = false;

            if (App.installedGamesList.ContainsKey(gInfo.gameName)) installedFlag = true;
            

            AppsWindow.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(gInfo.gameWndBgColor));

            GamtDesc.Text = (string)App.Current.Resources[string.Format("AppsGameDesc_{0}", gInfo.gameName)];
            GameTitle.Content = (string)App.Current.Resources[string.Format("AppsGameTitle_{0}", gInfo.gameName)];


            BitmapImage gLogo = new BitmapImage { };
            gLogo.BeginInit();
            gLogo.UriSource = new Uri(string.Format("pack://application:,,,/LauncherApp;component/resources/images/games/logo/main_{0}.png", gInfo.gameName.ToLower()));
            gLogo.EndInit();

            GameMainLogo.Source = gLogo;
            GameMainLogo.Tag = itemBtn.Tag;

            gLogo = new BitmapImage { };
            gLogo.BeginInit();
            gLogo.UriSource = new Uri(string.Format("pack://application:,,,/LauncherApp;component/resources/images/games/background/{0}.png", gInfo.gameName.ToLower()));
            gLogo.EndInit();

            ImageBrush tempImg = new ImageBrush(gLogo);
            tempImg.TileMode = TileMode.FlipXY;
            tempImg.Stretch = Stretch.UniformToFill;
            GameBackground.Background = tempImg;

            GameVideoUrl.Source = new Uri("http://tasdasdasdasdasd.com/");
            GameVideoUrl.Visibility = Visibility.Hidden;
            GameVideoBtn.Visibility = Visibility.Visible;

            GameForumBtn.Tag = gInfo.gameWebsiteReg;
            GameWebsiteBtn.Tag = gInfo.gameWebsite;

            GameStartUpBtn.Tag = "install";
            OfflineInstall.Visibility = Visibility.Visible;
            GameStartUpBtn.Content = (string)App.Current.Resources["GameDownloadBtn"];

            if (installedFlag)
            {
                GameStartUpBtn.Tag = "start";
                GameStartUpBtn.Content = (string)App.Current.Resources["GameStartBtn"];
                OfflineInstall.Visibility = Visibility.Hidden;

            }
            this.SelectedGameId = gInfo.gameID;
            GameIconList.IsEnabled = true;
            GameInfoPanel.Visibility = Visibility.Visible;
        }

        private void GameStartUpBtn_Click(object sender, RoutedEventArgs e)
        {
            Packets.GameData.GameData gInfo = gamesInfo.gameInfo[(int)GameMainLogo.Tag];
            Button sButton = (Button)sender;

            switch (sButton.Tag.ToString())
            {
                case "install":

                    InstallGame gInstallWnd = new InstallGame((int)GameMainLogo.Tag);
                    BlurWindow(true);
                    gInstallWnd.Owner = this;
                    LauncherFactory.SafeShow(gInstallWnd, true);

                    if (gInstallWnd.InstallDoneFlag)
                    {
                        // put here complete install code
                    }

                    break;

                case "offlineinstall":

                    

                    break;
                case "start":

                    Thread startGameThread = new Thread(() => { new GameManager().StartGame(gInfo.gameName);  });
                    startGameThread.Start();

                    break;
            }

        }

        public void RunGameStatus(bool status)
        {

            if (status)
            {
                GameStartUpBtn.Content = App.Current.Resources["GamePlayingBtn"];
                GameStartUpBtn.IsEnabled = false;
                GameIconList.IsEnabled = false;

                return;
            }

            GameStartUpBtn.Content = App.Current.Resources["GameStartBtn"];
            GameStartUpBtn.IsEnabled = true;
            GameIconList.IsEnabled = true;

        }

        private void AppsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void GameListMiniBtn_Click(object sender, RoutedEventArgs e)
        {

            if (GameIconListBorder.Width == 188) {

                LauncherFactory.ElementAnimation(GameIconListBorder, GameListItem.WidthProperty, 188, 70, 0.2, false);
                LauncherFactory.ElementAnimation(GameInfoPanel, StackPanel.MarginProperty, new Thickness(188, 0, 0, 0), new Thickness(70, 0, 0, 0), 0.2, true);
                LauncherFactory.ElementAnimation(CopyrightLibl, Label.MarginProperty, new Thickness(195, 0, 0, 0), new Thickness(85, 0, 0, 0), 0.2, true);

                GameListMiniBtn.Content = "";

                foreach (GameListItem gli in GameIconList.Children) gli.GameNameShow = true; ;
                return;
            }

            LauncherFactory.ElementAnimation(GameIconListBorder, GameListItem.WidthProperty, 70, 188, 0.2, false);
            LauncherFactory.ElementAnimation(GameInfoPanel, StackPanel.MarginProperty, new Thickness(70, 0, 0, 0), new Thickness(188, 0, 0, 0), 0.2, true);
            LauncherFactory.ElementAnimation(CopyrightLibl, Label.MarginProperty, new Thickness(85, 0, 0, 0), new Thickness(195, 0, 0, 0), 0.2, true);

            GameListMiniBtn.Content = "";

            foreach (GameListItem gli in GameIconList.Children) gli.GameNameShow = false; ;
        }




    }
}
