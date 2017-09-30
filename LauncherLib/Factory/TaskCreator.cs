using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LauncherLib.ServerConnection.PacketsProtocol.pLogin;

namespace LauncherLib.Factory
{
    class TaskCreator
    {
        public TaskCreator()
        {
            TaskFactory tf = TaskFactory.Instance;
            tf.AddTask("Login", new Login());
            tf.AddTask("Logout", new Logout());


        }
    }
}
