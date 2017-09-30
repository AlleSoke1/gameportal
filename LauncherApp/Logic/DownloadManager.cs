using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LauncherApp
{
    public class DownloadManager
    {


        private WebClient client;

        public List<string> getGameFileList(string fileUrl)
        {
            List<string> tempList = new List<string>();
            client = new WebClient();

            using (var stream = client.OpenRead(fileUrl))
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    tempList.Add(line);
                }
            }


            return tempList;
        }

        public string DownloadFirstString(string fileUrl)
        {
            client = new WebClient();

            using (var stream = client.OpenRead(fileUrl))
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    return line;
                }
            }

            return "";
        }

        public void DownloadFile(string DownloadUrl,string InstallPath, int filesCount, Action<long, long> progressCallback)
        {
            client = new WebClient();

            DownloadProgressChangedEventHandler progressReaction = (s, e) =>
            {
                progressCallback(e.BytesReceived, e.TotalBytesToReceive);
            };

            client.DownloadProgressChanged += progressReaction;

            var url = DownloadUrl;
            string FileName = "\\" + Path.GetFileName(url);

            client.DownloadFileAsync(new Uri(url), InstallPath + FileName);

            return;

        }



    }
}
