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
    /// Interaction logic for FriendList.xaml
    /// </summary>
    public partial class FriendRequestList : UserControl
    {


        #region RequestCount DP
        public int RequestCount
        {
            get { return (int)GetValue(RequestCountProperty); }
            set { SetValue(RequestCountProperty, value); }
        }

        public static readonly DependencyProperty RequestCountProperty
            = DependencyProperty.Register(
                  "RequestCount",
                  typeof(int),
                  typeof(FriendRequestList)
              );
        #endregion

        public event MouseButtonEventHandler OnMenuClick;
        public event RoutedEventHandler OnOptionClick;

        public FriendRequestList()
        {
            InitializeComponent();

            //this.AddItem(new FriendRequestListitem() { UesrName = "MGHossam" });
            //this.AddItem(new FriendRequestListitem() { UesrName = "TheBeast" });
            //this.AddItem(new FriendRequestListitem() { UesrName = "Loling" });

            if (RequestCount == 0) this.Height = 0;
            this.DataContext = this;

        }



        #region UI Functions

        private void TittlePanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            double oldHight = ListPanel.ActualHeight == 0 ? 90 : ListPanel.ActualHeight + 30;

            if (ListElement.Height == 30)
            {
                LauncherFactory.ElementAnimation(ListElement, UserControl.HeightProperty,30, oldHight, 0.1, false );
                return;
            }

            LauncherFactory.ElementAnimation(ListElement, UserControl.HeightProperty, oldHight, 30, 0.1, false);
            
        }

        #endregion

        public void AddItem(UIElement elm)
        {
            FriendRequestListitem temp = (FriendRequestListitem)elm;
            temp.Margin = new Thickness(0, 0, 0, 1);
            temp.MenuClick += ListItemMenuClicked;
            temp.OptionClick += ListItemOptionClicked;
            ListPanel.Children.Add(temp);

            double newHeight = (ListPanel.Children.Count * 60) + 30;
            LauncherFactory.ElementAnimation(ListElement, UserControl.HeightProperty, ListElement.ActualHeight, newHeight, 0.1, false);

            RequestCount = ListPanel.Children.Count;
        }

        private void ListItemOptionClicked(object sender, RoutedEventArgs e)
        {
            if (this.OnOptionClick != null)
            {
                this.OnOptionClick(sender, e);
            }
        }

        private void ListItemMenuClicked(object sender, MouseButtonEventArgs e)
        {
            if (this.OnMenuClick != null)
            {
                this.OnMenuClick(sender, e);
            }

        }

        public void RemoveItem(UIElement elm)
        {
            ListPanel.Children.Remove(elm);

            RequestCount = ListPanel.Children.Count;

            if (RequestCount == 0) LauncherFactory.ElementAnimation(ListElement, UserControl.HeightProperty, ListElement.ActualHeight, 0, 0.1, false);  
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

        private void ListElement_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Height <= 30)
            {
                arrowIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.AngleUp;
                return;
            }

            arrowIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.AngleDown;
        }


    }
}
