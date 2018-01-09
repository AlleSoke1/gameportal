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
    /// Interaction logic for CallUserIcon.xaml
    /// </summary>
    public partial class CallUserIcon : UserControl
    {


        #region MemeberName DP

        public string MemeberName
        {
            get { return (string)GetValue(MemeberNameProperty); }
            set { SetValue(MemeberNameProperty, value); SetMemeberLetter(value); }
        }
        public static readonly DependencyProperty MemeberNameProperty
            = DependencyProperty.Register(
                  "MemeberName",
                  typeof(string),
                  typeof(CallUserIcon)
              );

        #endregion

        #region Icon DP

        public FontAwesome.WPF.FontAwesomeIcon Icon
        {
            get { return (FontAwesome.WPF.FontAwesomeIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); SetStatusIcon(value); }
        }

        public static readonly DependencyProperty IconProperty
            = DependencyProperty.Register(
                  "Icon",
                  typeof(FontAwesome.WPF.FontAwesomeIcon),
                  typeof(CallUserIcon)
              );

        #endregion

        System.Windows.Media.Animation.Storyboard iconStoryBoard;

        public CallUserIcon()
        {
            InitializeComponent();

            iconStoryBoard = (System.Windows.Media.Animation.Storyboard)TryFindResource("StatusIconStoryboard");
            iconStoryBoard.Begin();
        }

        private void SetMemeberLetter(string name)
        {
            NameLetterLabel.Content = name[0].ToString().ToUpper();
            this.ToolTip = name;

            if (name.ToLower() == LauncherApp.Game_Data.Globals.nickname.ToLower())
            {
                IconBorder.Background = (Brush)new BrushConverter().ConvertFromString("#FFDA0029");
            }

        }


        private void SetStatusIcon(FontAwesome.WPF.FontAwesomeIcon icon)
        {
            if (icon == FontAwesome.WPF.FontAwesomeIcon.Phone)
            {
                StatusIcon.Icon = icon;
                iconStoryBoard.Begin();
                return;
            }

            iconStoryBoard.Stop();
            StatusIcon.Icon = icon;
        }

        private void ListElement_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MemeberName.ToLower() != LauncherApp.Game_Data.Globals.nickname.ToLower())
            {
                ContextMenu temp = (ContextMenu)this.Resources["UserIconMenu"];
                temp.PlacementTarget = (UserControl)sender;
                temp.IsOpen = true;
            }
          
        }





    }
}
