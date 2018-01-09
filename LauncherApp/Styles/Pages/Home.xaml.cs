using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using LauncherApp.Styles.Controls;
using System.Windows.Media.Effects;

namespace LauncherApp.Styles.Pages
{
    /// <summary>
    /// Interaction logic for ChatControl.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public Packets.GameData.SC_GameInfo gamesInfo;
        private int SelectedGameId;

        public Home()
        {
            InitializeComponent();
        }


        #region UI Functions

        private void GameWebsiteBtn_Click(object sender, RoutedEventArgs e)
        {
            Control sBtn = (Control)sender;
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


        private void OfflineInstall_Click(object sender, RoutedEventArgs e)
        {
            GameScanPage.Opacity = 0;
            GameScanPage.Visibility = Visibility.Visible;
            Action tempAction = new Action(() =>
                   {
                       GameScanPage.StartScanner();

                   });
            LauncherFactory.ElementAnimation(GameScanPage, UIElement.OpacityProperty, 0, 1, 0.2, false, tempAction);
        }


        private void ShopWebUrlComplete(object sender, RoutedEventArgs e)
        {
            LoadingStatus(false);
        }


        private void GameListMiniBtn_Click(object sender, RoutedEventArgs e)
        {

            if (GameIconListBorder.Width == 188)
            {

                LauncherFactory.ElementAnimation(GameIconListBorder, GameListItem.WidthProperty, 188, 70, 0.2, false);
                LauncherFactory.ElementAnimation(GameInfoPanel, StackPanel.MarginProperty, new Thickness(188, 0, 0, 0), new Thickness(70, 0, 0, 0), 0.2, true);
                LauncherFactory.ElementAnimation(HomeFooterStrings, Label.MarginProperty, new Thickness(195, 0, 0, 0), new Thickness(85, 0, 0, 0), 0.2, true);

                GameListMiniBtn.ExtraIcon = MahApps.Metro.IconPacks.PackIconMaterialKind.ArrowRight;

                foreach (GameListItem gli in GameIconList.Children) gli.GameNameShow = true; ;
                return;
            }

            LauncherFactory.ElementAnimation(GameIconListBorder, GameListItem.WidthProperty, 70, 188, 0.2, false);
            LauncherFactory.ElementAnimation(GameInfoPanel, StackPanel.MarginProperty, new Thickness(70, 0, 0, 0), new Thickness(188, 0, 0, 0), 0.2, true);
            LauncherFactory.ElementAnimation(HomeFooterStrings, Label.MarginProperty, new Thickness(85, 0, 0, 0), new Thickness(195, 0, 0, 0), 0.2, true);

            GameListMiniBtn.ExtraIcon = MahApps.Metro.IconPacks.PackIconMaterialKind.ArrowLeft;

            foreach (GameListItem gli in GameIconList.Children) gli.GameNameShow = false; ;
        }




        private void HomePage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width > 1100 && e.NewSize.Height > 600)
            {
                GameMainLogo.Visibility = Visibility.Visible;
                GameLogoName.Visibility = Visibility.Hidden;
                VideoPanel.Visibility = Visibility.Visible;
                GameTexts.Margin = new Thickness(0);

                GameBackground.Height = 737;
                GameBackground.Width = 1237;
                GameBackground.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                GameBackground.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                GameLogoName.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left;
                GameLinks.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                return;
            }

            GameMainLogo.Visibility = Visibility.Hidden;
            GameLogoName.Visibility = Visibility.Visible;
            VideoPanel.Visibility = Visibility.Hidden;
            GameTexts.Margin = new Thickness(-480, 0, 0, 0);

            GameBackground.Height = Double.NaN;
            GameBackground.Width = Double.NaN;
            GameBackground.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
            GameBackground.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            GameLogoName.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            GameLinks.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
        }

        #endregion


        public void onGameListRecv(Packets.GameData.SC_GameInfo packet)
        {
            Dispatcher.Invoke(() =>
            {
                GameIconList.Children.Clear();
            });

            gamesInfo = packet;
            for (int i = 0; i < packet.count; i++)
            {
                AddGameToList(i);
            }

            Thread.Sleep(100);
            LauncherFactory.getAppClass().SocialPage.SendFriendListPacket();

            Dispatcher.Invoke(() =>
            {
                LauncherFactory.getLoginClass().Hide();
                LauncherFactory.getAppClass().Show();
            });


        }


        private void AddGameToList(int gIndex)
        {

            Dispatcher.Invoke(() =>
            {

                Packets.GameData.GameData gInfo = gamesInfo.gameInfo[gIndex];

                GameListItem glItem = new GameListItem();
                glItem.GameNameID = gInfo.gameName;
                glItem.SetResourceReference(GameListItem.GameNameProperty, "AppsGameName_" + gInfo.gameName);
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

                if (App.installedGamesList.ContainsKey(gInfo.gameName))
                {
                    LauncherFactory.getAppClass().MyGamesPage.AddGameToList(gInfo.gameName, GameIconList.Children.Count);
                }
            });

        }

        public int GetGameListIDByName(string gameName) {

            int counter = 1;
            foreach (GameListItem item in GameIconList.Children)
            {
                if (item.GameName == gameName)
                {
                    return counter;
                }
            }

            return counter;
        }
        async Task Delay()
        {
            await Task.Delay(500);
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


            ElementGird.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString(gInfo.gameWndBgColor));

            GamtDesc.SetResourceReference(TextBlock.TextProperty, string.Format("AppsGameDesc_{0}", gInfo.gameName));
            GameTitle.SetResourceReference(ContentProperty, string.Format("AppsGameTitle_{0}", gInfo.gameName));
            var tempEffect = new DropShadowEffect()
            {

                Color = ((Color)ColorConverter.ConvertFromString(gInfo.gameWndColor)),
                ShadowDepth = 0,
                Direction = 1000,
                BlurRadius = 20
            };
            GameLogoName.Effect = tempEffect;
            GameTitle.Effect = tempEffect;

            BitmapImage gLogo = new BitmapImage { };
            gLogo.BeginInit();
            gLogo.UriSource = new Uri(string.Format("pack://application:,,,/LauncherResources;component/resources/images/games/logo/main_{0}.png", gInfo.gameName.ToLower()));
            gLogo.EndInit();

            GameMainLogo.Source = gLogo;
            GameMainLogo.Tag = itemBtn.Tag;
            GameLogoName.SetResourceReference(ContentProperty, "AppsGameName_" + gInfo.gameName);

            gLogo = new BitmapImage { };
            gLogo.BeginInit();
            gLogo.UriSource = new Uri(string.Format("pack://application:,,,/LauncherResources;component/resources/images/games/background/{0}.png", gInfo.gameName.ToLower()));
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
            GameStartUpBtn.SetResourceReference(SpecialButton.TextProperty, "GameDownloadBtn");
            GameStartUpBtn.ExtraIcon = MahApps.Metro.IconPacks.PackIconMaterialKind.Download;

            if (installedFlag)
            {
                GameStartUpBtn.Tag = "start";
                GameStartUpBtn.SetResourceReference(SpecialButton.TextProperty, "GameStartBtn");
                GameStartUpBtn.ExtraIcon = MahApps.Metro.IconPacks.PackIconMaterialKind.Play;
                OfflineInstall.Visibility = Visibility.Hidden;

            }
            this.SelectedGameId = gInfo.gameID;
            GameIconList.IsEnabled = true;
            GameInfoPanel.Visibility = Visibility.Visible;
        }


        public void UpdateInstalledGame(int gameID)
        {
            if (SelectedGameId != gameID) return;

            GameStartUpBtn.Tag = "start";
            GameStartUpBtn.SetResourceReference(SpecialButton.TextProperty, "GameStartBtn");
            GameStartUpBtn.ExtraIcon = MahApps.Metro.IconPacks.PackIconMaterialKind.Play;
            OfflineInstall.Visibility = Visibility.Hidden;
        }


        private void GameStartUpBtn_Click(object sender, RoutedEventArgs e)
        {
            Packets.GameData.GameData gInfo = gamesInfo.gameInfo[(int)GameMainLogo.Tag];
            Control sButton = (Control)sender;

            switch (sButton.Tag.ToString())
            {
                case "install":

                    GameInstallPage.NewGameInstall((int)GameMainLogo.Tag);
                    GameInstallPage.Opacity = 0;
                    GameInstallPage.Visibility = Visibility.Visible;
                    LauncherFactory.ElementAnimation(GameInstallPage, UIElement.OpacityProperty, 0, 1, 0.2, false);

                    break;

                case "offlineinstall":



                    break;
                case "start":

                    Thread startGameThread = new Thread(() => { new GameManager().StartGame(gInfo.gameName); });
                    startGameThread.Start();

                    break;
            }

        }


        public void RunGameStatus(bool status)
        {

            if (status)
            {
                GameStartUpBtn.SetResourceReference(SpecialButton.TextProperty, "GamePlayingBtn");
                GameStartUpBtn.ExtraIcon = MahApps.Metro.IconPacks.PackIconMaterialKind.Timer;
                GameStartUpBtn.IsEnabled = false;
                GameIconList.IsEnabled = false;
                PlayTimeLabel.Visibility = Visibility.Visible;

                return;
            }

            GameStartUpBtn.SetResourceReference(SpecialButton.TextProperty, "GameStartBtn");
            GameStartUpBtn.ExtraIcon = MahApps.Metro.IconPacks.PackIconMaterialKind.Play;
            GameStartUpBtn.IsEnabled = true;
            GameIconList.IsEnabled = true;
            PlayTimeLabel.Visibility = Visibility.Hidden;

        }


        internal void UpdatePlayTime(long secounds)
        {
            TimeSpan _timeSpan = TimeSpan.FromSeconds(secounds);
            PlayTimeLabel.Content = string.Format((string)App.Current.Resources["MainGameTimer"], _timeSpan.Hours, _timeSpan.Minutes, _timeSpan.Seconds);
        }



    }
}
