using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherLib.ServerConnection
{
    public enum PacketType : byte
    {
        /* Login */
        Login,
        Logout,


        /* Settings */
        GetSettings,
        SaveSettings,


        /* Friends */
        AddFriend,
        DelFriend,
        GetFriendList,


        /* FriendsNotification */
        FriendOnline,
        FriendOffline,
        FriendPlaying,
        FriendQuitPlaying,
        FriendIsGay,


        /* Chat */
        SendMessage,
        RecvMessage,
    }
}
