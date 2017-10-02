using FontAwesome.WPF;
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
    /// Interaction logic for FriendRequestListtem.xaml
    /// </summary>
    public partial class FriendRequestListitem : UserControl
    {

        public event MouseButtonEventHandler MenuClick;
        public event RoutedEventHandler OptionClick;


        #region UesrName DP
        public string UesrName
        {
            get { return (string)GetValue(UesrNameProperty); }
            set { SetValue(UesrNameProperty, value); }
        }

        public static readonly DependencyProperty UesrNameProperty
            = DependencyProperty.Register(
                  "UesrName",
                  typeof(string),
                  typeof(FriendListItem)
              );
        #endregion

        #region UserID DP
        public long UserID
        {
            get { return (long)GetValue(UserIDProperty); }
            set { SetValue(UserIDProperty, value);}
        }

        public static readonly DependencyProperty UserIDProperty
            = DependencyProperty.Register(
                  "UserID",
                  typeof(long),
                  typeof(FriendRequestListitem)
              );
        #endregion

        public FriendRequestListitem()
        {
            InitializeComponent();

            this.DataContext = this;
            
        }



        #region UI Functions

        #endregion


        private void onMenuClick(object sender, MouseButtonEventArgs e)
        {
            if (this.MenuClick != null)
            {
                this.MenuClick(sender, e);
            }
        }

        private void onOptionClick(object sender, RoutedEventArgs e)
        {
            if (this.OptionClick != null)
            {
                this.OptionClick(sender, e);
            }
        }



        private void UserMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu temp = (ContextMenu)this.Resources["UserMenu"];
            temp.PlacementTarget = (Button)sender;
            temp.IsOpen = true;
        }

        private void acceptRequest_Click(object sender, RoutedEventArgs e)
        {

        }






    }
}
