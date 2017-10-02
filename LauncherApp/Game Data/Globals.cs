using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherApp.Game_Data
{

    public class Globals
    {
        public static Int64 uid { get; set; }
        public static string username { get; set; }
        public static string nickname { get; set; }
        public static string email { get; set; }
        public static string password { get; set; }

        public static bool Initialized = false;
        public static Enums.UserInfo.UserStatus status { get; set; }
        public static int OnlineFriendCount { get; set; }



        // voice chat system
        public static bool CallCenterIsBusy { get; set; }
        public static int CallCenterCannelID { get; set; }

    }

}
