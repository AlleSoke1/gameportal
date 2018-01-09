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

namespace LauncherApp.Styles.Controls
{
    /// <summary>
    /// Interaction logic for FriendRequestControl.xaml
    /// </summary>
    public partial class FriendRequestControl : UserControl
    {


        public event RoutedEventHandler OnOptionClick;


        public FriendRequestControl()
        {
            InitializeComponent();
        }


        public void AddItem(UIElement elm)
        {
            FriendRequestListitem temp = (FriendRequestListitem)elm;
            temp.Margin = new Thickness(0, 0, 0, 1);
            temp.OptionClick += ListItemOptionClicked;
            ListPanel.Children.Add(temp);

        }

        public void RemoveItem(UIElement elm)
        {
            ListPanel.Children.Remove(elm);

        }

        public FriendRequestListitem RemoveByUserID(long id)
        {
            foreach (FriendRequestListitem item in ListPanel.Children)
            {
                if (item.UserID == id)
                {
                    this.RemoveItem(item);
                    return item;
                }
            }

            return null;

        }


        public void RemoveItemAt(int elmIndex)
        {
            ListPanel.Children.RemoveAt(elmIndex);
        }
        public void ClearItems()
        {
            ListPanel.Children.Clear();

        }

        private void ListItemOptionClicked(object sender, RoutedEventArgs e)
        {
            if (this.OnOptionClick != null)
            {
                this.OnOptionClick(sender, e);
            }
        }

    }
}
