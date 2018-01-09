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
    /// Interaction logic for ChatMessageItem.xaml
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
            set { SetValue(OwnerNameProperty, value);}
        }


        public static readonly DependencyProperty OwnerNameProperty
            = DependencyProperty.Register(
                  "OwnerName",
                  typeof(string),
                  typeof(ChatMessageItem)
              );
        #endregion

        #region SendDate DP
        public DateTime SendDate
        {
            get { return (DateTime)GetValue(SendDateProperty); }
            set { SetValue(SendDateProperty, value); }
        }

        public static readonly DependencyProperty SendDateProperty
            = DependencyProperty.Register(
                  "SendDate",
                  typeof(DateTime),
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
            paragraph.Margin = new Thickness(0);
            paragraph.Padding = new Thickness(0);

            Run name = new Run();
            name.Text = message;
            App.ChatMan.Emoticons(name.Text, paragraph, messageText, true);

        }

        private void MessagePanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //messageText.Width = MessagePanel.ActualWidth;
        }



    }


}
