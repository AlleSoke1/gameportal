using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LauncherApp
{

    public class FileDownload
    {
        private volatile bool _allowedToRun;
        private string _source;
        private Dictionary<string, DownloadFileInfo> _sources; // string is file url, long is downloaded BytesWritten
        private string _destination;
        private string _multiDestination;
        private int _chunkSize;

        private Lazy<long> _contentLength;
        private Dictionary<string, Lazy<long>> _contentLengths;

        private bool isMultiDownload = false;
        private int MultiDownloadFileCount = 0;
        private int MultiDownloadFileCompleteCount = 0;

        public long TotalMultiDownloadSize { get; private set; }
        public long TotalMultiDownloadBytesWritten { get; private set; }

        public long BytesWritten { get; private set; }
        public long ContentLength { get { return _contentLength.Value; } }

        public bool Done { get { return ContentLength == BytesWritten; } }

        public event RoutedEventHandler OnFileDownloadComplete;
        public event FileDownloadProgressHandler OnFileDownloadProgress;
        public delegate void FileDownloadProgressHandler(object sender, FileDownloadProgressArg e);

        public FileDownload(string source, string destination, int chunkSize)
        {
            _allowedToRun = true;

            _source = source;
            _destination = destination;
            _chunkSize = chunkSize;
            _contentLength = new Lazy<long>(() => Convert.ToInt32(GetContentLength(source)));

            BytesWritten = GetFileLength(_destination);
        }

        public void FileDownloadList(List<string> sources, string folder, int chunkSize)
        {
            this._sources = new Dictionary<string, DownloadFileInfo>();

            _allowedToRun = true;
            isMultiDownload = true;
            TotalMultiDownloadSize = 0;
            foreach (string str in sources)
            {
                if (str != null)
                    _sources.Add(str, new DownloadFileInfo() { TotalBytes = 0, DownloadedBytes = 0, FileUrl = str, PassProgress = false });
            }
            _multiDestination = folder;
            _chunkSize = chunkSize;
            MultiDownloadFileCount = _sources.Count;
            foreach (var url in _sources)
            {
                long fileBytes = Convert.ToInt32(GetContentLength(url.Key));
                TotalMultiDownloadSize += fileBytes;
                url.Value.TotalBytes = fileBytes;
                url.Value.FilePath = Path.Combine(folder, Path.GetFileName(new Uri(url.Key).LocalPath));
                url.Value.DownloadedBytes = GetFileLength(url.Value.FilePath);
            }

        }

        private long GetFileLength(string path)
        {
            if (File.Exists(path))
            {
                // file is already downloaded and need to resume
                var fileSize = new FileInfo(path).Length;
                return fileSize;
            }

            // file is new on pc and need to download it from 0
            return 0;
        }

        private long GetContentLength(string url)
        {
            if (url == null) return 0;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";

            using (var response = request.GetResponse())
                return response.ContentLength;
        }

        private async Task Start(long range)
        {
            if (!_allowedToRun)
                throw new InvalidOperationException();

            var request = (HttpWebRequest)WebRequest.Create(_source);
            request.Method = "GET";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
            request.AddRange(range);

            using (var response = await request.GetResponseAsync())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var fs = new FileStream(_destination, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                    {
                        while (_allowedToRun)
                        {
                            var buffer = new byte[_chunkSize];
                            var bytesRead = await responseStream.ReadAsync(buffer, 0, buffer.Length);

                            if (bytesRead == 0) break;

                            await fs.WriteAsync(buffer, 0, bytesRead);
                            BytesWritten += bytesRead;
                            this.TotalMultiDownloadBytesWritten += bytesRead;

                            if (isMultiDownload) this._sources[this._source].DownloadedBytes += bytesRead;

                            if (ContentLength == BytesWritten)
                            {
                                if (isMultiDownload)
                                {
                                    MultiDownloadFileCompleteCount++;
                                    _sources[this._source].PassProgress = true;
                                    this.StartList();
                                }
                                else
                                {
                                    this.OnFileDownloadComplete(this, null);
                                }
                            }
                            else
                            {
                                if (isMultiDownload)
                                {
                                    this.OnFileDownloadProgress(this, new FileDownloadProgressArg() { DownloadedBytes = this.TotalMultiDownloadBytesWritten, TotalBytes = this.TotalMultiDownloadSize });

                                }
                                else
                                {
                                    this.OnFileDownloadProgress(this, new FileDownloadProgressArg() { DownloadedBytes = this.BytesWritten, TotalBytes = this.ContentLength });

                                }
                            }
                        }
                        await fs.FlushAsync();
                    }
                }
            }
        }

        public Task Start()
        {
            _allowedToRun = true;
            return Start(BytesWritten);
        }

        public Task StartList()
        {

            foreach (var file in _sources)
            {
                if (file.Value.DownloadedBytes == file.Value.TotalBytes)
                {
                    file.Value.Complete = true;
                    if (!file.Value.PassProgress)
                    {
                        MultiDownloadFileCompleteCount++;
                        TotalMultiDownloadBytesWritten += file.Value.TotalBytes;
                    }
                    continue;
                }

                if (!file.Value.Complete)
                {
                    this._source = file.Key;
                    this._contentLength = new Lazy<long>(() => file.Value.TotalBytes);
                    this._destination = file.Value.FilePath;
                    this.BytesWritten = file.Value.DownloadedBytes;
                    TotalMultiDownloadBytesWritten += file.Value.DownloadedBytes;
                    break;
                }

            }

            //download is finish
            if (MultiDownloadFileCompleteCount >= MultiDownloadFileCount)
            {
                this.OnFileDownloadComplete(this, null);
                return null;
            }
            else
            {
                _allowedToRun = true;
                return Start(BytesWritten);
            }

        }
        public void Pause()
        {
            _allowedToRun = false;
        }


        public bool GetStatus()
        {
            return _allowedToRun;
        }


    }
    public class DownloadFileInfo
    {
        public string FileUrl { get; set; }
        public string FilePath { get; set; }
        public long TotalBytes { get; set; }
        public long DownloadedBytes { get; set; }
        public bool Complete { get; set; }

        public bool PassProgress { get; set; }
    }

    public class FileDownloadProgressArg
    {
        public long DownloadedBytes { get; set; }

        public long TotalBytes { get; set; }

        public int DownloadPercent { get { return Convert.ToInt32((this.DownloadedBytes * 100.0 / this.TotalBytes)); } }


    }

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
