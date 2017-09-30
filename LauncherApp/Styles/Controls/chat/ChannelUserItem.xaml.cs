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
    /// Interaction logic for ChannelUserItem.xaml
    /// </summary>
    public partial class ChannelUserItem : UserControl
    {


        #region UserName DP
        public string UserName
        {
            get { return (string)GetValue(UserNameProperty); }
            set { SetValue(UserNameProperty, value); }
        }

        public static readonly DependencyProperty UserNameProperty
            = DependencyProperty.Register(
                  "UserName",
                  typeof(string),
                  typeof(ChannelUserItem)
              );
        #endregion

        #region UserID DP
        public long UserID
        {
            get { return (long)GetValue(UserIDProperty); }
            set { SetValue(UserIDProperty, value); }
        }

        public static readonly DependencyProperty UserIDProperty
            = DependencyProperty.Register(
                  "UserID",
                  typeof(long),
                  typeof(ChannelUserItem)
              );
        #endregion

        #region IsOnline DP
        public bool IsOnline
        {
            get { return (bool)GetValue(IsOnlineProperty); }
            set { SetValue(IsOnlineProperty, value); SetOnlineStatus(value); }
        }


        public static readonly DependencyProperty IsOnlineProperty
            = DependencyProperty.Register(
                  "IsOnline",
                  typeof(bool),
                  typeof(ChannelUserItem)
              );
        #endregion


        public ChannelUserItem()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        private void SetOnlineStatus(bool value)
        {
            if (value)
            {
                this.Opacity = 1;
                return;
            }

            this.Opacity = 0.5;
        }


    }
}
