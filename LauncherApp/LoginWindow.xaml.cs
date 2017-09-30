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
using FontAwesome.WPF;
using System.Threading;
using LauncherApp.Network;
using LauncherData;
using System.Diagnostics;


namespace LauncherApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginsWindow : Window
    {




        private bool windowMaximizeState = false;

        public LoginsWindow()
        {

            //App.Current.MainWindow = LauncherFactory.getLoginClass();

            InitializeComponent();

            LoadStyles();

           
        }

        
        #region UI Functions

        private void LoadStyles()
        {

            LoginLoading.Visibility = Visibility.Hidden;

            loginID.GotFocus += textBox_OnFocus;
            loginID.LostFocus += textBox_FocusOut;
            loginPass.GotFocus += textBox_OnFocus;
            loginPass.LostFocus += textBox_FocusOut;
        }



        private void RememberCheck_Click(object sender, RoutedEventArgs e)
        {
            ImageBrush backimg = new ImageBrush();
            backimg.Stretch = Stretch.Fill;
            backimg.TileMode = TileMode.None; 

            if (RememberCheck.IsChecked == true)
                backimg.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/resources/images/checkbox_checked.png"));
            else
                backimg.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/resources/images/checkbox.png"));

            RememberCheck.Background = backimg;

           
        }

        private void textBox_OnFocus(object sender, RoutedEventArgs e)
        {
            string SenderName;

            if (sender is PasswordBox) {
                PasswordBox senderBox = ((PasswordBox)sender);
                SenderName = senderBox.Name.ToString();
            }else if (sender is TextBox)
            {
                TextBox senderBox = ((TextBox)sender);
                SenderName = senderBox.Name.ToString();
            }
            else { SenderName = ""; }

            object textboxlibl = MainGrid.FindName(SenderName + "_libl");

            ((Label)textboxlibl).Visibility = Visibility.Hidden;

        }
        private void textBox_FocusOut(object sender, RoutedEventArgs e)
        {
             string SenderName;
             string SenderText;

            if (sender is PasswordBox)
            {
                PasswordBox senderBox = ((PasswordBox)sender);
                SenderName = senderBox.Name;
                 SenderText = senderBox.Password;
            }else if (sender is TextBox)
            {
                TextBox senderBox = ((TextBox)sender);
                SenderName = senderBox.Name;
                SenderText = senderBox.Text;
            }
            else { SenderName = ""; SenderText = ""; }

            object textboxlibl = MainGrid.FindName(SenderName + "_libl");

            if (SenderText == "")
                ((Label)textboxlibl).Visibility = Visibility.Visible;
        }

        private void LoadingStatus(bool p)
        {
            if (p)
            {
                LoginLoading.Visibility = Visibility.Visible;
                LoginGrid.Visibility = Visibility.Hidden;
            }
            else
            {
                LoginLoading.Visibility = Visibility.Hidden;
                LoginGrid.Visibility = Visibility.Visible;
            }

        }

        private void LoginWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void showError(string text, LauncherApp.Styles.Controls.FastMessage.MessageTypes type)
        {
            ErrorMessage.Message = text;
            ErrorMessage.ShowTime = 3;
            ErrorMessage.Type = type;
            ErrorMessage.Show();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void tFullBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!windowMaximizeState)
            {
                tFullBtn.Icon = FontAwesomeIcon.WindowRestore;
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                tFullBtn.Icon = FontAwesomeIcon.WindowMaximize;
                this.WindowState = WindowState.Normal;
            }

            windowMaximizeState = !windowMaximizeState;
        }

        private void MinieBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }


        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://accounts.arcangames.com/register");

        }

        private void Forgot_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("https://accounts.arcangames.com/forgot-info");
        }

        #endregion

        async Task Delay()
        {
            await Task.Delay(1000);
        }

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {

            loginButton.IsEnabled = false;
            loginID.IsEnabled = false;
            loginPass.IsEnabled = false;
            LoadingStatus(true);
            bool ErrorStatus = false;

            if (loginID.Text == "" || loginPass.Password == "") {
                showError((string)App.Current.Resources["Error_EmptyInputs"], LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                ErrorStatus = true;
            }
            else if (loginID.Text.Length < 4 || loginID.Text.Length > 50 || loginPass.Password.Length < 4 || loginPass.Password.Length > 50)
            {
                showError((string)App.Current.Resources["Error_InputCount"], LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                ErrorStatus = true;
            }

            //
            if (!App.connection.IsConnected())
            {
                App.connection.Reconnect();
            }

            await Delay();

            //
            if (ErrorStatus == false)
            {
                SendLoginPacket(loginID.Text, loginPass.Password);

                if (RememberCheck.IsChecked == true) {
                    App.launcherSettings.data.RememberMe = true;
                    App.launcherSettings.data.RememberData = SaveUserPassword(loginID.Text, loginPass.Password);
                }
                else
                {
                    App.launcherSettings.data.RememberMe = false;
                }

                App.launcherSettings.Save();

            }
            else
            {
                loginButton.IsEnabled = true;
                loginID.IsEnabled = true;
                loginPass.IsEnabled = true;
                LoadingStatus(false);
            }



        }

        #region account save load from settings

        byte[] keys = {0x21, 0x12, 0x69, 0x22, 0xda, 0xfa};

       public string SaveUserPassword(string username,string password)
       {
           char[] temp =  String.Format(username + "&" + password).ToArray();

           for(int i=0;i<temp.Length;i++)
           {
               temp[i] ^= (char)keys[i % keys.Length];
           }

           return Base64Encode(new string(temp));
       }


       public void LoadUserPassword(string encrpytedData)
       {
            char[] temp =  String.Format(Base64Decode(encrpytedData)).ToArray();

            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] ^= (char)keys[i % keys.Length];
            }

            string temp2 = new string(temp);
            string[] userpass = temp2.Split(new char[]{'&'}, 2);

            loginID.Text         = userpass[0];
            loginPass.Password   = userpass[1];

           //if application was start first time then autologin!
            if (!Game_Data.Globals.Initialized)
                loginButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                //SendLoginPacket(userpass[0], userpass[1]);
       }

       public static string Base64Decode(string base64EncodedData)
       {
           var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
           return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
       }

       public static string Base64Encode(string plainText)
       {
           var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
           return System.Convert.ToBase64String(plainTextBytes);
       }

        #endregion



       private void LoginWindow_Loaded(object sender, RoutedEventArgs e)
       {
           if (App.launcherSettings.data.RememberMe)
           {
               RememberCheck.IsChecked = true;
               LoadUserPassword(App.launcherSettings.data.RememberData);

               if (this.loginID.Text != "") this.loginID_libl.Visibility = Visibility.Hidden;
               if (this.loginPass.Password != "") this.loginPass_libl.Visibility = Visibility.Hidden;
           }
       }

       public void OnLoginResult(Enums.Login.LoginResult result)
        {

            switch(result)
            {
                case Enums.Login.LoginResult.Successfull:

                    Thread.Sleep(1000);

                    Dispatcher.Invoke(() =>
                    {
                        //  Window appWnd = LauncherFactory.getAppWnd();
                        //App.Current.MainWindow = appWnd;
                        Game_Data.Globals.password = loginPass.Password;

                        //LauncherFactory.getAppClass().Show();
                        //LauncherFactory.getLoginClass().Hide();
                    });

                    Game_Data.Globals.Initialized = true;

                    break;
                    
                case Enums.Login.LoginResult.AccountDoesNotExists:

                    Dispatcher.Invoke(() =>
                    {
                        showError((string)App.Current.Resources["Error_AccountDoesNotExists"], LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                    });
                    break;

                    
                case Enums.Login.LoginResult.WrongPassword:

                    Dispatcher.Invoke(() =>
                    {
                        showError((string)App.Current.Resources["Error_WongPassword"], LauncherApp.Styles.Controls.FastMessage.MessageTypes.Warning);
                    });

                    break;

                    
                case Enums.Login.LoginResult.InvalidInputs: 

                    break;
            }

            if (result != Enums.Login.LoginResult.Successfull)
            {
                Dispatcher.Invoke(() =>
                {
                    LoadingStatus(false);
                    loginButton.IsEnabled = true;
                    loginID.IsEnabled = true;
                    loginPass.IsEnabled = true;
                });
            }
           
            
        }


        void SendLoginPacket(string user, string pass)
        {
            for(int i=0;i<1;i++)
            { 
                Packets.Login.CSLoginPacket packet = new Packets.Login.CSLoginPacket();

                packet.username = user;
                packet.password = pass;

                App.connection.Send(packet, "CS_Login");
            }
        }

        public void ShowLogout()
        {
            this.Show();
            LoadingStatus(false);
            loginButton.IsEnabled = true;
            loginID.IsEnabled = true;
            loginPass.IsEnabled = true;
        }
    }
}
