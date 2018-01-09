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
            factoryObjects.Add(new MainApp());
            //factoryObjects.Add(new FriendsWindow());
            factoryObjects.Add(new LoginsWindow());
            factoryObjects.Add(new NicknameWindow());
            factoryObjects.Add(new NotifyBox());
            
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

            foreach (ResourceDictionary tRD in App.Current.Resources.MergedDictionaries.ToList())
            {
                if (tRD.Source.ToString().Contains("/uistring/"))
                    App.Current.Resources.MergedDictionaries.Remove(tRD);
            }
            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri("pack://application:,,,/LauncherResources;component/resources/uistring/" + fileName + ".xaml");
            App.Current.Resources.MergedDictionaries.Add(dict);

           
        }

        public static void ElementAnimation(UIElement elm, DependencyProperty elmProp, dynamic anFrom, dynamic anTo, double anSpeed, bool ThicknessFlag, Action finishedAction = null)
        {

            ThicknessAnimation tempThicknessAnimation = new ThicknessAnimation();
            DoubleAnimation temDoubleAnimation = new DoubleAnimation();
            Storyboard sb = new Storyboard();

            if (finishedAction != null)
            {
                sb.Completed += (s, e) =>
                {
                    finishedAction();
                };
            }
           

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
        public static MainApp getAppClass() { return getObjByName<MainApp>(); }
        //public static AppWindow getAppClass() { return getObjByName<AppWindow>(); }
       // public static FriendsWindow getFriendsClass() { return getObjByName<FriendsWindow>(); }
        public static StartupWindow getStartClass() { return getObjByName<StartupWindow>(); }
        public static NicknameWindow getNicknameClass() { return getObjByName<NicknameWindow>(); }
      
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
