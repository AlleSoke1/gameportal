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

namespace LauncherApp.Styles.Pages
{
    /// <summary>
    /// Interaction logic for ChatControl.xaml
    /// </summary>
    public partial class MyGames : UserControl
    {

        public MyGames()
        {
            InitializeComponent();
        }


        #region UI Functions


        #endregion

        public void AddGameToList(string gameName, int listIndex)
        {

            GameList.Children.Add(new MyGamesItem() {
                ImageSource = string.Format("/LauncherResources;component/resources/images/games/background/mygames/{0}.png", gameName),
                GameListID = listIndex,
                GameID = gameName,
                Margin = new Thickness(5, 20, 5, 20)
            });

        }

        public void RunGameStatus(bool flag)
        {
            if (flag)
            {
                NotifyLabel.Height = double.NaN;
                GameList.IsEnabled = false;

                return;
            }

            NotifyLabel.Height =0;
            GameList.IsEnabled = true;

        }

    }
}
