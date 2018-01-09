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
    /// Interaction logic for ScanListItem.xaml
    /// </summary>
    public partial class ScanListItem : UserControl
    {
        #region IsSelected DP
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty
            = DependencyProperty.Register(
                  "IsSelected",
                  typeof(bool),
                  typeof(ScanListItem),
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
                  typeof(ScanListItem)
              );
        #endregion

        #region GamePath DP
        public string GamePath
        {
            get { return (string)GetValue(GamePathProperty); }
            set { SetValue(GamePathProperty, value); }
        }

        public static readonly DependencyProperty GamePathProperty
            = DependencyProperty.Register(
                  "GamePath",
                  typeof(string),
                  typeof(ScanListItem)
              );
        #endregion

        #region GameExeFile DP
        public string GameExeFile
        {
            get { return GameExeFileProb; }
            set { GameExeFileProb = value; }
        }
        public static string GameExeFileProb;
       
        #endregion

        #region GameNameID DP
        public string GameNameID
        {
            get { return gameNameIDProb; }
            set { gameNameIDProb = value; SetGameIcon(value); }
        }
        public static string gameNameIDProb;
        #endregion

        #region GameIndex DP
        public int GameIndex
        {
            get { return GameIndexProb; }
            set { GameIndexProb = value; }
        }
        public static int GameIndexProb;
        #endregion

        public ScanListItem()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void SetGameIcon(string gameNID)
        {
            BitmapImage gLogo = new BitmapImage { };
            gLogo.BeginInit();
            gLogo.UriSource = new Uri(string.Format("pack://application:,,,/LauncherResources;component/resources/images/games/logo/{0}.png", gameNID.ToLower()));
            gLogo.EndInit();
            
            GameIconImage.Source = gLogo;
        }

    }
}
