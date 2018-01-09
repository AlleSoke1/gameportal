using LauncherApp.Styles.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace LauncherApp.Logic
{
    public class ChatManager
    {
        //public Dictionary<long, ChatWindow> _openedChats = new Dictionary<long, ChatWindow>();
        //public Dictionary<long, ChannelWindow> _openedChannels = new Dictionary<long, ChannelWindow>();

        public Dictionary<long, ChatControl> _openedChatList = new Dictionary<long, ChatControl>();

        public ChatManager(){

            _mappings.Add(@":*", "01.png");
            _mappings.Add(@":'(", "02.png");
            _mappings.Add(@"-_-", "03.png");
            _mappings.Add(@"'_-", "04.png");
            _mappings.Add(@"*_*", "05.png");
            _mappings.Add(@":O", "06.png");
            _mappings.Add(@":*)", "07.png");
            _mappings.Add(@":=(", "08.png");
            _mappings.Add(@":(", "09.png");
            _mappings.Add(@"☺", "10.png");
            _mappings.Add(@":Z", "11.png");
            _mappings.Add(@":)", "12.png");
            _mappings.Add(@">:)", "13.png");
            _mappings.Add(@":D", "14.png");
            _mappings.Add(@"^_^", "15.png");
            _mappings.Add(@"☻", "16.png");
            _mappings.Add(@"':D", "17.png");
            _mappings.Add(@"-:)", "18.png");
            _mappings.Add(@"|B", "19.png");
            _mappings.Add(@":??", "20.png");
            _mappings.Add(@":P", "21.png");
            _mappings.Add(@":->", "22.png");
            _mappings.Add(@">_<", "23.png");
            _mappings.Add(@">*<", "24.png");
            _mappings.Add(@";)", "25.png");
            _mappings.Add(":\"D", "26.png");

        }

        #region add emotion to RichTextBox function

        public Dictionary<string, string> _mappings = new Dictionary<string, string>();

        private string GetEmoticonText(string text)
        {
            string match = string.Empty;
            int lowestPosition = text.Length;

            foreach (KeyValuePair<string, string> pair in _mappings)
            {
                if (text.Contains(pair.Key))
                {
                    int newPosition = text.IndexOf(pair.Key);
                    if (newPosition < lowestPosition)
                    {
                        match = pair.Key;
                        lowestPosition = newPosition;
                    }
                }
            }

            return match;

        }
        // And also function which add smiles in richtextbox, here is it:

        public void Emoticons(string msg, Paragraph para, RichTextBox rtbConversation, bool clearFlag)
        {

            Run r = new Run(msg);
            para.Inlines.Add(r);

            string emoticonText = GetEmoticonText(r.Text);
            //if paragraph does not contains smile only add plain text to richtextbox rtb2
            if (string.IsNullOrEmpty(emoticonText))
            {
                if(clearFlag) rtbConversation.Document.Blocks.Clear();
                rtbConversation.Document.Blocks.Add(para);
            }
            else
            {
                while (!string.IsNullOrEmpty(emoticonText))
                {

                    TextPointer tp = r.ContentStart;

                    // keep moving the cursor until we find the emoticon text
                    while (!tp.GetTextInRun(LogicalDirection.Forward).StartsWith(emoticonText))

                        tp = tp.GetNextInsertionPosition(LogicalDirection.Forward);

                    // select all of the emoticon text
                    var tr = new TextRange(tp, tp.GetPositionAtOffset(emoticonText.Length)) { Text = string.Empty };

                    //relative path to image smile file
                    string path = _mappings[emoticonText];

                    Image image = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/LauncherResources;component/resources/images/emoji/" + path)),
                        Width = 20,
                        Height = 20,
                    };

                    //insert smile
                    new InlineUIContainer(image, tp);

                    if (para != null)
                    {
                        var endRun = para.Inlines.LastInline as Run;

                        if (endRun == null)
                        {
                            break;
                        }
                        else
                        {
                            emoticonText = GetEmoticonText(endRun.Text);
                        }

                    }

                }

                if (clearFlag)  rtbConversation.Document.Blocks.Clear();
                rtbConversation.Document.Blocks.Add(para);

                rtbConversation.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            }
        }
        #endregion



        internal bool OpenChannelFromList(long channelID)
        {
            if (_openedChatList.ContainsKey(channelID))
            {
                LauncherFactory.getAppClass().SocialPage.SwitchChatWindow(channelID);
                return true;
            }

            return false;
        }

    }


}
