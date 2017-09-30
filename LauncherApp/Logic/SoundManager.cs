using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace LauncherApp.Logic
{
    public class SoundManager
    {
        SoundPlayer sPlayer = new SoundPlayer();
        public bool isBusy = false;


        public SoundManager()
        {
            
        }

        public void PlaySound(SoundsType sType)
        {

            new System.Threading.Thread(() => { this.RunSound((int)sType); }).Start();

        }

        private void RunSound(int sID)
        {
            if (this.isBusy)
            {
                return;
            }

            this.isBusy = true;

            this.sPlayer.Stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("LauncherApp.resources.sounds.{0}.wav", sID));
            this.sPlayer.PlaySync();

            this.isBusy = false;
        }

    }


    public enum SoundsType
    {
        ChatMessage = 0,
        NewNotify = 1,

    };

}
