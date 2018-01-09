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
    /// Interaction logic for ChatMemeberList.xaml
    /// </summary>
    public partial class ChatMemeberList : UserControl
    {

        #region isOpen DP
        public bool isOpen
        {
            get { return (bool)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); SwithOpenStatus(value); }
        }


        public static readonly DependencyProperty StatusProperty
            = DependencyProperty.Register(
                  "isOpen",
                  typeof(bool),
                  typeof(ChatMemeberList)
              );
        #endregion

        public ChatMemeberList()
        {
            InitializeComponent();

        }

        public void Add(string uName, long uID = 0, object sender = null)
        {
            MemeberListPanel.Children.Add(new ChatMemeberListItem()
            {
                UserName = uName,
                UserID = uID,
                UserStatus = Enums.UserInfo.UserStatus.Available
            });

            if (sender != null)
            {
                App.ChatMan._openedChatList[((ChatControl)sender).chatID].UpdateMemebersCount();
            }
        }

        public void Remove(long uID, object sender = null)
        {

            foreach (ChatMemeberListItem elm in this.MemeberListPanel.Children)
            {
                if (elm.UserID == uID)
                {
                    MemeberListPanel.Children.Remove((UIElement)elm);
                }
            }

            if (sender != null)
            {
                App.ChatMan._openedChatList[((ChatControl)sender).chatID].UpdateMemebersCount();
            }
        }


        public void Remove(string uName, object sender = null)
        {

            foreach (ChatMemeberListItem elm in MemeberListPanel.Children)
            {
                if (elm.UserName.ToLower() == uName.ToLower())
                {
                    MemeberListPanel.Children.Remove(elm);
                }
            }

            if (sender != null)
            {
                App.ChatMan._openedChatList[((ChatControl)sender).chatID].UpdateMemebersCount();
            }
        }

        public int GetCount()
        {
            return MemeberListPanel.Children.Count;
        }

        public void Clear()
        {
            MemeberListPanel.Children.Clear();
        }

        public void ChangeUserStatus(long uID,  Enums.UserInfo.UserStatus status)
        {

            foreach (ChatMemeberListItem elm in MemeberListPanel.Children)
            {
                if (elm.UserID == uID)
                {
                    elm.UserStatus = status;
                }
            }

        }

        private void SwithOpenStatus(bool value)
        {
            if (value)
            {

                LauncherFactory.ElementAnimation(this, ChatMemeberList.WidthProperty, 0, 240, 0.2, false);

                return;
            }

            LauncherFactory.ElementAnimation(this, ChatMemeberList.WidthProperty, 240, 0, 0.2, false);
        }



    }
}
