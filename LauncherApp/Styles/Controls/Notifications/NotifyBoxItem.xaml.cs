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
using System.Windows.Threading;

namespace LauncherApp.Styles.Controls
{
    /// <summary>
    /// Interaction logic for FastMessage.xaml
    /// </summary>
    
    public partial class NotifyBoxItem : UserControl
    {

        #region Message DP
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty
            = DependencyProperty.Register(
                  "Message",
                  typeof(string),
                  typeof(NotifyBox)
              );
        #endregion

        #region Title DP
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty
            = DependencyProperty.Register(
                  "Title",
                  typeof(string),
                  typeof(NotifyBox)
              );
        #endregion

        #region OwnerBox DP
        public object OwnerBox
        {
            get { return (object)GetValue(OwnerBoxProperty); }
            set { SetValue(OwnerBoxProperty, value); }
        }

        public static readonly DependencyProperty OwnerBoxProperty
            = DependencyProperty.Register(
                  "OwnerBox",
                  typeof(object),
                  typeof(NotifyBox)
              );
        #endregion

        #region ShowTime DP
        public int ShowTime
        {
            get { return (int)GetValue(ShowTimeProperty); }
            set { SetValue(ShowTimeProperty, value); ChangeShowTime(value); }
        }

        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public static readonly DependencyProperty ShowTimeProperty
            = DependencyProperty.Register(
                  "ShowTime",
                  typeof(int),
                  typeof(NotifyBox)
              );
        #endregion

        public NotifyBoxItem()
        {
            InitializeComponent();
            this.DataContext = this;
            Show();
        }

        private void ChangeShowTime(int value)
        {
            if (value > 0)
            {
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, value);
                dispatcherTimer.Start(); 
            }
            
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Hide();
            dispatcherTimer.Stop();
        }


        public void Show()
        {
            LauncherFactory.ElementAnimation(this, UserControl.HeightProperty, 1, 150, 0.3, false);
        }

        public void Hide()
        {
            LauncherFactory.ElementAnimation(this, UserControl.HeightProperty, 150, 0, 0.3, false);
        }

    }

}
