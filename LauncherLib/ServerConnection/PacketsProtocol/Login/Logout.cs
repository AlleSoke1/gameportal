using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherLib.ServerConnection.PacketsProtocol.pLogin
{
    public class Logout
    {
        public enum LogoutMessage : byte
        {
            Success,
            Failed,
        }

        private void Process(PacketType type, Packet packet)
        {
            Console.WriteLine(this.ToString() + "Packet Process!");

            byte result = packet.ReadByte();

            switch (result)
            {
                case (byte)LogoutMessage.Success:
                    Console.WriteLine("Logout OK!");
                    break;
                case (byte)LogoutMessage.Failed:
                    Console.WriteLine("Logout Failed!");
                    break;
            }

        }

        public static void SendLogout()
        {
            Packet packet = new Packet(PacketType.Logout);

            packet.Send();
        }
    }
}
