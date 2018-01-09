using LauncherApp.Styles.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace LauncherApp
{
    /// <summary>
    /// Interaction logic for Main.xaml
    /// </summary>
    public partial class MainApp : Window
    {
        public bool windowMaximizeState = false; //true = maximize , false = minimize
        public UIElement _currectPage = null;

        public MainApp()
        {
            InitializeComponent();

            LoadStyles();

        }


        #region UI FUNCTIONS

        private void LoadStyles()
        {

            _currectPage = HomePage;


        }


        private void MainBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && sender is Grid)
                this.DragMove();
        }

        private void tCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void tFullBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!windowMaximizeState)
            {
                tFullBtn.Icon = MahApps.Metro.IconPacks.PackIconMaterialKind.WindowMinimize;
                this.WindowState = WindowState.Maximized;
                MainGrid.Margin = new Thickness(0);
            }
            else
            {
                tFullBtn.Icon = MahApps.Metro.IconPacks.PackIconMaterialKind.WindowMaximize;
                this.WindowState = WindowState.Normal;
                MainGrid.Margin = new Thickness(10);
            }

            windowMaximizeState = !windowMaximizeState;
        }

        private void tMinieBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void MainUserNickname_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu temp = (ContextMenu)this.Resources["UserMenu"];
            temp.PlacementTarget = MainUserNickname;
            temp.IsOpen = true;
            
        }


        private void menuAccounts_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://accounts.arcangames.com/my/settings");
        }
        private void menuSupport_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://support.arcangames.com/");
        }
        private void menuSettings_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.SettingsPage.LoadSettings();
            this.SwitchToPage("Settings");
        }
        private void menuForums_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://forums.arcangames.com/");
        }
        private void menuExit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            App.Current.Shutdown();
        }
        private void MenwuLogout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SendLogoutPacket();
        }

        private void menuStatusOnline_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SendStatusChange(Enums.UserInfo.UserStatus.Available);
        }
        private void menuStatusBusy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SendStatusChange(Enums.UserInfo.UserStatus.Busy);

        }
        private void menuStatusAway_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SendStatusChange(Enums.UserInfo.UserStatus.Away);

        }


        private void AppLogo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ContextMenu temp = (ContextMenu)this.Resources["LogoMenu"];
            temp.PlacementTarget = this;
            temp.IsOpen = true;
        }


        private void SwitchPageButton_Click(object sender, RoutedEventArgs e)
        {
            // get objs
            HeaderButton senderOBJ = (HeaderButton)sender;
            string tragetName = string.Format("{0}Page", senderOBJ.Tag.ToString());

            if (((Control)_currectPage).Name == tragetName) return;

            var pervPage = _currectPage;
            UIElement nextPage = (UIElement)BodyGrid.FindName(tragetName);

            // switch pages
            Panel.SetZIndex(pervPage, 0);
            Panel.SetZIndex(nextPage, 2);
            nextPage.Opacity = 0;
            nextPage.Visibility = Visibility.Visible;

            //Action tempAction = new Action(() =>
            //       {
            //           pervPage.Visibility = Visibility.Hidden;

            //       });
            LauncherFactory.ElementAnimation(nextPage, UIElement.OpacityProperty, 0, 1, 0.2, false);
            LauncherFactory.ElementAnimation(pervPage, UIElement.OpacityProperty, 1, 0, 0.2, false);


            _currectPage = nextPage;

            //change actived button
            foreach (HeaderButton tempBtn in SideBar.Children) { tempBtn.isActive = false; }
            senderOBJ.isActive = true;

            switch(((Control)nextPage).Name){
                case "ShopPage":
                    this.ShopPage.LoadShopUrl();
                    break;
                case "SettingsPage":
                    this.SettingsPage.LoadSettings();
                    break;
            }
            

        }

        public void SwitchToPage(string pageName)
        {
            HeaderButton ownerBtn = (HeaderButton)SideBar.FindName(string.Format("sbar{0}", pageName));
            SwitchPageButton_Click(ownerBtn, null);
        }
        #endregion


        #region User Functions


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
        public void UpdateInfo()
        {
            Dispatcher.Invoke(() =>
            {
                MainUserNickname.Nickname = Game_Data.Globals.nickname;
            });

        }


        private void SendStatusChange(Enums.UserInfo.UserStatus status)
        {

            MainUserNickname.Status = status;


            //Update settings , set default status!
            App.launcherSettings.data.UserStatus = status;
            LauncherApp.Game_Data.Globals.status = status;

            //Send update to server
            Packets.UserInfo.CSUserStatus packet = new Packets.UserInfo.CSUserStatus()
            {
                userID = LauncherApp.Game_Data.Globals.uid,
                userStatus = status
            };

            App.connection.Send(packet, "CS_UserStatus");
        }
        #endregion






    }
}
