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
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using LauncherApp.Styles.Controls;
using System.Reflection;

namespace LauncherApp.Styles.Pages
{
    /// <summary>
    /// Interaction logic for TheSettings.xaml
    /// </summary>
    public partial class TheSettings : UserControl
    {

        public bool settingLoaded = false;

        public TheSettings()
        {
            InitializeComponent();
            LoadStyles();
        }


        #region UI Functions

        private void LoadStyles()
        {

            VolumFlagLabl.Content = SoundVolumeFlag.Value.ToString().Split('.')[0] + "%";
            SavedLibl.Visibility = Visibility.Hidden;

            var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly().Location);
            AppTitleLibl.Content = string.Format((string)App.Current.Resources["AppTitleLibl"], versionInfo.ProductName);
            AppVersionLibl.Content = string.Format((string)App.Current.Resources["AppVersionLibl"], versionInfo.ProductVersion);
            AppCompanyLibl.Content = string.Format((string)App.Current.Resources["AppCompanyLibl"], versionInfo.CompanyName);

        }


        private void SoundVolumeFlag_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            VolumFlagLabl.Content = e.NewValue.ToString().Split('.')[0] + "%";
        }


        private void InputDeviceVolumeFlag_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void OutputDeviceVolumeFlag_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }


        private void profileImageGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            profileImageOveral.Visibility = Visibility.Visible;
        }

        private void profileImageGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            profileImageOveral.Visibility = Visibility.Hidden;
        }

        private void profileImageGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void SwitchMenuItems(object sender, MouseButtonEventArgs e)
        {
            Label tSender = (Label)sender;
            string swtichedName = tSender.Tag.ToString();
            ScrollViewer nxWnd = (ScrollViewer)DisplayWindow.FindName(swtichedName);

            foreach (object tBtn in LeftMenuPanel.Children)
            {
                if (tBtn is Label)
                {
                    Label tempBtn = (Label)tBtn;
                    if (tempBtn.Tag.ToString() == swtichedName)
                    {
                        tempBtn.Background.Opacity = 1;
                        tempBtn.Foreground = Brushes.White;
                    }
                    else
                    {
                        tempBtn.Background.Opacity = 0;
                        tempBtn.Foreground = Brushes.Silver;
                    }

                }
            }

            foreach (ScrollViewer wNd in DisplayWindow.Children)
            {
                if (wNd is ScrollViewer)
                {
                    if (wNd.Name == swtichedName)
                    {
                        nxWnd.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        wNd.Visibility = Visibility.Hidden;
                    }
                }
            }

            if (swtichedName == "settings_Profile") { settingsButtons.Visibility = Visibility.Hidden; } else { settingsButtons.Visibility = Visibility.Visible; }
        }

        public void SwitchMenuItems(string BtnTag)
        {
            Label tBtn = new Label();
            tBtn.Tag = BtnTag;
            SwitchMenuItems(tBtn, null);
        }

        private void LaunchOnStartFlag_Checked(object sender, RoutedEventArgs e)
        {
            if (LaunchOnStartFlag.IsChecked == true)
            {
                LaunchAsIconFlag.IsEnabled = true;
            }
            else
            {
                LaunchAsIconFlag.IsEnabled = false;
            }
        }

        private void EnableBlocksFlag_Checked(object sender, RoutedEventArgs e)
        {
            if (EnableBlocksFlag.IsChecked == true)
            {
                BlocksPositionFlag.IsEnabled = true;
                BlocksPositionFlag.IsEnabled = true;
                evOnlineFriendsToastsFlag.IsEnabled = true;
                evOfflineFriendsToastsFlag.IsEnabled = true;
                evFriendStartGameToastsFlag.IsEnabled = true;
                evIncomingFreindRequestToastsFlag.IsEnabled = true;
                evIncomingMessageToastsFlag.IsEnabled = true;
                evChannelInviteToastsFlag.IsEnabled = true;
                evChatLeaveOrJoinToastsFlag.IsEnabled = true;

            }
            else
            {
                BlocksPositionFlag.IsEnabled = false;
                BlocksPositionFlag.IsEnabled = false;
                evOnlineFriendsToastsFlag.IsEnabled = false;
                evOfflineFriendsToastsFlag.IsEnabled = false;
                evFriendStartGameToastsFlag.IsEnabled = false;
                evIncomingFreindRequestToastsFlag.IsEnabled = false;
                evIncomingMessageToastsFlag.IsEnabled = false;
                evChannelInviteToastsFlag.IsEnabled = false;
                evChatLeaveOrJoinToastsFlag.IsEnabled = false;
            }

        }

        private void EnableSoundFlag_Checked(object sender, RoutedEventArgs e)
        {
            if (EnableSoundFlag.IsChecked == true)
            {
                SoundVolumeFlag.IsEnabled = true;
                evOnlineFriendsSoundFlag.IsEnabled = true;
                evOfflineFriendsSoundFlag.IsEnabled = true;
                evFriendStartGameSoundFlag.IsEnabled = true;
                evIncomingFreindRequestSoundFlag.IsEnabled = true;
                evIncomingMessageSoundFlag.IsEnabled = true;
                evNewMessageSoundFlag.IsEnabled = true;
                evChannelInviteSoundFlag.IsEnabled = true;
                evChatLeaveOrJoinSoundFlag.IsEnabled = true;

            }
            else
            {
                SoundVolumeFlag.IsEnabled = false;
                evOnlineFriendsSoundFlag.IsEnabled = false;
                evOfflineFriendsSoundFlag.IsEnabled = false;
                evFriendStartGameSoundFlag.IsEnabled = false;
                evIncomingFreindRequestSoundFlag.IsEnabled = false;
                evIncomingMessageSoundFlag.IsEnabled = false;
                evNewMessageSoundFlag.IsEnabled = false;
                evChannelInviteSoundFlag.IsEnabled = false;
                evChatLeaveOrJoinSoundFlag.IsEnabled = false;

            }

        }

        private void StartupShortcutIconFlag_Checked(object sender, RoutedEventArgs e)
        {
            if (InstallStartupIconFlag.IsChecked == true)
            {
                InstallDesktopIconFlag.IsEnabled = true;
            }
            else
            {
                InstallDesktopIconFlag.IsEnabled = false;
            }
        }

        private void ChangeInstallPathBtn_Click(object sender, RoutedEventArgs e)
        {
            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                System.Windows.Forms.DialogResult result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    DefaultInstallPathFlag.Text = fbd.SelectedPath.ToString();
                }
            }
        }

        #endregion

        async Task Delay()
        {
            await Task.Delay(2000);
        }

        private async void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;

            // change app language
            string langStr = ((ComboBoxItem)LanguageFlag.SelectedItem).Tag.ToString();
            if (langStr != App.launcherSettings.data.AppLanguage.ToString())
            {
                LauncherFactory.SwitchWindowsLanguage(langStr);
                LauncherFactory.SwitchWindowsAlgin();
            }



            // save settings before window close.
            SaveSettings();
            SavedLibl.Visibility = Visibility.Visible;
            App.launcherSettings.Save();
            await Delay();
            SavedLibl.Visibility = Visibility.Hidden;


            this.IsEnabled = true;

        }


        public void LoadSettings()
        {
            if (!settingLoaded)
            {

            foreach (ComboBoxItem lngItem in LanguageFlag.Items)
            {

                if (lngItem.Tag.ToString() == App.launcherSettings.data.AppLanguage.ToString())
                    LanguageFlag.SelectedItem = lngItem;

            }

            LaunchOnStartFlag.IsChecked = App.launcherSettings.data.LaunchOnStart;
            LaunchAsIconFlag.IsChecked = App.launcherSettings.data.IconOnLaunch;
            onClosingFlag.SelectedIndex = App.launcherSettings.data.OnClosing;
            onPlayingFlag.SelectedIndex = App.launcherSettings.data.OnPlaying;
            EnableBlocksFlag.IsChecked = App.launcherSettings.data.EnableBlocks;
            BlocksPositionFlag.SelectedIndex = App.launcherSettings.data.BlocksPosition;
            EnableSoundFlag.IsChecked = App.launcherSettings.data.EnableSound;
            SoundVolumeFlag.Value = App.launcherSettings.data.SoundVolume;
            PlayingHideBlocksFlag.IsChecked = App.launcherSettings.data.PlayingHideBlocks;
            PlayingHideNewMessagesFlag.IsChecked = App.launcherSettings.data.PlayingHideNewMessages;
            PlayingMuteSoundsFlag.IsChecked = App.launcherSettings.data.PlayingMuteSounds;
            evOnlineFriendsToastsFlag.IsChecked = App.launcherSettings.data.evOnlineFriendsToasts;
            evOnlineFriendsSoundFlag.IsChecked = App.launcherSettings.data.evOnlineFriendsSound;
            evOfflineFriendsToastsFlag.IsChecked = App.launcherSettings.data.evOfflineFriendsToasts;
            evOfflineFriendsSoundFlag.IsChecked = App.launcherSettings.data.evOfflineFriendsSound;
            evFriendStartGameToastsFlag.IsChecked = App.launcherSettings.data.evFriendStartGameToasts;
            evFriendStartGameSoundFlag.IsChecked = App.launcherSettings.data.evFriendStartGameSound;
            evIncomingFreindRequestToastsFlag.IsChecked = App.launcherSettings.data.evIncomingFreindRequestToasts;
            evIncomingFreindRequestSoundFlag.IsChecked = App.launcherSettings.data.evIncomingFreindRequestSound;
            evIncomingMessageToastsFlag.IsChecked = App.launcherSettings.data.evIncomingMessageToasts;
            evIncomingMessageSoundFlag.IsChecked = App.launcherSettings.data.evIncomingMessageSound;
            evNewMessageSoundFlag.IsChecked = App.launcherSettings.data.evNewMessageSound;
            evChannelInviteToastsFlag.IsChecked = App.launcherSettings.data.evChannelInviteToasts;
            evChannelInviteSoundFlag.IsChecked = App.launcherSettings.data.evChannelInviteSound;
            evChatLeaveOrJoinToastsFlag.IsChecked = App.launcherSettings.data.evChatLeaveOrJoinToasts;
            evChatLeaveOrJoinSoundFlag.IsChecked = App.launcherSettings.data.evChatLeaveOrJoinSound;
            HideFirnedsIDFlag.IsChecked = App.launcherSettings.data.HideFirnedsID;
            ShowFriendsPicFlag.IsChecked = App.launcherSettings.data.ShowFriendsPic;
            AlertNewMessageFlag.IsChecked = App.launcherSettings.data.AlertNewMessage;
            FilterbadWordsFlag.IsChecked = App.launcherSettings.data.FilterbadWords;
            ShownWhenTypingFlag.IsChecked = App.launcherSettings.data.ShownWhenTyping;
            OutputDeviceFlag.SelectedIndex = App.launcherSettings.data.OutputDevice;
            OutputDeviceVolumeFlag.Value = App.launcherSettings.data.OutputDeviceVolume;
            InputDeviceFlag.SelectedIndex = App.launcherSettings.data.InputDevice;
            InputDeviceVolumeFlag.Value = App.launcherSettings.data.InputDeviceVolume;
            InstallStartupIconFlag.IsChecked = App.launcherSettings.data.InstallStartupIcon;
            InstallDesktopIconFlag.IsChecked = App.launcherSettings.data.InstallDesktopIcon;
            DefaultInstallPathFlag.Text = App.launcherSettings.data.DefaultInstallPath;
            ScanGamesOnLaunchFlag.IsChecked = App.launcherSettings.data.ScanGamesOnLaunch;


            profileUsername.SetInputText(Game_Data.Globals.username);
            profileNickname.SetInputText(Game_Data.Globals.nickname);
            profileEmail.SetInputText(Game_Data.Globals.email);
            profilePassword.SetInputText(Game_Data.Globals.password);

            settingLoaded = true;

            }

        }
        public void SaveSettings()
        {

            App.launcherSettings.data.AppLanguage = ((ComboBoxItem)LanguageFlag.SelectedItem).Tag.ToString();
            App.launcherSettings.data.LaunchOnStart = LaunchOnStartFlag.IsChecked.Value;
            App.launcherSettings.data.IconOnLaunch = LaunchAsIconFlag.IsChecked.Value;
            App.launcherSettings.data.OnClosing = onClosingFlag.SelectedIndex;
            App.launcherSettings.data.OnPlaying = onPlayingFlag.SelectedIndex;
            App.launcherSettings.data.EnableBlocks = EnableBlocksFlag.IsChecked.Value;
            App.launcherSettings.data.BlocksPosition = BlocksPositionFlag.SelectedIndex;
            App.launcherSettings.data.EnableSound = EnableSoundFlag.IsChecked.Value;
            App.launcherSettings.data.SoundVolume = SoundVolumeFlag.Value;
            App.launcherSettings.data.PlayingHideBlocks = PlayingHideBlocksFlag.IsChecked.Value;
            App.launcherSettings.data.PlayingHideNewMessages = PlayingHideNewMessagesFlag.IsChecked.Value;
            App.launcherSettings.data.PlayingMuteSounds = PlayingMuteSoundsFlag.IsChecked.Value;
            App.launcherSettings.data.evOnlineFriendsToasts = evOnlineFriendsToastsFlag.IsChecked.Value;
            App.launcherSettings.data.evOnlineFriendsSound = evOnlineFriendsSoundFlag.IsChecked.Value;
            App.launcherSettings.data.evOfflineFriendsToasts = evOfflineFriendsToastsFlag.IsChecked.Value;
            App.launcherSettings.data.evOfflineFriendsSound = evOfflineFriendsSoundFlag.IsChecked.Value;
            App.launcherSettings.data.evFriendStartGameToasts = evFriendStartGameToastsFlag.IsChecked.Value;
            App.launcherSettings.data.evFriendStartGameSound = evFriendStartGameSoundFlag.IsChecked.Value;
            App.launcherSettings.data.evIncomingFreindRequestToasts = evIncomingFreindRequestToastsFlag.IsChecked.Value;
            App.launcherSettings.data.evIncomingFreindRequestSound = evIncomingFreindRequestSoundFlag.IsChecked.Value;
            App.launcherSettings.data.evIncomingMessageToasts = evIncomingMessageToastsFlag.IsChecked.Value;
            App.launcherSettings.data.evIncomingMessageSound = evIncomingMessageSoundFlag.IsChecked.Value;
            App.launcherSettings.data.evNewMessageSound = evNewMessageSoundFlag.IsChecked.Value;
            App.launcherSettings.data.evChannelInviteToasts = evChannelInviteToastsFlag.IsChecked.Value;
            App.launcherSettings.data.evChannelInviteSound = evChannelInviteSoundFlag.IsChecked.Value;
            App.launcherSettings.data.evChatLeaveOrJoinToasts = evChatLeaveOrJoinToastsFlag.IsChecked.Value;
            App.launcherSettings.data.evChatLeaveOrJoinSound = evChatLeaveOrJoinSoundFlag.IsChecked.Value;
            App.launcherSettings.data.HideFirnedsID = HideFirnedsIDFlag.IsChecked.Value;
            App.launcherSettings.data.ShowFriendsPic = ShowFriendsPicFlag.IsChecked.Value;
            App.launcherSettings.data.AlertNewMessage = AlertNewMessageFlag.IsChecked.Value;
            App.launcherSettings.data.FilterbadWords = FilterbadWordsFlag.IsChecked.Value;
            App.launcherSettings.data.ShownWhenTyping = ShownWhenTypingFlag.IsChecked.Value;
            App.launcherSettings.data.OutputDevice = OutputDeviceFlag.SelectedIndex;
            App.launcherSettings.data.OutputDeviceVolume = OutputDeviceVolumeFlag.Value;
            App.launcherSettings.data.InputDevice = InputDeviceFlag.SelectedIndex;
            App.launcherSettings.data.InputDeviceVolume = InputDeviceVolumeFlag.Value;
            App.launcherSettings.data.InstallStartupIcon = InstallStartupIconFlag.IsChecked.Value;
            App.launcherSettings.data.InstallDesktopIcon = InstallDesktopIconFlag.IsChecked.Value;
            App.launcherSettings.data.DefaultInstallPath = DefaultInstallPathFlag.Text;
            App.launcherSettings.data.ScanGamesOnLaunch = ScanGamesOnLaunchFlag.IsChecked.Value;


        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;

            App.launcherSettings.ResetDefaults();
            App.launcherSettings.Save();
            this.settingLoaded = false;
            LoadSettings();

            this.IsEnabled = true;
        }






    }
}
