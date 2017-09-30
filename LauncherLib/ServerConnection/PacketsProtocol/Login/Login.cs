using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherLib.ServerConnection.PacketsProtocol.pLogin
{
    public class Login
    {
        public static string ServerReply;

        public enum LoginMessage : byte
        {
            Failed,
            Invalid_Username,
            Invalid_Password,
            Banned,
            Maintenance,

            Success = 100,
        }

         void Process(PacketType type, Packet packet)
        {
            Console.WriteLine("Login Packet Process!");

            byte result = packet.ReadByte();

            switch(result)
            {
                case (byte)LoginMessage.Success:
                    Console.WriteLine("Login OK!");
                    ServerReply = "1";
                    break;
                case (byte)LoginMessage.Failed:
                    Console.WriteLine("Login Failed!");
                    ServerReply = "0";
                    break;
            }

        }

        public static void SendLogin(string username, string password)
        {
            Packet packet = new Packet(username.Length + password.Length, PacketType.Login);

            packet.Write(username);
            packet.Write(password);

            packet.Send();
        }
    }
}
