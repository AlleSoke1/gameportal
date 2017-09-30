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


namespace LauncherApp
{



    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class NicknameWindow : Window
    {

        public NicknameWindow()
        {
            InitializeComponent();

            LoadStyles();
        }


        #region UI Functions

        private void LoadStyles()
        {
            NicknameBox.GotFocus += textBox_OnFocus;
            NicknameBox.LostFocus += textBox_FocusOut;
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


        private void NicknameWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        #endregion
        


        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
           SendCreateNickname(Game_Data.Globals.username, NicknameBox.Text);
        }

        void SendCreateNickname(string username, string nickname)
        {
            Packets.Login.CScreateNickName packet = new Packets.Login.CScreateNickName();

            packet.username = username;
            packet.nickname = nickname;

            App.connection.Send(packet, "CS_LoginCreateNickName");
        }

        public void OnNicknameResult(int result)
        {
            switch((Enums.Login.NicknameResult)result)
            {
                case Enums.Login.NicknameResult.Successfull:
                    {
                        Dispatcher.Invoke(() =>
                        {
                            this.Hide();
                            LauncherFactory.getAppClass().BlurWindow(false);
                        });
                    }
                    break;

                case Enums.Login.NicknameResult.NicknameAlreadyExists:
                    {
                        //Show error message nickname already exists
                    }
                    break;

                case Enums.Login.NicknameResult.Error:
                    {
                        //general error try again later
                    }
                    break;

                default:
                    {
                        //"Rafael's mom"
                    }
                    break;
            }
        }


        public void ShowNicknameWindow()
        {
            Dispatcher.Invoke(() =>
            {
                LauncherFactory.getAppClass().BlurWindow(true);

                Window Nickname = LauncherFactory.getNicknameClass();
                Nickname.Owner = LauncherFactory.getAppClass();
                Nickname.Show();

                Console.WriteLine("testing line");
            });
        }

        public void CloseNicknameWindow()
        {
            Dispatcher.Invoke(() =>
            {
                LauncherFactory.getNicknameClass().Hide();
            });
        }
        
    }
}
