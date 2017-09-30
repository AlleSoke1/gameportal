using LauncherData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace LauncherApp
{
    class LauncherFactory
    {
        public static List<Window> factoryObjects = new List<Window>();

        public LauncherFactory()
        {
            //Set app lang 
            SwitchWindowsLanguage(App.launcherSettings.data.AppLanguage);


            //  factoryObjects.Add(new App());
            factoryObjects.Add(new StartupWindow());
            factoryObjects.Add(new AppWindow());
            factoryObjects.Add(new FriendsWindow());
            factoryObjects.Add(new LoginsWindow());
            factoryObjects.Add(new NicknameWindow());
            factoryObjects.Add(new SettingsWindow());
            factoryObjects.Add(new FriendAddWindow());
            //factoryObjects.Add(new ChatWindow());
            //factoryObjects.Add(new ChannelWindow());
            factoryObjects.Add(new ScanGames());
            factoryObjects.Add(new NotifyBox());
            factoryObjects.Add(new ChannelCreate());
            factoryObjects.Add(new ChannelInvite());
            
            // Change Windows Algin by lang
            SwitchWindowsAlgin();
            

            //Show default window!
            getStartClass().Show();
        }

        //GET WINDOW
        public static dynamic getWndByName<T>()
        {
            var first = Application.Current.Windows.OfType<T>().First();
            if (first != null) return first;

            return null;
        }

        // Close/hide all windo
        public static void HideAllWindows()
        {
            foreach (var wnd in factoryObjects)
            {
                if(wnd != null)
                    wnd.Hide();
            }

        }


        // Change All Windows Aling
        public static void SwitchWindowsAlgin()
        {
            foreach (Window wnd in factoryObjects)
            {
                if (wnd != null)
                {
                    if (App.Current.Resources["LangAlign"].ToString() == "RTL")
                    {
                        wnd.FlowDirection = FlowDirection.RightToLeft;
                    }
                    else
                    {
                        wnd.FlowDirection = FlowDirection.LeftToRight;
                    }

                }

            }
        }

        public static void SwitchWindowsLanguage(string fileName)
        {
            

            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri("/resources/uistring/" + fileName + ".xaml", UriKind.Relative);
            App.Current.Resources.MergedDictionaries.Add(dict);

           
        }

        public static void ElementAnimation(UIElement elm, DependencyProperty elmProp, dynamic anFrom, dynamic anTo, double anSpeed, bool ThicknessFlag)
        {

            ThicknessAnimation tempThicknessAnimation = new ThicknessAnimation();
            DoubleAnimation temDoubleAnimation = new DoubleAnimation();
            Storyboard sb = new Storyboard();

            if(ThicknessFlag){
                tempThicknessAnimation.From = anFrom;
                tempThicknessAnimation.To = anTo;
                tempThicknessAnimation.Duration = TimeSpan.FromSeconds(anSpeed);
                Storyboard.SetTarget(tempThicknessAnimation, elm);
                Storyboard.SetTargetProperty(tempThicknessAnimation, new PropertyPath(elmProp));
                sb.Children.Add(tempThicknessAnimation);
            }else{
                temDoubleAnimation.From = anFrom;
                temDoubleAnimation.To = anTo;
                temDoubleAnimation.Duration = TimeSpan.FromSeconds(anSpeed);
                Storyboard.SetTarget(temDoubleAnimation, elm);
                Storyboard.SetTargetProperty(temDoubleAnimation, new PropertyPath(elmProp));
                sb.Children.Add(temDoubleAnimation);
            }

            sb.Begin();

        }

        //GET CLASS
        public static dynamic getObjByName<T>()
        {
            var first = factoryObjects.OfType<T>().First();
            if (first != null) return first;

            return null;
        }

        public static LoginsWindow getLoginClass() { return getObjByName<LoginsWindow>(); }
        public static AppWindow getAppClass() { return getObjByName<AppWindow>(); }
        public static FriendsWindow getFriendsClass() { return getObjByName<FriendsWindow>(); }
        public static StartupWindow getStartClass() { return getObjByName<StartupWindow>(); }
        public static NicknameWindow getNicknameClass() { return getObjByName<NicknameWindow>(); }
        public static SettingsWindow getSettingsClass() { return getObjByName<SettingsWindow>(); }
        public static FriendAddWindow getNewFriendClass() { return getObjByName<FriendAddWindow>(); }
        public static ChannelCreate getChannelCreateClass() { return getObjByName<ChannelCreate>(); }
        public static ChannelInvite getChannelInviteClass() { return getObjByName<ChannelInvite>(); }
        public static ScanGames getScanGamesClass() { return getObjByName<ScanGames>(); }
        public static NotifyBox getNotifyClass() { return getObjByName<NotifyBox>(); }
        
        
        internal static void SafeShow(Window gObjWnd, bool ShowDialog)
        {
            if (App.Current.Resources["LangAlign"].ToString() == "RTL")
            {
                gObjWnd.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                gObjWnd.FlowDirection = FlowDirection.LeftToRight;
            }

            if (ShowDialog) gObjWnd.ShowDialog(); else gObjWnd.Show();


        }


    }


}
