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
    public partial class ChannelList : UserControl
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
                  typeof(ChannelList)
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
                  typeof(ChannelList)
              );
        #endregion

        #region ShowEmptyFlag DP
        public Visibility ShowEmptyFlag
        {
            get { return (Visibility)GetValue(ShowEmptyFlagProperty); }
            set { SetValue(ShowEmptyFlagProperty, value); }
        }

        public static readonly DependencyProperty ShowEmptyFlagProperty
            = DependencyProperty.Register(
                  "ShowEmptyFlag",
                  typeof(Visibility),
                  typeof(ChannelList)
              );
        #endregion

        public event MouseButtonEventHandler OnMenuClick;

        public ChannelList()
        {
            InitializeComponent();

            this.DataContext = this;

        }



        #region UI Functions

        private void TittlePanel_MouseEnter(object sender, MouseEventArgs e)
        {
            TittlePanel.Background.Opacity = 0.10;
            TittlePanelBorder.BorderBrush.Opacity = 1;
        }

        private void TittlePanel_MouseLeave(object sender, MouseEventArgs e)
        {
            TittlePanel.Background.Opacity = 0;
            TittlePanelBorder.BorderBrush.Opacity = 0;
        }

        private void TittlePanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            double oldHight = ListPanel.ActualHeight == 0 ? 60 : ListPanel.ActualHeight + 30;

            if (ListElement.Height == 30)
            {
                ArrowIcon.Content = "";
                LauncherFactory.ElementAnimation(ListElement, UserControl.HeightProperty,30, oldHight, 0.1, false );
                return;
            }

            ArrowIcon.Content = "";
            LauncherFactory.ElementAnimation(ListElement, UserControl.HeightProperty, oldHight, 30, 0.1, false);
            
        }

        private void ShowEmptyFlagStatus(bool value)
        {
            if(!value)
                EmptyFlag.Visibility = Visibility.Hidden;
        }

        #endregion

        public void AddItem(UIElement elm)
        {
            ChannelListItem temp = (ChannelListItem)elm;
            temp.Margin = new Thickness(0, 0, 0, 1);
            temp.MouseDown += ListItemClicked;
            temp.MenuClick += ListItemMenuClicked;
            ListPanel.Children.Add(temp);

            double newHeight = (ListPanel.Children.Count * 45) + 30;
            LauncherFactory.ElementAnimation(ListElement, UserControl.HeightProperty, ListElement.ActualHeight, newHeight, 0.1, false);

            EmptyFlag.Visibility = Visibility.Hidden;

            Counters = string.Format("- {0}/{1}", ListPanel.Children.Count, ListPanel.Children.Count);
            
        }

        private void ListItemMenuClicked(object sender, MouseButtonEventArgs e)
        {
            if (this.OnMenuClick != null)
            {
                this.OnMenuClick(sender, e);
            }


        }


        private void SetCounterValues(string counters)
        {
            if (counters.Contains(","))
            {
                string[] aryStr = counters.Split(',');
                ChannelCounter.Content = string.Format("- {0}/{1}", aryStr[0], aryStr[1]);
                return;
            }

            ChannelCounter.Content = counters;
        }

        public void FilterChannels(string keyWord)
        {
            if (keyWord == "")
            {
                foreach (ChannelListItem flItem in ListPanel.Children)
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

            foreach (ChannelListItem flItem in ListPanel.Children)
            {
                if (flItem.ChannelName.ToLower().Contains(keyWord.ToLower()))
                {

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

        private void ListItemClicked(object sender, MouseButtonEventArgs e)
        {
            ChannelListItem objSender = (ChannelListItem)sender;

            foreach (ChannelListItem flItem in ListPanel.Children)
            {
                if (flItem == objSender)
                {
                    flItem.IsSelected = !flItem.IsSelected;
                }
                else
                {
                    flItem.IsSelected = false;
                }

            }

        }

        public void RemoveItem(UIElement elm)
        {
            if (ListPanel.Children.Count == 1 && ShowEmptyFlag == Visibility.Visible)
            {
                EmptyFlag.Visibility = Visibility.Visible;
            }
            ListPanel.Children.Remove(elm);

            double newHeight = (ListPanel.Children.Count * 45) - 30;
            LauncherFactory.ElementAnimation(ListElement, UserControl.HeightProperty, ListElement.ActualHeight, newHeight, 0.1, false);
            
        }

        public ChannelListItem RemoveByChannelID(long id)
        {
            foreach (ChannelListItem item in ListPanel.Children)
            {
                if (item.ChatID == id)
                {
                    if (App.ChatMan._openedChannels.ContainsKey(id)) App.ChatMan._openedChannels[id].Close();

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
            this.Counters = "0,0";
        }


    }
}
