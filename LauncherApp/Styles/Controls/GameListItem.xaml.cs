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

namespace LauncherApp.Styles.Controls
{
    /// <summary>
    /// Interaction logic for GameListItem.xaml
    /// </summary>
    public partial class GameListItem : UserControl
    {
        #region IsSelected DP
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); ChangeSelectFlag(value); }
        }

        public static readonly DependencyProperty IsSelectedProperty
            = DependencyProperty.Register(
                  "IsSelected",
                  typeof(bool),
                  typeof(GameListItem),
                  new PropertyMetadata(false)
              );
        #endregion

        #region GameName DP
        public string GameName
        {
            get { return (string)GetValue(GameNameProperty); }
            set { SetValue(GameNameProperty, value); }
        }

        public static readonly DependencyProperty GameNameProperty
            = DependencyProperty.Register(
                  "GameName",
                  typeof(string),
                  typeof(GameListItem)
              );
        #endregion

        #region GameNameShow DP
        public bool GameNameShow
        {
            get { return (bool)GetValue(GameNameShowProperty); }
            set { SetValue(GameNameShowProperty, value); ShowGameName(value); }
        }


        public static readonly DependencyProperty GameNameShowProperty
            = DependencyProperty.Register(
                  "GameNameShow",
                  typeof(bool),
                  typeof(GameListItem)
              );
        #endregion

        #region GameNameID DP
        public string GameNameID
        {
            get { return gameNameIDProb; }
            set { gameNameIDProb = value; SetGameIcon(value); }
        }
        public static string gameNameIDProb;
        #endregion


        public GameListItem()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        private void ShowGameName(bool hide)
        {
            if (hide)
            {
                GameNameLibl.Visibility = Visibility.Hidden;
                return;
            }

            GameNameLibl.Visibility = Visibility.Visible;
        }

        private void ChangeSelectFlag(bool show)
        {

            if (show)
            {
                this.Background.Opacity = 0.10;
                this.SelectedBar.Visibility = Visibility.Visible;
                return;
            }

            this.Background.Opacity = 0;
            this.SelectedBar.Visibility = Visibility.Hidden;

        }

        private void SetGameIcon(string gameNID)
        {
            BitmapImage gLogo = new BitmapImage { };
            gLogo.BeginInit();
            gLogo.UriSource = new Uri(string.Format("pack://application:,,,/LauncherApp;component/resources/images/games/logo/{0}.png", gameNID.ToLower()));
            gLogo.EndInit();
            
            GameIconImage.Source = gLogo;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Background.Opacity = 0.10;
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if(!this.IsSelected)
                this.Background.Opacity = 0;
        }


    }
}
