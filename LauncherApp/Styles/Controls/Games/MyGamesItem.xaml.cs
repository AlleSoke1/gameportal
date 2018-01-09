using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LauncherApp.Styles.Controls
{
    /// <summary>
    /// Interaction logic for MyGamesItem.xaml
    /// </summary>
    public partial class    MyGamesItem : UserControl
    {

        #region ImageSource DP
        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty
            = DependencyProperty.Register(
                  "ImageSource",
                  typeof(string),
                  typeof(MyGamesItem)
              );
        #endregion

        #region GameID DP
        public string GameID
        {
            get { return (string)GetValue(GameIDProperty); }
            set { SetValue(GameIDProperty, value); }
        }

        public static readonly DependencyProperty GameIDProperty
            = DependencyProperty.Register(
                  "GameID",
                  typeof(string),
                  typeof(MyGamesItem)
              );
        #endregion

        #region GameListID DP
        public int GameListID
        {
            get { return (int)GetValue(GameListIDProperty); }
            set { SetValue(GameListIDProperty, value); }
        }

        public static readonly DependencyProperty GameListIDProperty
            = DependencyProperty.Register(
                  "GameListID",
                  typeof(int),
                  typeof(MyGamesItem)
              );
        #endregion

        public MyGamesItem()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void runGameBtn_Click(object sender, RoutedEventArgs e)
        {
            string gameName = this.GameID;
            Thread startGameThread = new Thread(() => { new GameManager().StartGame(gameName); });
            startGameThread.Start();

        }

        private void viewGameBtn_Click(object sender, RoutedEventArgs e)
        {

            LauncherFactory.getAppClass().SwitchToPage("Home");

            ((GameListItem)LauncherFactory.getAppClass().HomePage.GameIconList.Children[(this.GameListID - 1)]).RaiseEvent(new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left)
            {
                RoutedEvent = Mouse.MouseDownEvent,
                Source = LauncherFactory.getAppClass().HomePage,
            });

        }


    }
}
