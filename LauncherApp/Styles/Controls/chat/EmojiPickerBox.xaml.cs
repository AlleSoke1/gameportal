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
    /// Interaction logic for EmojiPickerBox.xaml
    /// </summary>
    public partial class EmojiPickerBox : UserControl
    {

        public EmojiPickerBox()
        {
            InitializeComponent();
            GridListPanel.Children.Clear();
            LoadEmojiList();

        }


        #region UI Functions

        private void LoadEmojiList()
        {


            foreach (var tempVar in App.ChatMan._mappings)
            {

                Image tempImage = new Image()
                {
                    Width = 37,
                    Height = 25,
                    Tag = tempVar.Key.ToString(),
                    Margin = new Thickness(0,3,0,3),
                    Cursor = Cursors.Hand,
                    Source = new BitmapImage(new Uri(string.Format("pack://application:,,,/LauncherResources;component/resources/images/emoji/{0}", tempVar.Value)))
                };

                tempImage.MouseDown += EmojiIcon_MouseDown;
                GridListPanel.Children.Add(tempImage);

            }
        }

        #endregion

        private void EmojiIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Image senderObj = (Image)sender;
            string emojiCode = " " + senderObj.Tag.ToString() + " ";

            for (int x = 0; x < emojiCode.Length; x++)
            {
                if (LauncherFactory.getAppClass().SocialPage._chatControl.TypeBox.Text.Length < LauncherFactory.getAppClass().SocialPage._chatControl.TypeBox.MaxLength)
                {
                    LauncherFactory.getAppClass().SocialPage._chatControl.TypeBox.AppendText(emojiCode.ToArray()[x].ToString());
                    this.Visibility = Visibility.Hidden;
                }
            }

        }


    }
}
