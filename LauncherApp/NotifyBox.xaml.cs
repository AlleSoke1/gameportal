using LauncherApp.Styles.Controls;
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
using System.Windows.Shapes;

namespace LauncherApp
{
    /// <summary>
    /// Interaction logic for NotifyBox.xaml
    /// </summary>
    public partial class NotifyBox : Window
    {
        public NotifyBox()
        {
            InitializeComponent();

            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.Width;
            this.Top = desktopWorkingArea.Bottom - this.Height;


        }

        public void AddItem(string boxTitle, string boxMessage, int ShowTime = 0)
        {
            LauncherFactory.getNotifyClass().Show();

            for (int index = ListPanel.Children.Count - 1; index >= 0; index--)
            {
                if (ListPanel.Children[index] is NotifyBoxItem && ((NotifyBoxItem)ListPanel.Children[index]).Height == 0)
                {
                    ListPanel.Children.RemoveAt(index);
                }
            }

            var tempItem = new NotifyBoxItem()
            {
                Title = boxTitle,
                ShowTime = ShowTime,
                Message = boxMessage,
                OwnerBox = ListPanel
            };
            tempItem.SizeChanged += ItemSizeChanged;

            if (ListPanel.Children.Count == 10) { ListPanel.Children.Clear(); }

            ListPanel.Children.Add(tempItem);

            App.SoundMan.PlaySound(LauncherApp.Logic.SoundsType.NewNotify);
        }

        private void ItemSizeChanged(object sender, SizeChangedEventArgs e)
        {
            NotifyBoxItem senderObj = (NotifyBoxItem)sender;
            if (senderObj.ActualHeight == 0)
            {
                ListPanel.Children.Remove(senderObj);
                if (ListPanel.Children.Count == 0) { 
                    this.Hide(); 
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddItem("testing title!", "testing message texts is here to see the box", 5);
        }
    }
}
