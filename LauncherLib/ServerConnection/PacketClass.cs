using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LauncherLib.Factory;
using LauncherLib.ServerConnection.PacketsProtocol.pLogin;
using System.Reflection;

namespace LauncherLib.ServerConnection
{
    class PacketClass
    {
        public void ProcessPacket(PacketType nMain, Packet Data)
        {
            Object Task = null;
            TaskFactory tf = TaskFactory.Instance;
            Task = tf.GetTask(nMain.ToString());
            if (!ReferenceEquals(null, Task))
            {
                Type thisType = Task.GetType();
                MethodInfo theMethod = thisType.GetMethod("Process", BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                theMethod.Invoke(Task, new object[] { nMain, Data });
            }
            else
            {
                //
                Console.WriteLine("Cannot find task : " + nMain.ToString());
            }
        }
    }
}
