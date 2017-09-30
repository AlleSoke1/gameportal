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
    /// Interaction logic for FriendListItem.xaml
    /// </summary>
    public partial class ChannelListItem : UserControl
    {


        public event MouseButtonEventHandler MenuClick;

        #region IsSelected DP
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); SwithSelectedStatus(value); }
        }

        public static readonly DependencyProperty IsSelectedProperty
            = DependencyProperty.Register(
                  "IsSelected",
                  typeof(bool),
                  typeof(ChannelListItem)
              );
        #endregion

        #region ChannelName DP
        public string ChannelName
        {
            get { return (string)GetValue(ChannelNameProperty); }
            set { SetValue(ChannelNameProperty, value); }
        }

        public static readonly DependencyProperty ChannelNameProperty
            = DependencyProperty.Register(
                  "ChannelName",
                  typeof(string),
                  typeof(ChannelListItem)
              );
        #endregion

        #region ChatID DP
        public long ChatID
        {
            get { return ChatIDProperty; }
            set { ChatIDProperty = value; SetChatIDLibl(value); }
        }
        public static long ChatIDProperty;
        #endregion

        public string menuOrder;

        public ChannelListItem()
        {
            InitializeComponent();

            this.DataContext = this;
            
        }

        #region UI Functions

        private void TittlePanel_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsSelected)
            {
                TittlePanel.Background.Opacity = 0.10;
                PanelBorder.BorderBrush.Opacity = 1;
            }
            
        }

        private void TittlePanel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsSelected)
            {
                TittlePanel.Background.Opacity = 0;
                PanelBorder.BorderBrush.Opacity = 0;
            }
            
        }

        private void SetChatIDLibl(long id)
        {
            ChannelID.Content = "#" + id;
        }


        private void SwithSelectedStatus(bool value)
        {
            if (value)
            {
                PanelBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFDC163B");
                TittlePanel.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFDC163B");
                TittlePanel.Background.Opacity = 0.10;
                PanelBorder.BorderBrush.Opacity = 1;
                return;
            }

            PanelBorder.BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF68394B");
            TittlePanel.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FFFFFFFF");
            TittlePanel.Background.Opacity = 0;
            PanelBorder.BorderBrush.Opacity = 0;

        }
        
        private void TittlePanel_MouseDown(object sender, MouseButtonEventArgs e)
        {

            
        }
        #endregion


        private void onMenuClick(object sender, MouseButtonEventArgs e)
        {
            if (this.MenuClick != null)
            {
                menuOrder = ((StackPanel)sender).Name.ToString();
                this.MenuClick(this, e);
                
            }
        }


        private void ChannelMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            ContextMenu temp = (ContextMenu)this.Resources["ChannelMenu"];
            temp.PlacementTarget = (Button)sender;
            temp.IsOpen = true;
        }






    }
}
