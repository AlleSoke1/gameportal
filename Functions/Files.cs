using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Functions
{
    public class fnFiles
    {

        public static void CheckFileCreate(string filePath)
        {
            if (File.Exists(filePath)) return;

            File.Create(filePath);

        }

        public static bool CheckFileExists(string filePath)
        {
            if (File.Exists(filePath)) return true;

            return false;

        }

        public static void CheckFolderCreate(string folderPath)
        {
            if (Directory.Exists(folderPath)) return;

            Directory.CreateDirectory(folderPath);

        }

        public static string SizeReadAble(long size)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            long len = size;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return String.Format("{0:0.##} {1}", len, sizes[order]);

        }


    }
}
