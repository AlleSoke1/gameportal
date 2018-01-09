using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FontAwesome.WPF;
using System.IO;
using LauncherData;
using LauncherResources;

namespace LauncherApp
{
    /// <summary>
    /// Interaction logic for StartupWindow.xaml
    /// </summary>
    public partial class StartupWindow : Window
    {
        public StartupWindow()
        {
            InitializeComponent();
            LoadStyles();

            App.notifyIcon = new System.Windows.Forms.NotifyIcon();
            Stream iconStream = Application.GetResourceStream(new Uri("pack://application:,,,/LauncherResources;component/resources/icon.ico")).Stream;
            App.notifyIcon.Icon = new System.Drawing.Icon(iconStream);
            App.notifyIcon.Visible = true;
            App.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(App.notifyIcon_Click);
            App.notifyIcon.ShowBalloonTip(1000, "Arcan App", "Work on background of your pc.", System.Windows.Forms.ToolTipIcon.Info);


            App.notifyIconMenu.MenuItems.Add("Show App", new EventHandler(App.notifyIcon_ShowApp));
            App.notifyIconMenu.MenuItems.Add("Exit", new EventHandler(App.notifyIcon_Exitbtn));

            App.notifyIcon.ContextMenu = App.notifyIconMenu;

            //new MainApp().Show();

        }


        #region UI Functions

        private void LoadStyles()
        {

        }

        private void StartupWindow1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        #endregion

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += PreInitialize;
            worker.ProgressChanged += worker_ProgressChanged;

            worker.RunWorkerAsync();

            Thread LoadInstallGames = new Thread(() => { LoadPcInstalledGames();  });
            LoadInstallGames.IsBackground = true;
            LoadInstallGames.Start();


        }

        private void LoadPcInstalledGames()
        {
            InstalledGames lg = new InstalledGames(true);
            App.installedGamesList = lg.iGameList;
        }


        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            StartUpBar.Value = e.ProgressPercentage;
            if (StartUpBar.Value == 100)
            {
                //LauncherFactory.getNotifyClass().Show();
                LauncherFactory.getLoginClass().Show();

                this.Hide();

            }
        }

        private void PreInitialize(object sender, DoWorkEventArgs e)
        {

            for (int i = 0; i <= 100; i++)
            {
                (sender as BackgroundWorker).ReportProgress(i);
                Thread.Sleep(25);
            }

            //starting up work here.
            checkScreenSize();
        }

        private void checkScreenSize()
        {
            double sWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double sHeight = System.Windows.SystemParameters.PrimaryScreenHeight;

            if (sWidth > 1300 && sHeight > 800)
            {
                Dispatcher.Invoke(() =>
                {
                    LauncherFactory.getAppClass().Height = 660;
                    LauncherFactory.getAppClass().Width = 1300;
                });
            }
        }
    }
}
