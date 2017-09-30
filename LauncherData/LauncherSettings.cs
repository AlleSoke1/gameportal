using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace LauncherData
{
    [Serializable]
    [XmlRoot(ElementName = "Settings", IsNullable = false)]
    public class LauncherData
    {
        //General Settings
        public bool RememberMe { get; set; }            // if remember me is true running RememberData as auto login.
        public string RememberData { get; set; }        // remember user data(id&password) for auto login
        public string AppLanguage { get; set; }           // selected launcher language 
        public bool LaunchOnStart { get; set; }         // if = true, run the app on windows start up
        public bool IconOnLaunch { get; set; }          // if = true, run the app on windows start up as icon in system tray... if = false, run the app on windows start up as normal window
        public int OnPlaying { get; set; }       // taked action when user launch game (1 = keep the app as window, 2 = minimized to taskbar, 3 = icon to tray, 4= exit  )
        public int OnClosing { get; set; }

        //Sounds and Notifications
        public bool EnableBlocks { get; set; }	// Blocs shwon on screen with events as notifications
        public int BlocksPosition { get; set; } 	// 1 = bottom right, 2 = bottom left, 3 = top right, 4 = top left
        public bool EnableSound { get; set; }
        public double SoundVolume { get; set; }
        public bool PlayingHideBlocks { get; set; }
        public bool PlayingHideNewMessages { get; set; }
        public bool PlayingMuteSounds { get; set; }

        public bool evOnlineFriendsToasts { get; set; }
        public bool evOnlineFriendsSound { get; set; }
        public bool evOfflineFriendsToasts { get; set; }
        public bool evOfflineFriendsSound { get; set; }
        public bool evFriendStartGameToasts { get; set; }
        public bool evFriendStartGameSound { get; set; }
        public bool evIncomingFreindRequestToasts { get; set; }
        public bool evIncomingFreindRequestSound { get; set; }

        public bool evIncomingMessageToasts { get; set; }
        public bool evIncomingMessageSound { get; set; }
        public bool evNewMessageSound { get; set; }

        public bool evChannelInviteToasts { get; set; }
        public bool evChannelInviteSound { get; set; }
        public bool evChatLeaveOrJoinToasts { get; set; }
        public bool evChatLeaveOrJoinSound { get; set; }


        //Friends and Chat
        public bool HideFirnedsID { get; set; }
        public bool ShowFriendsPic { get; set; }
        public bool AlertNewMessage { get; set; }
        public bool FilterbadWords { get; set; }
        public bool ShownWhenTyping { get; set; }

        //Voice Calls
        public int OutputDevice { get; set; }
        public double OutputDeviceVolume { get; set; }
        public int InputDevice { get; set; }
        public double InputDeviceVolume { get; set; }

        //Games Settings
        public bool InstallStartupIcon { get; set; }
        public bool InstallDesktopIcon { get; set; }
        public string DefaultInstallPath { get; set; }
        public bool ScanGamesOnLaunch { get; set; }

        //User status idle/busy/online
        public Enums.UserInfo.UserStatus UserStatus { get; set; } 
    }


    public class LauncherSettings
    {
        public LauncherData data = new LauncherData();
        private string SettingsFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"userdata\settings.xml");

        public LauncherSettings(bool firstLoad)
        {
           // data.RememberMe = true;
           // Console.WriteLine("Settings File Path = " + SettingsFile);
            //this.Save();

            if (firstLoad) this.Load();
            
        }

        public void Load()
        {
           // string testStr = "<?xml version=\"1.0\" encoding=\"utf-16\"?><LauncherData xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><RememberMe>true</RememberMe></LauncherData>";

            data = (LauncherData)RlktXmlData.Xml2Object.DeSerialize(SettingsFile, data);

        }

        public void Save()
        {

            string datax = RlktXmlData.Object2Xml.Serialize(data);

            string userdataPath = new FileInfo(SettingsFile).DirectoryName;

            if (!Directory.Exists(userdataPath)) Directory.CreateDirectory(userdataPath);

            File.WriteAllText(SettingsFile, datax);

        }

        public void ResetDefaults()
        {
            data.AppLanguage = "en";
            data.LaunchOnStart = false;
            data.IconOnLaunch = false;
            data.OnPlaying = 1;
            data.OnClosing = 1;

            //Sounds and Notifications
            data.EnableBlocks = true;
            data.BlocksPosition = 1;
            data.EnableSound = true;
            data.SoundVolume = 1;
            data.PlayingHideBlocks = false;
            data.PlayingHideNewMessages = false;
            data.PlayingMuteSounds = false;

            data.evOnlineFriendsToasts = true;
            data.evOnlineFriendsSound = true;
            data.evOfflineFriendsToasts = false;
            data.evOfflineFriendsSound = false;
            data.evFriendStartGameToasts = true;
            data.evFriendStartGameSound = true;
            data.evIncomingFreindRequestToasts = true;
            data.evIncomingFreindRequestSound = true;

            data.evIncomingMessageToasts = true;
            data.evIncomingMessageSound = true;
            data.evNewMessageSound = true;

            data.evChannelInviteToasts = true;
            data.evChannelInviteSound = true;
            data.evChatLeaveOrJoinToasts = false;
            data.evChatLeaveOrJoinSound = false;


            //Friends and Chat
            data.HideFirnedsID = false;
            data.ShowFriendsPic = true;
            data.AlertNewMessage = true;
            data.FilterbadWords = false;
            data.ShownWhenTyping = true;

            //Voice Calls
            data.OutputDevice = 1;
            data.OutputDeviceVolume = 1;
            data.InputDevice = 1;
            data.InputDeviceVolume = 1;

            //Games Settings
            data.InstallStartupIcon = true;
            data.InstallDesktopIcon = true;
            data.DefaultInstallPath = Path.Combine(Path.GetPathRoot(Environment.SystemDirectory));
            data.ScanGamesOnLaunch = true;
        }


    }

}
