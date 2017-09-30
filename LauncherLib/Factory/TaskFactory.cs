using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace LauncherLib.Factory
{
    public class TaskFactory
    {
        //Singleton
        private static TaskFactory instance = null;
        private static readonly object padlock = new object();

        TaskFactory()
        {
        }

        public static TaskFactory Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new TaskFactory();
                    }
                    return instance;
                }
            }
        }
        //End Singleton

        Dictionary<string, Object> curTasks = new Dictionary<string, Object>();

        public void AddTask(String name ,Object classObject)
        {
            if (curTasks.ContainsKey(name)) return;
            curTasks.Add(name, classObject);
        }

        public Object GetTask(String name)
        {
            if (curTasks.ContainsKey(name)) return curTasks[name];

            return (Object)null; //not found.
        }
    }
}
