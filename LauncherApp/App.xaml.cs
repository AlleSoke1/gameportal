using LauncherData;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace LauncherApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //TaskBar iCON
        public static System.Windows.Forms.NotifyIcon notifyIcon;
        public static System.Windows.Forms.ContextMenu notifyIconMenu = new System.Windows.Forms.ContextMenu();

        //List of Installed Games in this pc
        public static Dictionary<string, GameInfo> installedGamesList;

        public static string DefInstallPath = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory), "arcangames\\{0}");

        //Game Settings 
        public static LauncherData.LauncherSettings launcherSettings;

        public static NotifyManager.NotifyManager NotifyMan = new NotifyManager.NotifyManager();

        //Network
        public static Network.ServerConnection connection;


        public static LauncherApp.Logic.ChatManager ChatMan = new LauncherApp.Logic.ChatManager();
        public static LauncherApp.Logic.SoundManager SoundMan = new LauncherApp.Logic.SoundManager();


        #region Global Info

        public static string _conServerIp = "176.9.85.148";

        #endregion


        public App()
        {
            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri("pack://application:,,,/LauncherApp;component/Styles/Style.xaml");
            App.Current.Resources.MergedDictionaries.Add(dict);

            new LauncherFactory();

        }

        [STAThread]
        public static void NetworkThread()
        {
            //Network
            connection = new Network.ServerConnection();
        }

        [STAThread]
        public static void Main()
        {
            bool CheckRunningResult;
            new Mutex(true, "RlktGameLauncher", out CheckRunningResult);

            if (!CheckRunningResult)
            {
                // put here code to show the app on top of screen if it's already running.
                return;
            }

            //UI
            Thread NetThread = new Thread(NetworkThread);
            NetThread.Start();

            launcherSettings = new LauncherData.LauncherSettings(true);
           
            new App().Run();

        }


        #region UI Functions

        public static void notifyIcon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (Game_Data.Globals.uid > 0)
            {
                LauncherFactory.getAppClass().Show();
            }
            else
            {
                LauncherFactory.getLoginClass().Show();
            }
        }

        public static void notifyIcon_Exitbtn(object sender, EventArgs e)
        {
            //send logout packet to the server.

            //shutdown app
            App.Current.Shutdown();
        }


        public static void notifyIcon_ShowApp(object sender, EventArgs e)
        {
            if (Game_Data.Globals.uid > 0)
            {
                LauncherFactory.getAppClass().Show();
            }
            else
            {
                LauncherFactory.getLoginClass().Show();
            }
        }

        void App_Exit(object sender, ExitEventArgs e)
        {
            try
            {
                App.notifyIcon.Visible = false;
                Application.Current.Shutdown();
            }
            catch { }
        }

        #endregion



    }

}
