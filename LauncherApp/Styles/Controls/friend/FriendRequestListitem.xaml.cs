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

        #region Action DP
        public Enums.Friend.FriendRequestOptions Action
        {
            get { return (Enums.Friend.FriendRequestOptions)GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }

        public static readonly DependencyProperty ActionProperty
            = DependencyProperty.Register(
                  "Action",
                  typeof(Enums.Friend.FriendRequestOptions),
                  typeof(FriendRequestListitem)
              );
        #endregion

        public FriendRequestListitem()
        {
            InitializeComponent();

            this.DataContext = this;
            
        }

        private void ItemElement_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Background.Opacity = 0.10;
            this.bottomLine.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#303337"));
        }

        private void ItemElement_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Background.Opacity = 0;
            this.bottomLine.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF717171"));
        }

        #region UI Functions

        #endregion


        private void onOptionClick(object sender, RoutedEventArgs e)
        {
            switch(((Control)sender).Name){
                case "acceptRequest":
                    this.Action = Enums.Friend.FriendRequestOptions.Approve;
                    break;
                case "declineRequest":
                    this.Action = Enums.Friend.FriendRequestOptions.Ignore;
                    break;
                case "blockRequest":
                    this.Action = Enums.Friend.FriendRequestOptions.Block;
                    break;
            }

            if (this.OptionClick != null)
            {
                this.OptionClick(this, e);
            }
        }

    }


}
