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
    public partial class FriendList : UserControl
    {


        #region SectionName DP
        public string SectionName
        {
            get { return (string)GetValue(SectionNameProperty); }
            set { SetValue(SectionNameProperty, value); }
        }

        public static readonly DependencyProperty SectionNameProperty
            = DependencyProperty.Register(
                  "SectionName",
                  typeof(string),
                  typeof(FriendList)
              );
        #endregion

        #region Counters DP
        public string Counters
        {
            get { return (string)GetValue(CountersProperty); }
            set { SetValue(CountersProperty, value); SetCounterValues(value); }
        }

        public static readonly DependencyProperty CountersProperty
            = DependencyProperty.Register(
                  "Counters",
                  typeof(string),
                  typeof(FriendList)
              );
        #endregion


        public FriendList()
        {
            InitializeComponent();

            //this.AddItem(new FriendListItem() { FriendName = "MGHossam" });
            //this.AddItem(new FriendListItem() { FriendName = "TheBeast" });
            //this.AddItem(new FriendListItem() { FriendName = "Loling" });

            this.DataContext = this;

        }



        #region UI Functions

        private void TittlePanel_MouseEnter(object sender, MouseEventArgs e)
        {
            TittlePanel.Background.Opacity = 0.10;
        }

        private void TittlePanel_MouseLeave(object sender, MouseEventArgs e)
        {
            TittlePanel.Background.Opacity = 0;
        }

        private void TittlePanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            double oldHight = ListPanel.ActualHeight == 0 ? 60 : ListPanel.ActualHeight + 30;

            if (ListElement.Height == 30)
            {
                ArrowIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.ChevronDown;
                LauncherFactory.ElementAnimation(ListElement, UserControl.HeightProperty,30, oldHight, 0.1, false );
                return;
            }

            ArrowIcon.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.ChevronRight;
            LauncherFactory.ElementAnimation(ListElement, UserControl.HeightProperty, oldHight, 30, 0.1, false);
            
        }

        #endregion

        private void SetCounterValues(string counters)
        {
            if (counters.Contains(","))
            {
                string[] aryStr = counters.Split(',');
                FriendCounter.Content = string.Format("- {0}/{1}", aryStr[0], aryStr[1]);
                return;
            }

            FriendCounter.Content = counters;
        }

        public void FilterUseres(string keyWord)
        {
            if (keyWord == "")
            {
                foreach (FriendListItem flItem in ListPanel.Children)
                {
                    flItem.Visibility = Visibility.Visible;
                    flItem.Height = 45;
                    flItem.Margin = new Thickness(0,0,0,1);
                }
                int orgHeight = (ListPanel.Children.Count * 46) + 30;
                LauncherFactory.ElementAnimation(ListElement, UserControl.HeightProperty, ListElement.ActualHeight, orgHeight, 0.1, false);
                return;
            }


            int showedRows = 0;

            foreach (FriendListItem flItem in ListPanel.Children)
            {
                if (flItem.FriendName.ToLower().Contains(keyWord.ToLower())){

                    flItem.Visibility = Visibility.Visible;
                    flItem.Height = 45;
                    flItem.Margin = new Thickness(0, 0, 0, 1);
                    showedRows++;
                }
                else
                {
                    flItem.Visibility = Visibility.Hidden;
                    flItem.Margin = new Thickness(0);
                    flItem.Height = 0;
                }
                    
            }

            LauncherFactory.ElementAnimation(ListElement, UserControl.HeightProperty, ListElement.ActualHeight, showedRows * 45 + 30, 0.1, false);

                
        }

       


        public void AddItem(UIElement elm)
        {
            FriendListItem temp = (FriendListItem)elm;
            temp.Margin = new Thickness(0, 0, 0, 1);
            temp.MouseLeftButtonDown += ListItemClicked;
            ListPanel.Children.Add(temp);

            double newHeight = (ListPanel.Children.Count * 45) + 30;
            LauncherFactory.ElementAnimation(ListElement, UserControl.HeightProperty, ListElement.ActualHeight, newHeight, 0.1, false);

            EmptyFlag.Visibility = Visibility.Hidden;

            Counters = string.Format("- {0}/{1}", LauncherApp.Game_Data.Globals.OnlineFriendCount, ListPanel.Children.Count);


        }

        private void ListItemClicked(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                FriendListItem objSender = (FriendListItem)sender;

                LauncherFactory.getAppClass().SocialPage.SwitchChatWindow(objSender.ChatID, objSender.FriendName, objSender.UserID, objSender.Status);

                foreach (FriendListItem flItem in ListPanel.Children)
                {
                    if (flItem == objSender)
                    {
                        flItem.IsSelected = true;
                    }
                    else
                    {
                        flItem.IsSelected = false;
                    }

                }
            }
            

        }


        public void RemoveItem(UIElement elm)
        {
            if (ListPanel.Children.Count == 1)
            {
                EmptyFlag.Visibility = Visibility.Visible;
            }
            ListPanel.Children.Remove(elm);

            double newHeight = (ListPanel.Children.Count * 45) - 30;
            LauncherFactory.ElementAnimation(ListElement, UserControl.HeightProperty, ListElement.ActualHeight, newHeight, 0.1, false);

        }


        public FriendListItem RemoveByUserID(long id)
        {
            foreach (FriendListItem item in ListPanel.Children)
            {
                if (item.UserID == id)
                {
                    if (App.ChatMan._openedChatList.ContainsKey(item.ChatID)) LauncherFactory.getAppClass().SocialPage.wndChatControl.Children.Remove(App.ChatMan._openedChatList[item.ChatID]);

                    this.RemoveItem(item);
                    return item;
                }
            }

            return null;

        }

        public FriendListItem GetItemByID(long id)
        {
            foreach (FriendListItem item in ListPanel.Children)
            {
                if (item.UserID == id)
                {
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
            this.Counters = "0,0";
        }


    }
}
