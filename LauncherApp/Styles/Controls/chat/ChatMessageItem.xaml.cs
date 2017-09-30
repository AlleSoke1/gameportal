using FontAwesome.WPF;
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
    /// Interaction logic for FriendListItem.xaml
    /// </summary>
    public partial class ChatMessageItem : UserControl
    {


        #region Message DP
        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); SetMessageText(value); }
        }
        public static readonly DependencyProperty MessageProperty
            = DependencyProperty.Register(
                  "Message",
                  typeof(string),
                  typeof(ChatMessageItem)
              );
        #endregion

        #region OwnerName DP
        public string OwnerName
        {
            get { return (string)GetValue(OwnerNameProperty); }
            set { SetValue(OwnerNameProperty, value); SetOwnerName(value); }
        }


        public static readonly DependencyProperty OwnerNameProperty
            = DependencyProperty.Register(
                  "OwnerName",
                  typeof(string),
                  typeof(ChatMessageItem)
              );
        #endregion

        #region SendDate DP
        public string SendDate
        {
            get { return (string)GetValue(SendDateProperty); }
            set { SetValue(SendDateProperty, value); }
        }

        public static readonly DependencyProperty SendDateProperty
            = DependencyProperty.Register(
                  "SendDate",
                  typeof(string),
                  typeof(ChatMessageItem)
              );
        #endregion

        public ChatMessageItem()
        {
            InitializeComponent();

            this.DataContext = this;
            
        }

        private void SetMessageText(string message)
        {

            Paragraph paragraph = new Paragraph();
            paragraph.ToolTip = this.SendDate;
            paragraph.Margin = new Thickness(0);
            paragraph.Padding = new Thickness(0);

            Run name = new Run();
            name.Text = message;
            App.ChatMan.Emoticons(name.Text, paragraph, messageText, true);

        }

        private void MessagePanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            messageText.Width = MessagePanel.ActualWidth - ownerName.ActualWidth;
        }

        private void SetOwnerName(string value)
        {
            Run temp = new Run();
            temp.Text = string.Format("{0}:", value);

            if (value.ToLower() == LauncherApp.Game_Data.Globals.nickname.ToLower()) {
                ItemElement.Background.Opacity = 1;
                temp.Foreground = (Brush)new BrushConverter().ConvertFromString("#FFDA0029");
            }

            ownerName.Inlines.Add(temp);
        }

        public void AddMoreMessage(string message, string sendDate){

            Paragraph paragraph = new Paragraph();
            paragraph.ToolTip = sendDate;
            paragraph.Margin = new Thickness(0);
            paragraph.Padding = new Thickness(0);

            Run name = new Run();
            name.Text = message;
            App.ChatMan.Emoticons(name.Text, paragraph, messageText, false);
            messageText.UpdateLayout();
        }


    }


}
